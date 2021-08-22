using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_API.DTO;
using E_Commerce_Api.Interface;
using E_Commerce_API.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace E_Commerce_Api.Servicies
{
    public class OrderService : IOrder
    {
        //dependency injection
        #region
        private readonly E_CommerceContext _Context;
        private readonly IMapper _mapper;
        #endregion
        public OrderService(E_CommerceContext context , IMapper mapper)
        {
            _Context = context;
            _mapper = mapper;
        } 
        public async Task<Response> CreateOrderAsync(OrderDTO OrderDto)
        {
            var order = _mapper.Map<Order>(OrderDto);
            try
            {
                _Context.Add(order);
                await _Context.SaveChangesAsync();
                //obj from OrderDetailService to create orderDetaile after create order
                var createOrderDetail = new OrderDetailService(_Context ,_mapper);
                var OrderItems = await createOrderDetail.CreateOrderDetailAsync(OrderDto.OrderDetailes , order.OrderId);
                if (OrderItems.Code != 200)
                    return OrderItems;
                return new Response { Code = 200, Data = order, Message = "Create Successfully" };
            }
            catch (Exception e)
            {
                return new Response { Code = 400, Message = e.Message };
            }

        }

        public async Task<Response> CancelOrderAsync(int id)
        {
            var result =await _Context.Orders.Where(x => x.OrderId == id).FirstOrDefaultAsync();
            if (result != null)
            {
                //call func to delete all items in order 
               var DeletItems= await DeleteOrderDetailAsync(id);
                if (DeletItems.Code !=200)
                    return DeletItems;
                try
                {
                    _Context.Remove(result);
                  await  _Context.SaveChangesAsync();
                    return new Response { Code = 200, Data = result, Message = "Delete Successfully" };
                }
                catch (Exception e)
                {
                    return new Response { Code = 400, Data = e.Data, Message = e.Message };
                }
            }
            return new Response { Code = 400, Message = "not exist in database" };
        }

        public async Task<Response> GetOrderById(int id)
        {
            var result =await _Context.Orders.Include(x => x.CustomerUser).Include(x=>x.OrderDetailes).Where(x => x.OrderId == id).FirstOrDefaultAsync();

            if (result != null)
                return new Response { Code = 200, Data = result, Message = "Get Successfully" };
            return new Response { Code = 400, Message = "Not Exist in Database" };
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            var Orders =await _Context.Orders.Include(x => x.OrderDetailes).ToListAsync();

            if (Orders.Count != 0)
                return Orders;
            return null;
        }

        //this func delete all items that in the same order and increase the quantity of products
        public async Task<Response> DeleteOrderDetailAsync(int id)
        {
            var result = _Context.OrderDetails.Where(x => x.OrderId == id).ToList();
            if (result != null)
            {
                var productid =await _Context.OrderDetails.Include(x => x.Product).Where(x => x.OrderId == id).ToListAsync();
                foreach (var item in productid)
                {
                    var product = _Context.Products.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
                    product.Quanity += item.Quantity;
                    _Context.Update(product);
                }
                try
                {
                    foreach (var item in result)
                        _Context.Remove(item);
                   await _Context.SaveChangesAsync();
                    return new Response { Code = 200, Data = result, Message = "Delete items in order Details Successfully" };
                }
                catch (Exception e)
                {
                    return new Response { Code = 400, Message = e.ToString() };
                }
            }
            return new Response { Code = 400, Message = "item not exist in database" };
        }
    }
}
