﻿using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public class ProductlineService : IProductlineService
    {
        public void Insert(Productline productline)
        {
            using (var entities = new toysforboysEntities())
            {
                entities.productlines.Add(productline);
                entities.SaveChanges();
            }
        }

        public void Delete(Productline productline)
        {
            using (var entities = new toysforboysEntities())
            {
                entities.productlines.Remove(productline);
                entities.SaveChanges();
            }
        }

        public void Edit(Productline productline)
        {

            using (var entities = new toysforboysEntities())
            {
                var originalProductline = entities.productlines.Find(productline.id);
                originalProductline.name = productline.name;
                originalProductline.description = productline.description;

                entities.SaveChanges();
            }
        }

        public IEnumerable<Productline> GetAll()
        {
            return GetAll(string.Empty);
        }

        public Productline GetById(int productlineID)
        {
            return GetById(productlineID, string.Empty);        
        }

        public Productline GetById(int productlineID, string includes)
        {
            using (var entities = new toysforboysEntities())
            {
                var query = (from productline in entities.productlines
                             where productline.id == productlineID
                             orderby productline.id
                             select productline).Include(includes).First();

                return query;
            }
        }


        public IEnumerable<Productline> GetAll(string includes)
        {
            return GetAll(c => true, includes);
        }
        
        public IEnumerable<Productline> GetAll(Func<Productline, bool> predicate, string includes)
        {
            List<Productline> AllProductlines = new List<Productline>();
            using (var entities = new toysforboysEntities())
            {
                
                foreach (var Productline in ((DbSet) entities.productlines.Where(predicate)).Include(includes))
                {
                    AllProductlines.Add((Productline) Productline);
                }
            }

            return AllProductlines;
        }

       
    }
}
