using MusicStreamer.Domain.Contracts;
using MusicStreamer.Domain.Entity;

namespace MusicStreamer.Domain.Services
{
    public class Playlist : IPlayList
    {
        public Task<bool> AddMusicaAsync(Musica musica)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteMusicaAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Musica> GetMusicaByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Musica>> GetMusicasAsync()
        {
            throw new NotImplementedException();
        }
    }
}
