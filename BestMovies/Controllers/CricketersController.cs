using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BestCricketers.Models;
using CBestCricketers.Core.BL;

namespace BestMovies.Controllers
{
    public class CricketersController : ApiController
    {

        HttpResponseMessage response;
        CricketerBL cricketerBL;

        //this method is used to get cricketer list
        [HttpGet, ActionName("GetCricketerList")]
        public HttpResponseMessage GetCricketerList()
        {
            Result result;
            cricketerBL = new CricketerBL();

            try {

                var cricketerList = cricketerBL.GetCricketerList();
                if (!object.Equals(cricketerList, null))
                {
                    response = Request.CreateResponse<List<CricketerProfile>>(HttpStatusCode.OK, cricketerList);
                }
            }
            catch(Exception ex)
            {
                result = new Result();
                result.Status = 0;
                result.Message = ex.Message;
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
            return response;
        }

        [HttpGet, ActionName("GetCricketerInfoById")]
        public HttpResponseMessage GetCricketerInfoById(int cricketerId)
        {
            Result result;
            cricketerBL = new CricketerBL();
            try {

                var cricketerList = cricketerBL.GetCricketerDetailsById(cricketerId);
                if(!object.Equals(cricketerList, null))
                {
                    response = Request.CreateResponse<List<CricketerProfile>>(HttpStatusCode.OK, cricketerList);
                }

            }
            catch(Exception ex)
            {
                result = new Result();
                result.Status = 0;
                result.Message = ex.Message;
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, result);

            }
            return response;
        }

        [HttpPost, ActionName("AddCricketerInfo")]
        public HttpResponseMessage AddCricketerInfo(CricketerProfile cricketer)
        {
            Result objResult;
            int result;
            cricketerBL = new CricketerBL();
            try
            {
                result = cricketerBL.AddUpdateCricketerInfo(cricketer);
                if(result > 0)
                {
                    if (result == 1)
                    {
                        objResult = new Result();
                        objResult.Status = result;
                        objResult.Message = "Record Inserted successfully!";
                        response = Request.CreateResponse<Result>(HttpStatusCode.OK, objResult);
                    }
                    else if (result == 2)
                    {
                        objResult = new Result();
                        objResult.Status = result;
                        objResult.Message = "Record already exists!";
                        response = Request.CreateResponse<Result>(HttpStatusCode.OK, objResult);

                    }
                    else
                    {
                        objResult = new Result();
                        objResult.Status = result;
                        objResult.Message = "Record not inserted";
                        response = Request.CreateResponse<Result>(HttpStatusCode.OK, objResult);
                    }
                }
                
            }
            catch(Exception ex)
            {
                objResult = new Result();
                objResult.Status = 0;
                objResult.Message = ex.Message;
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, objResult);

            }
            return response;
        }

        //Update Info
       [HttpPut, ActionName("UpdateCricketerInfo")]
       public HttpResponseMessage UpdateCricketerInfo(CricketerProfile cricketer)
        {
            Result objresult;
            int result;
            cricketerBL = new CricketerBL();
            try
            {
                result = cricketerBL.AddUpdateCricketerInfo(cricketer);
                if(result > 0)
                {
                    if (result == 3)
                    {
                        objresult = new Result();
                        objresult.Status = result;
                        objresult.Message = "Record Updated Successfully";
                        response = Request.CreateResponse<Result>(HttpStatusCode.OK, objresult);
                    }
                    if (result == 2)
                    {
                        objresult = new Result();
                        objresult.Status = result;
                        objresult.Message = "Record Does not exist";
                        response = Request.CreateResponse<Result>(HttpStatusCode.OK, objresult);
                    }
                    else
                    {
                        objresult = new Result();
                        objresult.Status = result;
                        objresult.Message = "Record Not Added";
                        response = Request.CreateResponse<Result>(HttpStatusCode.OK, objresult);
                    }
                }
            }
            catch(Exception ex)
            {
                objresult = new Result();
                objresult.Status = 0;
                objresult.Message = ex.Message;
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, objresult);
            }
            return response;
        }

        //This method is used to delete the cricketer info
        [HttpDelete, ActionName("DeleteCricketerInfo")]
        public HttpResponseMessage DeleteCricketerInfo(int cricketerId)
        {
            int result;
            Result ObjResult;
            cricketerBL = new CricketerBL();
            try
            {
                CricketerProfile cricketer = new CricketerProfile();
                cricketer.Id = cricketerId;

                result = cricketerBL.DeleteCricketerInfo(cricketer);
                if (result > 0)
                {
                    if (result == 1)
                    {
                        ObjResult = new Result();
                        ObjResult.Status = result;
                        ObjResult.Message = "Record Deleted Successfully!!";
                        response = Request.CreateResponse<Result>(HttpStatusCode.OK, ObjResult);
                    }
                    else if (result == 2)
                    {
                        ObjResult = new Result();
                        ObjResult.Status = result;
                        ObjResult.Message = "Record does not Exists!!";
                        response = Request.CreateResponse<Result>(HttpStatusCode.OK, ObjResult);
                    }
                    else
                    {
                        ObjResult = new Result();
                        ObjResult.Status = result;
                        ObjResult.Message = "Record Not Found!!";
                        response = Request.CreateResponse<Result>(HttpStatusCode.OK, ObjResult);
                    }
                }
            }
            catch (Exception ex)
            {
                ObjResult = new Result();
                ObjResult.Status = 0;
                ObjResult.Message = ex.Message;
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ObjResult);
            }
            return response;

        }

    }
}
