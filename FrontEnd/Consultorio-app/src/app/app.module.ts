import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { NavbarComponent } from './components/shered/navbar/navbar.component';
import { CitasComponent } from './components/citas/citas.component';
import { PacientesComponent } from './components/pacientes/pacientes.component';
import { MedicosComponent } from './components/medicos/medicos.component';
import { EspecialidadesComponent } from '../app/components/especialidades/especialidades.component';
import { EspecialidadComponent } from './components/especialidad/especialidad.component';
import { PacienteComponent } from './components/paciente/paciente.component';
import { MedicoComponent } from './components/medico/medico.component';
import { ReactiveFormsModule } from '@angular/forms';
import { CitaComponent } from './components/cita/cita.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    NavbarComponent,
    CitasComponent,
    PacientesComponent,
    MedicosComponent,
    EspecialidadesComponent,
    EspecialidadComponent,
    PacienteComponent,
    MedicoComponent,
    CitaComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
