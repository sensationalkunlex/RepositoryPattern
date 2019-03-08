
using Data.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Migrations.Seeding
{
   public class StateSeed
    {
        public static void CreateState(DatabaseContext context)
        {
            var date = DateTime.Now;
            //var states = new List<State>
            //{
            //    new State {
            //        Id = 1,
            //        Name = "Abia",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 2,
            //        Name = "Adamawa",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 3,
            //        Name = "Akwa Ibom",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 4,
            //        Name = "Anambra ",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 5,
            //        Name = "Bauchi",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 6,
            //        Name = "Bayelsa",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 7,
            //        Name = "Benue",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 8,
            //        Name = "Borno",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 9,
            //        Name = "Cross River",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 10,
            //        Name = "Delta",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 11,
            //        Name = "Ebonyi",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 12,
            //        Name = "Edo",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 13,
            //        Name = "Ekiti",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 14,
            //        Name = "Enugu",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 15,
            //        Name = "Federal Capital Territory",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 16,
            //        Name = "Gombe",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 17,
            //        Name = "Imo",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 18,
            //        Name = "Jigawa",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 19,
            //        Name = "Kaduna",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 20,
            //        Name = "Kano",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 21,
            //        Name = "Katsina",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 22,
            //        Name = "Kebbi",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 23,
            //        Name = "Kogi",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 24,
            //        Name = "Kwara",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 25,
            //        Name = "Lagos",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 26,
            //        Name = "Nasarawa",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 27,
            //        Name = "Niger",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 28,
            //        Name = "Ogun",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 29,
            //        Name = "Ondo",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 30,
            //        Name = "Osun",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 31,
            //        Name = "Oyo",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 32,
            //        Name = "Plateau",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 33,
            //        Name = "Rivers",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 34,
            //        Name = "Sokoto",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 35,
            //        Name = "Taraba",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 36,
            //        Name = "Yobe",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    },
            //    new State
            //    {
            //        Id = 37,
            //        Name = "Zamfara",
            //        CreatedById = 1,
            //        LastModifiedById = 1,
            //        CreatedDate = date,
            //        IsDeleted = false
            //    }
            //};

            //context.Set<State>().AddOrUpdate(p => p.Id, states.ToArray());
        //   context.SaveChanges();
        }
    }
}
