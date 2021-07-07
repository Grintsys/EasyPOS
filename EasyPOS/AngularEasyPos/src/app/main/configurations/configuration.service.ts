import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { Config } from 'protractor';
import { ConfigurationDto } from './configuration.model';

@Injectable()
export class ConfigurationService implements Resolve<any>
{
    baseUrl: string;
    authToken: string;
    
    routeParams: any;
    configuration: any;
    onConfigurationChanged: BehaviorSubject<any>;

    constructor(
        private _httpClient: HttpClient,
        private router: Router
    ) {
        this.onConfigurationChanged = new BehaviorSubject({});
        this.checkSession();
    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> | Promise<any> | any {
        this.routeParams = route.params;

        return new Promise<void>((resolve, reject) => {

            Promise.all([
                this.getConfiguration()
            ]).then(
                () => {
                    resolve();
                },
                reject
            );
        });
    }

    getConfiguration(): Promise<any> {
        return new Promise((resolve, reject) => {
            if (this.routeParams.id === 'new') {
                this.onConfigurationChanged.next('new');
                resolve(false);
            }
            else {
                this.onConfigurationChanged.next(this.routeParams.id);
                resolve(true);
            }
        });
    }

    public get(configId: string): Promise<any> {
        var url = `${this.baseUrl}/${configId}`
        const promise = this._httpClient.get<ConfigurationDto>(url, this.getHttpOptions()).toPromise();
        return promise;
    }

    public getList(filter: string): Promise<any> {
        var url = `${this.baseUrl}/config-list${filter != `` ? `?filter=${filter}` : ``}`;
        const promise = this._httpClient.get<ConfigurationDto[]>(url, this.getHttpOptions()).toPromise();
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
        this.baseUrl = `${localStorage.getItem("baseUrl")}/api/app/configuration-manager`;
        this.authToken = localStorage.getItem("id_token");

        if (this.authToken == null || this.baseUrl == null) {
            this.router.navigate(["/login"]);
        }
    }
}
