import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { CitasComponent } from './components/citas/citas.component';
import { PacientesComponent } from './components/pacientes/pacientes.component';
import { MedicosComponent } from './components/medicos/medicos.component';
import { EspecialidadesComponent } from './components/especialidades/especialidades.component';
import { EspecialidadComponent } from './components/especialidad/especialidad.component';
import { PacienteComponent } from './components/paciente/paciente.component';
import { MedicoComponent } from './components/medico/medico.component';
import { CitaComponent } from './components/cita/cita.component';

import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard]},
  { path: 'citas', component: CitasComponent, canActivate: [AuthGuard]},
  { path: 'cita/:id', component: CitaComponent, canActivate: [AuthGuard] },
  { path: 'pacientes', component: PacientesComponent, canActivate: [AuthGuard]},
  { path: 'paciente/:id', component: PacienteComponent, canActivate: [AuthGuard] },
  { path: 'medicos', component: MedicosComponent, canActivate: [AuthGuard]},
  { path: 'medico/:id', component: MedicoComponent, canActivate: [AuthGuard]},
  { path: 'especialidades', component: EspecialidadesComponent, canActivate: [AuthGuard]},
  { path: 'especialidad/:id', component: EspecialidadComponent, canActivate: [AuthGuard]},
  { path: '*',   redirectTo: 'home', pathMatch: 'full' },
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
