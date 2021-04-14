import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AppSettings } from "../../app-settings/app-settings.model";
import { AppSettingsService } from "../../app-settings/app-settings.service";
import { LoginInput } from "./login.input";
import { LoginModel } from "./login.model";

@Injectable()
export class LoginService{

    baseUrl: string;
    settings: AppSettings;
    /**
     * Constructor
     *
     * @param {HttpClient} _httpClient
     */
    constructor(private _httpClient: HttpClient, private _appSettingService: AppSettingsService) {
        //TODO: cleanUp here
        _appSettingService.getSettings().then(response => {
            this.settings = response;
            this.baseUrl = `${this.settings.API_URL}/connect/token`;
        });
        
    }

    public authorize(data: LoginInput): Promise<any> {
        var body = {
            'client_id': this.settings.CLIENT_ID,
            'client_secret': this.settings.CLIENT_SECRET,
            'grant_type': this.settings.GRANT_TYPE,
            'username': data.username,
            'password': data.password
        }
        var httpOptions = {
            headers: new HttpHeaders({
                "Content-Type": "application/x-www-form-urlencoded",
            }),
        };

        const promise = this._httpClient.post<LoginModel>(this.baseUrl, body, httpOptions).toPromise();

        return promise.then(response => 
        {
            localStorage.setItem("token", response.access_token)
        });
    }
}