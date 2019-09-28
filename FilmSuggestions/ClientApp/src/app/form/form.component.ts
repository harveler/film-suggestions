import { Film, Genre } from './../models/film.model';
import { FilmService } from './../film.service';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {
  film$: Observable<Film> = null;
  genres$: Observable<Genre> = null;
  selectedGenres: Genre[] = null;

  constructor(private filmService: FilmService) { }

  ngOnInit() {
    this.genres$ = this.filmService.getGenres();
  }

  getMovie() {
    return this.film$ = this.filmService.getMovieSuggestion();
  }

  getMovieBasedOnGenre() {
    if (!this.selectedGenres || this.selectedGenres.length === 0) {
      return this.film$ = this.filmService.getMovieSuggestion();
    }
    const ids = this.getIds(this.selectedGenres);
    return this.film$ = this.filmService.getFilmSuggestionBasedOnGenre(ids);
  }

  getIds(genres: Genre[]): string[] {
    const ids = [];
    genres.forEach(genre => {
      ids.push(genre.id);
    });
    return ids;
  }
}
