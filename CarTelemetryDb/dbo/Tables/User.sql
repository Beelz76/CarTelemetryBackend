CREATE TABLE [dbo].[User] (
    [UserId]   INT              IDENTITY (1, 1) NOT NULL,
    [UserUid]  UNIQUEIDENTIFIER NOT NULL,
    [Email]    NVARCHAR (256)   NOT NULL,
    [Password] NVARCHAR (256)   NOT NULL,
    [IsAdmin]  BIT              NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC),
    UNIQUE NONCLUSTERED ([Email] ASC),
    UNIQUE NONCLUSTERED ([UserUid] ASC)
);

