using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
  public  class TestMapping : EntityTypeConfiguration<Test>
    {
        public TestMapping()
        {
            this.HasKey(t => t.Id);
        }
    }
}
