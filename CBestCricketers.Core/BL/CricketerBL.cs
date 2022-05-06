using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestCricketers.Models;
using CBestCricketers.Core.DAL;

namespace CBestCricketers.Core.BL
{
    public class CricketerBL
    {
        //This method is used to get the cricketer list

        public List<CricketerProfile> GetCricketerList()
        {
            List<CricketerProfile> objcricketers = null;
            try
            {
                objcricketers = new CricketerDAL().GetCricketerList();
            }
            catch(Exception)
            {
                throw;
            }
            return objcricketers;
        }

        //This mesthod is use to get cricketers details by cricketer id

        public List<CricketerProfile> GetCricketerDetailsById(int Id)
        {
            List<CricketerProfile> objCricketerDetails = null;
            try
            {
                objCricketerDetails = new CricketerDAL().GetCricketerDetailsById(Id);
            }
            catch (Exception)
            {
                throw;
            }
            return objCricketerDetails;
        }

        // This method is used to add update cricketer info  
        
        public int AddUpdateCricketerInfo(CricketerProfile cricketer)
        {
            int result = 0;
            try
            {
                result = new CricketerDAL().AddUpdateCricketerInfo(cricketer);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public int DeleteCricketerInfo(CricketerProfile cricketer)
        {
            int result = 0;
            try
            {
                result = new CricketerDAL().DeleteCricketerInfo(cricketer);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

    }
}
