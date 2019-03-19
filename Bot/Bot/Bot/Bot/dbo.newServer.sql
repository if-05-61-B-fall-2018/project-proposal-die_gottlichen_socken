CREATE PROCEDURE [dbo].[newServer]
	@sid int ,
	@sname varchar(20)
AS
	INSERT INTO Server([SId],[SName]) VALUES(@sid,@sname);
RETURN 0
