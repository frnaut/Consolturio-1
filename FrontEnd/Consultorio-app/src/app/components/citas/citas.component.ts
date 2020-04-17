import { Component, OnInit } from '@angular/core';
import { ConsultorioService } from '../../services/consultorio.service';
import { FormControl, FormGroup } from '@angular/forms';
import { ɵangular_packages_platform_browser_dynamic_testing_testing_a } from '@angular/platform-browser-dynamic/testing';
import { Router } from '@angular/router';

@Component({
  selector: 'app-citas',
  templateUrl: './citas.component.html',
  styleUrls: ['./citas.component.css']
})
export class CitasComponent implements OnInit {

  createForm (){
    return new FormGroup({
      pacienteId: new FormControl(''),
      medicoId: new FormControl(''),
      descripcion: new FormControl(''),
      fecha: new FormControl(''),
      realizada: new FormControl(false)
    });
  }
  postForm: FormGroup;

  nuevaCita: boolean;
  citas: any;
  medicos: any;
  pacientes: any;
  create: boolean;

  constructor(private _consultorio: ConsultorioService,
              private router: Router) { 

    this.postForm = this.createForm();
    this.nuevaCita = false;
    this.getData();

  }

  ngOnInit() {
  }

  getData(){
    this._consultorio.getCitas().subscribe( resp => this.citas = resp);
    this._consultorio.getMedicos().subscribe(resp => this.medicos = resp);
    this._consultorio.getPacientes().subscribe(resp => this.pacientes = resp)
    
  }
  verCita(id: number)
  {
    this.router.navigateByUrl(`cita/${id}`)
  }
  
  postCita()
  {
    
    console.log(this.postForm.valid)
    console.log(this.postForm.value)
    if (this.postForm.valid){
      try {
        
        this._consultorio.postCitas(this.postForm.value).subscribe()
        
        this.create = true;
        setTimeout(() => {
          this.create = false;
        }, 300);
        
        setTimeout(() => {
          this.getData();
        }, 100);

      } catch (error) {
        console.log(error)
      }
    }
  }

  deleteCita(id: number)
  {
    var result = confirm("Desea eliminar este elemento?");
    if (result)
    {
      this._consultorio.deleteCitas(id).subscribe();
      alert("Elemento eliminado")

      setTimeout(() => {
        this.getData();
      }, 200);
    }
  }

}
