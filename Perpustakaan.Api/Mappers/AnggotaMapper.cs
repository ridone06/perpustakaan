using AutoMapper;
using Perpustakaan.Api.Models.DTO;
using Perpustakaan.Api.Models.Entity;
using System.Collections.Generic;

namespace Perpustakaan.Api.Mappers
{
    public static class AnggotaMapper
    {
        internal static IMapper Mapper { get; }

        static AnggotaMapper()
        {
            Mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap(typeof(Anggota), typeof(AnggotaDTO)).ReverseMap();
            }).CreateMapper();
        }

        public static AnggotaDTO ToModel(this Anggota entity)
        {
            return entity == null ? null : Mapper.Map<AnggotaDTO>(entity);
        }

        public static List<AnggotaDTO> ToModel(this List<Anggota> entity)
        {
            return entity == null ? null : Mapper.Map<List<AnggotaDTO>>(entity);
        }

        public static Anggota ToEntity(this AnggotaDTO model)
        {
            return model == null ? null : Mapper.Map<Anggota>(model);
        }

        public static List<Anggota> ToEntity(this List<AnggotaDTO> model)
        {
            return model == null ? null : Mapper.Map<List<Anggota>>(model);
        }
    }
}
