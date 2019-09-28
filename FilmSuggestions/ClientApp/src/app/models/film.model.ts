export class Film {
    id: string;
    title: string;
    overview: string;
    year: number;
    genres: Genre[];
}

export class Genre {
    id: string;
    name: string;
}
