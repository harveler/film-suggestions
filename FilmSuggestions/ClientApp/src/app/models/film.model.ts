export class Film {
    id: string;
    title: string;
    overview: string;
    year: number;
    genres: Genres[];
}

export class Genres {
    id: string;
    name: string;
}
