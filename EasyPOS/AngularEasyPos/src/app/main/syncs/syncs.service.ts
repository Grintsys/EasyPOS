import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from "@angular/router";
import { BehaviorSubject, Observable } from "rxjs";
import { CreateUpdateSyncDto, SyncDto } from "./sync.model";

@Injectable()
export class SyncService implements Resolve<any> {
    baseUrl: string;
    authToken: string;
    routeParams: any;
    
    onSyncChanged: BehaviorSubject<any>;

    constructor(private _httpClient: HttpClient, private router: Router) {
        this.onSyncChanged = new BehaviorSubject({});
        this.checkSession();
    }

    resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any> | Promise<any> | any {
        this.routeParams = route.params;
        return new Promise<void>((resolve, reject) => {
            Promise.all([this.getSync()]).then(() => {
                resolve();
            }, reject);
        });
    }

    getSync(): Promise<any> {
        return new Promise((resolve, reject) => {
            var data = {
                Id: this.routeParams.id,
                Type: this.routeParams.handle,
            };
            this.onSyncChanged.next(data);
            resolve(data);
        });
    }

    public getList(filter: string): Promise<any> {
        var url = `${this.baseUrl}/sync-list${filter != `` ? `?filter=${filter}` : ``}`;
        const promise = this._httpClient.get<SyncDto[]>(url, this.getHttpOptions()).toPromise();
        return promise;
    }

    public update(id: string, data: CreateUpdateSyncDto): Promise<any> {
        var url = `${this.baseUrl}/${id}`
        const promise = this._httpClient.put<SyncDto>(url, data, this.getHttpOptions()).toPromise();
        return promise;
    }

    public retry(id: string): Promise<any> {
        var url = `${this.baseUrl}/${id}`
        const promise = this._httpClient.post<SyncDto>(url, this.getHttpOptions()).toPromise();
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

    private checkSession() {
        this.baseUrl = `${localStorage.getItem("baseUrl")}/api/app/sincronizador`;
        this.authToken = localStorage.getItem("token");

        if (this.authToken == null || this.baseUrl == null) {
            this.router.navigate(["/login"]);
        }
    }
}
