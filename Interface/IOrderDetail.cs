using E_Commerce_API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_API.Models;

namespace E_Commerce_Api.Interface
{
    public interface IOrderDetail
    {
        public Task<IEnumerable<OrderDetail>> GetOrderDetails();
        public Task<Response> GetOrderDetailById(int id);
        public Task<Response> CreateOrderDetailAsync(IList<OrderDetailDTO> OrderDetail , int orderId);
        //public Task<Response> UpdateOrderDetailAsync(int id, OrderDetailDTO OrderDetail);
      //  public Task<Response> DeleteOrderDetailAsync(int id);
        public Task<Response> DeleteItemFromOrderDetailAsync(int id, int ProductId);


    }
}
