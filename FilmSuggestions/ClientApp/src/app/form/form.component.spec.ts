import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { By } from '@angular/platform-browser';
import { FilmService } from './../film.service';
import { FormComponent } from './form.component';
import { Genre, Film } from '../models/film.model';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { of } from 'rxjs';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

const genresData: Genre[] = [
  {
    id: '12',
    name: 'Drama'
  },
  {
    id: '13',
    name: 'Comedy'
  }
];

const movieData: Film = {
  id: '12345',
  title: 'Rambo',
  overview: 'Movie about Rambo.',
  year: 1994,
  genres: [
    {
      id: '12',
      name: 'Drama',
    },
    {
      id: '13',
      name: 'Comedy',
    }
  ]
};

describe('FormComponent', () => {
  let component: FormComponent;
  let fixture: ComponentFixture<FormComponent>;
  let filmService: FilmService;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [FormComponent],
      imports: [
        HttpClientTestingModule,
        ReactiveFormsModule,
        MatButtonModule,
        BrowserAnimationsModule,
        MatFormFieldModule,
        FormsModule,
        MatSelectModule,
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FormComponent);
    component = fixture.componentInstance;
    filmService = TestBed.get(FilmService);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should get genres when created', () => {
    // arrange
    const spy = spyOn(component, 'ngOnInit').and.callThrough();
    const spyService = spyOn(filmService, 'getGenres').and.callThrough();

    // act
    fixture.detectChanges();

    // assert
    expect(spy).toHaveBeenCalled();
    expect(spyService).toHaveBeenCalled();
    expect(component.genres$).not.toBeNull();
  });

  it('should call service with getFilmSuggestion if get movie button is clicked', () => {
    // arrange
    const spy = spyOn(filmService, 'getFilmSuggestion').and.callThrough();
    const button = fixture.debugElement.query(By.css('#random-movie')).nativeElement;

    // act
    button.click();
    fixture.detectChanges();

    // assert
    expect(spy).toHaveBeenCalled();
    expect(component.film$).not.toBeNull();
  });

  it('should display movie details when received film object from server', () => {
    // arrange
    spyOn(filmService, 'getFilmSuggestion').and.returnValue(of(movieData));
    const button = fixture.debugElement.query(By.css('#random-movie')).nativeElement;

    // act
    button.click();
    fixture.detectChanges();

    const year = fixture.debugElement.query(By.css('.year'));
    const title = fixture.debugElement.query(By.css('.title'));
    const firstGenre = fixture.debugElement.query(By.css('li'));
    const secondGenre = fixture.debugElement.queryAll(By.css('li'));
    const overview = fixture.debugElement.query(By.css('.overview'));

    // assert
    expect(year.nativeElement.innerHTML).toEqual('1994');
    expect(title.nativeElement.innerHTML).toEqual('Rambo');
    expect(firstGenre.nativeElement.innerHTML).toEqual('Drama');
    expect(secondGenre[1].nativeElement.innerHTML).toEqual('Comedy');
    expect(overview.nativeElement.innerHTML).toEqual('Movie about Rambo.');
  });

  it('should call service with getFilmSuggestionBasedOnGenre with genre ids as parameters and return movie', () => {
    // arrange
    const spy = spyOn(filmService, 'getFilmSuggestionBasedOnGenre')
      .withArgs('12').and.callThrough().and.returnValue(of(movieData))
      .withArgs('13').and.callThrough().and.returnValue(of(movieData));
    component.selectedGenres = genresData;
    const button = fixture.debugElement.query(By.css('#random-movie-with-genre')).nativeElement;

    // act
    button.click();
    fixture.detectChanges();

    // assert
    expect(spy).toHaveBeenCalled();
    expect(component.film$).not.toBeNull();
  });

  it('should call service with getFilmSuggestion if no genres specified and return movie', () => {
    // arrange
    const spy = spyOn(filmService, 'getFilmSuggestionBasedOnGenre');
    const spyNoGenre = spyOn(filmService, 'getFilmSuggestion').and.returnValue(of(movieData));
    component.selectedGenres = [];
    const button = fixture.debugElement.query(By.css('#random-movie-with-genre')).nativeElement;

    // act
    button.click();
    fixture.detectChanges();

    // assert
    expect(spy).not.toHaveBeenCalled();
    expect(spyNoGenre).toHaveBeenCalled();
    expect(component.film$).not.toBeNull();
  });
});
