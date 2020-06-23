import {TeacherSearch, StudentResource } from './../../../models/models';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TeacherServiceService } from '../../../services/teacher-services.services';

@Component({
  selector: 'app-teacher-searcher',
  templateUrl: './teacher-searcher.component.html',
  styleUrls: ['./teacher-searcher.component.css']
})
export class TeacherSearcherComponent implements OnInit {
  //Declarations
  id: number;
  allStudents: any
  firstList: Array<StudentResource> = []
  myStudent: boolean = false
  iswhere = 1



  //End Of Declarations





  constructor(private route: ActivatedRoute,
    private router: Router,
    private teacherService: TeacherServiceService
  ) {
    route.params.subscribe(p => {
      this.id = +p['id'];
      if (isNaN(this.id) || this.id <= 0) {
        console.log("parameter issue " + this.id)
        router.navigate(['/login']);
        return;
      }
    });

  }

  ngOnInit() {

    this.teacherService.getStudents({ teacherID: this.id, myStudents: false })
      .subscribe(res => {
        this.allStudents = res;
        console.log("List :", this.allStudents)


      });
  }


  getMyStudents() {
    this.teacherService.getStudents({ teacherID: this.id, myStudents: true })
      .subscribe(res => {
        this.allStudents = res;
        console.log("for My students :", this.allStudents)
      });

  }


  RowSelected(rowID: number) {
    console.log("ID selected: " + rowID)
    this.router.navigateByUrl('/teacher_home/viewStudent/' + rowID + '/' + this.id);
  }

}


















  //{
  //  student:{
  //    personalInfo:{
  //      firstName:'',
  //      secondName:'',
  //      lastname:'',
  //      isReg:true,
  
  //      gender:'',
  //      emailId:'',
  //      contactNumber:'',
  //      contactAddress:'',
  //      dob:'',
  //      type:''
  //    },
  //    isReg:false,
  //    studentID:0,
  //  },
  //  studentID:0,
  //  teacherID:0,
  //  teacher:{
  //    personalInfo:{
  //      firstName:'',
  //      secondName:'',
  //      lastname:'',
  //      isReg:true,
  
  //      gender:'',
  //      emailId:'',
  //      contactNumber:'',
  //      contactAddress:'',
  //      dob:'',
  //      type:''
  //    },
  
  //    hod:{
  //      name:'',
  //      id:0
  //    },
  
  //    subjectInfo:{
  //      name:'',
  //      id:0
  //    },
  
  //    username:'',
  //    teacherId:this.id,
  //    courseId:0,
  //    isHod:false,
  //    isReg:false
  //  }

  //}

