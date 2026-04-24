using eHotelMartinez.DataAccess.Context;
using eHotelMartinez.Domain.Entities.Product;
using eHotelMartinez.Domain.Models.Base;
using eHotelMartinez.Domain.Models.Product;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;


namespace eHotelMartinez.BusinessLogic.Core.Products
{
    public class ProductActions
    {
        protected List<ProductDTO> ExecuteGetAllProducts()
        {
            using (var db = new ProductContext())
            {
                return db.Products
                   .Where(p => p.IsActive)
                   .Select(p => new ProductDTO
                   {
                       Id = p.Id,
                       Name = p.Name,
                       Description = p.Description,
                       Price = p.Price,
                       Images = p.Images,
                   }).ToList();
            }
        }

        protected ProductDTO ExecuteGetProductById(int id)
        {
            using (var db = new ProductContext())
            {
                var product = db.Products.FirstOrDefault(p => p.Id == id && p.IsActive);
                if (product == null)
                    return null;
                return new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Images = product.Images,
                };
            }
        }

        protected ResponseMsg ExecuteCreateProductAction(ProductDTO product)
        {

            if (string.IsNullOrWhiteSpace(product.Name))
                return new ResponseMsg { IsSuccess = false, Message = "Product name is required." };

            if (product.Price <= 0)
                return new ResponseMsg { IsSuccess = false, Message = "Price must be greater than 0." };

            using (var db = new ProductContext())
            {
                var existingProduct = db.Products.FirstOrDefault(p => p.Name.ToLower() == product.Name.ToLower() && p.IsActive);

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
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Images = product.Images.ToList(),

                IsActive = true
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
                var existingProduct = db.Products.FirstOrDefault(p => p.Id == product.Id && p.IsActive);

                if (existingProduct == null)
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Product not found."
                    };

                if(!string.IsNullOrWhiteSpace(product.Name)) existingProduct.Name = product.Name;
                if (product.Price > 0) existingProduct.Price = product.Price;
                if (!string.IsNullOrWhiteSpace(product.Description)) existingProduct.Description = product.Description;
                
                if (product.ClearImages)
                {
                    existingProduct.Images.Clear();
                }

                if (product.RemoveImageById != null && product.RemoveImageById.Any())
                {
                    var imagesToRemove = existingProduct.Images
                        .Where(i => product.RemoveImageById.Contains(i.Id))
                        .ToList();

                    foreach (var image in imagesToRemove)
                    {
                        existingProduct.Images.Remove(image);
                    }
                }

                if (product.AddImages != null && product.AddImages.Any())
                {
                    foreach (var url in product.AddImages)
                    {
                        existingProduct.Images.Add(new ProductImgData
                        {
                            Url = url,
                            ProductId = existingProduct.Id
                        });
                    }
                }

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
                var existingProduct = db.Products.FirstOrDefault(p => p.Id == id);

                if (existingProduct == null)
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Product not found."
                    };

                existingProduct.IsActive = false;
                db.SaveChanges();
            }

            return new ResponseMsg
            {
                IsSuccess = true,
                Message = "Product deleted successfully."
            };
        }
    }
}
