using MusicStreamer.Domain.Entity;

namespace MusicStreamer.Domain.Contracts
{
    public interface IPlayList
    {
        Task<IEnumerable<Musica>> GetMusicasAsync();
        Task<Musica> GetMusicaByIdAsync(int id);
        Task<bool> AddMusicaAsync(Musica musica);
        Task<bool> DeleteMusicaAsync(int id);
    }
}
