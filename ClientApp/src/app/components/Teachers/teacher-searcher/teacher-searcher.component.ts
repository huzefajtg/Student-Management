import {TeacherSearch, StudentResource } from './../../../models/models';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TeacherServiceService } from '../../../services/teacher-services.services';


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
  tmpallStudents:any=[]
  allStudents: any
  firstList: Array<StudentResource> = []
  myStudent: boolean = false
  iswhere = 1

  searchVal:string;
  isSort:string=''
  sortObj:SorterType={
    name:'',
    isAsc:false
  }


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
        this.tmpallStudents=res;
        console.log("List :", this.allStudents)

      });
      this.isSort=''
      this.searchVal=''
  }


  getMyStudents() {
    this.teacherService.getStudents({ teacherID: this.id, myStudents: true })
      .subscribe(res => {
        this.tmpallStudents=res;
        this.allStudents = res;
        console.log("for My students :", this.allStudents)
      });

      this.searchVal=''
      this.isSort=''
  }

  searcher(){
    console.log("Function Entered ",this.searchVal)
    this.allStudents=this.tmpallStudents;

    if(this.searchVal !=null && this.searchVal != ""){

      if(Number.isInteger(Number(this.searchVal))){
        console.log("number Entered ")
        this.allStudents = this.tmpallStudents.filter(ts => ts.studentId.includes( this.searchVal ));
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

