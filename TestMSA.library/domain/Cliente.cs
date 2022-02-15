using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMSA.library.domain
{
    [Serializable, UnionSubclass(Name = "TestMSA.library.domain.Cliente,TestMSA.library",
       Table = "tbNh_cliente",
       Extends = "TestMSA.library.domain.EntityNH,TestMSA.library")]
    public class Cliente: EntityNH
    {
        [Property(Column = "cli_nome", Length = 100)]
        public virtual string Nome { get; set; }
        [Property(Column = "cli_email", Length = 255)]
        public virtual string Email { get; set; }
        [Property(Column = "cli_dataNascimento")]
        public virtual DateTime DataNascimento { get; set; }
        [Bag(0, Table = "tbNh_telefone", Cascade = "all", Lazy = CollectionLazy.True)]
        [Key(1, Column = "cli_id")]
        [OneToMany(2, Class = "TestMSA.library.domain.Telefone,TestMSA.library")]
        public virtual IList<Telefone> Telefones { get; set; }
    }
}
