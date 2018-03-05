﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace WebsiteUI.Infrastructure
{
    public static class Extensions
    {
        /// <summary>
        /// Renders a (partial) view to string.
        /// </summary>
        /// <param name="controller">Controller to extend</param>
        /// <param name="viewName">(Partial) view to render</param>
        /// <returns>Rendered (partial) view as string</returns>
        public static string RenderPartialViewToString(this Controller controller, string viewName)
        {
            return controller.RenderPartialViewToString(viewName, null);
        }

        /// <summary>
        /// Renders a (partial) view to string.
        /// </summary>
        /// <param name="controller">Controller to extend</param>
        /// <param name="viewName">(Partial) view to render</param>
        /// <param name="model">Model</param>
        /// <returns>Rendered (partial) view as string</returns>
        public static string RenderPartialViewToString(this Controller controller, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = controller.ControllerContext.RouteData.GetRequiredString("action");

            controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}