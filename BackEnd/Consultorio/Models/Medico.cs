using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consultorio.Models
{
    public class Medico
    {
        public int MedicoId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Identificacion { get; set; }
        public int EspecialidadId { get; set; }
        public Especialidad Especialidad { get; set; }
        public List<Cita> Citas { get; set; }
    }
}
