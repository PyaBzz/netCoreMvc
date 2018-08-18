using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using myCoreMvc.Controllers;
using PooyasFramework;

namespace myCoreMvc.PooyasFramework.Filters
{
    public class AddAuthoriseAttributeConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel model)
        {
            var protectedControllers = new[]
            {
                typeof(AuthenticatedOnlyController)
            };

            if (protectedControllers.Contains(model.ControllerType))
            {
                var filter = new AuthorizeFilter();
                model.Filters.Add(filter);
            }
        }

        public static string ShortNameOf<T>() where T : Controller
        {
            return typeof(T).Name.TrimEnd("Controller");
        }
    }
}
