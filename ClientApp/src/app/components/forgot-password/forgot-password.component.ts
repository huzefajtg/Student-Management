import { User } from './../../models/models';
import { Component, OnInit } from '@angular/core';
import { LoginServiceService } from '../../services/login-service.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {

  u:User={
    username:'',
    password:''
  }

  otp:number
  userOTP:number
  //Declaration END
  constructor(private loginService:LoginServiceService) { }

  ngOnInit() {
  }

  getOTP(){
    console.log("u",this.u)
    this.loginService.getOTP(this.u).subscribe(res=>{
      this.otp=+res
    })
  }

  checkOTP(){
    if(this.userOTP==this.otp)
      console.log("success")
  }

  changePassword(){
    this.loginService.changePass(this.u).subscribe(res=>{
      if(res==1)
        console.log("password changed");
    })
  }

}
