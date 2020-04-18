import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { ConsultorioService } from '../services/consultorio.service';


@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private consultorio: ConsultorioService,
              private router: Router){}

  canActivate():boolean {
    if(this.consultorio.isLogin()){
      return true;
    }else{
      this.router.navigateByUrl('/login')
    }
  }
  
}
