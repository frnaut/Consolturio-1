using Consultorio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consultorio.Request
{
    public class CitaRequest
    {
        public string Descripcion { get; set; }
        public string Fecha { get; set; }
        public bool Realizada { get; set; }
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
    }
}
