namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Obligor", "ObligorBankStatement_Id", "dbo.ObligorBankStatement");
            DropIndex("dbo.Obligor", new[] { "ObligorBankStatement_Id" });
            RenameColumn(table: "dbo.ObligorRelease", name: "CollateralReleasedId", newName: "BankAcknowledgementFcaalId");
            RenameColumn(table: "dbo.ObligorRelease", name: "ConfirmationLetterId", newName: "ObligorAcknowledgementBankId");
            RenameColumn(table: "dbo.ObligorRelease", name: "InSentToBankId", newName: "ObligorAcknowledgementFcaalId");
            RenameIndex(table: "dbo.ObligorRelease", name: "IX_ConfirmationLetterId", newName: "IX_ObligorAcknowledgementBankId");
            RenameIndex(table: "dbo.ObligorRelease", name: "IX_CollateralReleasedId", newName: "IX_BankAcknowledgementFcaalId");
            RenameIndex(table: "dbo.ObligorRelease", name: "IX_InSentToBankId", newName: "IX_ObligorAcknowledgementFcaalId");
            CreateTable(
                "dbo.ObligorReleaseDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        Organization = c.String(),
                        OrganizeRecipient = c.String(),
                        RemoveCRMS = c.Boolean(nullable: false),
                        Subject = c.String(),
                        SentToBank = c.Boolean(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            AddColumn("dbo.ObligorRelease", "InSentToBank", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Obligor", "AmountLoaned", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Obligor", "InformingObligorDateAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ObligorRelease", "RosdCompleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ObligorRelease", "CRMS", c => c.Boolean(nullable: false));
            DropColumn("dbo.Obligor", "ObligorBankStatement_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Obligor", "ObligorBankStatement_Id", c => c.Int());
            DropForeignKey("dbo.ObligorReleaseDetails", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.ObligorReleaseDetails", "CreatedById", "dbo.User");
            DropIndex("dbo.ObligorReleaseDetails", new[] { "LastModifiedById" });
            DropIndex("dbo.ObligorReleaseDetails", new[] { "CreatedById" });
            AlterColumn("dbo.ObligorRelease", "CRMS", c => c.Boolean());
            AlterColumn("dbo.ObligorRelease", "RosdCompleted", c => c.Boolean());
            AlterColumn("dbo.Obligor", "InformingObligorDateAmount", c => c.String());
            AlterColumn("dbo.Obligor", "AmountLoaned", c => c.String());
            DropColumn("dbo.ObligorRelease", "InSentToBank");
            DropTable("dbo.ObligorReleaseDetails");
            RenameIndex(table: "dbo.ObligorRelease", name: "IX_ObligorAcknowledgementFcaalId", newName: "IX_InSentToBankId");
            RenameIndex(table: "dbo.ObligorRelease", name: "IX_BankAcknowledgementFcaalId", newName: "IX_CollateralReleasedId");
            RenameIndex(table: "dbo.ObligorRelease", name: "IX_ObligorAcknowledgementBankId", newName: "IX_ConfirmationLetterId");
            RenameColumn(table: "dbo.ObligorRelease", name: "ObligorAcknowledgementFcaalId", newName: "InSentToBankId");
            RenameColumn(table: "dbo.ObligorRelease", name: "ObligorAcknowledgementBankId", newName: "ConfirmationLetterId");
            RenameColumn(table: "dbo.ObligorRelease", name: "BankAcknowledgementFcaalId", newName: "CollateralReleasedId");
            CreateIndex("dbo.Obligor", "ObligorBankStatement_Id");
            AddForeignKey("dbo.Obligor", "ObligorBankStatement_Id", "dbo.ObligorBankStatement", "Id");
        }
    }
}
