CREATE TABLE [dbo].[Playlists] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [ParentUserId] INT            NOT NULL,
    [Name]         NVARCHAR (200) NOT NULL,
    [Description]  NVARCHAR (200) NOT NULL,
    [IsPublic]     BIT            NOT NULL,
    CONSTRAINT [PK_Playlists] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Playlists_Users] FOREIGN KEY ([ParentUserId]) REFERENCES [dbo].[Users] ([Id])
);

