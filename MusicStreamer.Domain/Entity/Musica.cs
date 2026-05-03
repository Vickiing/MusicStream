namespace MusicStreamer.Domain.Entity
{
    public class Musica
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Artista { get; set; }
        public required string Titulo { get; set; }
        public  string? Album { get; set; }

    }
}
