import { Component, OnInit } from '@angular/core';
import { ConsultorioService } from 'src/app/services/consultorio.service';
import { EspecialidadModel } from 'src/app/Models/EspecialidadModel';
import { ActivatedRoute } from '@angular/router';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-especialidad',
  templateUrl: './especialidad.component.html',
  styleUrls: ['./especialidad.component.css']
})
export class EspecialidadComponent implements OnInit {

  createForm(){
    return new FormGroup({
      nombre: new FormControl(''),
      descripcion: new FormControl('')
    })
  }

  editForm: FormGroup;
  especialidad: EspecialidadModel;
  editarForm: boolean;
  alert: boolean;
  id;

  constructor(private _consultorio: ConsultorioService, private _router:ActivatedRoute ) {
   
    this.editForm = this.createForm();
    this.alert = false;
    this.editarForm = false;
    this._router.params.subscribe(data => this.id = data.id)
    this.getEspecialidad()

   }

  ngOnInit() {
    
  }

  getEspecialidad()
  {
    this._consultorio.getEspecialidad(this.id)
                  .subscribe( (data: EspecialidadModel) => this.especialidad = data)
  }

  PutEspecialidad()
  {
    if (this.editForm.valid)
    {
      try {
        this._consultorio.putEspecialidad(this.id, this.editForm.value)
                                          .subscribe();
        
        this.alert = true;
  
        setTimeout(()=>{
          this.getEspecialidad()
        },100)
  
        setTimeout(() => {
          this.alert = false;
        }, 3000)

      } catch (error) {
        console.log(error)
      }
    }
  }
}
