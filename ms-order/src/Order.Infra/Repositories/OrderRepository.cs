using Dapper;
using Microsoft.Extensions.Configuration;
using Order.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Order.Infra.Repositories
{
    public class OrderRepository : IOrderRepository
    {

        private IConfiguration _configuration;
        private string ConnectionString;

        public OrderRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetSection("ConnectionStrings").GetSection("ConnectionString").Value;
        }
       

        public IEnumerable<Domain.Entities.Order> GetAll()
        {
            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<Domain.Entities.Order, Domain.Entities.OrderItem, Domain.Entities.Order>(
                    @"SELECT * FROM Order O
                      INNER JOIN OrderItem OI
                      ON O.Id = OI.OrderId",
                    map: (order, orderItem) => {
                        orderItem.OrderId = order.Id; //non-reference back link

                        //check if this order has been seen already
                        if (orderMap.TryGetValue(order.OrderId, out Order existingOrder))
                        {
                            order = existingOrder;
                        }
                        else
                        {
                            order.Lines = new List<OrderLine>();
                            orderMap.Add(order.OrderId, order);

                        }

                        order.Lines.Add(orderLine);
                        return order;
                    },
                    splitOn: "OrderId");
            }
        }

        public Domain.Entities.Order GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.QueryFirst<Domain.Entities.Order>(@"INSERT INTO Produtos(Nome, Estoque, Preco) VALUES(@Nome, @Estoque, @Preco);");
            }
        }

        public void Add(Domain.Entities.Order order)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Execute(@"INSERT INTO Produtos(Nome, Estoque, Preco) VALUES(@Nome, @Estoque, @Preco);", order);
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Execute(@"DELETE FROM Produtos WHERE ProdutoId =" + id);
            }
        }

        public void Update(Domain.Entities.Order order)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Execute(@"UPDATE Produtos SET Name = @Nome, Estoque = @Estoque, Preco = @Preco WHERE ProdutoId = @Id", order);
            }
        }    
       
    }
}
