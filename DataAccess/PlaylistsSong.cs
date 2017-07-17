//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;

    public partial class PlaylistsSong : IBaseEntity
    {
        public int Id { get; set; }
        public int PlaylistId { get; set; }
        public int SongId { get; set; }

        public virtual Playlist Playlist { get; set; }
        public virtual Song Song { get; set; }

        public PlaylistsSong(Song songDb, Playlist playlistDb)
        {
            this.SongId = songDb.Id;
            this.PlaylistId = playlistDb.Id;
        }

        public PlaylistsSong(Playlist playlistDb)
        {
            this.PlaylistId = playlistDb.Id;
        }

        public PlaylistsSong()
        {

        }
    }
}