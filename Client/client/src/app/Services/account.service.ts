import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  //base url
  BaseUrl = 'https://localhost:5001/account/';

  constructor(private http: HttpClient) { }


  //login method
  login(model: any){
    return this.http.post(this.BaseUrl + "login", model)
  }

  //register method
  Register(model: any){
    return this.http.post(this.BaseUrl + "register", model)
  }

}
