CREATE TABLE [dbo].[Users] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Username] NVARCHAR (200) NOT NULL,
    [Password] NVARCHAR (200) NOT NULL,
    [Email]    NVARCHAR (200) NOT NULL,
    [IsAdmin]  BIT            NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);

