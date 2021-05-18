using AutoMapper;
using Perpustakaan.Api.Models.DTO;
using Perpustakaan.Api.Models.Entity;
using System.Collections.Generic;

namespace Perpustakaan.Api.Mappers
{
    public static class PengembalianMapper
    {
        internal static IMapper Mapper { get; }

        static PengembalianMapper()
        {
            Mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap(typeof(Pengembalian), typeof(PengembalianDTO)).ReverseMap();
            }).CreateMapper();
        }

        public static PengembalianDTO ToModel(this Pengembalian entity)
        {
            return entity == null ? null : Mapper.Map<PengembalianDTO>(entity);
        }

        public static List<PengembalianDTO> ToModel(this List<Pengembalian> entity)
        {
            return entity == null ? null : Mapper.Map<List<PengembalianDTO>>(entity);
        }

        public static Pengembalian ToEntity(this PengembalianDTO model)
        {
            return model == null ? null : Mapper.Map<Pengembalian>(model);
        }

        public static List<Pengembalian> ToEntity(this List<PengembalianDTO> model)
        {
            return model == null ? null : Mapper.Map<List<Pengembalian>>(model);
        }
    }
}
