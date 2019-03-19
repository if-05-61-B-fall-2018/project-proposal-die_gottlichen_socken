CREATE PROCEDURE [dbo].[newItem]
	@iid int,
	@iname varchar(20),
	@price int
AS
	INSERT INTO Items([IId],[IName],[Price]) VALUES(@iid,@iname,@price);
RETURN 0
