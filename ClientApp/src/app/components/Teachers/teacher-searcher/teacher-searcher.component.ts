import { StudentResource } from './../../../models/models';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TeacherServiceService } from '../../../services/teacher-services.services';
import { StudentService } from './../../../services/student.services';


interface SorterType{
  name:string,
  isAsc:boolean
}
@Component({
  selector: 'app-teacher-searcher',
  templateUrl: './teacher-searcher.component.html',
  styleUrls: ['./teacher-searcher.component.css']
})

export class TeacherSearcherComponent implements OnInit {
  //Declarations
  id: number;
  tmpallStudents: any = []
  allStudents: any
  firstList: Array<StudentResource> = []
  myStudent: boolean = false
  isStudent: boolean = true
  iswhere = 1;

  searchVal: string;
  isSort: string = 'id'
  sortObj: SorterType = {
    name: '',
    isAsc: false
  }


  //End Of Declarations
  
  constructor(private route: ActivatedRoute,
    private router: Router,
    private StudentService: StudentService,
    private teacherService: TeacherServiceService,
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
        this.tmpallStudents = res;
        console.log("List :", this.allStudents)

      });
    this.isSort = ''
    this.searchVal = ''
  }


  getMyStudents() {
    this.teacherService.getStudents({ teacherID: this.id, myStudents: true })
      .subscribe(res => {
        this.tmpallStudents = res;
        this.allStudents = res;
        console.log("for My students :", this.allStudents)
      });

    this.searchVal = ''
    this.isSort = ''
  }

  getTeachers(){
    this.StudentService.getTeachersList()
      .subscribe(res => {
        this.allStudents = res;
        console.log("List :", this.allStudents)

        console.log("Sorter Value ", this.isSort)
      });
      
    this.searchVal = ''
    this.isSort = ''
  }

  //Not a part for the Table. It is a separate search bar
  searcher() {
    console.log("Function Entered ", this.searchVal)
    this.allStudents = this.tmpallStudents;

    if (this.searchVal != null && this.searchVal != "") {

      if (Number.isInteger(Number(this.searchVal))) {
        console.log("number Entered ")
        this.allStudents = this.tmpallStudents.filter(ts => ts.studentId.includes(this.searchVal));
      }
      else {
        console.log("String Entered ")
        this.allStudents = this.tmpallStudents.filter(ts =>
          ts.personalInfo.firstName.toLowerCase().includes(this.searchVal));
      }
    }

  }

}

