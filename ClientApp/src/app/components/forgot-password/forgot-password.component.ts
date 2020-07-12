import { User } from './../../models/models';
import { Component, OnInit } from '@angular/core';
import { LoginServiceService } from '../../services/login-service.service';
import { Router } from '@angular/router';

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
  
  timer:boolean=true
  //if user manages to change username after readonly=true the username will not change
  user:User={
    username:'',
    password:''
  }
  btn1:boolean=true
  btn2:boolean=false
  btn3:boolean=false

  otp:number
  userOTP:number
  //Declaration END
  constructor(private loginService:LoginServiceService
    , private router: Router) { }

  ngOnInit() {
  }

  getOTP(){
    this.timer=false
    this.btn1=false
    console.log("u",this.u)
    this.loginService.getOTP(this.u).subscribe(res=>{
      this.otp=+res
      if(this.otp==0){
        this.timer=true
      alert("ERROR")
        this.btn1=true
    }
      else{
        this.timer=true
        this.btn2=!this.btn2
        this.user.username=this.u.username
      }
        console.log("otp= ",this.otp)
    })
  }

  checkOTP(){
    if(this.userOTP==this.otp){
      this.btn3=!this.btn3
      console.log("success",this.btn3)
      this.btn2=!this.btn2
    }
    else{
      alert("Wrong OTP Entered")
    }
  }

  changePassword(){
    this.user.password=this.u.password
    console.log("change PAssword",this.user)
    this.loginService.changePass(this.user).subscribe(res=>{
      this.router.navigate(['/']);
    })
  }

}
