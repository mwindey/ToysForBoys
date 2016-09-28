﻿using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.Queries;

namespace WPFToysForBoys.Mock
{
    public class ProductStatisticServiceMock : IProductStatisticService
    {
        public BestSoldProduct GetBestSoldProduct(int productId, int productlineId)
        {
            throw new NotImplementedException();
        }

        public List<BestSoldProduct> GetBestSoldProducts()
        {
            throw new NotImplementedException();
        }

        public int GetCountSold(int productlineID, int year, int month)
        {
            using (var entities = new toysforboysEntities())
            {
                int count = 0;
                if (year != -1)
                {
                    var query = from od in entities.orderdetails
                                join p in entities.products on od.productId equals p.id
                                join o in entities.orders on od.orderId equals o.id
                                where p.productlineId == productlineID
                                && o.orderDate.Value.Year == year
                                select od;

                    foreach (var od in query)
                        count += od.quantityOrdered.Value;
                }
                else
                {
                    var query = from od in entities.orderdetails
                                join p in entities.products on od.productId equals p.id
                                where p.productlineId == productlineID
                                select od;

                    foreach (var od in query)
                        count += od.quantityOrdered.Value;
                }

                return count;


            }
        }

        public List<BestSoldProduct> GetProductsSortedByLeastSold()
        {
            throw new NotImplementedException();
        }

        public List<BestSoldProduct> GetProductsSortedByMostSold()
        {
            throw new NotImplementedException();
        }

        List<Product> IProductStatisticService.GetProductStatistics(ProductQuery productQuery)
        {
            throw new NotImplementedException();
        }
    }
}
