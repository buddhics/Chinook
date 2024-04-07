using Chinook.Models;

namespace Chinook.Services
{
    public interface IPlaylistService
    {
        Task<ClientModels.Playlist> GetPlaylistByIdAsync(long playlistId, string userId);
        Task RemoveTrackFromPlaylistAsync(long trackId, long playlistId);
    }
}
