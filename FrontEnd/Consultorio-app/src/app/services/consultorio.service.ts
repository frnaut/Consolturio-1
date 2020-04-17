import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserModel } from '../Models/UserModel';
import { map } from 'rxjs/operators';
import { EspecialidadModel } from '../Models/EspecialidadModel';
import { NgForm } from '@angular/forms';
import { PacienteModel } from '../Models/PacienteModel';
import { MedicoModel } from '../Models/MedicoModel';
import { MedicoRequest } from '../Models/MedicoRequest';
import { CitaModel } from '../Models/CitaModel';


@Injectable({
  providedIn: 'root'
})
export class ConsultorioService {

  URL = "https://localhost:5001"

  constructor( private http: HttpClient ) { 


  }
  login( user: UserModel  )
  {

    return this.http.post(`${this.URL}/api/cuentas/login`, user)
                      
  }


  //#region Citas
  getCitas()
  {
    return this.http.get(`${this.URL}/api/citas`);
    
  }

  getCita(id: number)
  {
    return this.http.get(`${this.URL}/api/citas/${id}`)
  }

  postCitas(cita)
  {
    var headers = new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
      'Authorization': 'Bearer' +' '+localStorage.getItem('token')
  
    });
    return this.http.post(`${this.URL}/api/citas`, cita,{headers});

  }
  putCita(id: number, cita)
  {
    var headers = new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
      'Authorization': 'Bearer' +' '+localStorage.getItem('token')
  
    });
    return this.http.put(`${this.URL}/api/citas/${id}`, cita, {headers})
  }
  deleteCitas(id: number){
    var headers = new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
      'Authorization': 'Bearer' +' '+localStorage.getItem('token')
  
    });
    return this.http.delete(`${this.URL}/api/citas/${id}`, {headers})
  }
  

  //#endregion

  //#region Medicos
  getMedicos()
  {
    return this.http.get(`${this.URL}/api/medicos`);
  }

  getMedico(id: number)
  {
    return this.http.get(`${this.URL}/api/medicos/${id}`)
  }

  postMedico(medico: MedicoModel)
  {
    var headers = new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
      'Authorization': 'Bearer' +' '+localStorage.getItem('token')
  
    });

    return this.http.post(`${this.URL}/api/medicos`, medico, {headers});
  }

  deleteMedico(id)
  {
    var headers = new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
      'Authorization': 'Bearer' +' '+localStorage.getItem('token')
  
    });

    return this.http.delete(`${this.URL}/api/medicos/${id}`, {headers})

  }

  putMedico(id, medico)
  {
    var headers = new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
      'Authorization': 'Bearer' +' '+localStorage.getItem('token')
  
    });

    return this.http.put(`${this.URL}/api/medicos/${id}`, medico, {headers})

  }
  //#endregion

  //#region Pacientes 
  getPacientes()
  {
    return this.http.get(`${this.URL}/api/paciente`);
  }

  getPaciente(id)
  {

    return this.http.get(`${this.URL}/api/paciente/${id}`);
  }

  postPaciente(paciente: PacienteModel)
  {
    var headers = new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
      'Authorization': 'Bearer' +' '+localStorage.getItem('token')
  
    })

    return this.http.post(`${this.URL}/api/paciente`, paciente, {headers})
  }

  deletePaciente(id)
  {
    var headers = new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
      'Authorization': 'Bearer' +' '+localStorage.getItem('token')
  
    });

    return this.http.delete(`${this.URL}/api/paciente/${id}`, {headers})

  }

  putPaciente(id, paciente: PacienteModel)
  {
    var headers = new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
      'Authorization': 'Bearer' +' '+localStorage.getItem('token')
  
    });

    return this.http.put(`${this.URL}/api/paciente/${id}`,paciente, {headers})
  }

  //#endregion

  //#region Especialidades
  getEspecialidades()
  {
    return this.http.get(`${this.URL}/api/especialidades`);
  }

  postEspecialidades(especialidad: EspecialidadModel){

    var headers = new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
      'Authorization': 'Bearer' +' '+localStorage.getItem('token')
  
    })

    return this.http.post(`${this.URL}/api/especialidades`, especialidad, { headers })
  }

  getEspecialidad(id)
  {
  
    return this.http.get(`${this.URL}/api/especialidades/${id}`);
    

  }

  putEspecialidad(id, especialidad: EspecialidadModel)
  {
    var headers = new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
      'Authorization': 'Bearer' +' '+localStorage.getItem('token')
  
    })

    return this.http.put(`${this.URL}/api/especialidades/${id}`, especialidad, {headers})
    
  }

  deleteEspecialidad(id)
  {
    var headers = new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
      'Authorization': 'Bearer' +' '+localStorage.getItem('token')
  
    })

    return this.http.delete(`${this.URL}/api/especialidades/${id}`, {headers})
  }

  //#endregion

  
}
