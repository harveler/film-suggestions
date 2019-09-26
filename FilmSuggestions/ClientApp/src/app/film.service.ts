import { Film } from './models/film.model';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { HttpHeaders, HttpClient } from '@angular/common/http';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class FilmService {

  constructor(private http: HttpClient) { }

  getQuestions() {
    const url = '/api/Film';
    console.log("I'm triggered again!")
    return this.http.get(url, httpOptions);
  }

  private handleError(error: any) {
    const errorMessage = (error.message) ? error.message : error.status ? `${error.status} - ${error.statusText}` : 'Server error';
    return throwError(error);
  }
}
