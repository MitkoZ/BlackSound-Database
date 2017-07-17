CREATE TABLE [dbo].[UsersPlaylists] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [UserId]     INT NOT NULL,
    [PlaylistId] INT NOT NULL,
    CONSTRAINT [PK_UsersPlaylists] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UsersPlaylists_Playlists] FOREIGN KEY ([PlaylistId]) REFERENCES [dbo].[Playlists] ([Id]),
    CONSTRAINT [FK_UsersPlaylists_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

