using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Shop.DataLayer.Entities.User;

namespace Shop.DataLayer.Context
{
    public class ShopContext : DbContext
    {

        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {

        }

        #region User

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        #endregion

    }
}
