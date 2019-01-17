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
                .ForMember(u => u.username, uvm => uvm.MapFrom(o => o.Username))
                .ForMember(u => u.password, uvm => uvm.MapFrom(o => o.Password));

            CreateMap<Order, OrderVM>()
                .ForMember(o => o.Date, e => e.MapFrom(o => o.CreationDate.ToString("dd-MM-yyyy")))
                .ForMember(o => o.Name, e => e.MapFrom(o => o.Name + " - " + o.CreationDate.ToLongDateString()));


            CreateMap<OrderVM, Order>();

            CreateMap<Item, ItemVM>()
                //.ForMember(o => o.Price, o => o.MapFrom(i => i.Price + " LE"))
                .ForMember(o => o.Type, o => o.MapFrom(i => i.Type == 0 ? "BLADY" : "SHAMY"))
                .ForMember(o => o.ElementId, o => o.MapFrom(i => i.Id));

            CreateMap<ItemVM, Item>()
                //.ForMember(o => o.Price, o => o.MapFrom(i => decimal.Parse(i.Price)))
                .ForMember(o => o.Type, o => o.MapFrom(i => i.Type == "BLADY" ? 0 : 1))
                .ForMember(o => o.Id , o => o.MapFrom(i => i.ElementId));
        }
    }
}
