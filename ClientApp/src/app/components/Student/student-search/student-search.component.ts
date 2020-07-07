import { StudentService } from './../../../services/student.services';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TeacherServiceService } from '../../../services/teacher-services.services';

interface SorterType{
  name:string,
  isAsc:boolean
}

@Component({
  selector: 'app-student-search',
  templateUrl: './student-search.component.html',
  styleUrls: ['./student-search.component.css']
})

export class StudentSearchComponent implements OnInit {
  //Declarations
  isStudent:boolean=false//for Table Part

  id: number;
  tmpallStudents: any = []
  allStudents: any
  iswhere = 2

  searchVal: string;
  isSort: string = 'id'
  sortObj: SorterType = {
    name: '',
    isAsc: true
  }

  //End Of Declarations

  constructor(private route: ActivatedRoute,
    private router: Router,
    private teacherService: TeacherServiceService,
    private StudentService: StudentService
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

    this.StudentService.getTeachersList()
      .subscribe(res => {
        this.allStudents = res;
        this.tmpallStudents = res;
        console.log("List :", this.allStudents)

        console.log("Sorter Value ", this.isSort)
      });
    this.isSort = 'id'
    this.searchVal = ''
  }


  getisStudents() {
    this.teacherService.getStudents({ teacherID: this.id, myStudents: false })
      .subscribe(res => {
        this.tmpallStudents = res;
        this.allStudents = res;
        console.log("for My students :", this.allStudents)
      });

    this.searchVal = ''
    this.isSort = 'id'
  }

  searcher() {
    console.log("Function Entered ", this.searchVal)
    this.allStudents = this.tmpallStudents;

    if (this.searchVal != null && this.searchVal != "") {

      if (Number.isInteger(Number(this.searchVal))) {
        console.log("number Entered ")
        if (this.isStudent == true)
          this.allStudents = this.tmpallStudents.filter(ts => ts.studentId.toString().includes(this.searchVal));
        else
          this.allStudents = this.tmpallStudents.filter(ts => ts.teacherId.toString().includes(this.searchVal));
      }
      else {
        console.log("String Entered ")
        this.allStudents = this.tmpallStudents.filter(ts =>
          ts.personalInfo.firstName.toLowerCase().includes(this.searchVal));
      }
    }

  }


}


