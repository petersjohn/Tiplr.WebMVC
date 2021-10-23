using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Tiplr.Data;
using Tiplr.Models;

namespace Tiplr.Services
{
    public class ProductService
    {
        private readonly Guid _userId;

        public ProductService(Guid userId)
        {
            _userId = userId;
        }

        public ProductCreate GetCreateView()
        {
            var ctx = new ApplicationDbContext();
            var viewModel = new ProductCreate();
            viewModel.ProductCategories = ctx.ProductCategories.Select(category => new SelectListItem
            {
                Text = category.CategoryName,
                Value = category.CategoryId.ToString()
            });
            return viewModel;
        }

        public bool CreateProduct(ProductCreate model)
        {
            var entity = new Product()
            {
                ProductName = model.ProductName,
                ProductDescription = model.ProductDescription,
                ProductCategory = model.ProductCategory,
                CountBy = model.CountBy,
                OrderBy = model.OrderBy,
                UnitsPerPack = model.UnitsPerPack,
                UnitPrice = model.UnitPrice,
                Par = model.Par,
                Active = true,
                CreatedDtTm = DateTime.Now,
                LastModifiedDtTm = DateTime.Now,
                InactiveDtTm = null,
                UserId = _userId.ToString()
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Products.Add(entity);
                return ctx.SaveChanges() == 1;
            }

        }
    }
}
