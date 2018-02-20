﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TechPortal.Data.Domain;
using TechPortal.Data.Domain.Crud;
using TechPortal.Data.Domain.DataAccessObjects;

namespace TechPortal.Data.Client.Controllers
{
    public class TechRolesController : ApiController
    {
        private static AccessHelper helper = new AccessHelper();
        private TPDBEntities db = new TPDBEntities();

        [HttpGet]
        public HttpResponseMessage Get()
        {
            List<TechRoleDAO> t;
            try
            {
                if ((t = helper.GetTechRoles()) != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, t, "application/json");
                }

                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        public HttpResponseMessage Get(string id)
        {
            try
            {
                TechRoleDAO t;
                if ((t = helper.GetTechRoles().FirstOrDefault(m => m.TRName.Equals(id))) != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, t, "application/json");
                }

                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]TechRoleDAO value)
        {
            if (value != null && ModelState.IsValid)
            {
                try
                {

                    if (helper.InsertTechRole(value))
                    {

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }

                    {
                        return Request.CreateResponse(HttpStatusCode.NotModified);
                    }
                }
                catch (Exception e)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [HttpPut]
        public HttpResponseMessage Put(string id, [FromBody]TechRoleDAO value)
        {
            if (value != null && ModelState.IsValid)
            {
                try
                {
                     if (helper.UpdateTechRole(id, value))
                    {
                          return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotModified);
                    }
                }
                catch (Exception e)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }



        [HttpDelete]
        public HttpResponseMessage Delete(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                try
                {
                    return Request.CreateResponse(helper.DeleteTechRole(helper.GetTechRoles().FirstOrDefault(m => m.TRName.Equals(id))) ? HttpStatusCode.OK : HttpStatusCode.NotModified);
                }
                catch (Exception e)
                {
                     return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
             return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TechRoleExists(int id)
        {
            return db.TechRole.Count(e => e.TechRoleID == id) > 0;
        }
    }
}