import { catchError } from 'rxjs/operators';
import { Film, Genre } from './models/film.model';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class FilmService {

  constructor(private http: HttpClient) { }

  getFilmSuggestion(): Observable<Film> {
    const url = '/api/Film';
    return this.http.get<Film>(url, httpOptions)
      .pipe(catchError(this.handleError));
  }

  getGenres(): Observable<Genre> {
    const url = '/api/Film/GetGenres';
    return this.http.get<Genre>(url, httpOptions)
      .pipe(catchError(this.handleError));
  }

  getFilmSuggestionBasedOnGenre(genres: string[]): Observable<Film> {
    const url = '/api/Film/GetFilmSuggestionBasedOnGenre/' + `${genres}`;
    return this.http.get<Film>(url, httpOptions)
      .pipe(catchError(this.handleError));
  }

  handleError(error: any) {
    const errorMessage = (error.message) ? error.message : error.status ? `${error.status} - ${error.statusText}` : 'Server error';
    alert('Something went wrong. Please try again.');
    return throwError(error);
  }
}
