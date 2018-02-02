using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MusicDB
{
    public class MusicDbContext : Database
    {
        #region Artists

        public DataTable GetArtists()
        {
            return GetTable("SELECT * FROM " + GetTableName("Artists"));
        }

        public bool AddArtist(string ArtistName, string Country, bool IsGroup, string RealName, int StartedYear)
        {
            string is_group = "";
            if (IsGroup)
                is_group = "t";
            else
                is_group = "f";
            string insertQuery = "INSERT INTO " + GetTableName("Artists") +
     $" (artist_name,country,is_group,real_name,started_year) VALUES('{ArtistName}','{Country}','{is_group}','{RealName}',{StartedYear})";
            return ExecuteQuery(insertQuery);
        }

        public bool UpdateArtist(int ArtistId, string ArtistName, string Country, bool IsGroup, string RealName, int StartedYear)
        {
            string is_group = "";
            if (IsGroup)
                is_group = "t";
            else
                is_group = "f";
            string updateQuery = "UPDATE " + GetTableName("Artists") +
 $" SET artist_name = '{ArtistName}', country = '{Country}', is_group = '{is_group}', real_name = '{RealName}', started_year = {StartedYear} WHERE artist_id = {ArtistId};";
            return ExecuteQuery(updateQuery);
        }

        public bool DeleteArtist(int ArtistId)
        {
            string deleteQuery = "DELETE FROM " + GetTableName("Artists") + $" WHERE artist_id={ArtistId};";
            return ExecuteQuery(deleteQuery);
        }

        #endregion

        #region Albums

        public DataTable GetAlbums()
        {
            return GetTable("SELECT * FROM " + GetTableName("Albums"));
        }

        public bool AddAlbum(string AlbumName, int ArtistId, string Barcode, string Country, bool IsSingle, int ReleaseYear)
        {
            string is_single = "";
            if (IsSingle)
                is_single = "t";
            else
                is_single = "f";
            string insertQuery = "INSERT INTO " + GetTableName("Albums") +
$"(album_name,artist_id,barcode,country,is_single,release_year) VALUES('{AlbumName}',{ArtistId},'{Barcode}','{Country}','{is_single}',{ReleaseYear})";
            return ExecuteQuery(insertQuery);
        }

        public bool UpdateAlbum(int AlbumId, string AlbumName, int ArtistId, string Barcode, string Country, bool IsSingle, int ReleaseYear)
        {
            string is_single = "";
            if (IsSingle)
                is_single = "t";
            else
                is_single = "f";
            string updateQuery = "UPDATE " +
 GetTableName("Albums") + $" SET album_name='{AlbumName}',artist_id={ArtistId},barcode='{Barcode}',country='{Country}',is_single='{is_single}',release_year={ReleaseYear} WHERE album_id={AlbumId}";
            return ExecuteQuery(updateQuery);
        }

        public bool DeleteAlbum(int AlbumId)
        {
            string deleteQuery = "DELETE FROM " + GetTableName("Albums") + $" WHERE album_id={AlbumId}";
            return ExecuteQuery(deleteQuery);
        }

        #endregion

        #region Featurings

        public DataTable GetFeaturings()
        {
            return GetTable("SELECT * FROM " + GetTableName("Featurings"));
        }

        public bool AddFeaturing(int ArtistId, int SongId)
        {
            string insertQuery = "INSERT INTO " + GetTableName("Featurings") + $" (artist_id,song_id) VALUES ({ArtistId},{SongId})";
            return ExecuteQuery(insertQuery);
        }

        public bool UpdateFeaturing(int FeaturingId, int ArtistId, int SongId)
        {
            string updateQuery = "UPDATE " + GetTableName("Featurings") + $" SET artist_id={ArtistId}, song_id={SongId} WHERE featuring_id={FeaturingId}";
            return ExecuteQuery(updateQuery);
        }

        public bool DeleteFeaturing(int FeaturingId)
        {
            string deleteQuery = "DELETE FROM " + GetTableName("Featurings") + $" WHERE featuring_id={FeaturingId}";
            return ExecuteQuery(deleteQuery);
        }

        #endregion

        #region Genres

        public DataTable GetGenres()
        {
            return GetTable("SELECT * FROM " + GetTableName("Genres"));
        }

        public bool AddGenre(string GenreName)
        {
            string insertQuery = "INSERT INTO " + GetTableName("Genres") + $"(genre_name) VALUES ('{GenreName}')";
            return ExecuteQuery(insertQuery);
        }

        public bool UpdateGenre(int GenreId, string GenreName)
        {
            string updateQuery = "UPDATE " + GetTableName("Genres") + $" SET genre_name='{GenreName}' WHERE genre_id={GenreId}";
            return ExecuteQuery(updateQuery);
        }

        public bool DeleteGenre(int GenreId)
        {
            string deleteQuery = "DELETE FROM " + GetTableName("Genres") + $" WHERE genre_id={GenreId}";
            return ExecuteQuery(deleteQuery);
        }

        #endregion

        #region Lyrics

        public DataTable GetLyrics()
        {
            return GetTable("SELECT * FROM " + GetTableName("Lyrics"));
        }

        public bool AddLyric(string Language, string Lyrics, int SongId)
        {
            string insertQuery = "INSERT INTO " + GetTableName("Lyrics") +
  $" (language,lyrics,song_id) VALUES ('{Language}','{Lyrics}',{SongId})";
            return ExecuteQuery(insertQuery);
        }

        public bool UpdateLyric(int LyricId, string Language, string Lyrics, int SongId)
        {
            string updateQuery = "UPDATE " + GetTableName("Lyrics") +
 $" SET language='{Language}', lyrics='{Lyrics}', song_id={SongId} WHERE lyric_id={LyricId}";
            return ExecuteQuery(updateQuery);
        }

        public bool DeleteLyric(int LyricId)
        {
            string deleteQuery = "DELETE FROM " + GetTableName("Lyrics") + $" WHERE lyric_id={LyricId}";
            return ExecuteQuery(deleteQuery);
        }

        #endregion

        #region Songs

        public DataTable GetSongs()
        {
            return GetTable("SELECT * FROM " + GetTableName("Songs"));
        }

        public bool AddSong(int AlbumId, int ArtistId, int GenreId, bool IsFeaturing, string Language, string SongName)
        {
            string insertQuery = "INSERT INTO " + GetTableName("Songs") +
        " (album_id,artist_id,genre_id,is_featuring,language,song_name) " +
        $"VALUES({AlbumId},{ArtistId},{GenreId},'{IsFeaturing}','{Language}','{SongName}')";
            return ExecuteQuery(insertQuery);
        }

        public bool UpdateSong(int SongId, int AlbumId, int ArtistId, int GenreId, bool IsFeaturing, string Language, string SongName)
        {
            string is_featuring = "";
            if (IsFeaturing)
            {
                is_featuring = "t";
            }
            else
            {
                is_featuring = "f";
            }
            string updateQuery = "UPDATE " + GetTableName("Songs") +
        $" SET album_id={AlbumId}, artist_id={ArtistId}, genre_id={GenreId}, is_featuring='{is_featuring}',language='{Language}',song_name='{SongName}' WHERE song_id={SongId}";
            return ExecuteQuery(updateQuery);
        }

        public bool DeleteSong(int SongId)
        {
            string deleteQuery = "DELETE FROM " + GetTableName("Songs") + $" WHERE song_id={SongId}";
            return ExecuteQuery(deleteQuery);
        }

        #endregion

        #region LogTable

        public DataTable GetLogTable()
        {
            return GetTable("SELECT * FROM " + GetTableName("log_table"));
        }

        #endregion

        #region SongInfo

        public DataTable GetSongInfo()
        {
            return GetTable("SELECT * FROM " + GetTableName("song_info"));
        }

        public DataTable Search(string kelime)
        {
            string searchQuery = "SELECT * FROM " + GetTableName("song_info") +
 $" WHERE song_name LIKE '%{kelime}%' OR artist_name LIKE '%{kelime}%' OR album_name LIKE '%{kelime}%' ";
            return GetTable(searchQuery);
        }

        #endregion

        #region Helper Methods

        private string GetTableName(string tableName)
        {
            return @"public.""" + tableName + @"""";
        }

        #endregion
    }
}
