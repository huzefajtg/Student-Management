import { StudentService } from './../../../services/student.services';
import { StudentResource } from './../../../models/models';
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
  id: number;
  tmpallStudents:any=[]
  allStudents: any
  firstList: Array<StudentResource> = []
  myStudent: boolean = false
  iswhere = 2

  searchVal:string;
  isSort:string='id'
  sortObj:SorterType={
    name:'',
    isAsc:true
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
        this.tmpallStudents=res;
        console.log("List :", this.allStudents)

      console.log("Sorter Value ",this.isSort)
    });
      this.isSort='id'
      this.searchVal=''
  }


  getMyStudents() {
    this.teacherService.getStudents({ teacherID: this.id, myStudents: false })
      .subscribe(res => {
        this.tmpallStudents=res;
        this.allStudents = res;
        console.log("for My students :", this.allStudents)
      });

      this.searchVal=''
      this.isSort='id'
  }

  searcher(){
    console.log("Function Entered ",this.searchVal)
    this.allStudents=this.tmpallStudents;

    if(this.searchVal !=null && this.searchVal != ""){

      if(Number.isInteger(Number(this.searchVal))){
        console.log("number Entered ")
        if(this.myStudent==true)
          this.allStudents = this.tmpallStudents.filter(ts => ts.studentId.toString().includes( this.searchVal ));
        else
          this.allStudents = this.tmpallStudents.filter(ts => ts.teacherId.toString().includes( this.searchVal ));
      }
      else{
        console.log("String Entered ")
        this.allStudents = this.tmpallStudents.filter(ts =>
          ts.personalInfo.firstName.toLowerCase().includes( this.searchVal));
      }
    }

  }

  sorter(name:string){
    console.log("in sort ",name)
    console.log("isSort ",this.isSort)
    if(this.sortObj.name != name){
      this.sortObj.isAsc=false
      this.sortObj.name=name
    }
    else
      this.sortObj.isAsc = !this.sortObj.isAsc

    if(name=='id'){
      if(this.sortObj.isAsc)
        this.allStudents.sort((a, b) => (a.studentId > b.studentId) ? 1 : -1)
        else
        this.allStudents.sort((a, b) => (a.studentId < b.studentId) ? 1 : -1)
    }

    if(name=='fname'){
      if(this.sortObj.isAsc)
        this.allStudents.sort((a, b) => (a.personalInfo.firstName > b.personalInfo.firstName) ? 1 : -1)
        else
        this.allStudents.sort((a, b) => (a.personalInfo.firstName < b.personalInfo.firstName) ? 1 : -1)
    }

  }



  RowSelected(rowID: number) {
    console.log("ID selected: " + rowID)

    if(this.myStudent==true)
    this.router.navigateByUrl('/student/viewer/' + rowID + '/' + this.id + '/S');
    else
    this.router.navigateByUrl('/student/viewer/' + rowID + '/' + this.id + '/T');
  }

}


