using AutoMapper;

namespace Server.Mappers {
    public class ModelMapper<S, D>{
        IMapper _mapper;

        public ModelMapper()
        {
            MapperConfiguration _configuration = new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<S, D>();
            });

            _mapper = _configuration.CreateMapper();
        }

        public D Map(S source) {
            return _mapper.Map<D>(source);
        }
    }

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