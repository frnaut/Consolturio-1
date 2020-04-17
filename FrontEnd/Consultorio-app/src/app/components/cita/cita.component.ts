import { Component, OnInit } from '@angular/core';
import { ConsultorioService } from 'src/app/services/consultorio.service';
import { ActivatedRoute } from '@angular/router';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-cita',
  templateUrl: './cita.component.html',
  styleUrls: ['./cita.component.css']
})
export class CitaComponent {
  newForm(){
    return new FormGroup({
      medicoId: new FormControl(''),
      pacienteId: new FormControl(''),
      fecha: new FormControl(''),
      descripcion: new FormControl('')
    })
  }

  editeForm: FormGroup;

  id: number;
  cita;
  medicos;
  pacientes;

  constructor( private consultorio: ConsultorioService,
               private router: ActivatedRoute ) {
    this.editeForm = this.newForm()
    this.router.params.subscribe(resp => this.id = resp.id);
    this.getData();
  }

  getData()
  {
    this.consultorio.getCita(this.id).subscribe(resp => this.cita = resp);
    this.consultorio.getMedicos().subscribe(resp => this.medicos = resp);
    this.consultorio.getPacientes().subscribe(resp =>{ 
      this.pacientes = resp
      console.log(this.pacientes)});
  }

  putCita(){
    console.log(this.editeForm.value)
    if (this.editeForm.valid)
    try {
      this.consultorio.putCita(this.id, this.editeForm.value).subscribe();
       
      setTimeout(()=>{
         this.getData();
      },200)

    } catch (error) {
      console.log(error)
    }
  }


}
