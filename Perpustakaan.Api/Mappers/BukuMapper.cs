using AutoMapper;
using Perpustakaan.Api.Models.DTO;
using Perpustakaan.Api.Models.Entity;
using System.Collections.Generic;

namespace Perpustakaan.Api.Mappers
{
    public static class BukuMapper
    {
        internal static IMapper Mapper { get; }

        static BukuMapper()
        {
            Mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap(typeof(Buku), typeof(BukuDTO)).ReverseMap();
            }).CreateMapper();
        }

        public static BukuDTO ToModel(this Buku entity)
        {
            return entity == null ? null : Mapper.Map<BukuDTO>(entity);
        }

        public static List<BukuDTO> ToModel(this List<Buku> entity)
        {
            return entity == null ? null : Mapper.Map<List<BukuDTO>>(entity);
        }

        public static Buku ToEntity(this BukuDTO model)
        {
            return model == null ? null : Mapper.Map<Buku>(model);
        }

        public static List<Buku> ToEntity(this List<BukuDTO> model)
        {
            return model == null ? null : Mapper.Map<List<Buku>>(model);
        }
    }
}
