using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiplr.Data;
using Tiplr.Models;

namespace Tiplr.Services
{
    public class InventoryItemService
    {
        private readonly Guid _userId;

        public InventoryItemService(Guid userId)
        {
            _userId = userId;
        }

        public bool InventoryCountCreate(InventoryItemCreate model)
        {
            var entity = new InventoryItem()
            {
                InventoryId = model.InventoryId,
                ProductId = model.ProductId,
                OnHandCount = model.OnHandCount,
                LastModifiedDtTm = DateTimeOffset.Now,
                Id = model.ApplicationUser.Id
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.InventoryItems.Add(entity);
                return ctx.SaveChanges() == 1;
            }

        }

        public IEnumerable<InventoryCountItem> GetOnHandInventory(int inventoryId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.InventoryItems.Where(e => e.InventoryId == inventoryId).OrderBy(e => e.Product.ProductCategory).ThenBy(e => e.Product.ProductName)
                    .Select(e => new InventoryCountItem
                    {
                        InventoryItemId = e.InventoryItemId,
                        InventoryId = e.InventoryId,
                        ProductId = e.ProductId,
                        OnHandCount = e.OnHandCount
                    });
                return query.ToArray();
            }
        }

        public IEnumerable<InventoryCountItem> GetCountsByProductCategory(int inventoryId, int productCatId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                /*var query = ctx.InventoryItems
                    .Join(ctx.Products,
                    inv => inv.InventoryId,
                    prd => prd.ProductId,
                    (inv,prd)=>new {Inv = inv, Prd = prd})
                    .Select(e=> new InventoryCountItem*/
                var query = ctx.InventoryItems.Where(q => q.InventoryId == inventoryId && q.Product.CategoryId == productCatId).OrderBy(q => q.Product.ProductName)
                    .Select(q => new InventoryCountItem
                    {
                        InventoryItemId = q.InventoryItemId,
                        InventoryId = q.InventoryId,
                        ProductId = q.ProductId,
                        OnHandCount = q.OnHandCount
                    });
                return query.ToArray();

            }
        }

        public InvItemDetail GetInvItemDetail(int invItemId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.InventoryItems.Single(e => e.InventoryItemId == invItemId);
                return new InvItemDetail
                {
                    InventoryItemId = entity.InventoryItemId,
                    InventoryId = entity.InventoryId,
                    ProductId = entity.ProductId,
                    OnHandCount = entity.OnHandCount,
                    UpdtUser = entity.Id //user string guid

                };
            }
        }

        public bool UpdateInvItem(InvItemEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.InventoryItems.Single(e => e.InventoryItemId == model.InventoryItemId);
                entity.OnHandCount = model.OnHandCount;
                entity.LastModifiedDtTm = DateTimeOffset.Now;
                entity.Id = model.Id;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteInvItem(int invItemId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.InventoryItems.Single(e => e.InventoryItemId == invItemId);
                ctx.InventoryItems.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }


    }

}
