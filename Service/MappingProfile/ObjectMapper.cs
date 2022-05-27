using System;
using AutoMapper;

namespace Service.MappingProfile
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> _lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cnfg => { cnfg.AddProfile<DtoMapper>(); });
            return config.CreateMapper();
        });
        public static IMapper Mapper => _lazy.Value;
    }
}