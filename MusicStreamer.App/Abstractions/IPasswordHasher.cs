namespace MusicStreamer.App.Abstractions;

public interface IHashSenha
{
    string Hash(string password);
    bool Verify(string hashedPassword, string providedPassword);
}

