using System.Collections.Generic;

namespace Order.Domain.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Entities.Order> GetAll();
        Entities.Order GetById(int id);
        void Add(Entities.Order order);
        void Update(Entities.Order order);
        void Delete(int id);
    }
}
