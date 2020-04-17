using Consultorio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consultorio.Request
{
    public class MedicoRequest
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Identificacion { get; set; }
        public int EspecialidadId { get; set; }
    }
}
