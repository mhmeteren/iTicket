using AutoMapper.Internal;
using AutoMapper;

namespace iTicket.Mapper.AutoMapper
{
    public class Mapper : Application.Interfaces.AutoMapper.IMapper
    {
        public static List<TypePair> typePairs = [];
        private IMapper MapperContainer;

        public TDestination Map<TDestination, TSource>(TSource source, string? ignore = null)
        {
            Config<TDestination, TSource>(depth: 5, ignore);
            return MapperContainer.Map<TSource, TDestination>(source);
        }

        public IList<TDestination> Map<TDestination, TSource>(IList<TSource> sources, string? ignore = null)
        {
            Config<TDestination, TSource>(depth: 5, ignore);
            return MapperContainer.Map<IList<TSource>, IList<TDestination>>(sources);
        }

        public TDestination Map<TDestination>(object source, string? ignore = null)
        {
            Config<TDestination, object>(depth: 5, ignore);
            return MapperContainer.Map<TDestination>(source);
        }

        public IList<TDestination> Map<TDestination>(IList<object> sources, string? ignore = null)
        {
            Config<TDestination, IList<object>>(depth: 5, ignore);
            return MapperContainer.Map<IList<TDestination>>(sources);
        }


        protected void Config<TDestination, TSource>(int depth = 5, string? ignore = null)
        {
            var typePair = new TypePair(typeof(TSource), typeof(TDestination));

            if (typePairs.Any(x => x.DestinationType == typePair.DestinationType && x.SourceType == typePair.SourceType) && ignore is null)
                return;

            typePairs.Add(typePair);

            var config = new MapperConfiguration(conf =>
            {
                foreach (var pair in typePairs)
                {
                    if (ignore is not null)
                    {
                        conf.CreateMap(pair.SourceType, pair.DestinationType)
                            .MaxDepth(depth)
                            .ForMember(ignore, x => x.Ignore())
                            .ReverseMap();
                    }
                    else
                    {
                        conf.CreateMap(pair.SourceType, pair.DestinationType)
                            .MaxDepth(depth)
                            .ReverseMap();
                    }
                }

            });

            MapperContainer = config.CreateMapper();
        }
    }
}
