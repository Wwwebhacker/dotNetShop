import { Component } from '@angular/core';
import { AuthModel } from '../models/auth.model';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent {

  type: 'login' | 'register' = 'login';

  authForm = this.fb.group({
    email: ['', Validators.required],
    password: ['', Validators.required]
  });
  constructor (private fb: FormBuilder, private authService: AuthService, private router: Router){

  }

  submit() {
    if (this.authForm.valid) {
      let value = this.authForm.value;
      let authModel: AuthModel = {email: value.email || '', password: value.password || ''}
      switch (this.type) {
        case 'login':
          this.authService.login(authModel).subscribe((response) => {
            this.router.navigate(['products']);
          });
          break;
        case 'register':
          this.authService.register(authModel).subscribe((response) => {
            this.router.navigate(['products']);
          });
          break;
        default:
          break;
      }
      
    }
  }
  
}
