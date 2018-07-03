namespace PolRegio.Domain.Services.Shared
{
    public interface IHashingService
    {
        string Hash(string source);
        bool Compare(string oldHash, string newSource);
    }
}
