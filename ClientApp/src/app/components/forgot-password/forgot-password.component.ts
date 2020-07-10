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

  btn1:boolean=true
  btn2:boolean=false
  btn3:boolean=false

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
      if(this.otp==0)
      alert("ERROR")
      else{
        this.btn2=!this.btn2
      }
        console.log("otp= ",this.otp)
    })
  }

  checkOTP(){
    if(this.userOTP==this.otp){
      this.btn3=!this.btn3
      console.log("success")
    }
    else{
      alert("Wrong OTP Entered")
    }
  }

  changePassword(){
    console.log("change PAssword",this.u)
    this.loginService.changePass(this.u).subscribe(res=>{
      if(res==1)
        console.log("password changed");
    })
  }

}
