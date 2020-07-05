import { StudentService } from './../../../services/student.services';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-student-home',
  templateUrl: './student-home.component.html',
  styleUrls: ['./student-home.component.css']
})
export class StudentHomeComponent implements OnInit {

  //Declaration Start
  iswhere=2
  id:number;
  notification:any=[]
  //Declaration End

  constructor(
    private route: ActivatedRoute, 
    private router: Router,
    private sservice:StudentService) {
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
    this.sservice.getNotification(this.id).subscribe(res=>{
      this.notification=res;
      console.log("Notifications ",this.notification)
    })
  }

  vChange(msgid:number){
    console.log("msg id",msgid)
    this.sservice.changeViwed(msgid).subscribe(res=>{
      console.log("result",res);
    })
  }

}
