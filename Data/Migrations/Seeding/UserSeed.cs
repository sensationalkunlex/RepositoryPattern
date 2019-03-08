using ApplicationCore.Entities;

using Data.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Migrations.Seeding
{
    public class UserSeed
    {
        //public static int CreateAdminUser(DatabaseContext context)
        //{
        //    //var date = DateTime.Now;

        //    //var user = new User
        //    //{
        //    //    Password = AuthHelper.HashPassword("admin_password"),
        //    //    Email = CommonConstants.AdminUserEmail,
        //    //    UserName = CommonConstants.AdminUserEmail,
        //    //    CreatedDate = DateTime.Now,
        //    //    FirstName = "Super",
        //    //    LastName = "Admin",
        //    //    IsSystemAccount = true
        //    //};

        //    //context.Set<User>().AddOrUpdate(x => x.Email, user);
        //    //context.SaveChanges();

        //    //return user.Id;

        //}

        public static void CreateAdminUserRoles(DatabaseContext context, int userId)
        {
            //create admin user role 

        }
    }
}
