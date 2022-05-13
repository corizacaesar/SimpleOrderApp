using HotChocolate.AspNetCore.Authorization;
using UserService.Models;
using HotChocolate;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.GraphQL
{
    public class Query
    {
        [Authorize(Roles = new[] { "ADMIN" })] // dapat diakses kalau sudah login
        public IQueryable<UserData> GetUsers([Service] SimpleOrderKafkaContext context) =>
               context.Users.Select(p => new UserData()
               {
                   Id = p.Id,
                   FullName = p.FullName,
                   Email = p.Email,
                   Username = p.Username
               });
    }
}
