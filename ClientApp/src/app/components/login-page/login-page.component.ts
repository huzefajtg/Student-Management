import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/map'; 
import { LoginServiceService } from '../../services/login-service.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {
  user={
    username:'',
    password:''
  };
  usernameIsValid:any;
  userRedirect:any={};

  constructor(
              private LoginService:LoginServiceService)
              { }
  
  ngOnInit() {

    this.user.username="huzefajtg";
    this.user.password="admin"
    this.submit();
  }

  checkUsername(){
    this.LoginService.CheckUsername(this.user.username).subscribe(
      res=>{
        this.usernameIsValid=res;
        if(this.usernameIsValid!=true)
          alert("Username "+ this.user.username +" does not exist ")
      }
    )

  }


  submit(){
    this.LoginService.CheckUser(this.user).subscribe(
      res=>{
        if(res!=null){
          this.userRedirect=res;
          console.log("user details from server ",this.userRedirect);
          if(this.userRedirect[0].userType=="S")
            console.log("Student")
            else
            if(this.userRedirect[0].userType=='T')
            console.log("Teacher")
        }
        else alert("wrong user");
      }
    )
  }

}
