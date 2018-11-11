namespace Erp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.System_BOLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 200),
                        Action = c.String(nullable: false, maxLength: 200),
                        Note = c.String(nullable: false),
                        CreatedDate = c.DateTime(),
                        Controller = c.String(maxLength: 200),
                        Area = c.String(maxLength: 200),
                        Data = c.String(),
                        Type = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.System_Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 150),
                        Value = c.String(maxLength: 150),
                        Code = c.String(maxLength: 50),
                        Description = c.String(),
                        OrderNo = c.Int(),
                        ParentId = c.Int(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(),
                        CreatedUserId = c.Int(),
                        ModifiedUserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.System_Category", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.System_News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedUser = c.Int(),
                        UpdateUser = c.Int(),
                        OrderNo = c.Int(),
                        ThumbnailPath = c.String(maxLength: 500),
                        IsDeleted = c.Boolean(),
                        ImagePath = c.String(maxLength: 500),
                        CategoryId = c.Int(),
                        IsPublished = c.Boolean(),
                        Title = c.String(maxLength: 250),
                        ShortContent = c.String(),
                        Content = c.String(),
                        Url = c.String(maxLength: 100),
                        PublishedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.System_Category", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.System_Language",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 10),
                        Name = c.String(nullable: false, maxLength: 100),
                        IsDefault = c.Boolean(nullable: false),
                        ActiveImage = c.String(),
                        DeactiveImage = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.System_PageMenu",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LanguageId = c.String(maxLength: 10),
                        PageId = c.Int(),
                        Name = c.String(maxLength: 100),
                        Url = c.String(),
                        OrderNo = c.Int(),
                        CssClassIcon = c.String(),
                        ParentId = c.Int(),
                        IsVisible = c.Boolean(),
                        IsDashboard = c.Boolean(),
                        DashboardView = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.System_Language", t => t.LanguageId)
                .Index(t => t.LanguageId);
            
            CreateTable(
                "dbo.vwSystem_Location",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 10),
                        Name = c.String(maxLength: 250),
                        Type = c.String(maxLength: 250),
                        ParentId = c.String(maxLength: 10),
                        Group = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.System_LoginLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        LoginTime = c.DateTime(),
                        TypeWebsite = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.webpages_Membership",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        CreateDate = c.DateTime(),
                        ConfirmationToken = c.String(maxLength: 128),
                        IsConfirmed = c.Boolean(),
                        LastPasswordFailureDate = c.DateTime(),
                        PasswordFailuresSinceLastSuccess = c.Int(nullable: false),
                        Password = c.String(nullable: false, maxLength: 128),
                        PasswordChangedDate = c.DateTime(),
                        PasswordSalt = c.String(nullable: false, maxLength: 128),
                        PasswordVerificationToken = c.String(maxLength: 128),
                        PasswordVerificationTokenExpirationDate = c.DateTime(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.System_User", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.System_User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        UserName = c.String(maxLength: 150),
                        Status = c.Int(),
                        UserCode = c.String(),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        FullName = c.String(maxLength: 100),
                        DateOfBirth = c.DateTime(),
                        Address = c.String(maxLength: 200),
                        Mobile = c.String(maxLength: 200),
                        Email = c.String(),
                        Sex = c.Boolean(),
                        UserTypeId = c.Int(),
                        LoginFailedCount = c.Int(),
                        LoginFailedCount2 = c.Int(),
                        LoginFailedCount3 = c.Int(),
                        LastChangedPassword = c.DateTime(),
                        BranchId = c.Int(),
                        ProfileImage = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.System_MetadataField",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(),
                        CreatedUserId = c.Int(),
                        CreatedDate = c.DateTime(),
                        ModifiedUserId = c.Int(),
                        ModifiedDate = c.DateTime(),
                        Name = c.String(maxLength: 150),
                        ModuleId = c.Int(),
                        IsVisible = c.Boolean(),
                        OrderNo = c.Int(),
                        Expression = c.String(maxLength: 300),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.System_Module",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(),
                        CreatedUserId = c.Int(),
                        CreatedDate = c.DateTime(),
                        ModifiedUserId = c.Int(),
                        ModifiedDate = c.DateTime(),
                        Name = c.String(maxLength: 150),
                        TableName = c.String(maxLength: 50),
                        OrderNo = c.Int(),
                        IsVisible = c.Boolean(),
                        DisplayName = c.String(maxLength: 150),
                        AreaName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.System_ModuleRelationship",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(),
                        CreatedUserId = c.Int(),
                        CreatedDate = c.DateTime(),
                        ModifiedUserId = c.Int(),
                        ModifiedDate = c.DateTime(),
                        First_ModuleName = c.String(),
                        First_MetadataFieldName = c.String(maxLength: 100),
                        Second_ModuleName = c.String(maxLength: 100),
                        Second_ModuleName_Alias = c.String(),
                        Second_MetadataFieldName = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.System_News_ViewedUser",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NewsId = c.Int(nullable: false),
                        ViewedUser = c.Int(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        ViewedDT = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.System_Page",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        AreaName = c.String(maxLength: 50),
                        ActionName = c.String(maxLength: 50),
                        ControllerName = c.String(maxLength: 50),
                        Url = c.String(maxLength: 255),
                        OrderNo = c.Int(),
                        Status = c.Boolean(),
                        CssClassIcon = c.String(maxLength: 100),
                        ParentId = c.Int(),
                        IsDeleted = c.Boolean(),
                        IsVisible = c.Boolean(),
                        IsView = c.Boolean(),
                        IsAdd = c.Boolean(),
                        IsEdit = c.Boolean(),
                        IsDelete = c.Boolean(),
                        IsImport = c.Boolean(),
                        IsExport = c.Boolean(),
                        IsPrint = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.System_Setting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 200),
                        Value = c.String(nullable: false),
                        Note = c.String(),
                        Code = c.String(),
                        IsLocked = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.System_UserPage",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        PageId = c.Int(nullable: false),
                        View = c.Boolean(),
                        Edit = c.Boolean(),
                        Add = c.Boolean(),
                        Delete = c.Boolean(),
                        Import = c.Boolean(),
                        Export = c.Boolean(),
                        Print = c.Boolean(),
                    })
                .PrimaryKey(t => new { t.UserId, t.PageId });
            
            CreateTable(
                "dbo.System_UserSetting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SettingId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Value = c.String(maxLength: 300),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.System_UserType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        OrderNo = c.Int(),
                        Note = c.String(maxLength: 500),
                        Scope = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.System_UserTypePage",
                c => new
                    {
                        UserTypeId = c.Int(nullable: false),
                        PageId = c.Int(nullable: false),
                        View = c.Boolean(),
                        Edit = c.Boolean(),
                        Add = c.Boolean(),
                        Delete = c.Boolean(),
                        Import = c.Boolean(),
                        Export = c.Boolean(),
                        Print = c.Boolean(),
                    })
                .PrimaryKey(t => new { t.UserTypeId, t.PageId });
            
            CreateTable(
                "dbo.vwSystem_MetadataField",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(),
                        CreatedUserId = c.Int(),
                        CreatedDate = c.DateTime(),
                        ModifiedUserId = c.Int(),
                        ModifiedDate = c.DateTime(),
                        Name = c.String(maxLength: 150),
                        ModuleId = c.Int(),
                        IsVisible = c.Boolean(),
                        OrderNo = c.Int(),
                        Expression = c.String(maxLength: 300),
                        ModuleName = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.vwSystem_PageMenu",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LanguageId = c.String(maxLength: 10),
                        PageId = c.Int(),
                        Name = c.String(maxLength: 100),
                        Url = c.String(),
                        OrderNo = c.Int(),
                        CssClassIcon = c.String(),
                        ParentId = c.Int(),
                        IsVisible = c.Boolean(),
                        AreaName = c.String(),
                        PageUrl = c.String(),
                        IsDashboard = c.Boolean(),
                        DashboardView = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.vwSystem_Page",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        AreaName = c.String(maxLength: 50),
                        ActionName = c.String(maxLength: 50),
                        ControllerName = c.String(maxLength: 50),
                        Url = c.String(maxLength: 255),
                        OrderNo = c.Int(),
                        Status = c.Boolean(),
                        CssClassIcon = c.String(maxLength: 100),
                        ParentId = c.Int(),
                        IsDeleted = c.Boolean(),
                        IsVisible = c.Boolean(),
                        IsView = c.Boolean(),
                        IsAdd = c.Boolean(),
                        IsEdit = c.Boolean(),
                        IsDelete = c.Boolean(),
                        IsImport = c.Boolean(),
                        IsExport = c.Boolean(),
                        IsPrint = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.vwSystem_Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        UserName = c.String(maxLength: 150),
                        Status = c.Int(nullable: false),
                        UserCode = c.String(),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        FullName = c.String(maxLength: 100),
                        DateOfBirth = c.DateTime(),
                        Address = c.String(maxLength: 200),
                        Mobile = c.String(maxLength: 200),
                        Email = c.String(),
                        Sex = c.Boolean(),
                        UserTypeId = c.Int(nullable: false),
                        LoginFailedCount = c.Int(),
                        LoginFailedCount2 = c.Int(),
                        LoginFailedCount3 = c.Int(),
                        LastChangedPassword = c.DateTime(),
                        UserTypeName = c.String(),
                        BranchId = c.Int(),
                        BranchName = c.String(),
                        BranchCode = c.String(),
                        ProfileImage = c.String(),
                        WarehouseId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.webpages_OAuthMembership",
                c => new
                    {
                        Provider = c.String(nullable: false, maxLength: 30),
                        ProviderUserId = c.String(nullable: false, maxLength: 100),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Provider, t.ProviderUserId });
            
            CreateTable(
                "dbo.webpages_Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.webpages_Membership", "User_Id", "dbo.System_User");
            DropForeignKey("dbo.System_PageMenu", "LanguageId", "dbo.System_Language");
            DropForeignKey("dbo.System_Category", "ParentId", "dbo.System_Category");
            DropForeignKey("dbo.System_News", "CategoryId", "dbo.System_Category");
            DropIndex("dbo.webpages_Membership", new[] { "User_Id" });
            DropIndex("dbo.System_PageMenu", new[] { "LanguageId" });
            DropIndex("dbo.System_News", new[] { "CategoryId" });
            DropIndex("dbo.System_Category", new[] { "ParentId" });
            DropTable("dbo.webpages_Roles");
            DropTable("dbo.webpages_OAuthMembership");
            DropTable("dbo.vwSystem_Users");
            DropTable("dbo.vwSystem_Page");
            DropTable("dbo.vwSystem_PageMenu");
            DropTable("dbo.vwSystem_MetadataField");
            DropTable("dbo.System_UserTypePage");
            DropTable("dbo.System_UserType");
            DropTable("dbo.System_UserSetting");
            DropTable("dbo.System_UserPage");
            DropTable("dbo.System_Setting");
            DropTable("dbo.System_Page");
            DropTable("dbo.System_News_ViewedUser");
            DropTable("dbo.System_ModuleRelationship");
            DropTable("dbo.System_Module");
            DropTable("dbo.System_MetadataField");
            DropTable("dbo.System_User");
            DropTable("dbo.webpages_Membership");
            DropTable("dbo.System_LoginLog");
            DropTable("dbo.vwSystem_Location");
            DropTable("dbo.System_PageMenu");
            DropTable("dbo.System_Language");
            DropTable("dbo.System_News");
            DropTable("dbo.System_Category");
            DropTable("dbo.System_BOLog");
        }
    }
}
