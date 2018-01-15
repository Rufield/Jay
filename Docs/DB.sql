CREATE DATABASE IF NOT EXISTS DBWorker;
CREATE TABLE [dbo].[AccountTable]
(
	[IdAccount] INT NOT NULL AUTO_INCREMENT, 
    [Email] VARCHAR(50) NULL, 
    [Login] VARCHAR(50) NULL, 
    [Password] VARCHAR(50) NULL
);
CREATE TABLE [dbo].[CommentsTable]
(
	[IdComments] INT NOT NULL AUTO_INCREMENT, 
    [Text] VARCHAR(50) NULL, 
    [IdAccount] INT NULL, 
    [DataCreated] DATETIME NULL
);
CREATE TABLE [dbo].[LikeTable]
(
	[IdLike] INT NOT NULL AUTO_INCREMENT, 
	[Likeed] INT NULL
);
CREATE TABLE [dbo].[PostTable]
(
	[IdPost] INT NOT NULL AUTO_INCREMENT, 
    [Artical] VARCHAR(50) NULL, 
    [HashTag] VARCHAR(50) NULL, 
    [IdLike] INT NULL, 
    [IdRetweet] INT NULL, 
    [IdComments] INT NULL
);
CREATE TABLE [dbo].[RetweetPost]
(
	[IdRetweet] INT NOT NULL AUTO_INCREMENT, 
    [Retweeted] INT NULL
);
INSERT INTO dbo.AccountTable (Email, Login, Password) VALUE ('Nub@gmail.com', 'Nub', '123');
INSERT INTO dbo.AccountTable (Email, Login, Password) VALUE ('Huck@gmail.com', 'Huck', 'hUck');
INSERT INTO dbo.AccountTable (Email, Login, Password) VALUE ('Admin@gmail.com', 'Admin', '111');
INSERT INTO dbo.CommentsTable (Text, IdAccount, DataCreated) VALUE ('Life', '1', GETDATE());
INSERT INTO dbo.CommentsTable (Text, IdAccount, DataCreated) VALUE ('is', '2', GETDATE());
INSERT INTO dbo.CommentsTable (Text, IdAccount, DataCreated) VALUE ('good', '3', GETDATE());
INSERT INTO dbo.LikeTable (Likeed) VALUE ('13');
INSERT INTO dbo.LikeTable (Likeed) VALUE ('13');
INSERT INTO dbo.LikeTable (Likeed) VALUE ('13');
INSERT INTO dbo.RetweetPost (Retweeted) VALUE ('13');
INSERT INTO dbo.RetweetPost (Retweeted) VALUE ('13');
INSERT INTO dbo.RetweetPost (Retweeted) VALUE ('13');
INSERT INTO dbo.PostTable (Artical, HashTag, IdLike, IdRetweet, IdComments) VALUE ('Why', 'deep', '1', '2', '3');
INSERT INTO dbo.PostTable (Artical, HashTag, IdLike, IdRetweet, IdComments) VALUE ('so', 'deep', '3', '2', '1');
INSERT INTO dbo.PostTable (Artical, HashTag, IdLike, IdRetweet, IdComments) VALUE ('difficult', 'deep', '3', '1', '2');