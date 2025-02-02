import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Store } from '@ngrx/store';


import { HttpService } from './http.service';
import { Course } from './Models/Course';
import { resourceUsage } from 'process';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'lms';
  Courses : Course[] = [];
  constructor(private httpService: HttpService) {
    httpService.fetchCourses().subscribe((result) => {
      this.Courses = result as Course[];
      console.log(JSON.stringify(result));
    });
  }
}

