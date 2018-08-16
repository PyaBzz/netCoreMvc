using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace myCoreMvc.PooyasFramework.Filters
{
    public class AddAuthoriseAttributeConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel model)
        {
            //if (model.ControllerName.Contains("Api"))
            //{
            //    model.Filters.Add(new AuthorizeFilter("apipolicy"));
            //}
            //else
            //{
            //    model.Filters.Add(new AuthorizeFilter("defaultpolicy"));
            //}

            if (model.ControllerName.Contains("Auth"))
            {
                //model.Filters.Add(new AuthorizeFilter("apipolicy"));
            }
            else
            {
                model.Filters.Add(new AuthorizeFilter("denyAll"));
            }
        }
    }
}
