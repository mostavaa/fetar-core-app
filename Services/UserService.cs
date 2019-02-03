using AutoMapper;
using Data;
using Data.Repositories;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IUserService
    {
        void Register(UserVM userVM);
        void Login(UserVM user);
        User FindByGuid(Guid id);
    }
    public class UserService : IUserService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public IMapper Mapper { get; }
        public IResponseService ResponseService { get; }

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IResponseService responseService)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            ResponseService = responseService;
        }
        public User FindByGuid(Guid id)
        {
           return UnitOfWork.UserRepository.GetByGuid(id);
        }
        public void Login(UserVM user)
        {
            ResponseService.Init();
            User item = UnitOfWork.UserRepository.GetByNameAndPassword(user.username, user.password);
            string token = "";
            string refreshToken = "";
            if (item != null)
            {
                
                token = TokenService.GenerateTokens(user.username, new List<string>());
                ResponseService.Status = true;
                ResponseService.Errors.Add("Successfully Login");
                refreshToken = item.GUID.ToString();
                ResponseService.Data = new { token , refreshToken };
            }
            else
            {
                ResponseService.Status = false;
                ResponseService.Errors.Add("Wrong User Name or Password !");
            }
        }
        public void Register(UserVM userVM)
        {
            ResponseService.Init();
            bool UserExists = UnitOfWork.UserRepository.IsUserExist(userVM.username);
            if (UserExists)
            {
                ResponseService.Status = false;
                ResponseService.Errors.Add("This User Name Is Used Before , Please Choose Another User Name");
                return;
            }

            User user = new User();
            user = Mapper.Map<User>(userVM);
            UnitOfWork.UserRepository.Register(user);
            Task<int> task = UnitOfWork.SaveAsync();
            task.Wait();
            if (task.Result > 0)
            {
                ResponseService.Status = true;
                ResponseService.Success.Add("User Added Successfully");
            }
        }
    }
}
