using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consultorio.Models
{
    public class Paciente
    {
        public int PacienteId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Identificacion { get; set; }
        public List<Cita> Citas { get; set; }
    }
}
