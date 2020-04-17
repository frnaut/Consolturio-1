import { Component, OnInit } from '@angular/core';
import { timeout } from 'rxjs/operators';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {


  constructor( private _router: Router ) { 
   
  }

  ngOnInit() {
  }

  cerrarSesion()
  {
    localStorage.removeItem('token');
    localStorage.removeItem('expiration')
    localStorage.removeItem('user')

    this._router.navigateByUrl('login');
  }

}
