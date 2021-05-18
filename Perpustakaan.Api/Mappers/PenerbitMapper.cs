using AutoMapper;
using Perpustakaan.Api.Models.DTO;
using Perpustakaan.Api.Models.Entity;
using System.Collections.Generic;

namespace Perpustakaan.Api.Mappers
{
    public static class PenerbitMapper
    {
        internal static IMapper Mapper { get; }

        static PenerbitMapper()
        {
            Mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap(typeof(Penerbit), typeof(PenerbitDTO)).ReverseMap();
            }).CreateMapper();
        }

        public static PenerbitDTO ToModel(this Penerbit entity)
        {
            return entity == null ? null : Mapper.Map<PenerbitDTO>(entity);
        }

        public static List<PenerbitDTO> ToModel(this List<Penerbit> entity)
        {
            return entity == null ? null : Mapper.Map<List<PenerbitDTO>>(entity);
        }

        public static Penerbit ToEntity(this PenerbitDTO model)
        {
            return model == null ? null : Mapper.Map<Penerbit>(model);
        }

        public static List<Penerbit> ToEntity(this List<PenerbitDTO> model)
        {
            return model == null ? null : Mapper.Map<List<Penerbit>>(model);
        }
    }
}
