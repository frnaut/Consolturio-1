using Consultorio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consultorio.Response
{
    public class CitaResponse
    {
        public int CitaId { get; set; }
        public string Descripcion { get; set; }
        public string Fecha { get; set; }
        public bool Realizada { get; set; }
        public MedicoClientResponse Medico { get; set; }
        public PacienteClientResponse Paciente { get; set; }
    }
}
