import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import {
    ActivatedRouteSnapshot,
    Resolve,
    RouterStateSnapshot,
} from "@angular/router";
import { BehaviorSubject, Observable } from "rxjs";

@Injectable()
export class OrderService implements Resolve<any> {
    baseUrl: string = '/api/app/order';
    routeParams: any;
    order: any;
    onOrderChanged: BehaviorSubject<any>;

    /**
     * Constructor
     *
     * @param {HttpClient} _httpClient
     */
    constructor(private _httpClient: HttpClient) {
        // Set the defaults
        this.onOrderChanged = new BehaviorSubject({});
    }

    /**
     * Resolver
     *
     * @param {ActivatedRouteSnapshot} route
     * @param {RouterStateSnapshot} state
     * @returns {Observable<any> | Promise<any> | any}
     */
    resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any> | Promise<any> | any {
        this.routeParams = route.params;

        return new Promise<void>((resolve, reject) => {
            Promise.all([this.getOrder()]).then(() => {
                resolve();
            }, reject);
        });
    }

    /**
     * Get Order
     *
     * @returns {Promise<any>}
     */
    getOrder(): Promise<any> {
        return new Promise((resolve, reject) => {
            if (this.routeParams.id === "detail") {
                this.onOrderChanged.next(false);
                resolve(false);
            } else {
                this.onOrderChanged.next(true);
                resolve(true);
            }
        });
    }

    private fetchData(){
        const promise = this._httpClient.get(this.baseUrl).toPromise();
    }
}
