﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using System.Windows;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.SqlClient;

namespace DataAccessLayer.Services
{
    public class StatisticService : IStatisticService
    {
        public List<Order> GetFilteredStatistics(List<Order> orders, SortDateEnum SortDateCompareLeft, char DateCompareMode, SortDateEnum SortDateCompareRight)
        {
            var filteredOrders = new List<Order>();
            var datesLeft = GetDateTimes(SortDateCompareLeft, orders);
            var datesRight = GetDateTimes(SortDateCompareRight, orders);

            for (int i = 0; i < orders.Count; i++)
            {
                switch (DateCompareMode)
                {
                    case '=':
                        if (datesLeft[i] == datesRight[i])
                        {
                            filteredOrders.Add(orders[i]);
                        }
                        break;
                    case '<':
                        if (datesLeft[i]<datesRight[i])
                        {
                            filteredOrders.Add(orders[i]);
                        }
                        break;
                    case '>':
                        if (datesLeft[i]>datesRight[i])
                        {
                            filteredOrders.Add(orders[i]);
                        }
                        break;
                }
            }

            return filteredOrders;
        }

        public List<Order> GetStatistics(OrderQuery orderQuery)
        {

            var queryString = new StringBuilder();
            queryString.Append("from order in entities.orders where ");

            //toevoegen kiezen van soort datum uit enum als deze megegeven wordt als parameter
            if (orderQuery.SortDateRange != null)
            {
                switch ((int)orderQuery.SortDateRange)
                {
                    case 0:
                        queryString.Append("order.orderDate ");
                        break;
                    case 1:
                        queryString.Append("order.requiredDate ");
                        break;
                    case 2:
                        queryString.Append("order.shippedDate ");
                        break;

                    default:
                        break;
                }
            }

            //checken of DateRangeStart is ingevuld
            bool DateRangeUsed = false;
            if (orderQuery.DateRangeStart != null)
            {
                queryString.Append("> " + orderQuery.DateRangeStart.ToString() + " ");
                DateRangeUsed = true;
            }

            if (orderQuery.DateRangeEnd != null)
            {
                queryString.Append("< " + orderQuery.DateRangeEnd.ToString() + " ");
                DateRangeUsed = true;
            }

            //checken of customer ID is ingevuld
            bool customerIdUsed = false;
            if (orderQuery.CustomerId != null)
            {
                if (DateRangeUsed == true)
                {
                    queryString.Append("and where ");
                }

                queryString.Append("order.customerId == " + orderQuery.CustomerId +" ");
                customerIdUsed = true;
            }

            //Checken of status is ingevuld
            if (orderQuery.Status != string.Empty)
            {
                if (DateRangeUsed == true || customerIdUsed == true)
                {
                    queryString.Append("and where ");
                }

                queryString.Append("order.status == " + orderQuery.Status + " ");
            }


            //effectieve query uitvoeren en list van orders teruggeven
            queryString.Append("select order");

            using (var entities = new toysforboysEntities())
            {
                var cmd = new SqlCommand(queryString.ToString());
                var orders = (List<Order>)cmd.ExecuteScalar();
                return orders;
            }
        }

        //Switch method
        private List<DateTime> GetDateTimes(SortDateEnum sortDateEnum, List<Order> orders)
        {
            List<DateTime> datetime = new List<DateTime>();

            foreach (var order in orders)
            {
                switch (sortDateEnum)
                {
                    case SortDateEnum.orderDate: datetime.Add(Convert.ToDateTime(order.orderDate));
                        break;
                    case SortDateEnum.requiredDate: datetime.Add(Convert.ToDateTime(order.requiredDate));
                        break;
                    case SortDateEnum.shippedDate: datetime.Add(Convert.ToDateTime(order.shippedDate));
                        break;                   
                }
            }
            return datetime;
        }
    }
}
