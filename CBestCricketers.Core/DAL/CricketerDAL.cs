using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using BestCricketers.Models;

namespace CBestCricketers.Core.DAL
{
    class CricketerDAL
    {
        //specify the database variable
        Database objDB;

        //specify the static variable
        static string ConnectionString;

        //This constructor is used to get the connection string from the config file
        public CricketerDAL()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["CricketerConnectionString"].ToString();
        }
        //region Database Method

        public List < T > ConvertTo < T > (DataTable datatable) where T: new()
        {
            List<T> Temp = new List<T>();
            try
            {
                List<string> columnsNames = new List<string>();
                foreach (DataColumn datacolumn in datatable.Columns)
                    columnsNames.Add(datacolumn.ColumnName);
                Temp = datatable.AsEnumerable().ToList().ConvertAll<T>(row => getObject<T>(row, columnsNames));
                return Temp;
            }
            catch
            {
                return Temp;
            }
        }

        private T getObject<T>(DataRow row, List<string> columnsNames) where T : new()
        {
            T obj = new T();
            try
            {
                String columnname = "";
                string value = "";
                PropertyInfo[] properties;
                properties = typeof(T).GetProperties();
                foreach (PropertyInfo objproperty in properties)
                {
                    columnname = columnsNames.Find(name => name.ToLower() == objproperty.Name.ToLower());
                    if (!String.IsNullOrEmpty(columnname))
                    {
                        value = row[columnname].ToString();
                        if (!string.IsNullOrEmpty(value))
                        {
                            if (Nullable.GetUnderlyingType(objproperty.PropertyType) != null)
                            {
                                value = row[columnname].ToString().Replace("$", "").Replace(",", "");
                                objproperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(Nullable.GetUnderlyingType(objproperty.PropertyType).ToString())), null);

                            }
                            else
                            {
                                value = row[columnname].ToString();
                                objproperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(objproperty.PropertyType.ToString())), null);
                            }
                        }
                    }

                }
                return obj;
            }
            catch(Exception ex)
            {
                return obj;
            }
        }

        //this method is used to get the cricket data
        public List<CricketerProfile> GetCricketerList()
        {
            List<CricketerProfile> objGetCricketers = null;
            objDB = new SqlDatabase(ConnectionString);
            using (DbCommand objcmd = objDB.GetStoredProcCommand("CC_GetCricketerList"))
            {
                try
                {
                    using (DataTable dataTable = objDB.ExecuteDataSet(objcmd).Tables[0])
                    {
                        objGetCricketers = ConvertTo<CricketerProfile>(dataTable);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                    
                }
            }
            return objGetCricketers;
        }

        //This method is used to get cricketers details by cricketer id

        public List<CricketerProfile> GetCricketerDetailsById(int Id)
        {
            List<CricketerProfile> objCricketerDetails = null;
            objDB = new SqlDatabase(ConnectionString);
            using(DbCommand objcmd = objDB.GetStoredProcCommand("CC_GetCricketerDetailsById")){
                try
                {
                    objDB.AddInParameter(objcmd, "@ID", DbType.Int32, Id);
                    using (DataTable datatable = objDB.ExecuteDataSet(objcmd).Tables[0])
                    {
                        objCricketerDetails = ConvertTo<CricketerProfile>(datatable);
                    }
                }
                catch (Exception ex) {
                    throw ex;
                    return null;
                }

            }
            return objCricketerDetails;
        }

        //This method is used to update cricketer info

        public int AddUpdateCricketerInfo(CricketerProfile cricketer)
        {
            int result = 0;
            objDB = new SqlDatabase(ConnectionString);
            using(DbCommand objcmd = objDB.GetStoredProcCommand("CC_AddUpdateCricketerDetails"))
            {
                objDB.AddInParameter(objcmd, "@ID", DbType.Int32, cricketer.Id);
                if (string.IsNullOrEmpty(cricketer.Name)) objDB.AddInParameter(objcmd, "@Name", DbType.String, DBNull.Value);
                else objDB.AddInParameter(objcmd, "@Name", DbType.String, cricketer.Name);
                if (cricketer.ODI == 0) objDB.AddInParameter(objcmd, "@ODI", DbType.Int32, DBNull.Value);
                else objDB.AddInParameter(objcmd, "@ODI", DbType.Int32, cricketer.ODI);
                if (cricketer.Tests == 0) objDB.AddInParameter(objcmd, "@Tests", DbType.Int32, DBNull.Value);
                else objDB.AddInParameter(objcmd, "@Tests", DbType.Int32, cricketer.Tests);
                if (cricketer.OdiRuns == 0) objDB.AddInParameter(objcmd, "@OdiRuns", DbType.Int32, DBNull.Value);
                else objDB.AddInParameter(objcmd, "@OdiRuns", DbType.Int32, cricketer.OdiRuns);
                if (cricketer.TestRuns == 0) objDB.AddInParameter(objcmd, "@TestRuns", DbType.Int32, DBNull.Value);
                else objDB.AddInParameter(objcmd, "@TestRuns", DbType.Int32, cricketer.TestRuns);

                objDB.AddInParameter(objcmd, "@Type", DbType.Int32, cricketer.Type);
                objDB.AddOutParameter(objcmd, "@Status", DbType.Int16, 0);

                try
                {
                    objDB.ExecuteNonQuery(objcmd);
                    result = Convert.ToInt32(objDB.GetParameterValue(objcmd, "@Status"));
                }
                catch(Exception ex)
                {
                    throw;
                }
            }
            return result;
        }

        public int DeleteCricketerInfo(CricketerProfile cricketer)
        {
            int result = 0;
            objDB = new SqlDatabase(ConnectionString);
             using(DbCommand objcmd = objDB.GetStoredProcCommand("CC_DeleteCricketerProfile"))
            {
                objDB.AddInParameter(objcmd, "@Id", DbType.Int32, cricketer.Id);
                objDB.AddOutParameter(objcmd, "@Status", DbType.Int16, 0);
                try
                {
                    objDB.ExecuteNonQuery(objcmd);
                    result = Convert.ToInt32(objDB.GetParameterValue(objcmd, "@Status"));
                }
                catch(Exception ex)
                {
                    throw;
                }
            }
            return result;

        }

    }
}
