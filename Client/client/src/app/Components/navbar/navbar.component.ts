import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/Services/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

 model: any = {};
 loggedIn: boolean;
 error: any;
 isRegistered = false;

  constructor(private accountService: AccountService) {
      
  }

  ngOnInit(): void {
    
  }

  login(){

    this.accountService.login(this.model).subscribe(response => {
      console.log(response) 
    },
    error => {
      this.error = error.error;
      console.log("error from response = " + this.error);
      if(this.error === "Invalid password"){
        this.loggedIn = false;
      }console.log(this.loggedIn);
      
    },
    () => {
      this.loggedIn = true;
      console.log(this.loggedIn);

    })
    console.log(this.loggedIn);

  }

  SignUp()
  {
    this.isRegistered = !this.isRegistered;
  }
  
 
}

