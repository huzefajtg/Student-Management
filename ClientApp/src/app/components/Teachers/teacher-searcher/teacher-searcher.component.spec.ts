import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TeacherSearcherComponent } from './teacher-searcher.component';

describe('TeacherSearcherComponent', () => {
  let component: TeacherSearcherComponent;
  let fixture: ComponentFixture<TeacherSearcherComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TeacherSearcherComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TeacherSearcherComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
