using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consultorio.Response
{
    public class CitaClientResponse
    {
        public int CitaId { get; set; }
        public string Descripcion { get; set; }
        public string Fecha { get; set; }
        public bool Realizada { get; set; }
    }
}
