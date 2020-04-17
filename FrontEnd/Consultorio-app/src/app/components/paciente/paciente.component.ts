import { Component, OnInit } from '@angular/core';
import { PacienteModel } from 'src/app/Models/PacienteModel';
import { ConsultorioService } from 'src/app/services/consultorio.service';
import { ActivatedRoute } from '@angular/router';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-paciente',
  templateUrl: './paciente.component.html',
  styleUrls: ['./paciente.component.css']
})
export class PacienteComponent implements OnInit {

  createForm(){
    return new FormGroup({
      nombre: new FormControl(''),
      apellido: new FormControl(''),
      identificacion: new FormControl('')
    })
  }

  editeForm: FormGroup;

  paciente: PacienteModel;
  formEditar: boolean;
  edited: boolean;

  constructor(private _consultorio: ConsultorioService,
              private router: ActivatedRoute) {

    this.editeForm = this.createForm();
    this.formEditar = false;
    this.edited = false;

    this.getPaciente();
    


  }

  ngOnInit() {
  }

  getPaciente()
  {
    var id; 
    this.router.params.subscribe(data => id = data.id);

    this._consultorio.getPaciente(id).subscribe( (data: PacienteModel) => {
      this.paciente = data;
      console.log(this.paciente)
    })
  }

  putPaciente()
  {
    console.log(this.editeForm.value)
    if(this.editeForm.valid)
    {
      try {
          var id;
          this.router.params.subscribe(data => id = data.id)

          this._consultorio.putPaciente(id,this.editeForm.value).subscribe();
          this.edited = true;

          setTimeout(() => {
            this.edited = false;
          }, 3000);

          setTimeout(() => {
            this.getPaciente()
          }, 100);

      } catch (error) {
        console.log(error)
      }

    }
    
  }

}
