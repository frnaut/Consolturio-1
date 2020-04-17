import { Component, OnInit } from '@angular/core';
import { ConsultorioService } from '../../services/consultorio.service';
import { Router } from '@angular/router';
import { MedicoModel } from 'src/app/Models/MedicoModel';
import { FormGroup, FormControl } from '@angular/forms';
import { EspecialidadModel } from 'src/app/Models/EspecialidadModel';
import { timeout } from 'rxjs/operators';
import { EspecialidadResponse } from 'src/app/Models/EspecialidadResponse';

@Component({
  selector: 'app-medicos',
  templateUrl: './medicos.component.html',
  styleUrls: ['./medicos.component.css']
})
export class MedicosComponent implements OnInit {

  creteForm(){
    return new FormGroup({
      nombre: new FormControl(''),
      apellido: new FormControl(''),
      identificacion: new FormControl(''),
      especialidadId: new FormControl('')
    })
  }

  postForm: FormGroup;

  formMedico: boolean;
  created: boolean;
  medicos: any;
  especialidades: EspecialidadResponse;

  constructor( private consultorio: ConsultorioService,
               private router: Router) { 
    
    this.getData()
    this.postForm = this.creteForm();

  }

  ngOnInit() {
  }

  getData(){
    this.consultorio.getMedicos().subscribe(data =>{
      this.medicos = data;
    });

    this.consultorio.getEspecialidades().subscribe((data: EspecialidadResponse) => {
      this.especialidades = data
    })

  }

  verMedico(id){
    var id;
    this.router.navigateByUrl(`medico/${id}`)
  }

  postMedico()
  {
    console.log(this.postForm.value)
    if (this.postForm.valid)
    {
      try {
        this.consultorio.postMedico(this.postForm.value).subscribe(resp => console.log(resp))
  
        setTimeout(() => {
          this.getData();
        }, 100);

        this.created = true;

        setTimeout(() => {
          this.created = false;
        }, 3000);

        this.formMedico = false;

      } catch (error) {
        
        console.log(error);
      }
    }
  }

  deleteMedico(id)
  {
    var result = confirm("Desea elimininar este elemento");
    if(result){
      this.consultorio.deleteMedico(id).subscribe()
      alert("elemento eliminado")

      setTimeout(() => {
        this.getData();
      }, 100);
    }



  }


}
