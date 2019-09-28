import { FilmService } from './film.service';
import { Film, Genre } from './models/film.model';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { HttpErrorResponse } from '@angular/common/http';
import { TestBed, fakeAsync, tick } from '@angular/core/testing';

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

describe('FilmService', () => {
  let filmService: FilmService;
  let httpMock: HttpTestingController;

  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientTestingModule,
    ],
    providers: [
      FilmService
    ]
  }));

  beforeEach(() => {
    filmService = TestBed.get(FilmService);
    httpMock = TestBed.get(HttpTestingController);
  });

  it('should be created', () => {
    const service: FilmService = TestBed.get(FilmService);
    expect(service).toBeTruthy();
  });

  it(
    'getFilmSuggestion should return film', fakeAsync(() => {

      // arrange
      const responseObject = movieData;
      let response = null;

      // act
      filmService.getFilmSuggestion().subscribe(
        (receivedResponse: any) => {
          response = receivedResponse;
        },
        (error: any) => { }
      );
      const requestWrapper = httpMock.expectOne(`/api/Film`, 'get api call');
      requestWrapper.flush(responseObject);
      tick();

      // assert
      expect(requestWrapper.request.method).toEqual('GET');
      expect(response).toBe(movieData);
    }
    ));

  it(
    'getGenres should return genres', fakeAsync(() => {

      // arrange
      const responseObject = genresData;
      let response = null;

      // act
      filmService.getGenres().subscribe(
        (receivedResponse: any) => {
          response = receivedResponse;
        },
        (error: any) => { }
      );
      const requestWrapper = httpMock.expectOne(`/api/Film/GetGenres`, 'get api call');
      requestWrapper.flush(responseObject);
      tick();

      // assert
      expect(requestWrapper.request.method).toEqual('GET');
      expect(response).toBe(genresData);
    }
    ));

  it(
    'GetFilmSuggestionBasedOnGenre should return film', fakeAsync(() => {

      // arrange
      const genres = ['12', '13'];
      const responseObject = movieData;
      let response = null;

      // act
      filmService.getFilmSuggestionBasedOnGenre(genres).subscribe(
        (receivedResponse: any) => {
          response = receivedResponse;
        },
        (error: any) => { }
      );
      const requestWrapper = httpMock.expectOne(`/api/Film/GetFilmSuggestionBasedOnGenre/${genres}`, 'get api call');
      requestWrapper.flush(responseObject);
      tick();

      // assert
      expect(requestWrapper.request.method).toEqual('GET');
      expect(response).toBe(movieData);
    }
    ));

    it(
      'GetFilmSuggestionBasedOnGenre should return null and error if no parameter passed', fakeAsync(() => {

        // arrange
        const genres = [];
        const responseObject = null;
        let response = null;

        // act
        filmService.getFilmSuggestionBasedOnGenre(genres).subscribe(
          (receivedResponse: any) => {
            response = receivedResponse;
          },
          (error: HttpErrorResponse) => {
            expect(filmService.handleError).toHaveBeenCalled();
           }
        );
        const requestWrapper = httpMock.expectOne(`/api/Film/GetFilmSuggestionBasedOnGenre/${genres}`, 'get api call');
        requestWrapper.flush(responseObject);
        tick();

        // assert
        expect(requestWrapper.request.method).toEqual('GET');
        expect(response).toBeNull();
      }
      ));
});
