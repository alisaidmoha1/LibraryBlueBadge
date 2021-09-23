namespace Library.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        AdminId = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        ISBN = c.String(nullable: false),
                        AuthorName = c.String(nullable: false),
                        PublishedDate = c.String(nullable: false),
                        Quantity = c.Int(nullable: false),
                        LibraryCard_LibraryCardId = c.Int(),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.LibraryCard", t => t.LibraryCard_LibraryCardId)
                .Index(t => t.LibraryCard_LibraryCardId);
            
            CreateTable(
                "dbo.Checkout",
                c => new
                    {
                        CheckoutID = c.Int(nullable: false, identity: true),
                        AdminId = c.Guid(nullable: false),
                        BookId = c.Int(nullable: false),
                        LibraryCardId = c.Int(nullable: false),
                        DateOfCheckout = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CheckoutID)
                .ForeignKey("dbo.Book", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.LibraryCard", t => t.LibraryCardId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.LibraryCardId);
            
            CreateTable(
                "dbo.LibraryCard",
                c => new
                    {
                        LibraryCardId = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        Address = c.String(),
                        AdminId = c.Guid(nullable: false),
                        BookId = c.Int(nullable: false),
                        Books_BookId = c.Int(),
                    })
                .PrimaryKey(t => t.LibraryCardId)
                .ForeignKey("dbo.Book", t => t.Books_BookId)
                .Index(t => t.Books_BookId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        LibraryCardId = c.Int(nullable: false),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Checkout", "LibraryCardId", "dbo.LibraryCard");
            DropForeignKey("dbo.LibraryCard", "Books_BookId", "dbo.Book");
            DropForeignKey("dbo.Book", "LibraryCard_LibraryCardId", "dbo.LibraryCard");
            DropForeignKey("dbo.Checkout", "BookId", "dbo.Book");
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.LibraryCard", new[] { "Books_BookId" });
            DropIndex("dbo.Checkout", new[] { "LibraryCardId" });
            DropIndex("dbo.Checkout", new[] { "BookId" });
            DropIndex("dbo.Book", new[] { "LibraryCard_LibraryCardId" });
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.LibraryCard");
            DropTable("dbo.Checkout");
            DropTable("dbo.Book");
        }
    }
}
