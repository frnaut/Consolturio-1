import { Component, OnInit } from '@angular/core';
import { ConsultorioService } from '../../services/consultorio.service';
import { NgForm } from '@angular/forms'
import { UserModel } from '../../Models/UserModel'; 
import { TokenModel } from 'src/app/Models/TokenModel';
import { Router } from '@angular/router';
import { timeout } from 'rxjs/operators';
import { error } from 'util';



@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  user: UserModel;
  errores: boolean;

  constructor( private _consultorio: ConsultorioService, private _router: Router ) { 
    this.user = new UserModel;
    this.errores = false;

  }

  ngOnInit() {
  }

  login( form: NgForm )
  { 
  
    if (form.valid)
    {
      this._consultorio.login(this.user).subscribe((data: TokenModel) => {
        console.log(data)

        localStorage.setItem('token', data.token);
        localStorage.setItem('expiration', data.expiration);
    
        if(data != null)
        {
          this._router.navigateByUrl('home');
          
        }

    })

    } else {

      this.errores = true;
      setTimeout(() => this.errores = false, 3000);
    }

  }

  borrar()
  {
    this.user.email = '';
    this.user.password = '';
  }

}
