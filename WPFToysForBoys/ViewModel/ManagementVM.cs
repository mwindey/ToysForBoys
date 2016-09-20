﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using DataAccessLayer;
using DataAccessLayer.Interfaces;

namespace WPFToysForBoys.ViewModel
{
    public partial class ManagementVM:ViewModelBase
    {
        private IProductService pService;
        public ManagementVM()
        {
            pService = new ProductServiceMock();
            ProductList = pService.GetAll().ToList();
        }

        private List<Product> productList;
        public List<Product> ProductList
        {
            get { return productList; }
            set
            {
                productList = value;
                RaisePropertyChanged("ProductList");
            }
        }

    }
}
