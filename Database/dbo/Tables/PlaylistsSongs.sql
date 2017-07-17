CREATE TABLE [dbo].[PlaylistsSongs] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [PlaylistId] INT NOT NULL,
    [SongId]     INT NOT NULL,
    CONSTRAINT [PK_PlaylistsSongs] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PlaylistsSongs_Playlists] FOREIGN KEY ([PlaylistId]) REFERENCES [dbo].[Playlists] ([Id]),
    CONSTRAINT [FK_PlaylistsSongs_Songs] FOREIGN KEY ([SongId]) REFERENCES [dbo].[Songs] ([Id])
);

