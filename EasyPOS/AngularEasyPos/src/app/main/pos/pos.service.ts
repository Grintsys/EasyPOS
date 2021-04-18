import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from "@angular/router";
import { BehaviorSubject, Observable } from "rxjs";
import { ProductDto } from "../products/product.model";

@Injectable()
export class PosService {
    baseUrl: string;
    authToken: string;
    routeParams: any;
    onPosChanged: BehaviorSubject<any>;

    constructor(private _httpClient: HttpClient, private router: Router){
        this.onPosChanged = new BehaviorSubject({});
        this.checkSession();
    }

    public getProductList(filter: string): Promise<any> {
        var url = `${this.baseUrl}/product-list${
            filter != `` ? `?filter=${filter}` : ``
        }`;
        const promise = this._httpClient
            .get<ProductDto[]>(url, this.getHttpOptions())
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
        this.baseUrl = `${localStorage.getItem("baseUrl")}/api/app/product`;
        this.authToken = localStorage.getItem("token");

        if (this.authToken == null || this.baseUrl == null) {
            this.router.navigate(["/login"]);
        }
    }
}