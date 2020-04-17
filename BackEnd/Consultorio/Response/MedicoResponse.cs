using Consultorio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consultorio.Response
{
    public class MedicoResponse
    {
        public int MedicoId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public EspecialidadResponse Especialidad { get; set; }
        public List<CitaClientResponse> Citas { get; set; }
    }
}
