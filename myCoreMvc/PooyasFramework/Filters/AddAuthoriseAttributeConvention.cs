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
            var openPages = new[]
            {
                "Auth",
                "Base",
                "Api" //Task: Make it case-insensitive. And avoid hard-coded values.
            };

            if (openPages.Contains(model.ControllerName) == false)
            {
                var filter = new AuthorizeFilter();
                model.Filters.Add(filter);
            }
        }
    }
}
