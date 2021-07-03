import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import {
    ActivatedRouteSnapshot,
    Resolve,
    RouterStateSnapshot,
} from "@angular/router";
import { BehaviorSubject, Observable } from "rxjs";
import { ProductDto } from "./product.model";
import { Router } from "@angular/router";
import { ConfigurationDto } from "../configurations/configuration.model";

@Injectable()
export class ProductService implements Resolve<any> {
    baseUrl: string;
    authToken: string;
    routeParams: any;
    onProductChanged: BehaviorSubject<any>;

    /**
     * Constructor
     *
     * @param {HttpClient} _httpClient
     */
    constructor(private _httpClient: HttpClient, private router: Router) {
        // Set the defaults
        this.onProductChanged = new BehaviorSubject({});
        this.checkSession();
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
            Promise.all([this.getProduct()]).then(() => {
                resolve();
            }, reject);
        });
    }

    /**
     * Get product
     *
     * @returns {Promise<any>}
     */
    getProduct(): Promise<any> {
        return new Promise((resolve, reject) => {
            var data = {
                Id: this.routeParams.id,
                Type: "view",
            };
            this.onProductChanged.next(data);
            resolve(data);
        });
    }

    public getProductLookup(): Promise<any> {
        const promise = this._httpClient
            .get<ProductDto[]>(this.baseUrl, this.getHttpOptions())
            .toPromise();
        return promise;
    }

    public get(productId: string): Promise<any> {
        var warehouseId = localStorage.getItem('warehouseId');
        var url = `${this.baseUrl}product/${productId}/product/${warehouseId}`;
        const promise = this._httpClient
            .get<ProductDto>(url, this.getHttpOptions())
            .toPromise();
        return promise;
    }

    public getList(filter: string): Promise<any> {
        var warehouseId = localStorage.getItem('warehouseId');
        var url = `${this.baseUrl}product/product-list/${warehouseId}${
            filter != `` ? `?filter=${filter}` : ``
        }`;
        const promise = this._httpClient
            .get<ProductDto[]>(url, this.getHttpOptions())
            .toPromise();
        return promise;
    }

    public getListByWarehouse(wareHouseId: string): Promise<any> {
        var url = `${this.baseUrl}product/product-list-by-warehouse/${wareHouseId}`;
        const promise = this._httpClient
            .get<ProductDto[]>(url, this.getHttpOptions())
            .toPromise();
        return promise;
    }
    
    public getConfList(filter: string): Promise<any> {
        var url = `${this.baseUrl}configuration-manager/config-list${filter != `` ? `?filter=${filter}` : ``}`;
        const promise = this._httpClient.get<ConfigurationDto[]>(url, this.getHttpOptions()).toPromise();
        return promise;
    }

    private getHttpOptions() {
        return {
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                Authorization: this.authToken,
            }),
        };
    }

    private checkSession() {
        this.baseUrl = `${localStorage.getItem("baseUrl")}/api/app/`;
        this.authToken = localStorage.getItem("token");

        if (this.authToken == null || this.baseUrl == null) {
            this.router.navigate(["/login"]);
        }
    }
}
