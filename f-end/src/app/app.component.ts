import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'f-end';
  logged = false;
  user: any;

  constructor(private authService: AuthService){
      
  }

  ngOnInit(){
    this.logged = this.authService.isLoggedIn();
    this.user = this.authService.getUser();
  }
}
