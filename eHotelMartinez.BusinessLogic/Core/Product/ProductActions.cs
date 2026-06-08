using eHotelMartinez.BusinessLogic.Helpers;
using eHotelMartinez.DataAccess.Context;
using eHotelMartinez.Domain.Entities.Product;
using eHotelMartinez.Domain.Entities.Room;
using eHotelMartinez.Domain.Enums;
using eHotelMartinez.Domain.Models.Base;
using eHotelMartinez.Domain.Models.Product;
using eHotelMartinez.Domain.Models.Room;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace eHotelMartinez.BusinessLogic.Core.Products
{
    public class ProductActions
    {
        protected async Task<List<ProductDTO>> ExecuteGetAllProducts()
        {
            await using var db = new CategoryContext();

            var categories = await db.Categories
                .AsNoTracking()
                .Where(c => c.IsActive)
                .ToDictionaryAsync(c => c.Id, c => c.Name);

            var products = await db.Products
                .AsNoTracking()
                .Include(p => p.Images)
                .Where(p => p.Status != ProductStatus.Inactive)
                .ToListAsync();
            return products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Category = p.CategoryId.HasValue && categories.TryGetValue(p.CategoryId.Value, out var categoryName) ? categoryName : null,
                Price = p.Price,
                Images = p.Images
                    .Where(i => i.IsActive)
                    .Select(i => new ProductImageDTO
                    {
                        Url = i.Url
                    }).ToList(),
                Stock = p.Stock,
                RequireBooking = p.RequireBooking,
                Status = p.Status
            }).ToList();
        }


        protected async Task<ProductDTO> ExecuteGetProductById(int id)
        {
            await using var db = new CategoryContext();

            var categories = await db.Categories
                .AsNoTracking()
                .Where(c => c.IsActive)
                .ToDictionaryAsync(c => c.Id, c => c.Name);

            var p = await db.Products
                .AsNoTracking()
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id && p.Status != ProductStatus.Inactive);

            if(p == null)
                return null;

            return new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Category = p.CategoryId.HasValue && categories.TryGetValue(p.CategoryId.Value, out var categoryName) ? categoryName : null,
                Price = p.Price,
                Images = p.Images
                    .Where(i => i.IsActive)
                    .Select(i => new ProductImageDTO
                    {
                        Url = i.Url
                    }).ToList(),
                Stock = p.Stock,
                RequireBooking = p.RequireBooking,
                Status = p.Status
            };
        }

        protected async Task<ResponseAction> ExecuteCreateProductAction(CreateProductDTO product)
        {

            if (string.IsNullOrWhiteSpace(product.Name))
                return new ResponseAction { IsSuccess = false, Message = "Product name is required." };

            if (product.Price <= 0)
                return new ResponseAction { IsSuccess = false, Message = "Price must be greater than 0." };

            if (product.Stock < 0)
                return new ResponseAction { IsSuccess = false, Message = "Stock must be greater than or equal to 0." };

            if (!await CategoryCheck.CategoryExists(product.CategoryId))
                return new ResponseAction { IsSuccess = false, Message = "Category does not exist." };

            using (var db = new CategoryContext())
            {
                var existingProduct = await db.Products.FirstOrDefaultAsync(p => p.Name.ToLower() == product.Name.ToLower() && p.Status == ProductStatus.Active);

                if (existingProduct != null)
                {
                    return new ResponseAction
                    {
                        IsSuccess = false,
                        Message = "A product with the same name already exists.",
                        Id = existingProduct.Id
                    };
                }
            }

            var productData = new ProductData
            {
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.CategoryId,
                Price = product.Price,
                Images = product.Images?.Select(imageData => new ProductImageData
                {
                    Url = imageData.Url
                }).ToList() ?? new List<ProductImageData>(),
                Status = ProductStatus.Active,
                RequireBooking = product.RequireBooking,
                Stock = product.Stock,
            };

            using (var db = new CategoryContext())
            {
                db.Products.Add(productData);
                await db.SaveChangesAsync();
            }

            return new ResponseAction
            {
                IsSuccess = true,
                Message = "Product created successfully.",
                Id = productData.Id
            };
        }

        protected async Task<ResponseMsg> ExecuteUpdateProductAction(UpdateProductDTO product)
        {
            using (var db = new CategoryContext())
            {
                var existingProduct = await db.Products
                    .Include(p => p.Images)
                    .FirstOrDefaultAsync(p => p.Id == product.Id);

                if (existingProduct == null)
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Product not found."
                    };

                if (product.Stock < 0)
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Stock must be greater or equal to 0."
                    };

                if (!string.IsNullOrWhiteSpace(product.Name))
                    existingProduct.Name = product.Name;

                if (!string.IsNullOrWhiteSpace(product.Description))
                    existingProduct.Description = product.Description;

                if (!await CategoryCheck.CategoryExists(product.CategoryId))
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Category does not exist."
                    };
                existingProduct.CategoryId = product.CategoryId;

                if (product.Price <= 0)
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Price must be greater than 0."
                    };

                if (product.Price > 0) existingProduct.Price = product.Price;

                var existingUrls = existingProduct.Images
                .Where(i => i.IsActive)
                .Select(i => i.Url)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

                foreach (var image in existingProduct.Images.Where(i => i.IsActive))
                {
                    image.IsActive = false;
                }

                foreach (var img in product.Images)
                {
                    var url = img.Url?.Trim();
                    if (string.IsNullOrWhiteSpace(url))
                        continue;

                    var existingImage = existingProduct.Images.FirstOrDefault(i =>
                    i.Url.Equals(url, StringComparison.OrdinalIgnoreCase));

                    if (existingImage != null)
                    {
                        existingImage.IsActive = true;
                    }
                    else
                    {
                        existingProduct.Images.Add(new ProductImageData
                        {
                            Url = url,
                            IsActive = true
                        });
                    }
                }

                if (product.RequireBooking != null) existingProduct.RequireBooking = product.RequireBooking;

                if (product.Stock >= 0) existingProduct.Stock = product.Stock;

                if (product.Stock == 0) existingProduct.Status = ProductStatus.OutOfStock;
                else
                    existingProduct.Status = (ProductStatus)product.Status;

                await db.SaveChangesAsync();
            }

            return new ResponseMsg
            {
                IsSuccess = true,
                Message = "Product updated successfully."
            };
        }

        protected async Task<ResponseMsg> ExecuteDeleteProductAction(int id)
        {
            using (var db = new CategoryContext())
            {
                var existingProduct = await db.Products
                    .Include(p => p.Images)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (existingProduct == null)
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Product not found."
                    };

                existingProduct.Status = ProductStatus.Inactive;
                existingProduct.CategoryId = null;

                foreach (var image in existingProduct.Images)
                {
                    image.IsActive = false;
                }

                await db.SaveChangesAsync();
            }

            return new ResponseMsg
            {
                IsSuccess = true,
                Message = "Product deleted successfully."
            };
        }
    }
}
