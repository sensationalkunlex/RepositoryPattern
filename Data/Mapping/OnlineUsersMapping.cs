using ApplicationCore.Entities;
using ApplicationCore.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
   public class OnlineUsersMapping : EntityTypeConfiguration<OnlineUsers>
    {
        public OnlineUsersMapping()
        {  
          
        }
    }
}
