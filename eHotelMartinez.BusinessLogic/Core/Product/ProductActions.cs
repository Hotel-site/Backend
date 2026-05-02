using eHotelMartinez.DataAccess.Context;
using eHotelMartinez.Domain.Entities.Product;
using eHotelMartinez.Domain.Models.Base;
using eHotelMartinez.Domain.Models.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using eHotelMartinez.Domain.Enums;
using eHotelMartinez.BusinessLogic.Helpers;

namespace eHotelMartinez.BusinessLogic.Core.Products
{
    public class ProductActions
    {
        protected List<ProductDTO> ExecuteGetAllProducts()
        {
            using (var db = new ProductContext())
            {
                return db.Products
                    .AsNoTracking()
                    .Include(p => p.Images)
                   .Where(p => p.Status == ProductStatus.Active)
                   .Join(db.Categories,
                   p => p.CategoryId,
                   c => c.Id,
                   (p, c) => new ProductDTO
                   {
                       Id = p.Id,
                       Name = p.Name,
                       Description = p.Description,
                       Category = c.Name,
                       Price = p.Price,
                       Images = p.Images.Select(i => new ProductImageDTO
                       {
                           Url = i.Url
                       }).ToList(),
                       Stock = p.Stock,
                       Status = p.Status
                   }).ToList();
            }
        }

        protected ProductDTO ExecuteGetProductById(int id)
        {
            using (var db = new ProductContext())
            {
                var p = db.Products.FirstOrDefault(p => p.Id == id && p.Status == ProductStatus.Active);
                if (p == null)
                    return null;

                return db.Products
                    .AsNoTracking()
                    .Include(p => p.Images)
                   .Where(p => p.Status == ProductStatus.Active)
                   .Join(db.Categories,
                   p => p.CategoryId,
                   c => c.Id,
                   (p, c) => new ProductDTO
                   {
                       Id = p.Id,
                       Name = p.Name,
                       Description = p.Description,
                       Category = c.Name,
                       Price = p.Price,
                       Images = p.Images.Select(i => new ProductImageDTO
                       {
                           Url = i.Url
                       }).ToList(),
                       Stock = p.Stock,
                       Status = p.Status
                   }).First();
            }
        }

        protected ResponseMsg ExecuteCreateProductAction(ProductData product)
        {

            if (string.IsNullOrWhiteSpace(product.Name))
                return new ResponseMsg { IsSuccess = false, Message = "Product name is required." };

            if (product.Price <= 0)
                return new ResponseMsg { IsSuccess = false, Message = "Price must be greater than 0." };

            if (product.Stock < 0)
                return new ResponseMsg { IsSuccess = false, Message = "Stock must be greater than or equal to 0." };

            if (CategoryExists(product.CategoryId) == false)
                return new ResponseMsg { IsSuccess = false, Message = "Category does not exist." };

            using (var db = new ProductContext())
            {
                var existingProduct = db.Products.Any(p => p.Name.ToLower() == product.Name.ToLower() && p.Status == ProductStatus.Active);

                if (existingProduct != null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "A product with the same name already exists."
                    };
                }
            }

            var productData = new ProductData
            {
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.CategoryId,
                Price = product.Price,
                Images = product.Images?.Select(imgDto => new ProductImageData
                {
                    ProductId = product.Id,
                    Url = imgDto.Url
                }).ToList() ?? new List<ProductImageData>(),
                Status = ProductStatus.Active,
                Stock = product.Stock,
            };

            using (var db = new ProductContext())
            {
                db.Products.Add(productData);
                db.SaveChanges();
            }

            return new ResponseMsg
            {
                IsSuccess = true,
                Message = "Product created successfully."
            };
        }

        protected ResponseMsg ExecuteUpdateProductAction(UpdateProductDTO product)
        {
            using (var db = new ProductContext())
            {
                var existingProduct = db.Products
                    .Include(p => p.Images)
                    .FirstOrDefault(p => p.Id == product.Id);

                if (existingProduct == null)
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Product not found."
                    };

                if (product.Price < 0)
                    return new ResponseMsg { IsSuccess = false, Message = "Price must be greater or equal to 0." };

                if (product.Stock < 0)
                    return new ResponseMsg { IsSuccess = false, Message = "Stock must be greater than or equal to 0." };


                if (!string.IsNullOrWhiteSpace(product.Name)) 
                    existingProduct.Name = product.Name;
                if (!string.IsNullOrWhiteSpace(product.Description)) 
                    existingProduct.Description = product.Description;

                if (product.CategoryId > 0 && product.CategoryId != existingProduct.CategoryId)
                {
                    if (CategoryExists(product.CategoryId) == false)
                        return new ResponseMsg { IsSuccess = false, Message = "Category does not exist." };
                    existingProduct.CategoryId = product.CategoryId;
                }

                if (product.Price > 0) existingProduct.Price = product.Price;

                foreach (var image in existingProduct.Images)
                {
                    var exists = existingProduct.Images
                        .Any(i => i.Url == image.Url && i.IsActive);

                    if(!exists)
                    {
                        existingProduct.Images.Add(new ProductImageData
                        {
                            ProductId = existingProduct.Id,
                            Url = image.Url,
                        });
                    }
                }

                if (product.Stock >= 0) existingProduct.Stock = product.Stock;
                existingProduct.Status = (ProductStatus)product.Status;
                db.SaveChanges();
            }

            return new ResponseMsg
            {
                IsSuccess = true,
                Message = "Product updated successfully."
            };
        }

        protected ResponseMsg ExecuteDeleteProductAction(int id)
        {
            using (var db = new ProductContext())
            {
                var existingProduct = db.Products
                    .Include(p => p.Images)
                    .FirstOrDefault(p => p.Id == id);

                if (existingProduct == null)
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Product not found."
                    };

                existingProduct.Status = ProductStatus.Inactive;
                existingProduct.CategoryId = null;

                foreach (var img in existingProduct.Images)
                {
                    img.IsActive = false;
                }

                db.SaveChanges();
            }

            return new ResponseMsg
            {
                IsSuccess = true,
                Message = "Product deleted successfully."
            };
        }

        private static bool CategoryExists(int? categoryId)
        {
            using (var db = new ProductContext())
            {
                return db.Categories.Any(c => c.Id == categoryId && c.IsActive);
            }
        }

    }
}
