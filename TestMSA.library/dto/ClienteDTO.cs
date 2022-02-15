using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMSA.library.domain
{
    public class ClienteDTO
    {
        public virtual String Nome { get; set; }
        public virtual String Email { get; set; }
        public virtual DateTime DataNascimento { get; set; }
        public virtual Guid Id { get; set; }
    }
}
