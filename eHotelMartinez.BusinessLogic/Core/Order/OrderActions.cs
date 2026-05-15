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
        protected ResponseMsg ExecuteAddToCartAction(int userId, OrderItemDTO item, decimal price)
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

            using (var db = new UserContext())
            {
                var cart = db.Orders.FirstOrDefault(o => o.UserId == userId && o.Status == OrderStatus.Pending);
                
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
                    db.SaveChanges();
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
                db.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Item added to Cart successfuly!"
                };
            }
        }
        protected OrderDTO ExecuteGetCartAction (int userId)
        {
            if (userId <= 0) return null;
            
            using (var db = new UserContext())
            {
                var order = db.Orders
                    .Include(o => o.OrderItems)
                    .Where(o => o.UserId == userId && o.Status == OrderStatus.Pending)
                    .FirstOrDefault();

                if (order == null) return null;

                return MapOrderToDTO(order);
            }
        }
        protected List<OrderDTO> ExecuteGetOrderHistoryAction(int userId)
        {
            if (userId <= 0) return null;

            using (var db = new UserContext())
            {
                var history = db.Orders
                    .Include(o => o.OrderItems)
                    .Where(o => o.UserId == userId && o.Status == OrderStatus.Completed)
                    .ToList();

                return history.Select(o => MapOrderToDTO(o)).ToList();
            }
        }
        protected ResponseMsg ExecuteCheckoutAction(int userId)
        {
            if (userId <= 0)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "User ID is invalid"
                };
            }
            using (var db = new UserContext())
            {
                var cart = db.Orders
                    .Include(o => o.OrderItems)
                    .Where(o => o.UserId == userId && o.Status == OrderStatus.Pending)
                    .FirstOrDefault();

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
                db.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Order completed successfuly"
                };
                
            }
        }
        protected ResponseMsg ExecuteRemoveFromCartAction(int orderItemId)
        {
            if (orderItemId <= 0)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Item Id is invalid"
                };
            }
            using (var db = new UserContext())
            {
                var item = db.OrderItems.FirstOrDefault(i => i.Id == orderItemId);
                if (item == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Item doesn't exist in Order"
                    };
                }
                db.OrderItems.Remove(item);
                db.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Item removed from cart successfuly"
                };
            }
        }
        protected ResponseMsg ExecuteUpdateCartItemQuantityAction(int orderItemId, int quantity)
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

            using (var db = new UserContext())
            {
                var item = db.OrderItems.FirstOrDefault(i => i.Id == orderItemId);
                if (item == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Item doesn't exist in Order"
                    };
                }
                item.Quantity = quantity;
                db.SaveChanges();
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