using Chinook.ClientModels;
using Chinook.Models;

namespace Chinook.Services
{
    public interface IArtistService
    {
        public Task<Artist> GetArtistByArtistIdAsync(long artistId);
        public Task<List<Artist>> GetArtistsAsync();
        public Task<List<Album>> GetAlbumsForArtistByArtistIdAsync(long artistId);
        public Task<List<Artist>> SearchArtistsByNameAsync(string searchQuery);
        public Task<List<PlaylistTrack>> GetTracksCreatedByArtistByArtistIdAsync(long artistId, string userId);
    }
}
