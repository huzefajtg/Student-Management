import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TeacherResource } from '../../../models/models';
import { TeacherServiceService } from '../../../services/teacher-services.services';
import { userInfo } from 'os';

@Component({
  selector: 'app-teacher-profile',
  templateUrl: './teacher-profile.component.html',
  styleUrls: ['./teacher-profile.component.css']
})
export class TeacherProfileComponent implements OnInit {

  tmp:boolean=false;
  id:number;
  val:boolean=true
  
  isUpdate:boolean=false;

  user:any={
    personalInfo:{
      firstName:'',
      secondName:'',
      lastname:'',
      isReg:true,

      gender:'',
      emailId:'',
      contactNumber:'',
      contactAddress:'',
      dob:'',
      type:''
    },

    hod:{
      name:'',
      id:0
    },

    subjectInfo:{
      name:'',
      id:0
    },

    username:'',
    teacherId:this.id,
    courseId:0,
    isHod:false,
    isReg:false
  }


  constructor(private route: ActivatedRoute, private router: Router,
              private teacherService:TeacherServiceService
    ) {
    route.params.subscribe(p => {
      this.id = +p['id'];
      if (isNaN(this.id) || this.id <= 0) {
        console.log("parameter issue "+this.id)
        router.navigate(['/login']);
        return;
      }
      
    }); 

   }

  ngOnInit() {
    this.teacherService.getTeacher(this.id).subscribe(res=>{
      this.user=res;
      console.log("res=>",res)
      console.log("userInfo=>",this.user)
    })

  }

  canceled(){
    this.isUpdate=!this.isUpdate
    this.ngOnInit();
  }

  submit(){
    this.teacherService.updateTeacher(this.id,this.user).subscribe(res=>{
      console.log("Responce from the service :",res)
    })
    this.router.navigate(['/teacher_home/'+this.id]);
  }

}
