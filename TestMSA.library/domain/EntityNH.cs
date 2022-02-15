using br.com.ussolucoes.persistence.Dao;
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMSA.library.domain
{
    [Serializable, Class(Name = "TestMSA.library.domain.EntityNH ,TestMSA.library",
   Abstract = true, Lazy = false)]
    public abstract class EntityNH : IEntityNH
    {
        [NHibernate.Mapping.Attributes.Id(0, Name = "Id", UnsavedValue = "00000000-0000-0000-0000-000000000000")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "guid")]
        public virtual Guid Id { get; set; }
    }
}
