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
    public class DBSeed
    {
        public static void CreatePermissions(DatabaseContext context, int createUserId)
        {

            // add permissions
            //var permissions = PermissionProvider.GetSystemPermission();
            //var permissionList = new List<ApplicationPermission>();
            //var savedPerms = context.Set<ApplicationPermission>().ToList();
            //foreach (var perm in permissions)
            //{
            //    if (!savedPerms.Any(x => x.SystemName == perm.SystemName))
            //    {
            //        var appPermission = new ApplicationPermission
            //        {
            //            Name = perm.Name,
            //            SystemName = perm.SystemName,
            //            IsFCAALPermission = perm.IsFCAALPermission,
            //            IsDefaultPermission = perm.IsDefaultPermission
            //        };

            //        permissionList.Add(appPermission);
            //    }

            //}

            //context.Set<ApplicationPermission>().AddOrUpdate(x => x.SystemName, permissionList.ToArray());
            //context.SaveChanges();


            //var systemPermissions = PermissionProvider.GetSystemDefaultRoles();

            //if (systemPermissions == null || !systemPermissions.Any())
            //    return;


            //foreach (var systemPermission in systemPermissions)
            //{
            //    //create role
            //    var role = context.Set<Role>().Where(x => x.Name == systemPermission.Key).FirstOrDefault();

            //    if (role == null)
            //    {
            //        role = new Role
            //        {
            //            Name = systemPermission.Key,
            //            CreatedById = createUserId,
            //            LastModifiedById = createUserId,
            //            //IsSystemRole = systemPermission.Key == CommonConstants.SystemRole.DRARole ? false : true
            //            IsSystemRole = true
            //        };

            //        var r = context.Set<Role>().Add(role);
            //        context.SaveChanges();
            //    }


            //    foreach (var permision1 in systemPermission.Value)
            //    {
            //        var permission = context.Set<ApplicationPermission>().Include(x => x.RolePermissions).FirstOrDefault(x => x.SystemName.Equals(permision1.SystemName));


            //        var alreadyExist = permission.RolePermissions.Any(x => x.RoleId.Equals(role.Id));

            //        if (!alreadyExist)
            //        {
            //            var rolePerms = new RolePermissions
            //            {
            //                RoleId = role.Id,
            //                CreatedById = createUserId,
            //                LastModifiedById = createUserId,
            //                ApplicationPermissionId = permission.Id
            //            };

            //            context.Set<RolePermissions>().Add(rolePerms);
            //        }



            //    };


            //    context.SaveChanges();

            //}


            ////create admin user role 

            //var adminRole = context.Set<Role>().Where(x => x.Name == CommonConstants.SystemRole.Admin).FirstOrDefault();
            //var userRole = context.Set<UserRole>().Where(x => x.UserId == createUserId && x.RoleId == adminRole.Id).FirstOrDefault();

            //if (userRole == null)
            //{
            //    context.Set<UserRole>().Add(new UserRole
            //    {
            //        RoleId = adminRole.Id,
            //        UserId = createUserId,
            //        CreatedById = createUserId,
            //        LastModifiedById = createUserId,
            //        CreatedDate = DateTime.Now,
            //        ModifiedDate = DateTime.Now
            //    });
            //}

            //context.SaveChanges();

        }

        public static void CreatePaymentReminders(DatabaseContext context, int createUserId)
        {
            // insert reminders before payment date

            //var paymentReminders = new List<PaymentReminderSettings>
            //{
            //    new PaymentReminderSettings
            //    {
            //        Frequency = 1,
            //        Hour = 8,
            //        IsBefore = true,
            //        PeriodType = PeriodType.Monthly,
            //        CreatedById = createUserId,
            //        CreatedDate = CommonHelper.GetCurrentDate(),
            //        LastModifiedById = createUserId,
            //        ModifiedDate = DateTime.Now
            //    },
            //     new PaymentReminderSettings
            //    {
            //        CreatedById = createUserId,
            //        Frequency = 1,
            //        Hour = 8,
            //        IsBefore = true,
            //        CreatedDate = CommonHelper.GetCurrentDate(),
            //        PeriodType = PeriodType.Weekly,
            //        LastModifiedById = createUserId,
            //        ModifiedDate = DateTime.Now
            //    },

            //       new PaymentReminderSettings
            //    {
            //        CreatedById = createUserId,
            //        Frequency = 1,
            //        Hour = 8,
            //        IsBefore = true,
            //        CreatedDate = CommonHelper.GetCurrentDate(),
            //        PeriodType = PeriodType.Daily,
            //        LastModifiedById = createUserId,
            //        ModifiedDate = DateTime.Now
            //    },
            //         new PaymentReminderSettings
            //    {
            //        CreatedById = createUserId,
            //        Frequency = 0,
            //        Hour = 8,
            //        IsBefore = true,
            //        CreatedDate = CommonHelper.GetCurrentDate(),
            //        PeriodType = PeriodType.Daily,
            //        LastModifiedById = createUserId,
            //        ModifiedDate = DateTime.Now
            //    },
            //           new PaymentReminderSettings
            //    {
            //        CreatedById = createUserId,
            //        Frequency = 2,
            //        Hour = 8,
            //        IsBefore = false,
            //        CreatedDate = CommonHelper.GetCurrentDate(),
            //        PeriodType = PeriodType.Daily,
            //        LastModifiedById = createUserId,
            //        ModifiedDate = DateTime.Now
            //    },
            //               new PaymentReminderSettings
            //    {
            //        CreatedById = createUserId,
            //        Frequency = 2,
            //        Hour = 8,
            //        IsBefore = false,
            //        CreatedDate = CommonHelper.GetCurrentDate(),
            //        PeriodType = PeriodType.Weekly,
            //        LastModifiedById = createUserId,
            //        ModifiedDate = DateTime.Now
            //    },
            //new PaymentReminderSettings
            //    {
            //        CreatedById = createUserId,
            //        Frequency = 2,
            //        Hour = 8,
            //        IsBefore = false,
            //        CreatedDate = CommonHelper.GetCurrentDate(),
            //        PeriodType = PeriodType.Monthly,
            //        IsRecurrent = true,
            //        LastModifiedById = createUserId,
            //        ModifiedDate = DateTime.Now
            //    }
            //};

            //context.Set<PaymentReminderSettings>().AddOrUpdate(p => new { p.Frequency, p.Hour, p.IsBefore, p.PeriodType, p.IsRecurrent }, paymentReminders.ToArray());
            //context.SaveChanges();
            
        }


        public static void CreateRegionSeed(DatabaseContext context, int createUserId)
        {
            // Region State Seed
            //var datenow = CommonHelper.GetCurrentDate();
            //var Regions = new List<Region>
            //{
            //    new Region
            //    {
            //        Name = "North Central",
            //        CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow
            //       , RegionState = new List<RegionState>
            //        {
            //            new RegionState{State = State.Niger,CreatedById = createUserId, CreatedDate = datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow},

            //            new RegionState{State = State.Kogi,CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow},

            //            new RegionState{State = State.Benue , CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow},

            //            new RegionState{State = State.Plateau, CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow},

            //            new RegionState{State = State.Nasarawa, CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow},

            //            new RegionState{State = State.Kwara, CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow}
            //        }
            //    },
            //    new Region
            //    {
            //        Name = "North West",
            //        CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow
            //        ,RegionState = new List<RegionState>
            //        {
            //            new RegionState{State = State.Zamfara , CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow},

            //            new RegionState{State = State.Sokoto , CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow},

            //            new RegionState{State = State.Kaduna, CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow},

            //            new RegionState{State = State.Kebbi , CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow},

            //            new RegionState{State = State.Katsina, CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow},

            //            new RegionState{State = State.Kano , CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow}
            //        }
            //    },
            //    new Region
            //    {
            //        Name = "North East",
            //        CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow
            //        ,RegionState = new List<RegionState>
            //        {
            //            new RegionState{State = State.Bauchi, CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow},

            //            new RegionState{State = State.Borno, CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow},

            //            new RegionState{State = State.Taraba, CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow},

            //            new RegionState{State = State.Adamawa, CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow},

            //            new RegionState{State = State.Gombe, CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow},

            //            new RegionState{State = State.Yobe, CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow}
            //        }
            //    },
            //     new Region
            //    {
            //        Name = "South East",
            //        CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow
            //        ,RegionState = new List<RegionState>
            //        {
            //            new RegionState{State = State.Enugu, CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow},

            //            new RegionState{State = State.Imo, CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow},

            //            new RegionState{State = State.Ebonyi, CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow},

            //            new RegionState{State = State.Abia, CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow},

            //            new RegionState{State = State.Anambra , CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow}
            //        }
            //    },
            //      new Region
            //    {
            //        Name = "South South",
            //        CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow
            //        ,RegionState = new List<RegionState>
            //        {
            //            new RegionState{State = State.Bayelsa , CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow},

            //            new RegionState{State = State.AkwaIbom, CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow},

            //            new RegionState{State = State.Edo, CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow},

            //            new RegionState{State = State.Rivers, CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow},

            //            new RegionState{State = State.CrossRiver, CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow},

            //            new RegionState{State = State.Delta, CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow}
            //        }
            //    },
            //         new Region
            //    {
            //        Name = "South West",
            //        CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow
            //        ,RegionState = new List<RegionState>
            //        {
            //            new RegionState{State = State.Oyo, CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow},

            //            new RegionState{State = State.Ekiti, CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow},

            //            new RegionState{State = State.Osun, CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow},

            //            new RegionState{State = State.Ondo, CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow},

            //            new RegionState{State = State.Lagos, CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow},

            //            new RegionState{State = State.Ogun, CreatedById = createUserId, CreatedDate =datenow, LastModifiedById = createUserId,
            //                        ModifiedDate = datenow}
            //        }
            //    }
            //};

            //context.Set<Region>().AddOrUpdate(p => p.Name, Regions.ToArray());
            //context.SaveChanges();

        }

        public static void CreateApplicationSettings(DatabaseContext context, int createUserId)
        {
            /// create Application Setting 
            /// 
            //var curSetting = context.Set<ApplicationSetting>().FirstOrDefault();
            //if (curSetting == null)
            //{
            //    var setting = new ApplicationSetting
            //    {
            //        CompanyAddress = "UBA House (8th Floor) 57 Marina, P O Box 70776, Victoria Island, Lagos, Nigeria.",
            //        CompanyTelNo = "270 2298-9",
            //        CompanyFaxNo = "2711896",
            //        CreatedById = createUserId,
            //        LastModifiedById = createUserId,
            //        CreatedDate = DateTime.Now,
            //        ModifiedDate = DateTime.Now,
            //        IsActive = true
            //    };

            //    context.Set<ApplicationSetting>().Add(setting);
            //    context.SaveChanges();
            //}
        }

        public static void CreateNotificationPermissions(DatabaseContext context)
        {
            

        }

    }
}
