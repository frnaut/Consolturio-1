import { Component, OnInit } from '@angular/core';
import { ConsultorioService } from '../../services/consultorio.service';
import { EspecialidadModel } from 'src/app/Models/EspecialidadModel';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { EspecialidadResponse } from 'src/app/Models/EspecialidadResponse';

@Component({
  selector: 'app-especialidades',
  templateUrl: './especialidades.component.html',
  styleUrls: ['./especialidades.component.css']
})
export class EspecialidadesComponent implements OnInit {

  createForm(){
    return new FormGroup({
      nombre: new FormControl(''),
      descripcion: new FormControl('')
    })
  }

  postForm: FormGroup;

  especialidades: any;
  especialidadForm: EspecialidadModel
  espeFormulario: boolean;
  crear: boolean;

  constructor( private _consulturio: ConsultorioService,
               private router: Router ) { 
    
    this.postForm = this.createForm();
    this.crear = false;
    this.espeFormulario = false;
    this.getEspecialidades();
    this.especialidadForm = new EspecialidadModel();

  }

  ngOnInit() {
  }

  getEspecialidades(){
    this._consulturio.getEspecialidades().subscribe(data => {
      this.especialidades = data;
    })
  }

  postEspecialidades(){

    console.log(this.postForm.value)
    if (this.postForm.valid)
    {
      try {
        this._consulturio.postEspecialidades(this.postForm.value).subscribe()

        this.crear = true
        this.espeFormulario = false;
        
        setTimeout(()=>{
          this.crear = false
        }, 3000);

        setTimeout(() => {
          this.getEspecialidades();
        }, 100)
      
      } catch (error) {
        console.log(error)
      }
    }

  }

  verEspecialidad(id)
  {
    
    this.router.navigateByUrl(`especialidad/${id}`)
  }


  deleteEspecialidad(id){

    var result = confirm("Esta seguro que desea realizar esta accion?");

    this._consulturio.deleteEspecialidad(id).subscribe(data =>{
      alert(`se ha eliminado la especialidad`)
    })
    
    setTimeout(() => {
      this.getEspecialidades();
    }, 500)

  }
  
  
}
