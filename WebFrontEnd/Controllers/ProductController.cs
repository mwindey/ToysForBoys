﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Services;
using WebFrontEnd.Models;
using DataAccessLayer;
using WebFrontEnd.Infrastructure;

namespace WebFrontEnd.Controllers
{
    public class ProductController : Controller
    {
        private IProductService productService;
        private IProductlineService productLineService;

        public ProductController()
        {
            productService = new ProductService();
            productLineService = new ProductlineService();
        }
        public ActionResult List(int? id)
        {
            if (id == null)
            {
                ViewBag.Page = 1;
            }
            else
            {
                ViewBag.Page = id;
            }

            var products = productService.GetAll();

            var productListViewModel = new Models.ProductListViewModel();

            productListViewModel.AllProductLines = productLineService.GetAll();

            var productList = new List<ProductListItem>();


            foreach (var product in products)
            {
                product.productline = productListViewModel.AllProductLines
                    .FirstOrDefault(pl => pl.id == product.productlineId);

                productList.Add
                   (
                       new ProductListItem(product)
                   );
            }

            if (this.Session["filters"] == null)
            {
                productListViewModel.Products = productList;
                productListViewModel.FilterProductLines = productListViewModel.AllProductLines.Select(pl => pl.id).ToList();
            }
            else
            {
                return List((ProductListViewModel)this.Session["filters"], id);

            }

            return View(productListViewModel);
        }
        [HttpPost]
        public ActionResult List([ModelBinder(typeof(Infrastructure.ProductListViewModelBinder))] Models.ProductListViewModel model, int? id)
        {


            if (id == null || (ProductListViewModel)this.Session["filters"] != model)
            {
                ViewBag.Page = 1;
            }
            else
            {
                ViewBag.Page = id;
            }

            var productLines = productLineService.GetAll();
            IEnumerable<Product> products;

            if (ModelState.IsValid)
            {
                Func<Product, bool> filter;

                if (string.IsNullOrEmpty(model.FilterProductName))
                {
                    if (model.FilterOnPrice)
                    {
                        filter = product => model.FilterProductLines.Contains(product.productlineId) &&
                                            product.buyPrice >= model.FilterBuyPriceMinimum && product.buyPrice <= model.FilterBuyPriceMaximum;
                    }
                    else
                    {
                        filter = product => model.FilterProductLines.Contains(product.productlineId);
                    }
                }
                else
                {
                    if (model.FilterOnPrice)
                    {

                        filter = product => model.FilterProductLines.Contains(product.productlineId) &&
                                        product.name.ToLower().Contains(model.FilterProductName.ToLower()) &&
                                        product.buyPrice >= model.FilterBuyPriceMinimum && product.buyPrice <= model.FilterBuyPriceMaximum;
                    }
                    else
                    {
                        filter = product => model.FilterProductLines.Contains(product.productlineId) &&
                                        product.name.ToLower().Contains(model.FilterProductName.ToLower());
                    }
                }


                products = productService
                    .GetAll
                    (
                        filter,
                        "productline"
                    );


            }
            else
            {
                products = productService.GetAll("productline");
            }

            model.AllProductLines = productLines;

            var productList = new List<ProductListItem>();

            foreach (var product in products)
            {
                productList.Add
                    (
                        new ProductListItem(product)
                    );
            }

            model.Products = productList;

            this.Session["filters"] = model;

            return View(model);
        }

        public ActionResult Buy(int id)
        {
            OrderViewModel order = new OrderViewModel();

            //Kijk of er voor dit product al een bestelling in het mandje ligt
            if (this.Session["cart"] != null)
            {
                foreach (OrderViewModel f in ((ShoppingCart)this.Session["cart"]).orders)
                {
                    //voorstelling gevonden
                    if (f.Product.id == id)
                    {
                        order = f;
                        return View(order);
                    }
                }
            }

            Product product = productService.GetById(id);


            if (product == null)
            {
                return HttpNotFound();
            }

            order.Product = product;

            order.Aantal = 1;

            return View(order);
        }

        public ActionResult AddToCart(OrderViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                ShoppingCart cart;
                if (this.Session["cart"] == null)
                {
                    cart = new ShoppingCart();

                }
                else
                {
                    cart = (ShoppingCart)this.Session["cart"];

                    //Kijk of dit product reeds in het mandje ligt
                    foreach (OrderViewModel f in ((ShoppingCart)this.Session["cart"]).orders)
                    {
                        //voorstelling gevonden
                        if (f.Product.id == model.Product.id)
                        {
                            f.Aantal += model.Aantal;
                        }

                    }
                }
                Product product = productService.GetById(model.Product.id);
                model.Product = product;
                cart.orders.Add(model);

                this.Session["cart"] = cart;
            }

            return RedirectToAction("ViewCart", "Checkout");
        }

    }
}