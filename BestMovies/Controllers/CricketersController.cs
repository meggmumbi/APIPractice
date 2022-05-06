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


    }
}
