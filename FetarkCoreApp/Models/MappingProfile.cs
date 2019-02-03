using AutoMapper;
using Data;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetarkCoreApp.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserVM, User>()
                .ForMember(u => u.Username, uvm => uvm.MapFrom(o => o.username))
                .ForMember(u => u.Password, uvm => uvm.MapFrom(o => o.password));

            CreateMap<User, UserVM>()
                .ForMember(u => u.UserId, uvm => uvm.MapFrom(o => o.Id))
                .ForMember(u => u.UserGuid, uvm => uvm.MapFrom(o => o.GUID))
                .ForMember(u => u.username, uvm => uvm.MapFrom(o => o.Username))
                .ForMember(u => u.password, uvm => uvm.MapFrom(o => o.Password))
                .ForMember(u => u.OrderDetailsVM, uvm => uvm.MapFrom(o => o.OrderDetails));


            CreateMap<Order, OrderVM>()
                .ForMember(o => o.Date, e => e.MapFrom(o => o.CreationDate.ToString("dd-MM-yyyy")))
                .ForMember(o => o.Name, e => e.MapFrom(o => o.Name + " - " + o.CreationDate.ToLongDateString()))
                .ForMember(o => o.OrderId, e => e.MapFrom(o => o.Id))
                .ForMember(o => o.OrderDetailsVM, e => e.MapFrom(o => o.OrderDetails));


            CreateMap<OrderVM, Order>();

            CreateMap<Item, ItemVM>()
                //.ForMember(o => o.Price, o => o.MapFrom(i => i.Price + " LE"))
                .ForMember(o => o.Type, o => o.MapFrom(i => i.Type == 0 ? "BLADY" : "SHAMY"))
                .ForMember(o => o.ElementId, o => o.MapFrom(i => i.Id))
                .ForMember(o => o.ItemDetailsVM, o => o.MapFrom(i => i.ItemDetails));
            
            CreateMap<ItemVM, Item>()
                //.ForMember(o => o.Price, o => o.MapFrom(i => decimal.Parse(i.Price)))
                .ForMember(o => o.Type, o => o.MapFrom(i => i.Type == "BLADY" ? 0 : 1))
                .ForMember(o => o.Id, o => o.MapFrom(i => i.ElementId));

            CreateMap<ItemDetailsVM, ItemDetails>();
            CreateMap<ItemDetails, ItemDetailsVM>()
                .ForMember(o => o.ItemDetailsId, o => o.MapFrom(e => e.Id))
                .ForMember(o => o.ItemDetailsGuid, o => o.MapFrom(e => e.GUID))
                .ForMember(o => o.ItemVM, o => o.MapFrom(e => e.Item))
                .ForMember(o => o.OrderDetailsVM, o => o.MapFrom(e => e.OrderDetails));

            CreateMap<OrderDetailsVM, OrderDetails>();
            CreateMap<OrderDetails, OrderDetailsVM>()
                .ForMember(o => o.OrderDetailsId, o => o.MapFrom(e => e.Id))
                .ForMember(o => o.OrderDetailsGuid, o => o.MapFrom(e => e.GUID))
                .ForMember(o => o.OrderVM, o => o.MapFrom(e => e.Order))
                .ForMember(o => o.UserVM, o => o.MapFrom(e => e.User))
                .ForMember(o => o.ItemDetailsVM, o => o.MapFrom(e => e.ItemDetails));
        }
    }
}
