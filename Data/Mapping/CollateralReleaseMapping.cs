using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    class CollateralReleaseMapping : EntityTypeConfiguration<CollateralRelease>
    {
        public CollateralReleaseMapping()
        {
            this.HasKey(t => t.Id);
            this.HasOptional(t => t.CreatedBy).WithMany().HasForeignKey(fk => fk.CreatedById).WillCascadeOnDelete(false);
            HasRequired(x => x.Collateral).WithMany().WillCascadeOnDelete(false);
            //HasOptional(x => x.BankReleaseDocument).WithMany().HasForeignKey(y=>y.BankReleaseDocumentId).WillCascadeOnDelete(false);
            //HasOptional(x => x.DeclarationOfReleaseDocument).WithMany().HasForeignKey(y => y.DeclarationOfReleaseDocumentId).WillCascadeOnDelete(false);
            //HasOptional(x => x.ObligorWrittenDocumentOfRelease).WithMany().HasForeignKey(y => y.ObligorWrittenDocumentOfReleaseId).WillCascadeOnDelete(false);
            //HasOptional(x => x.ReceiptOfCollateralDocument).WithMany().HasForeignKey(y => y.ReceiptOfCollateralDocumentId).WillCascadeOnDelete(false);


        }
    }
}
