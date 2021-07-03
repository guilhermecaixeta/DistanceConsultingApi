import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map, retry } from 'rxjs/operators';
import {Response} from 'src/app/models/Response.model';

@Injectable()
export class CraftableService {
    constructor(private http: HttpClient) { }

    craftableApi = 'https://localhost:5001/api/postcode';

    getAddress<T>(code: string) {
        return this.http.get<Response<T>>(`${this.craftableApi}/${code}`).pipe(
            retry(3),
            map(response => response.data));
    }

    getAllAddresses<T>() {
        return this.http.get<Response<T>>(`${this.craftableApi}`).pipe(
            retry(3),
            map(response => response.data));
    }
}