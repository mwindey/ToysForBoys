﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebFrontEnd.Models
{
    public class ProductListItem
    {
        [Display(Name = "Productnaam")]
        public string ProductName { get; set; }

        [Display(Name = "Schaal")]
        public string Scale { get; set; }

        [Display(Name = "Beschrijving")]
        public string Description { get; set; }

        [Display(Name = "Categorie")]
        public string Category { get; set; }

        [Display(Name = "Prijs")]
        public decimal BuyPrice { get; set; }

        public int ProductID { get; set; }
    }
}