import { Component, OnInit } from '@angular/core';
import { ConsultorioService } from '../../services/consultorio.service';
import { PacienteModel } from 'src/app/Models/PacienteModel';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-pacientes',
  templateUrl: './pacientes.component.html',
  styleUrls: ['./pacientes.component.css']
})
export class PacientesComponent implements OnInit {

  createForm(){
    return new FormGroup({
      nombre: new FormControl(''),
      apellido: new FormControl(''),
      identificacion: new FormControl('')
    })
  }

  postForm: FormGroup;

  formPaciente: boolean;
  pacientes: any;
  newPaciente;
  creado: boolean;

  constructor( private _consultorio: ConsultorioService, private router: Router ) { 

    this.postForm = this.createForm();

    this.creado = false;
    this.newPaciente = new PacienteModel();
    this.formPaciente = false;
    this.getPacientes();
  }

  ngOnInit() {
  }

  getPacientes()
  {
    this._consultorio.getPacientes().subscribe(data => {
      this.pacientes = data;
    })

  }

  postPaciente()
  {
    console.log(this.postForm.value)
    if (this.postForm.valid)
    {
      try {
        this._consultorio.postPaciente(this.postForm.value).subscribe();

        this.creado = true;
        setTimeout(() => {
          this.creado = false;
        }, 3000);
    
        setTimeout(() => {
          this.getPacientes();
        }, 100);
    
        this.formPaciente = false;
      } catch (error) {
        console.log(error)
      }
    }
  }

  verPaciente(id)
  {
    this.router.navigateByUrl(`paciente/${id}`)
  }

  deletePaciente(id)
  {
    var result = confirm("Desea eliminar este elemento?")

    if (result)
    {
      this._consultorio.deletePaciente(id).subscribe()

      alert("El elemento a sido elimindo")

      setTimeout(() => {
        this.getPacientes();
      }, 100);

    }
  }
}
