import { Film, Genres } from './../models/film.model';
import { FilmService } from './../film.service';
import { Component, OnInit } from '@angular/core';
import { map } from 'rxjs/operators';
import {MatButtonModule} from '@angular/material/button';
import {MatSelectModule} from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { Observer, Observable } from 'rxjs';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {
  film$: Observable<Film>;
  genres$: Observable<Genres>;
  selectedGenres: Genres[];

  constructor(private filmService: FilmService) { }

  ngOnInit() {
    this.genres$ = this.filmService.getGenres();
  }

  getMovie() {
    return this.film$ = this.filmService.getMovieSuggestion();
  }

  getMovieBasedOnGenre() {
    const ids = this.getIds(this.selectedGenres);
    return this.film$ = this.filmService.getFilmBasedOnGenre(ids);
  }

  getIds(genres: Genres[]): string[] {
    const ids = [];
    genres.forEach(genre => {
      ids.push(genre.id);
    });
    return ids;
  }
}
