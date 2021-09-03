using AutoMapper;

namespace Server.Mappers {
    public class GenericMapper{
        public static D Map<S, D>(S source) {
            MapperConfiguration _configuration = new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<S, D>();
            });

            var mapper = _configuration.CreateMapper();

            return mapper.Map<D>(source);
        }
    }
}