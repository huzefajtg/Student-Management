import { Personal } from './../../../models/models';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TeacherServiceService } from '../../../services/teacher-services.services';
import { TeacherSearch } from '../../../models/models';

@Component({
  selector: 'app-viewer-page',
  templateUrl: './viewer-page.component.html',
  styleUrls: ['./viewer-page.component.css']
})
export class ViewerPageComponent implements OnInit {

  //Decalratio Start
  otherId: number;
  id: number;
  type: string
  type2: number

  studentInfo: any = {
    teacherID: 0,
    studentId: 0,
    personalInfo: {
      firstName: '',
      secondName: '',
      lastname: '',
      gender: '',
      contactAddress: '',
      contactNumber: '',
      dob: '',
      emailId: '',
      type: '',
      isReg: false
    },
    teacher: {
      personalInfo: {
        firstName: '',
        secondName: '',
        lastname: '',
        gender: '',
        contactAddress: '',
        contactNumber: '',
        dob: '',
        emailId: '',
        type: '',
        isReg: false
      },

      hod: {
        id: 0, name: ''
      },

      Course: {
        courseId: 0, courseName: ''
      },

      subjectInfo: {
        id: 0, name: ''
      },
      username: '',
      teacherId: 0,
      courseId: 0,
      isReg: false,
      isHod: false

    },

    course: {
      courseId: 0, courseName: ''
    },
    hod: {
      id: 0, name: ''
    },

    subjectInfo: {
      id: 0, name: ''
    },
  }

  iswhere = 2
  allStudents: any

  //Decalration End
  constructor(private route: ActivatedRoute,
    private router: Router,
    private teacherService: TeacherServiceService
  ) {
    route.params.subscribe(p => {
      this.otherId = +p['id'];
      this.id = +p['id2'];
      this.type = p['type'];
      this.type2 = +p['type2'];
      console.log("parameters " + this.id + " other ID " + this.otherId + " Type1 " 
        + this.type+" type2 "+this.type2)
      if (isNaN(this.otherId) || this.otherId <= 0 || isNaN(this.id) || this.id <= 0) {
        console.log("parameter issue " + this.id)
        //router.navigate(['/login']);
        return;
      }

      this.iswhere=this.type2
    });

  }

  ngOnInit() {
    if (this.type == 'S')
      this.teacherService.getStudentRecord(this.otherId).subscribe(res => {
        this.studentInfo = res;
        console.log("StudentInfo ", this.studentInfo)
      })

    else {
      this.teacherService.getTeacher(this.otherId).subscribe(res => {
        this.studentInfo = res;
        console.log("StudentInfo ", this.studentInfo)
      })
    }
    var q: TeacherSearch = {
      myStudents: true,
      teacherID: this.id
    }
    this.teacherService.getStudents(q).subscribe(res => {
      this.allStudents = res;
      console.log("MyStudent ", this.allStudents)
    })
  }

  ChangeRegister(){
    let ob:any={
      id:this.otherId,
      isReg:!this.studentInfo.personalInfo.isReg,
      otherId:this.id
    }
    console.log(ob);
    this.teacherService.registerStudent(ob).subscribe(res=>{
      console.log(res)
      this.studentInfo.personalInfo.isReg=!this.studentInfo.personalInfo.isReg
    });
  }
}
