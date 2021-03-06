﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using System.Web.Http.OData.Routing;
using BitopiApprovalSystemWebApiModels;
using Microsoft.Data.OData;
using BitopiDBContext;

namespace BitopiApprovalSystemWebApiNew.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using BitopiApprovalSystemWebApiModels;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<ProdcutionAccountingDBModel>("ProdcutionAccounting");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */

    public class ProdcutionAccountingController : ApiController
    {
        DBProduction Context=new DBProduction();
        [HttpGet]
        public List<ProductionAccountingDBModel> Get(string UserID,string ProcessID, string LocationID, string PRStatus, int EntryType,
            string RefID="")
        {
            UserID=Cipher.Decrypt(UserID);
           List< ProductionAccountingDBModel> Lsist= Context.Get(UserID, ProcessID, LocationID,  PRStatus, EntryType, RefID);
            return Lsist;
        }
        
        [HttpGet]
        public List<DDL> Get(string UserID)
        {
            UserID = Cipher.Decrypt(UserID);
            List<DDL> Lsist = Context.GetDDL(UserID);
            return Lsist;
        }
        //[HttpGet]
        //public int Set(string RefNO, DateTime ProdDateTime,string LocationRef, int Qty, string AddedBy)
        //{
        //    int result = Context.Set( RefNO,  ProdDateTime, LocationRef,  Qty,   AddedBy);
        //    return result;
        //}

        [HttpPost]
        public int Set(ProductionAccountingDBModel ProductionAccountingDBModel)
        {
            int result = Context.Set(ProductionAccountingDBModel);
            return result;
        }
        public List<DefectMaster> GetGetDefectList(string RefID="")
        {
          return  Context.GetDefectList();
        }
        
        public string GetAQL(string RefID,int LotQ,int? DefectUnit=null)
        {
            return Context.GetAQL( RefID,LotQ,DefectUnit);
        }

    }
}
