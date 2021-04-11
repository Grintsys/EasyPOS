import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import {
    ActivatedRouteSnapshot,
    Resolve,
    RouterStateSnapshot,
} from "@angular/router";
import { BehaviorSubject, Observable } from "rxjs";
import { CreateUpdateCustomerDto, CustomerDto } from "./customer.model";

@Injectable()
export class CustomerService implements Resolve<any> {
    baseUrl: string = 'https://localhost:44339/api/app/customer';
    routeParams: any;
    customer: any;
    onCustomerChanged: BehaviorSubject<any>;

    httpOptions: any = {
        headers: new HttpHeaders({
            "Content-Type": "application/json",
            Authorization: "CfDJ8LgWl1xuO5pDiRiK3SLIzjX1aWU4lUFH6TwN9kKYs1B9_kdxVw3zuxEke3MgZX-Exqsq4Oz921JLEz4tCd6eCKb3coajpiAGBW7WMVIsJO03TV9Fx0CgNUYJuS9gTyauP1TnyQdJbUflzCsxVRTJdms",
        }),
    };
    /**
     * Constructor
     *
     * @param {HttpClient} _httpClient
     */
    constructor(private _httpClient: HttpClient) {
        // Set the defaults
        this.onCustomerChanged = new BehaviorSubject({});
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
            Promise.all([this.getcustomer()]).then(() => {
                resolve();
            }, reject);
        });
    }

    /**
     * Get customer
     *
     * @returns {Promise<any>}
     */
    getcustomer(): Promise<any> {
        return new Promise((resolve, reject) => {
            var data = {
                Id: this.routeParams.id,
                Type: this.routeParams.handle,
            };
            this.onCustomerChanged.next(data);
            resolve(data);
        });
    }

    public create(data: CreateUpdateCustomerDto): Promise<any> {
        const promise = this._httpClient.post<CustomerDto>(this.baseUrl, data, this.httpOptions).toPromise();
        return promise;
    }

    public update(customerId: string, data: CreateUpdateCustomerDto): Promise<any> {
        var url = `${this.baseUrl}/${customerId}`
        const promise = this._httpClient.put<CustomerDto>(url, data, this.httpOptions).toPromise();
        return promise;
    } 
    
    public delete(customerId: string): Promise<any> {
        var url = `${this.baseUrl}/${customerId}`
        const promise = this._httpClient.delete<CustomerDto>(url, this.httpOptions).toPromise();
        return promise;
    }

    public get(customerId: string): Promise<any> {
        var url = `${this.baseUrl}/${customerId}`
        const promise = this._httpClient.get<CustomerDto>(url, this.httpOptions).toPromise();
        return promise;
    }
    
    public getList(filter: string): Promise<any> {
        var url = `${this.baseUrl}/customer-list${filter != `` ? `?filter=${filter}` : ``}`;
        const promise = this._httpClient.get<CustomerDto[]>(url, this.httpOptions).toPromise();
        return promise;
    }
}
