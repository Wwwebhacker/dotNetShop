import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthModel } from '../models/auth.model';
import { Env } from '../env';
import { shareReplay, tap } from 'rxjs/operators';
import * as moment from 'moment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  login(authModel: AuthModel){
    return this.http.post<any>(Env.baseUrl + `/api/Auth/Login`, authModel)
    .pipe(
      tap(res =>this.setSession(res)),
      shareReplay()
    );
  }

  private setSession(authResult: {
    user: object; expires_at: any; token: string; 
  }){
    const expiresAt = moment(authResult.expires_at);
    localStorage.setItem('user', JSON.stringify(authResult.user));
    localStorage.setItem('token', authResult.token);
    localStorage.setItem("expires_at", JSON.stringify(expiresAt.valueOf()) );
  }

  logout() {
      localStorage.removeItem("user");
      localStorage.removeItem("token");
      localStorage.removeItem("expires_at");
  }

  public isLoggedIn() {
      return moment().isBefore(this.getExpiration());
  }

  getExpiration() {
      const expiration = localStorage.getItem("expires_at");
      if(!expiration){
        throw new Error('local storage is empty');
      }
      const expiresAt = JSON.parse(expiration);
      return moment(expiresAt);
  }   

  getUser(){
    return JSON.parse(localStorage.getItem('user') || '');
  }
}
