using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface IProductRepository
    {
        Task<Product> Add(Product item);
        IEnumerable<Product> GetAll();
        Task<Product> Find(int id);
        Task<Product> Remove(int id);
        Task<Product> Update(Product item);
        Task<bool> Exists(int id);
        Task Save();
    }
}