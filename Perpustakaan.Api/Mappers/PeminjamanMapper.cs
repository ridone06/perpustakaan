using AutoMapper;
using Perpustakaan.Api.Models.DTO;
using Perpustakaan.Api.Models.Entity;
using System.Collections.Generic;

namespace Perpustakaan.Api.Mappers
{
    public static class PeminjamanMapper
    {
        internal static IMapper Mapper { get; }

        static PeminjamanMapper()
        {
            Mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap(typeof(Peminjaman), typeof(PeminjamanDTO)).ReverseMap();
            }).CreateMapper();
        }

        public static PeminjamanDTO ToModel(this Peminjaman entity)
        {
            return entity == null ? null : Mapper.Map<PeminjamanDTO>(entity);
        }

        public static List<PeminjamanDTO> ToModel(this List<Peminjaman> entity)
        {
            return entity == null ? null : Mapper.Map<List<PeminjamanDTO>>(entity);
        }

        public static Peminjaman ToEntity(this PeminjamanDTO model)
        {
            return model == null ? null : Mapper.Map<Peminjaman>(model);
        }

        public static List<Peminjaman> ToEntity(this List<PeminjamanDTO> model)
        {
            return model == null ? null : Mapper.Map<List<Peminjaman>>(model);
        }
    }
}
