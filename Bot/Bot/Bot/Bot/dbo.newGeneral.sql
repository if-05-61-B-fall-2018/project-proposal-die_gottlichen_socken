CREATE PROCEDURE [dbo].[newGeneral]
	@sid int ,
	@uid int
AS
	INSERT INTO general([SId],[UId]) VALUES(@sid, @uid);
RETURN 0
