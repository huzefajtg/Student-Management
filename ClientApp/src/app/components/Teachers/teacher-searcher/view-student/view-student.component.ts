import { TeacherStudentResource } from './../../../../models/models';

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TeacherServiceService } from '../../../../services/teacher-services.services';
import { isDefaultChangeDetectionStrategy } from '@angular/core/src/change_detection/constants';

@Component({
  selector: 'app-view-student',
  templateUrl: './view-student.component.html',
  styleUrls: ['./view-student.component.css']
})
export class ViewStudentComponent implements OnInit {

  //Decalratio Start
  studentId:number;
  id:number;

  studentInfo:any={
    teacherID:0,
    studentId:0,
    personalInfo:{
      firstName:'',
      secondName:'',
      lastname:'',
      gender:'',
      contactAddress:'',
      contactNumber:'',
      dob:'',
      emailId:'',
      type:'',
      isReg:false
    },
    teacher:{
      personalInfo:{
        firstName:'',
      secondName:'',
      lastname:'',
      gender:'',
      contactAddress:'',
      contactNumber:'',
      dob:'',
      emailId:'',
      type:'',
      isReg:false
      },

      hod:{
        id:0,name:''
      },

      Course:{
        courseId:0,courseName:''
      },

      subjectInfo:{
        id:0,name:''
      },
      username:'',
      teacherId:0,
      courseId:0,
      isReg:false,
      isHod:false

    }
  }

  iswhere=1


  //Decalration End
  constructor(private route: ActivatedRoute,
    private router: Router,
    private teacherService: TeacherServiceService
  ) {
    route.params.subscribe(p => {
      this.studentId = +p['id'];
      this.id = +p['id2'];
      console.log("parameters " + this.id+" "+this.studentId)
      if (isNaN(this.studentId) || this.studentId <= 0 || isNaN(this.id) || this.id <= 0) {
        console.log("parameter issue " + this.id)
        //router.navigate(['/login']);
        return;
      }
    });

  }

  ngOnInit() {
    this.teacherService.getStudentRecord(this.studentId).subscribe(res=>{
      this.studentInfo=res;
      console.log("StudentInfo ",this.studentInfo)
    })
  }


  regStudent(){
    let q={
      id:this.studentId,
      isReg:!this.studentInfo.personalInfo.isReg
    }
    this.teacherService.registerStudent(q).subscribe(res=>{
      console.log(res);
    })
    this.studentInfo.personalInfo.isReg=!this.studentInfo.personalInfo.isReg
  }

}
