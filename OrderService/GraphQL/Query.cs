using OrderService.Models;
using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.AspNetCore.Authorization;

using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace OrderService.GraphQL
{
    public class Query
    {

        [Authorize]
        public IQueryable<Order> GetOrders([Service] SimpleOrderKafkaContext context, ClaimsPrincipal claimsPrincipal)
        {
            var userName = claimsPrincipal.Identity.Name;

            // check admin role ?
            var managerRole = claimsPrincipal.Claims.Where(o => o.Type == ClaimTypes.Role && o.Value == "MANAGER").FirstOrDefault();
            var user = context.Users.Where(o => o.Username == userName).FirstOrDefault();
            if (user != null)
            {
                if (managerRole != null)
                {
                    return context.Orders;
                }
                var orders = context.Orders.Where(o => o.UserId == user.Id);
                return orders.AsQueryable();
            }


            return new List<Order>().AsQueryable();
        }
    }
}
