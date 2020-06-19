import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TeacherServiceService } from '../../../services/teacher-services.services';

@Component({
  selector: 'app-teacher-searcher',
  templateUrl: './teacher-searcher.component.html',
  styleUrls: ['./teacher-searcher.component.css']
})
export class TeacherSearcherComponent implements OnInit {
  //Declarations
  id:number;



  //End Of Declarations





  constructor(private route: ActivatedRoute, private router: Router,
    private teacherService:TeacherServiceService
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
