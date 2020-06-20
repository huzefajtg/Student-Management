import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TeacherServiceService } from '../../../../services/teacher-services.services';

@Component({
  selector: 'app-view-student',
  templateUrl: './view-student.component.html',
  styleUrls: ['./view-student.component.css']
})
export class ViewStudentComponent implements OnInit {

  //Decalratio Start
  studentId:number;
  teacherId:number;

  //Decalration End
  constructor(private route: ActivatedRoute,
    private router: Router,
    private teacherService: TeacherServiceService
  ) {
    route.params.subscribe(p => {
      this.studentId = +p['id'];
      this.teacherId = +p['id2'];
      console.log("parameters " + this.teacherId+" "+this.studentId)
      if (isNaN(this.studentId) || this.studentId <= 0 || isNaN(this.teacherId) || this.teacherId <= 0) {
        console.log("parameter issue " + this.teacherId)
        //router.navigate(['/login']);
        return;
      }
    });

  }

  ngOnInit() {
  }

}
