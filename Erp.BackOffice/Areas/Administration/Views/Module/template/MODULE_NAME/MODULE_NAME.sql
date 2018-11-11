create table <MODULE_NAME>
(
	Id						int identity
	, IsDeleted				bit default(0)
	, CreatedDate			datetime default getdate()
	, CreatedUserId			int
	, ModifiedDate			datetime default getdate()
	, ModifiedUserId		int
	, AssignedUserId		int
	
	, Name					nvarchar(150)
<CONTENT>
		
	Primary Key(Id)
)

-- lấy id của dòng trên sau khi Execute điền vào cột [ParentId]
INSERT INTO [dbo].[System_Page]
           ([Name],[AreaName],[ActionName],[ControllerName],[Url],[OrderNo],[Status],[CssClassIcon]
           ,[IsDeleted],[IsVisible],[IsView],[IsAdd],[IsEdit],[IsDelete],[IsImport],[IsExport],[IsPrint])
     VALUES
           ('<AREA_NAME>_<MODULE_NAME>_Index', N'<AREA_NAME>', N'Index', N'<MODULE_NAME>', NULL, 1, 1, NULL
           , 0, 1, 0, 0, 0, 0, 0, 0, 0);

INSERT INTO [dbo].[System_Page]
           ([Name],[AreaName],[ActionName],[ControllerName],[Url],[OrderNo],[Status],[CssClassIcon]
           ,[IsDeleted],[IsVisible],[IsView],[IsAdd],[IsEdit],[IsDelete],[IsImport],[IsExport],[IsPrint])
     VALUES
           ('<AREA_NAME>_<MODULE_NAME>_Create', N'<AREA_NAME>', N'Create', N'<MODULE_NAME>', NULL, 2, 1, NULL
           , 0, 0, 0, 0, 0, 0, 0, 0, 0);

INSERT INTO [dbo].[System_Page]
           ([Name],[AreaName],[ActionName],[ControllerName],[Url],[OrderNo],[Status],[CssClassIcon]
           ,[IsDeleted],[IsVisible],[IsView],[IsAdd],[IsEdit],[IsDelete],[IsImport],[IsExport],[IsPrint])
     VALUES
           ('<AREA_NAME>_<MODULE_NAME>_Edit', N'<AREA_NAME>', N'Edit', N'<MODULE_NAME>', NULL, 3, 1, NULL
           , 0, 0, 0, 0, 0, 0, 0, 0, 0);
		   
INSERT INTO [dbo].[System_Page]
           ([Name],[AreaName],[ActionName],[ControllerName],[Url],[OrderNo],[Status],[CssClassIcon]
           ,[IsDeleted],[IsVisible],[IsView],[IsAdd],[IsEdit],[IsDelete],[IsImport],[IsExport],[IsPrint])
     VALUES
           ('<AREA_NAME>_<MODULE_NAME>_Detail', N'<AREA_NAME>', N'Detail', N'<MODULE_NAME>', NULL, 3, 1, NULL
           , 0, 0, 0, 0, 0, 0, 0, 0, 0);

INSERT INTO [dbo].[System_Page]
           ([Name],[AreaName],[ActionName],[ControllerName],[Url],[OrderNo],[Status],[CssClassIcon]
           ,[IsDeleted],[IsVisible],[IsView],[IsAdd],[IsEdit],[IsDelete],[IsImport],[IsExport],[IsPrint])
     VALUES
           ('<AREA_NAME>_<MODULE_NAME>_Delete', N'<AREA_NAME>', N'Delete', N'<MODULE_NAME>', NULL, 4, 1, NULL
           , 0, 0, 0, 0, 0, 0, 0, 0, 0);
GO

---- sau khi chạy 5 dòng trên thì lấy id tương ứng của 5 dòng đó điền vào cột [System_PageId]
---------------------------------------------------------
INSERT INTO [dbo].[System_PageMenu]
           ([LanguageId],[PageId],[Name])
     VALUES
           ('vi-VN', (select Id from [System_Page] where [ActionName] = N'Index' and [ControllerName] = N'<MODULE_NAME>'), N'<MODULE_LABEL>')
GO

SELECT TOP 5 *
  FROM [dbo].[System_Page]
  order by id desc

SELECT TOP 1 [LanguageId], Name
  FROM [dbo].[System_PageMenu]
  order by id desc