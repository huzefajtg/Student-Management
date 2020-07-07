import { TeacherServiceService } from './../../../services/teacher-services.services';
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

interface Noti{
  notificationMessage:string,
  messageDate:string
}

@Component({
  selector: 'app-teacher-home',
  templateUrl: './teacher-home.component.html',
  styleUrls: ['./teacher-home.component.css']
})
export class TeacherHomeComponent implements OnInit {


  //Declaration Start
  id: number;
  tmp: boolean = false
  iswhere = 1

  notification: any = []

  //Declaration End


  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private tservice: TeacherServiceService) {
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
    this.tservice.getNotification(this.id).subscribe(res => {
      this.notification = res;
      console.log("Notifications ", this.notification)
    })
  }

  vChange(msgid: number) {
    console.log("msg id", msgid)
    this.tservice.changeViwed(msgid).subscribe(res => {
      console.log("result", res);
    })
  }

}
