using Chinook.ClientModels;
using Chinook.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Chinook.Services
{
    public class ArtistService: IArtistService
    {
        private readonly IDbContextFactory<ChinookContext> _dbContextFactory;

        public ArtistService(IDbContextFactory<ChinookContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<List<Artist>> GetArtistsAsync()
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext.Artists.ToListAsync();
            }
        }

        public async Task<List<Artist>> SearchArtistsByNameAsync(string searchQuery)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext.Artists
                    .Where(artist => artist.Name.Contains(searchQuery))
                    .ToListAsync();
            }
        }

        public async Task<List<Album>> GetAlbumsForArtistByArtistIdAsync(long artistId)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext.Albums
                    .Where(a => a.ArtistId == artistId)
                    .ToListAsync();
            }
        }

        public async Task<Artist> GetArtistByArtistIdAsync(long artistId)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext.Artists.SingleOrDefaultAsync(a => a.ArtistId == artistId);
            }
        }

        public async Task<List<PlaylistTrack>> GetTracksCreatedByArtistByArtistIdAsync(long artistId, string userId)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext.Tracks.Where(a => a.Album.ArtistId == artistId)
                    .Include(a => a.Album)
                    .Select(t => new PlaylistTrack()
                    {
                        AlbumTitle = (t.Album == null ? "-" : t.Album.Title),
                        TrackId = t.TrackId,
                        TrackName = t.Name,
                        IsFavorite = t.Playlists.Where(p => p.UserPlaylists.Any(up => up.UserId == userId && up.Playlist.Name == "Favorites")).Any()
                    })
                    .ToListAsync();
            }
        }
    }
}
