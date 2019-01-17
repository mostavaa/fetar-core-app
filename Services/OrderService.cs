using AutoMapper;
using Data;
using Data.Repositories;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IOrderService
    {
        List<OrderVM> GetAll(int page);
        bool CanAddOrder();
        void CreateOrder(OrderVM model);
        int Count();
    }
    public class OrderService : IOrderService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public IMapper Mapper { get; }
        public IResponseService ResponseService { get; }

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IResponseService responseService)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            ResponseService = responseService;
        }
        public int Count()
        {
            return UnitOfWork.OrderRepository.NumOfOrders();
        }
        public List<OrderVM> GetAll(int page)
        {
            List<Order> res = UnitOfWork.OrderRepository.GetAll(page, Constants.PageSize);
            List<OrderVM> Orders = new List<OrderVM>();
            foreach (var item in res)
            {
                Orders.Add(Mapper.Map<OrderVM>(item));
            }
            return Orders;
        }
        public bool CanAddOrder()
        {
            return !UnitOfWork.OrderRepository.OrderCreatedToday();
        }
        public void CreateOrder(OrderVM model)
        {
            ResponseService.Init();
            if (CanAddOrder())
            {
                if (string.IsNullOrEmpty(model.Name))
                    model.Name = "Order";

                var order = Mapper.Map<Order>(model);
                UnitOfWork.OrderRepository.CreateOrderToday(order);
                if (UnitOfWork.Save() > 0)
                {
                    ResponseService.Status = true;
                    ResponseService.Success.Add("Order Created Successfully");
                }
                else
                {
                    ResponseService.Status = false;
                    ResponseService.Errors.Add("Server Error");
                }
            }
            else
            {
                ResponseService.Status = false;
                ResponseService.Errors.Add("Order Created Today");
            }
        }
    }
}
