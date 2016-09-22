﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebFrontEnd.Models;

namespace WebFrontEnd.Infrastructure
{
    public class ProductLineFilterModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType == typeof(ProductListViewModel))
            {
                var request = controllerContext.HttpContext.Request;

                var allKeys = new List<string>();

                var model = new ProductListViewModel();

                model.FilterProductLines = new List<int>();
                
                foreach (string key in request.Form.Keys)
                {
                    if (key.ToLower().Contains("productline"))
                    {
                        bool filterProductLine = false;
                        bool.TryParse(request.Form.GetValues(key)[0], out filterProductLine);

                        if (filterProductLine)
                        {
                            model.FilterProductLines.Add(int.Parse((key.Remove(0, 11))));
                        }
                    }
                }

                return model;
                
            } 
            else
            {
                return base.BindModel(controllerContext, bindingContext);
            }
        }
                
    }
}