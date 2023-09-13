import { Component } from '@angular/core';
import { AuthModel } from '../models/auth.model';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent {

  authForm = this.fb.group({
    email: ['', Validators.required],
    password: ['', Validators.required]
  });
  constructor (private fb: FormBuilder, private authService: AuthService){

  }

  login() {
    if (this.authForm.valid) {
      let value = this.authForm.value;
      let authModel: AuthModel = {email: value.email || '', password: value.password || ''}
      this.authService.login(authModel).subscribe((response) => {
          console.log(response);
      });
    }
  }
}
