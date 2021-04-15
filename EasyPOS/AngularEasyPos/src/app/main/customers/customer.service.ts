import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import {
    ActivatedRouteSnapshot,
    Resolve,
    RouterStateSnapshot,
} from "@angular/router";
import { BehaviorSubject, Observable } from "rxjs";
import { CreateUpdateCustomerDto, CustomerDto } from "./customer.model";
import { AppSettingsService } from "../../app-settings/app-settings.service"

@Injectable()
export class CustomerService implements Resolve<any> {
    baseUrl: string;
    authToken: string;
    routeParams: any;
    customer: any;
    onCustomerChanged: BehaviorSubject<any>;

    /**
     * Constructor
     *
     * @param {HttpClient} _httpClient
     */
    constructor(private _httpClient: HttpClient, private _appSettingService: AppSettingsService) {
        // Set the defaults
        this.onCustomerChanged = new BehaviorSubject({});
        this.baseUrl = `${localStorage.getItem('baseUrl')}/api/app/customer`;
        this.authToken = localStorage.getItem('token');
        
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
        const promise = this._httpClient.post<CustomerDto>(this.baseUrl, data, this.getHttpOptions()).toPromise();
        return promise;
    }

    public update(customerId: string, data: CreateUpdateCustomerDto): Promise<any> {
        var url = `${this.baseUrl}/${customerId}`
        const promise = this._httpClient.put<CustomerDto>(url, data, this.getHttpOptions()).toPromise();
        return promise;
    } 
    
    public delete(customerId: string): Promise<any> {
        var url = `${this.baseUrl}/${customerId}`
        const promise = this._httpClient.delete<CustomerDto>(url, this.getHttpOptions()).toPromise();
        return promise;
    }

    public get(customerId: string): Promise<any> {
        var url = `${this.baseUrl}/${customerId}`
        const promise = this._httpClient.get<CustomerDto>(url, this.getHttpOptions()).toPromise();
        return promise;
    }
    
    public getList(filter: string): Promise<any> {
        var url = `${this.baseUrl}/customer-list${filter != `` ? `?filter=${filter}` : ``}`;
        const promise = this._httpClient.get<CustomerDto[]>(url, this.getHttpOptions()).toPromise();
        return promise;
    }

    private getHttpOptions(){
        return {
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                Authorization: this.authToken,
            }),
        };
    }
}
