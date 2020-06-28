import { StudentService } from './../../../services/student.services';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-student-details',
  templateUrl: './student-details.component.html',
  styleUrls: ['./student-details.component.css']
})
export class StudentDetailsComponent implements OnInit {

  //Declaration Start
  iswhere=2
  id:number;

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
    
  }

  val: boolean = true
  isUpdateCourse: boolean = false;
  isUpdate: boolean = false;

  
  courses: any = []  //comes from server
  coursesSelect: any = [];
  courseId: number;
  subjectId: number;

  //Declaration End

  constructor(
    private route: ActivatedRoute, 
    private router: Router,
    private Stuservices:StudentService) {
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
    this.Stuservices.getStudent(this.id).subscribe(res=>{
      this.user=res;
      console.log("Student Information",this.user)
    })

  }




  canceled(){
    this.ngOnInit();
    this.isUpdate=false
    this.isUpdateCourse=false
  }

}
