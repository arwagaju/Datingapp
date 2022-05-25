import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'client';
  users: any;
  //baseUrl = 'https://localhost:5001/';


  constructor(private httpService: HttpClient ){}

  ngOnInit(){

    this.getUsers();
    
  }


 

  getUsers(){
    this.httpService.get('https://localhost:5001/user').subscribe(
      response => {
        this.users = response;
      }, error => {
        console.log(error)
      })

  }
}
