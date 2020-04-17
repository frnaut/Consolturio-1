using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Consultorio.Models;
using Microsoft.AspNetCore.Identity;

namespace Consultorio.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        #region DbSets
        public DbSet<Cita> Citas { get; set; }
        //public DbSet<CitaMedicoPaciente> CitaMedicoPacientes { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }

        #endregion

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Especialidad
            builder.Entity<Especialidad>().Property(x => x.Nombre)
                                          .HasMaxLength(50)
                                          .IsRequired();
            #endregion

            #region Paciente
            builder.Entity<Paciente>().Property(x => x.Nombre).HasMaxLength(50)
                                      .IsRequired();

            builder.Entity<Paciente>().Property(x => x.Identificacion ).HasMaxLength(15)
                                      .IsRequired();

            #endregion

            #region Medico
            builder.Entity<Medico>().Property(x => x.Nombre)
                                    .IsRequired();

            builder.Entity<Medico>().Property(x => x.EspecialidadId)
                                   .IsRequired();

            builder.Entity<Medico>().Property(x => x.Identificacion )
                                   .IsRequired();
            #endregion

            #region Cita
            builder.Entity<Cita>().Property(x => x.Realizada).HasDefaultValue(false);

            builder.Entity<Cita>().Property(x => x.MedicoId)
                                  .IsRequired();
            builder.Entity<Cita>().Property(x => x.PacienteId)
                                  .IsRequired();
            builder.Entity<Cita>().Property(x => x.Fecha )
                                  .IsRequired();
            #endregion

            var rolAdmin = new IdentityRole()
            {
                Id = "e76fb594-494d-468c-87c8-5159eca35aef",
                Name = "admin",
                NormalizedName = "admin"
            };

            builder.Entity<IdentityRole>().HasData(rolAdmin);

            base.OnModelCreating(builder);
        }



    }
}
