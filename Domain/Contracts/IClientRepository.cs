using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface IClientRepository
    {
        Task<Client> Add(Client client);
        IEnumerable<Client> GetAll();
        Task<Client> Find(int id);
        Task<Client> Update(Client client);
        Task<Client> Remove(int id);
        Task<bool> Exist(int id);
        Task Save();

    }
}