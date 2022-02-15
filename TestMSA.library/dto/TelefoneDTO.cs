using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMSA.library.domain
{
    public class TelefoneDTO
    {
        public virtual String DDD { get; set; }
        public virtual String Numero { get; set; }
        public virtual TipoTelefone Tipo { get; set; }
        public virtual Guid Id { get; set; }
        public virtual Guid ClienteId { get; set; }


    }
}
