using AutoMapper;
using PolRegio.Domain.Services.Shared;

namespace PolRegio.Services.Shared
{
    public class MappingService : IMappingService
    {
        public TOut Map<TIn, TOut>(TIn input)
        {
            return Mapper.Map<TIn, TOut>(input);
        }

        public void Map<TIn, TOut>(TIn input, TOut output)
        {
            Mapper.Map(input, output);
        }
    }
}
