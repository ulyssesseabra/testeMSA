using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMSA.library.domain
{
    [Serializable, UnionSubclass(Name = "TestMSA.library.domain.Telefone,TestMSA.library",
   Table = "tbNh_telefone",
   Extends = "TestMSA.library.domain.EntityNH,TestMSA.library")]
    public class Telefone : EntityNH
    {
        [Property(Column = "tel_ddd", Length = 3)] 
        public virtual string DDD { get; set; }
        [Property(Column = "tel_numero", Length = 10)]
        public virtual string Numero { get; set; }
        [Property(Column = "tel_tipo")]
        public virtual TipoTelefone Tipo { get; set; }


    }
    public enum TipoTelefone
    {
        Pessoal = 0,
        Comercial = 1,
        Residencial = 2,
        Outros = 3
    }
}
