using AutoMapper;
using br.com.ussolucoes.persistence.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMSA.library.domain;

namespace TestMSA.library.business
{
    public class MapperBusiness
    {
        public virtual IMapper getMapper()
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<long?, long>().ConvertUsing((src, dest) => { if (src.HasValue) return src.Value; return dest; });
                cfg.CreateMap<int?, int>().ConvertUsing((src, dest) => { if (src.HasValue) return src.Value; return dest; });
                cfg.CreateMap<Double?, Double>().ConvertUsing((src, dest) => { if (src.HasValue) return src.Value; return dest; });
                cfg.CreateMap<Decimal?, Decimal>().ConvertUsing((src, dest) => { if (src.HasValue) return src.Value; return dest; });
                cfg.CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => { if (src.HasValue) return src.Value; return dest; });
                cfg.CreateMap<Int32?, Int32>().ConvertUsing((src, dest) => { if (src.HasValue) return src.Value; return dest; });



                cfg.CreateMap<PagedList<Cliente>, PagedList<ClienteDTO>>();
                cfg.CreateMap<Cliente, ClienteDTO>();
                cfg.CreateMap<ClienteDTO, Cliente>()
                 .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

                cfg.CreateMap<PagedList<Telefone>, PagedList<TelefoneDTO>>();
                cfg.CreateMap<Telefone, TelefoneDTO>();
                cfg.CreateMap<TelefoneDTO, Telefone>()
                 .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            });
            return config.CreateMapper();


        }
    }
}
