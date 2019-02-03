using AutoMapper;
using Data;
using Data.Repositories;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public interface IItemService
    {
        List<ItemVM> GetAll();
        void CreateItem(ItemVM model);
        ItemVM GetByGuid(Guid id);
        void EditItem(ItemVM model);
        void Delete(Guid id);
        void DeleteItemDetails(Guid id);
    }
    public class ItemService : IItemService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public IMapper Mapper { get; }
        public IResponseService ResponseService { get; }

        public ItemService(IUnitOfWork unitOfWork, IMapper mapper, IResponseService responseService)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            ResponseService = responseService;
        }
        public ItemVM GetByGuid(Guid id)
        {
            Item item =  UnitOfWork.ItemRepository.GetByGuid(id);
            if (item != null)
            {
                return Mapper.Map<ItemVM>(item);
            }
            return null;
        }
        public List<ItemVM> GetAll()
        {
            List<Item> items = UnitOfWork.ItemRepository.GetAll();
            List<ItemVM> ItemsVM = new List<ItemVM>();
            foreach (var item in items)
            {
                ItemsVM.Add(Mapper.Map<ItemVM>(item));
            }
            return ItemsVM.OrderBy(o => o.Name).ToList() ;
        }
        public void DeleteItemDetails(Guid id)
        {
            ResponseService.Init();
            ItemDetails item =  UnitOfWork.ItemDetailsRepository.GetById(id);
            if (item != null)
            {
                if (item.OrderDetails.Order.Ordered)
                {
                    ResponseService.Status = false;
                    ResponseService.Errors.Add("Order Has Been Ordered!");
                    return;
                }
                UnitOfWork.ItemDetailsRepository.Remove(item.Id);
                if (UnitOfWork.Save() > 0)
                {
                    ResponseService.Status = true;
                    ResponseService.Errors.Add("Item Deleted Successfully");
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
        public void Delete(Guid id)
        {
            ResponseService.Init();

            ItemVM itemVM = GetByGuid(id);
            if (itemVM!=null)
            {
                UnitOfWork.ItemRepository.Remove(itemVM.ElementId);
                if (UnitOfWork.Save() > 0)
                {
                    ResponseService.Status = true;
                    ResponseService.Errors.Add("Item Deleted Successfully");
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
        private Item Map(ItemVM model)
        {
            ResponseService.Init();
            var item = Mapper.Map<Item>(model);
            bool Created = UnitOfWork.ItemRepository.CreatedBefore( model.ElementId,model.Name, model.SubName, item.Type);
            if (Created)
            {
                ResponseService.Status = false;
                ResponseService.Errors.Add("Item Is Created Before");
                return null;
            }
            return item;
        }
        public void EditItem(ItemVM model)
        {
            //Item item =  UnitOfWork.ItemRepository.GetById(model.ElementId);
            Item item = Map(model);
            if (item != null)
            {
                UnitOfWork.ItemRepository.Edit(item);
                if (UnitOfWork.Save() > 0)
                {
                    ResponseService.Status = true;
                    ResponseService.Errors.Add("Item Edited Successfully");
                }
                else
                {
                    ResponseService.Status = false;
                    ResponseService.Errors.Add("Server Error");
                }
            }
        }
        public void CreateItem(ItemVM model)
        {
            Item item = Map(model);
            if (item != null)
            {
     
                UnitOfWork.ItemRepository.Create(item);
                if (UnitOfWork.Save() > 0)
                {
                    ResponseService.Status = true;
                    ResponseService.Errors.Add("Item Created Successfully");
                }
                else
                {
                    ResponseService.Status = false;
                    ResponseService.Errors.Add("Server Error");
                }
            }
        }
    }
}
