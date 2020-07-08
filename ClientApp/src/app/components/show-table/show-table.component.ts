import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { TeacherServiceService } from '../../services/teacher-services.services';
import { StudentService } from '../../services/student.services';

interface SorterType{
  name:string,
  isAsc:boolean
}

@Component({
  selector: 'teacher-Searcher',
  templateUrl: './show-table.component.html',
  styleUrls: ['./show-table.component.css']
})
export class ShowTableComponent implements OnInit {
  //Declarations
  @Input() id:number
  @Input() iswhere:number
  
  @Input() TableList: any=[]
  @Input() teacherMode: boolean = false

  searchVal: string;
  isSort: string = 'id'

  sortObj: SorterType = {
    name: '',
    isAsc: true
  }

  //End Of Declarations

  constructor(
    private router: Router,
    private teacherService: TeacherServiceService,
    private StudentService: StudentService) {}

  ngOnInit() {

    // this.StudentService.getTeachersList()
    //   .subscribe(res => {
    //     this.TableList = res;
    //     this.tmpTableList = res;
    //     console.log("List :", this.TableList)

    //     console.log("Sorter Value ", this.isSort)
    //   });


    this.isSort = 'id'
    this.searchVal = ''
  }


  sorter(name: string) {
    console.log("in sort ", name)
    console.log("isSort ", this.isSort)
    if (this.sortObj.name != name) {
      this.sortObj.isAsc = false
      this.sortObj.name = name
    }
    else
      this.sortObj.isAsc = !this.sortObj.isAsc

    if (name == 'id') {
      if (this.sortObj.isAsc)
        this.TableList.sort((a, b) => (a.studentId > b.studentId) ? 1 : -1)
      else
        this.TableList.sort((a, b) => (a.studentId < b.studentId) ? 1 : -1)
    }

    if (name == 'fname') {
      if (this.sortObj.isAsc)
        this.TableList.sort((a, b) => (a.personalInfo.firstName > b.personalInfo.firstName) ? 1 : -1)
      else
        this.TableList.sort((a, b) => (a.personalInfo.firstName < b.personalInfo.firstName) ? 1 : -1)
    }

  }


  RowSelected(rowID: number) {
    console.log("ID selected: " + rowID)

    if (this.teacherMode == false)
      this.router.navigateByUrl('/student/viewer/' + rowID + '/' + this.id + '/S/'+this.iswhere);
    else
      this.router.navigateByUrl('/student/viewer/' + rowID + '/' + this.id + '/T/'+this.iswhere);
  }




}
