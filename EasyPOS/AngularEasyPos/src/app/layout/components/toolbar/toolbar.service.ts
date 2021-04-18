import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { WarehouseDto } from "app/main/products/product.model";
import { BehaviorSubject } from "rxjs";

@Injectable()
export class ToolbarService {
    baseUrl: string;
    authToken: string;
    routeParams: any;
    onToolbarChanged: BehaviorSubject<any>;

    constructor(private _httpClient: HttpClient, private router: Router){
        this.onToolbarChanged = new BehaviorSubject({});
        this.checkSession();
    }

    public getWarehouseList(filter: string){
        var url = `${this.baseUrl}/warehouse-list${
            filter != `` ? `?filter=${filter}` : ``
        }`;
        const promise = this._httpClient
            .get<WarehouseDto[]>(url, this.getHttpOptions())
            .toPromise();
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
        this.baseUrl = `${localStorage.getItem("baseUrl")}/api/app/warehouse`;
        this.authToken = localStorage.getItem("token");

        if (this.authToken == null || this.baseUrl == null) {
            this.router.navigate(["/login"]);
        }
    }
}