CREATE PROCEDURE [dbo].[Procedure]
	@uid int = 0,
	@nickname varchar(25)
AS
	INSERT INTO User([UId], [NickName]) values (@uid, @nickname);
RETURN 0

