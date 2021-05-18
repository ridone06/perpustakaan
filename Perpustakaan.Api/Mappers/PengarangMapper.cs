using AutoMapper;
using Perpustakaan.Api.Models.DTO;
using Perpustakaan.Api.Models.Entity;
using System.Collections.Generic;

namespace Perpustakaan.Api.Mappers
{
    public static class PengarangMapper
    {
        internal static IMapper Mapper { get; }

        static PengarangMapper()
        {
            Mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap(typeof(Pengarang), typeof(PengarangDTO)).ReverseMap();
            }).CreateMapper();
        }

        public static PengarangDTO ToModel(this Pengarang entity)
        {
            return entity == null ? null : Mapper.Map<PengarangDTO>(entity);
        }

        public static List<PengarangDTO> ToModel(this List<Pengarang> entity)
        {
            return entity == null ? null : Mapper.Map<List<PengarangDTO>>(entity);
        }

        public static Pengarang ToEntity(this PengarangDTO model)
        {
            return model == null ? null : Mapper.Map<Pengarang>(model);
        }

        public static List<Pengarang> ToEntity(this List<PengarangDTO> model)
        {
            return model == null ? null : Mapper.Map<List<Pengarang>>(model);
        }
    }
}
