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
        OrderVM FullGetByGuid(Guid id);
        void ChangePayment(int id, bool isChecked);
        void SaveNotes(int id, string Notes);
        void RequestOrderItem(Guid userId, int amount, Guid itemId, string notes);
        void GetMyOrders(Guid userId);
        void OrderTheOrder(int id, bool isChecked);
        void GroupOrder(OrderVM order);
    }
    public class OrderService : IOrderService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public IMapper Mapper { get; }
        public IResponseService ResponseService { get; }
        public IUserService UserService { get; }
        public IItemService ItemService { get; }

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IResponseService responseService, IUserService userService, IItemService itemService)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            ResponseService = responseService;
            UserService = userService;
            ItemService = itemService;
        }
        public void GroupOrder(OrderVM order)
        {
            Dictionary<ItemVM, int> groupItems = new Dictionary<ItemVM, int>();
            foreach (var orderDetails in order.OrderDetailsVM)
            {
                foreach (var itemDetails in orderDetails.ItemDetailsVM)
                {
                    if (groupItems.ContainsKey(itemDetails.ItemVM))
                    {
                        groupItems[itemDetails.ItemVM]+= itemDetails.Amount;
                    }
                    else
                    {
                        groupItems.Add(itemDetails.ItemVM, itemDetails.Amount);
                    }
                }
            }
            ResponseService.Init();
            ResponseService.Data = groupItems;
        }
        public int Count()
        {
            return UnitOfWork.OrderRepository.NumOfOrders();
        }
        public void GetMyOrders(Guid userId)
        {
            User user = null;
            //1-find user
            user = UserService.FindByGuid(userId);

            //2-find order
            CreateOrderIfNotExist();

            ResponseService.Init();

            if (user == null)
            {
                ResponseService.Status = false;
                ResponseService.Errors.Add("User Not Found");
                return;
            }

            Order order = FindCurrentOrder();

            //4-find order details
            OrderDetails orderDetails = null;
            orderDetails = CreateOrderDetailsIfNotExist(order, user);

            var result = new List<object>();
            foreach (var itemDetails in orderDetails.ItemDetails)
            {
                result.Add(new { guid = itemDetails.GUID, amount = itemDetails.Amount, item = itemDetails.Item.GUID });
            }
            ResponseService.Status = true;
            ResponseService.Data = result;
            ResponseService.Success.Add("Fetched Successfully");
        }
        public void RequestOrderItem(Guid userId, int amount, Guid itemId, string notes)
        {
            User user = null;
            ItemVM item = null;
            //1-find user
            user = UserService.FindByGuid(userId);


            //2-find order
            CreateOrderIfNotExist();



            ResponseService.Init();
            if (user == null)
            {
                ResponseService.Status = false;
                ResponseService.Errors.Add("User Not Found");
                return;
            }


            Order order = FindCurrentOrder();
            if (order.Ordered)
            {
                ResponseService.Status = false;
                ResponseService.Errors.Add("Order Has Been Ordered!");
                return;
            }
            //3-find order details
            OrderDetails orderDetails = null;
            orderDetails = CreateOrderDetailsIfNotExist(order, user);

            //4-find item

            item = ItemService.GetByGuid(itemId);

            if (item == null)
            {
                ResponseService.Status = false;
                ResponseService.Errors.Add("User Not Found");
                return;
            }

            //5-create item details
            ItemDetails itemDetails = new ItemDetails()
            {
                OrderDetailsId = orderDetails.Id,
                ItemId = item.ElementId,
                Amount = amount,
                Notes = notes
            };

            UnitOfWork.ItemDetailsRepository.Create(itemDetails);

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

        private OrderDetails CreateOrderDetailsIfNotExist(Order order, User user)
        {
            OrderDetails orderDetails = UnitOfWork.OrderDetailsRepository.FindOrderDetailsForUser(user.Id, order.Id);
            if (orderDetails == null)
            {
                orderDetails = new OrderDetails()
                {
                    OrderId = order.Id,
                    UserId = user.Id,
                    Payed = false,
                };
                UnitOfWork.OrderDetailsRepository.Create(orderDetails);
                UnitOfWork.Save();
            }
            return orderDetails;
        }
        private void CreateOrderIfNotExist()
        {
            if (CanAddOrder())
            {
                CreateOrder(new OrderVM() { });
            }
            return;
        }
        private Order FindCurrentOrder()
        {
            return UnitOfWork.OrderRepository.FindTodayOrder();
        }

        public void SaveNotes(int id, string Notes)
        {
            ResponseService.Init();
            OrderDetails item = UnitOfWork.OrderDetailsRepository.GetById(id);
            if (item != null)
            {
                item.Notes = Notes;
                UnitOfWork.OrderDetailsRepository.Edit(item);
                if (UnitOfWork.Save() > 0)
                {
                    ResponseService.Status = true;
                    ResponseService.Success.Add("Notes Saved Successfully");
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
                ResponseService.Errors.Add("Item Not Found");

            }
        }
        public void OrderTheOrder(int id, bool isChecked)
        {
            ResponseService.Init();
            Order item = UnitOfWork.OrderRepository.GetById(id);
            if (item != null)
            {
                item.Ordered = isChecked;
                UnitOfWork.OrderRepository.Edit(item);
                if (UnitOfWork.Save() > 0)
                {
                    ResponseService.Status = true;
                    ResponseService.Success.Add("Order Changed Successfully");
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
                ResponseService.Errors.Add("Item Not Found");

            }
        }
        public void ChangePayment(int id, bool isChecked)
        {
            ResponseService.Init();
            OrderDetails item = UnitOfWork.OrderDetailsRepository.GetById(id);
            if (item != null)
            {
                item.Payed = isChecked;
                UnitOfWork.OrderDetailsRepository.Edit(item);
                if (UnitOfWork.Save() > 0)
                {
                    ResponseService.Status = true;
                    ResponseService.Success.Add("Payment Changed Successfully");
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
                ResponseService.Errors.Add("Item Not Found");

            }
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
        public OrderVM FullGetByGuid(Guid id)
        {
            Order item = UnitOfWork.OrderRepository.FullGetByGuid(id);
            if (item != null)
            {
                return Mapper.Map<OrderVM>(item);
            }
            return null;
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
