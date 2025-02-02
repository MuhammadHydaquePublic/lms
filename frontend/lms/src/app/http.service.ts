import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Course } from './Models/Course';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private http: HttpClient) { }

  fetchCourses(): Observable<Course[]> {
    return this.http.get<Course[]>('http://localhost:8080');
  }
}