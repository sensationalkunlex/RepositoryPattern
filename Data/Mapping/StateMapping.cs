using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
   public class StateMapping : EntityTypeConfiguration<State>
    {
        public StateMapping()
        {
            this.HasKey(t => t.Id);

            this.HasOptional(t => t.CreatedBy).WithMany().HasForeignKey(fk => fk.CreatedById).WillCascadeOnDelete(false);


           // this.HasMany(p => p.Regions).WithRequired().HasForeignKey(p => p.StateId);
        }
    }
}
