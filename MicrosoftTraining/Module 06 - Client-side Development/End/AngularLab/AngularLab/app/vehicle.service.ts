import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Vehicle } from './vehicle';

import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/observable/throw';


@Injectable()
export class VehicleService {
    private _url = '/home/loadvehicles';

    constructor(private _http: Http) { }

    getVehicles() {
        return this._http.get(this._url)
            .map((response: Response) => <Vehicle[]>response.json())
            .do(data => console.log(data))
            .catch(this.handleError);
    }

    private extractData(res: Response) {
        return <Vehicle[]>res.json();
    }

    private handleError(error: Response) {
        console.error(error);
        let msg = `Error status code ${error.status} at ${error.url}`;
        return Observable.throw(msg);
    }

}
