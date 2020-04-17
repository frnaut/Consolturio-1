using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consultorio.Models
{
    public class Cita
    {
        public int CitaId { get; set; }
        public string Descripcion { get; set; }
        public string Fecha { get; set; }
        public bool Realizada { get; set; }

        public int MedicoId { get; set; }
        public Medico Medico { get; set; }

        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
       
    }
}
