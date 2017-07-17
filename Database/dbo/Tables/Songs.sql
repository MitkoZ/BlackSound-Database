CREATE TABLE [dbo].[Songs] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Title]      NVARCHAR (200) NOT NULL,
    [ArtistName] NVARCHAR (200) NOT NULL,
    [Year]       INT            NOT NULL,
    CONSTRAINT [PK_Songs] PRIMARY KEY CLUSTERED ([Id] ASC)
);

