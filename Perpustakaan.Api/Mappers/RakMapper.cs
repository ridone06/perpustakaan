using AutoMapper;
using Perpustakaan.Api.Models.DTO;
using Perpustakaan.Api.Models.Entity;
using System.Collections.Generic;

namespace Perpustakaan.Api.Mappers
{
    public static class RakMapper
    {
        internal static IMapper Mapper { get; }

        static RakMapper()
        {
            Mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap(typeof(Rak), typeof(RakDTO)).ReverseMap();
            }).CreateMapper();
        }

        public static RakDTO ToModel(this Rak entity)
        {
            return entity == null ? null : Mapper.Map<RakDTO>(entity);
        }

        public static List<RakDTO> ToModel(this List<Rak> entity)
        {
            return entity == null ? null : Mapper.Map<List<RakDTO>>(entity);
        }

        public static Rak ToEntity(this RakDTO model)
        {
            return model == null ? null : Mapper.Map<Rak>(model);
        }

        public static List<Rak> ToEntity(this List<RakDTO> model)
        {
            return model == null ? null : Mapper.Map<List<Rak>>(model);
        }
    }
}
