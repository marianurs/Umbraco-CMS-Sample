namespace PolRegio.Domain.Services.Shared
{
    public interface IMappingService
    {
        TOut Map<TIn, TOut>(TIn input);
        void Map<TIn, TOut>(TIn input, TOut output);
    }
}
