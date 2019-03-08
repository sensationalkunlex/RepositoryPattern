
using Data.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Migrations.Seeding
{
    public static class PermissionSeed
    {

      

        public static void CreatePermissions(DatabaseContext context)
        {

            //var systemPermissions = PermissionProvider.GetSystemDefaultRoles();

            //if (systemPermissions == null || !systemPermissions.Any())
            //    return;


            //foreach (var systemPermission in systemPermissions)
            //{

            //    foreach (var permision1 in systemPermission.Value)
            //    {
            //        var permission = context.Set<ApplicationPermission>().Include(x => x.RolePermissions).FirstOrDefault(x => x.SystemName.Equals(permision1.SystemName));

            //        if (permission == null)
            //        {
            //            permission = new ApplicationPermission
            //            {
            //                Name = permision1.Name,
            //                SystemName = permision1.SystemName,
            //            };
            //        }
            //        context.Set<ApplicationPermission>().AddOrUpdate(x => x.SystemName, permission);
            //    };
            //    context.SaveChanges();
            //}
        }

    }
}
