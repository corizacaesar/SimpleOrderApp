using HotChocolate.AspNetCore.Authorization;
using ProductService.Models;

namespace ProductService.GraphQL
{
    public class Query
    {
        [Authorize] // dapat diakses kalau sudah login
        public IQueryable<ProductData> GetProduct([Service] SimpleOrderKafkaContext context) =>
                  context.Products.Select(p => new ProductData()
                  {
                      Id = p.Id,
                      Name = p.Name,
                      Stock = p.Stock,
                      Price = p.Price
                  });
    }
}
