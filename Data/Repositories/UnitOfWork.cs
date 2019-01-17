using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IUnitOfWork
    {
        UserRepository UserRepository { get; }
        OrderRepository OrderRepository { get; }
        ItemRepository ItemRepository { get; }
        int Save();
        Task<int> SaveAsync();
    }
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(DataContext context)
        {
            Context = context;
        }
        public DataContext Context { get; set; }

        private UserRepository UserRepo { get; set; }
        public UserRepository UserRepository { get => UserRepo ?? new UserRepository(Context); }

        private OrderRepository OrderRepo { get; set; }
        public OrderRepository OrderRepository { get => OrderRepo ?? new OrderRepository(Context); }

        private ItemRepository ItemRepo { get; set; }
        public ItemRepository ItemRepository { get => ItemRepo ?? new ItemRepository(Context); }
        public Task<int> SaveAsync()
        {
            return Context.SaveChangesAsync();
        }

        public int Save()
        {
            return Context.SaveChanges();
        }
    }
}
