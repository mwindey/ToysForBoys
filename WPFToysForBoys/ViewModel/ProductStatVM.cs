﻿using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFToysForBoys.Model;

namespace WPFToysForBoys.ViewModel
{
    public class ProductStatVM : ViewModelBase
    {
        public IProductlineService plService;
        public IProductStatisticService pService;
        public IProductService prService;
        public IOrderStatisticService oService;

        public ProductStatVM()
        {
            plService = new ProductlineService();
            pService = new ProductStatisticService();
            oService = new OrderStatisticService();
            prService = new ProductService();

            {
                List<YearStruct> yl = new List<YearStruct>() { new YearStruct() { year = -1, display = "---All---" } };

                List<int> year = oService.GetDistinctYear(SortDateEnum.orderDate).ToList();
                year.Sort();
                foreach (int y in year)
                    yl.Add(new YearStruct() { year = y, display = "--" + y.ToString() + "--" });

                YearList = yl;
            }

            MonthList = new List<MonthStruct>() { new MonthStruct() { month = -1, display = "All" }, new MonthStruct() { month = 1, display = "January" }, new MonthStruct() { month = 2, display = "February" }, new MonthStruct() { month = 3, display = "March" }, new MonthStruct() { month = 4, display = "April" }, new MonthStruct() { month = 5, display = "May" }, new MonthStruct() { month = 6, display = "June" }, new MonthStruct() { month = 7, display = "July" }, new MonthStruct() { month = 8, display = "August" }, new MonthStruct() { month = 9, display = "September" }, new MonthStruct() { month = 10, display = "October" }, new MonthStruct() { month = 11, display = "November" }, new MonthStruct() { month = 12, display = "December" } };

            {
                List<ProductStatStruct> pList = new List<ProductStatStruct>();

                foreach (Productline pl in plService.GetAll())
                {
                    pList.Add(new ProductStatStruct() { id = pl.id, name = pl.name, countProductsSold = 0 });
                }

                ProductlineList = pList;
            }

            SelectedMonth = -1;
            SelectedYear = -1;
            sort = true;
            ProductListName = "Products sorted by most sold";
        }

        private bool sort;
        private string productListName;
        public string ProductListName
        {
            get { return productListName; }
            set
            {
                productListName = value;
                RaisePropertyChanged("ProductListName");
            }
        }

        public RelayCommand SortCommand
        {
            get { return new RelayCommand(SortProduct); }
        }

        public void SortProduct()
        {
            sort = !sort;
            GetProduct();
        }

        private void GetProduct()
        {
            if (SelectedProductline != null)
                if (sort)
                {
                    ProductList = pService.GetProductsSortedByMostSold(SelectedProductline.id, SelectedMonth, SelectedYear);
                    ProductListName = "Products sorted by most sold (click to reverse sorting)";
                }
                else
                {
                    ProductList = pService.GetProductsSortedByLeastSold(SelectedProductline.id, SelectedMonth, SelectedYear);
                    ProductListName = "Products sorted by least sold (click to reverse sorting)";
                }
            else
                ProductlineList = new List<ProductStatStruct>();
        }

        private List<BestSoldProduct> productList;
        public List<BestSoldProduct> ProductList
        {
            get { return productList; }
            set
            {
                productList = value;
                RaisePropertyChanged("ProductList");
            }
        }

        private List<ProductShortageStruct> emergencyProductList;
        public List<ProductShortageStruct> EmergencyProductList
        {
            get { return emergencyProductList; }
            set
            {
                emergencyProductList = value;
                RaisePropertyChanged("EmergencyProductList");
            }
        }

        private void ProductlineListRefresh()
        {
            List<ProductStatStruct> prod = new List<ProductStatStruct>();
            prod.AddRange(ProductlineList);
            try
            {
                foreach (ProductStatStruct p in prod)
                    p.countProductsSold = pService.GetCountSold(p.id, SelectedYear, SelectedMonth);
            }
            catch (NotImplementedException) { }

            GetBestMonth();
            GetProduct();
            ProductlineList = prod;
        }

        private int selectedYear;
        public int SelectedYear
        {
            get { return selectedYear; }
            set
            {
                selectedYear = value;
                ProductlineListRefresh();
                RaisePropertyChanged("SelectedYear");
            }
        }

        private int selectedMonth;
        public int SelectedMonth
        {
            get { return selectedMonth; }
            set
            {
                selectedMonth = value;
                ProductlineListRefresh();
                RaisePropertyChanged("SelectedMonth");
            }
        }

        private List<YearStruct> yearList;
        public List<YearStruct> YearList
        {
            get { return yearList; }
            set
            {
                yearList = value;
                RaisePropertyChanged("YearList");
            }
        }

        private List<MonthStruct> monthList;
        public List<MonthStruct> MonthList
        {
            get { return monthList; }
            set
            {
                monthList = value;
                RaisePropertyChanged("MonthList");
            }
        }

        private List<ProductStatStruct> productlineList;
        public List<ProductStatStruct> ProductlineList
        {
            get { return productlineList; }
            set
            {
                productlineList = value;
                RaisePropertyChanged("ProductlineList");
            }
        }

        private ProductStatStruct selectedProductline;
        public ProductStatStruct SelectedProductline
        {
            get { return selectedProductline; }
            set
            {
                selectedProductline = value;
                GetProduct();
                GetBestMonth();
                GetEmergencyProducts();
                RaisePropertyChanged("SelectedProductline");
            }
        }

        private void GetEmergencyProducts()
        {
            List<ProductShortageStruct> e = new List<ProductShortageStruct>();

            foreach (Product pr in prService.GetAll().ToList().FindAll(p => p.productlineId == SelectedProductline.id))
                e.Add(new ProductShortageStruct(pr));
            
            EmergencyProductList = e.FindAll(ps => ps.itemShortage > 0);
        }

        private void GetBestMonth()
        {
            if (SelectedProductline == null)
                BestMonth = "";
            else
            {
                if (SelectedYear == -1)
                    BestMonth = "";
                else
                {
                    List<MonthStruct> list = new List<MonthStruct>() { new MonthStruct() { month = 1, display = "January" }, new MonthStruct() { month = 2, display = "February" }, new MonthStruct() { month = 3, display = "March" }, new MonthStruct() { month = 4, display = "April" }, new MonthStruct() { month = 5, display = "May" }, new MonthStruct() { month = 6, display = "June" }, new MonthStruct() { month = 7, display = "July" }, new MonthStruct() { month = 8, display = "August" }, new MonthStruct() { month = 9, display = "September" }, new MonthStruct() { month = 10, display = "October" }, new MonthStruct() { month = 11, display = "November" }, new MonthStruct() { month = 12, display = "December" } };

                    int max = 0, id = 0;

                    for (int i = 1; i <= 12; i++)
                    {
                        int count = pService.GetCountSold(SelectedProductline.id, SelectedYear, i);
                        if (max < count)
                        {
                            max = count;
                            id = i;
                        }
                    }

                    if (id > 0)
                        BestMonth = list[id].display;
                    else
                        BestMonth = "";
                }
            }

        }

        private string bestMonth;
        public string BestMonth
        {
            get { return bestMonth; }
            set
            {
                bestMonth = value;
                RaisePropertyChanged("BestMonth");
            }
        }

        //public RelayCommand<EventArgs> SelectionChangedCommand
        //{
        //    get { return new RelayCommand<EventArgs>(ProductlineListRefresh); }
        //}
    }
}
