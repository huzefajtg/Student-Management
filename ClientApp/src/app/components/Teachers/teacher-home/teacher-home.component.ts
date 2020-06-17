import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-teacher-home',
  templateUrl: './teacher-home.component.html',
  styleUrls: ['./teacher-home.component.css']
})
export class TeacherHomeComponent implements OnInit {

  id:number;
  tmp:boolean=false
   

  constructor(private route: ActivatedRoute, private router: Router,
    ) {
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
  }

}
