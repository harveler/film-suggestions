import { Film, Genres } from './models/film.model';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { HttpHeaders, HttpClient, HttpParams } from '@angular/common/http';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class FilmService {

  constructor(private http: HttpClient) { }

  getMovieSuggestion(): Observable<Film> {
    const url = '/api/Film';
    return this.http.get<Film>(url, httpOptions)
    .pipe(catchError(this.handleError));
  }

  getGenres(): Observable<Genres> {
    const url = '/api/Film/Genres';
    return this.http.get<Genres>(url, httpOptions)
    .pipe(catchError(this.handleError));
  }

  getFilmBasedOnGenre(genres: string[]): Observable<Film> {
    const url = '/api/Film/GetFilmBasedOnGenre/' + `${genres}`;
    return this.http.get<Film>(url, httpOptions)
    .pipe(catchError(this.handleError));
  }

  private handleError(error: any) {
    const errorMessage = (error.message) ? error.message : error.status ? `${error.status} - ${error.statusText}` : 'Server error';
    alert('Something went wrong. Please try again.');
    return throwError(error);
  }
}
