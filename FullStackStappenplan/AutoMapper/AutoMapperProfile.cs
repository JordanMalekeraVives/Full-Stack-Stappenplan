using AutoMapper;
using FullStackStappenplan.Domains.Entities;

namespace FullStackStappenplan;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Beer, BeerVM>();
        //CreateMap<TSource, TDestination>;
    }
}
