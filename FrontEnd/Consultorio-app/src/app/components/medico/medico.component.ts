import { Component, OnInit } from '@angular/core';
import { ConsultorioService } from 'src/app/services/consultorio.service';
import { ActivatedRoute } from '@angular/router';
import { FormControl, FormGroup } from '@angular/forms';
import { MedicoRequest } from 'src/app/Models/MedicoRequest';
import { MedicoModel } from 'src/app/Models/MedicoModel';
import { EspecialidadModel } from 'src/app/Models/EspecialidadModel';
import { EspecialidadResponse } from 'src/app/Models/EspecialidadResponse';


@Component({
  selector: 'app-medico',
  templateUrl: './medico.component.html',
  styleUrls: ['./medico.component.css']
})
export class MedicoComponent {
  
  createdForm()
  {
    return new FormGroup({
      nombre: new FormControl(''),
      apellido: new FormControl(''),
      identificacion: new FormControl(''),
      especialidadId: new FormControl('')
    });
  }
  
  editeForm: FormGroup;
  id: number;
  especialidades: EspecialidadResponse;
  medico: any;
  form: false;
  edited = false;

  constructor(private consultorio: ConsultorioService,
              private router: ActivatedRoute) { 
                
    this.editeForm = this.createdForm();
    router.params.subscribe(resp => this.id = resp.id);
    this.getData();
 }

 getData()
 {
   this.consultorio.getMedico(this.id)
                    .subscribe((resp: any) =>{ 
                      this.medico = resp
                      console.log(this.medico)
                    });

   this.consultorio.getEspecialidades()
                    .subscribe((resp: EspecialidadResponse) => this.especialidades = resp)
 }



 sendData()
 {
   console.log(this.editeForm.value)
   if (this.editeForm.valid)
   {
     try {
      this.consultorio.putMedico(this.id,this.editeForm.value)
                       .subscribe();
                  
      setTimeout(() => {
        this.getData();
      }, 100);

      this.edited = true;
      setTimeout(() => {
        this.edited = false;
      }, 3000);
  
     } catch (error) {
      console.log(error)   
     }
     
    }
 }

}
