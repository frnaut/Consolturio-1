import { Component, OnInit } from '@angular/core';
import { ConsultorioService } from '../../services/consultorio.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  citas: any;

  constructor( private _consultorio: ConsultorioService,
               private router: Router ) { 
    this.getCitas();
    
    
  }

  ngOnInit() {
    
  }

  getCitas()
  {
     return this._consultorio.getCitas().subscribe(data => {
      this.citas = data;
    })
  }

  verCita(id: number){
    this.router.navigateByUrl(`cita/${id}`)
  }

}
