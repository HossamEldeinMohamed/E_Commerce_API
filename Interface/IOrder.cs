using E_Commerce_API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_API.Models;

namespace E_Commerce_Api.Interface
{
    public interface IOrder
    {
        public Task<IEnumerable<Order>> GetOrders();
        public Task<Response> GetOrderById(int id);
        public Task<Response> CreateOrderAsync(OrderDTO Order);
        public Task<Response> CancelOrderAsync(int id);

    }
}
