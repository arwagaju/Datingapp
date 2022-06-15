import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/Services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  // login property for data binding
  model: any = {};
  
  cancelReg:boolean;
  

  //boolean that tracks if a user has been registered
  isRegistered: boolean;

  constructor(private router: Router, private accountService: AccountService) { }

  ngOnInit(): void {
  }

  //Register method
  Register(){

    this.accountService.Register(this.model).subscribe(response =>{
      console.log(response)
    },
    error => {
      this.isRegistered = false;
      console.log(error)
    },
    () => {
      this.isRegistered = true;
      console.log(this.isRegistered);

    })
    console.log(this.isRegistered)

    
  }

  //cancel method
  Cancel(){
    this.cancelReg = !this.cancelReg;

  }

}
