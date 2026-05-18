using eHotelMartinez.DataAccess.Context;
using eHotelMartinez.Domain.Entities.Order;
using eHotelMartinez.Domain.Enums;
using eHotelMartinez.Domain.Models.Base;
using eHotelMartinez.Domain.Models.Order;
using Microsoft.EntityFrameworkCore;

namespace eHotelMartinez.BusinessLogic.Core.Order
{
    public class OrderActions
    {
        protected async Task<ResponseMsg> ExecuteAddToCartAction(int userId, OrderItemDTO item, decimal price)
        {
            if (userId <= 0)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "User ID is invalid"
                };
            }
            if (item.ItemId <= 0)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Item Id is invalid"
                };
            }
            if (item.Quantity <= 0)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Quantity of Item must be greater than 0"
                };
            }

            await using (var db = new UserContext())
            {
                var cart = await db.Orders.FirstOrDefaultAsync(o => o.UserId == userId && o.Status == OrderStatus.Pending);
                
                if (cart == null)
                {
                    cart = new OrderData
                    {
                        UserId = userId,
                        Status = OrderStatus.Pending,
                        TotalSum = 0,
                        CreatedAt = DateTime.Now,
                    };
                    db.Orders.Add(cart);
                    await db.SaveChangesAsync();
                }

                var cartItem = new OrderItemData
                {
                    OrderId = cart.Id,
                    Type = item.Type,
                    ItemId = item.ItemId,
                    Quantity = item.Quantity,
                    PriceAtPurchase = price,
                    CreatedAt = DateTime.Now
                };
                db.OrderItems.Add(cartItem);
                await db.SaveChangesAsync();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Item added to Cart successfuly!"
                };
            }
        }
        protected async Task<OrderDTO> ExecuteGetCartAction (int userId)
        {
            if (userId <= 0) return null;
            
            await using (var db = new UserContext())
            {
                var order = await db.Orders
                    .Include(o => o.OrderItems)
                    .Where(o => o.UserId == userId && o.Status == OrderStatus.Pending)
                    .FirstOrDefaultAsync();

                if (order == null) return null;

                return MapOrderToDTO(order);
            }
        }
        protected async Task<List<OrderDTO>> ExecuteGetOrderHistoryAction(int userId)
        {
            if (userId <= 0) return null;

            await using (var db = new UserContext())
            {
                var history = await db.Orders
                    .Include(o => o.OrderItems)
                    .Where(o => o.UserId == userId && o.Status == OrderStatus.Completed)
                    .ToListAsync();

                return history.Select(o => MapOrderToDTO(o)).ToList();
            }
        }
        protected async Task<ResponseMsg> ExecuteCheckoutAction(int userId)
        {
            if (userId <= 0)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "User ID is invalid"
                };
            }
            await using (var db = new UserContext())
            {
                var cart = await db.Orders
                    .Include(o => o.OrderItems)
                    .Where(o => o.UserId == userId && o.Status == OrderStatus.Pending)
                    .FirstOrDefaultAsync();

                if (cart == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Cart doesn't exist!"
                    };
                }
                if (!cart.OrderItems.Any())
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Cart is empty"
                    };
                }

                cart.TotalSum = cart.OrderItems.Sum(i => i.PriceAtPurchase * i.Quantity);
                cart.Status = OrderStatus.Completed;
                await db.SaveChangesAsync();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Order completed successfuly"
                };
                
            }
        }
        protected async Task<ResponseMsg> ExecuteRemoveFromCartAction(int orderItemId)
        {
            if (orderItemId <= 0)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Item Id is invalid"
                };
            }
            await using (var db = new UserContext())
            {
                var item = await db.OrderItems.FirstOrDefaultAsync(i => i.Id == orderItemId);
                if (item == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Item doesn't exist in Order"
                    };
                }
                db.OrderItems.Remove(item);
                await db.SaveChangesAsync();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Item removed from cart successfuly"
                };
            }
        }
        protected async Task<ResponseMsg> ExecuteUpdateCartItemQuantityAction(int orderItemId, int quantity)
        {
            if (orderItemId <= 0)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Item Id is invalid"
                };
            }
            if (quantity <= 0)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Quantity of Item is invalid"
                };
            }

            await using (var db = new UserContext())
            {
                var item = await db.OrderItems.FirstOrDefaultAsync(i => i.Id == orderItemId);
                if (item == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Item doesn't exist in Order"
                    };
                }
                item.Quantity = quantity;
                await db.SaveChangesAsync();
                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Item Quantity updated successfuly"
                };

            }
        }
        protected OrderDTO MapOrderToDTO(OrderData order)
        {
            return new OrderDTO
            {
                Id = order.Id,
                UserId = order.UserId,
                Status = order.Status,
                TotalSum = order.TotalSum,
                CreatedAt = order.CreatedAt,
                OrderItems = order.OrderItems?.Select(i => new OrderItemDTO
                {
                    Id = i.Id,
                    OrderId = i.OrderId,
                    Type = i.Type,
                    ItemId = i.ItemId,
                    Quantity = i.Quantity,
                    PriceAtPurchase = i.PriceAtPurchase,
                    CreatedAt = i.CreatedAt,
                }).ToList() ?? new List<OrderItemDTO>()
            };
        }
    }
}