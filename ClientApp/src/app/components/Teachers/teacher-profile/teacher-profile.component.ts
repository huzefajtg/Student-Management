import { TeacherResource } from './../../../models/models';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TeacherServiceService } from '../../../services/teacher-services.services';

@Component({
  selector: 'app-teacher-profile',
  templateUrl: './teacher-profile.component.html',
  styleUrls: ['./teacher-profile.component.css']
})
export class TeacherProfileComponent implements OnInit {

  tmp: boolean = false;
  id: number;
  val: boolean = true
  isUpdateCourse: boolean = false;
  isUpdate: boolean = false;

  user: any = {
    personalInfo: {
      firstName: '',
      secondName: '',
      lastname: '',
      isReg: true,

      gender: '',
      emailId: '',
      contactNumber: '',
      contactAddress: '',
      dob: '',
      type: ''
    },

    hod: {
      name: '',
      id: 0
    },

    subjectInfo: {
      name: '',
      id: 0
    },

    username: '',
    teacherId: this.id,
    courseId: 0,
    isHod: false,
    isReg: false
  }


  courses: any = []  //comes from server
  coursesSelect: any = [];
  courseId: number;
  subjectId: number;

  iswhere=1
  //Declaration End
  constructor(private route: ActivatedRoute, private router: Router,
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
    this.teacherService.getTeacher(this.id).subscribe(res => {
      this.user = res;
      console.log("res=>", res)
      console.log("userInfo=>", this.user)
    });
    this.getCourses();

  }

  canceled() {
    this.isUpdate = false
    this.isUpdateCourse = false
    this.ngOnInit();
  }

  getCourses() {
    this.teacherService.getCourses().subscribe(res => {
      res
      console.log("Response after courses call", res)
      this.courses = res;

    })
  }

  subName: string;
  OnSubjectChange() {
    this.coursesSelect = this.courses.find(c => c.subjectId == this.subjectId)
    this.subName = this.coursesSelect.subjectName
    console.log("coursesSelect:", this.coursesSelect)
  }

  submit() {
    if (this.isUpdateCourse) {
      console.log("courses Update")
      this.user.courseId = this.courseId
      this.user.subjectInfo.name = this.subName
      this.user.subjectInfo.id = this.subjectId
      this.user.personalInfo.isReg = false

    }


    this.teacherService.updateTeacher(this.id, this.user).subscribe(res => { })
    this.router.navigate(['/teacher_home/' + this.id]);


  }

}
