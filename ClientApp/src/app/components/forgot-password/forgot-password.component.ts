import { Component, OnInit } from '@angular/core';
import { LoginServiceService } from '../../services/login-service.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {

  username:string
  otp:any
  userOTP:number
  //Declaration END
  constructor(private loginService:LoginServiceService) { }

  ngOnInit() {
  }

  getOTP(){
    this.loginService.getOTP(this.username).subscribe(res=>{
      this.otp=res
    })
  }

  checkOTP(){
    if(this.userOTP==this.otp)
      console.log("success")
  }

  changePassword(){
    let ob:{
      username:string,
      password:string
    }
    this.loginService.changePass(ob).subscribe(res=>{
      if(res==1)
        console.log("password changed");
    })
  }

}
