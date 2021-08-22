using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_API.DTO;
using E_Commerce_Api.Interface;
using Microsoft.EntityFrameworkCore;
using E_Commerce_API.Models;
using AutoMapper;

namespace E_Commerce_Api.Servicies
{
    public class OrderDetailService : IOrderDetail
    {
        IList<OrderDetail> orderList = new List<OrderDetail>();
        private readonly E_CommerceContext _Context;
        private readonly IMapper _mapper;
        public OrderDetailService(E_CommerceContext context, IMapper mapper)
        { 
            _Context = context; 
            _mapper = mapper ;
        } 
        public async Task<Response> CreateOrderDetailAsync(IList<OrderDetailDTO> OrderDetailDTO , int orderId)
        {
            foreach (var item in OrderDetailDTO)
            {
                var Product =await _Context.Products.Where(x => x.ProductId == item.ProductId).FirstOrDefaultAsync();
                var Order = _Context.Orders.Where(x => x.OrderId == orderId).FirstOrDefault();
                if (Product != null)
                {
                    if (Product.Quanity >= item.Quantity)
                    {
                        var result = _mapper.Map<OrderDetail>(OrderDetailDTO);

                        orderList.Add(result);
                        Order.Total += result.TotalPrice;
                        Product.Quanity -= item.Quantity;
                        try
                        {
                            _Context.Update(Product);
                            _Context.Update(Order);
                            _Context.Add(orderList);
                           await _Context.SaveChangesAsync();
                        }
                        catch (Exception e)
                        {
                            return new Response { Code = 400,Data=e.Data, Message = e.Message };
                        }
                    }
                    else
                        return new Response { Code = 404, Message = "please the required quantity is not available " };
                }
                else
                    return new Response { Code = 404, Message = "The Product is null" };
            }
            return new Response { Code = 200, Data = orderList, Message = "Create Successfully" };


        }

        public async Task<Response> DeleteItemFromOrderDetailAsync(int OrderId, int ProductId)
        {
            var result =await _Context.OrderDetails.Where(x => x.ProductId == ProductId && x.OrderId == OrderId).FirstOrDefaultAsync();

            if (result != null)
            {
                var product = _Context.Products.Where(x => x.ProductId == ProductId).FirstOrDefault();
                var order = _Context.Orders.Where(x => x.OrderId == OrderId).FirstOrDefault();
                product.Quanity += result.Quantity;
                order.Total -= result.TotalPrice;
                try
                {
                    _Context.Update(product);
                    _Context.Update(order);
                    _Context. Remove(result);
                    _Context.SaveChanges();
                    return new Response { Code = 200, Data = result, Message = "Delete Successfully" };
                }
                catch (Exception e)
                {
                    return new Response { Code = 400, Message = e.Message };
                }
            }
            return new Response { Code = 400, Message = "not exist in database" };
        }

        public async Task<Response> GetOrderDetailById(int id)
        {
            var result =await _Context.OrderDetails.Include(x => x.Order).Include(x => x.Product).Where(x => x.OrderId == id).FirstOrDefaultAsync();
            if (result != null)
                return new Response { Code = 200, Data = result, Message = "Get Successfully" };
            return new Response { Code = 400, Message = "Not Exist in Database" };
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetails()
        {
            var Orders =await _Context.OrderDetails.Include(x => x.Product).ToListAsync();
            if (Orders.Count != 0)
                return Orders;
            return null;
        }
    }
}
