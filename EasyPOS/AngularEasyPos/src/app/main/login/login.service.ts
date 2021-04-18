import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AppSettings } from "../../app-settings/app-settings.model";
import { AppSettingsService } from "../../app-settings/app-settings.service";
import { LoginInput } from "./login.input";
import { LoginModel } from "./login.model";

@Injectable()
export class LoginService {
    baseUrl: string;
    settings: AppSettings;
    /**
     * Constructor
     *
     * @param {HttpClient} _httpClient
     */
    constructor(
        private _httpClient: HttpClient,
        private _appSettingService: AppSettingsService
    ) {
        //TODO: cleanUp here
        _appSettingService.getSettings().then((response) => {
            this.settings = response;
            this.baseUrl = `${this.settings.API_URL}/connect/token`;
        });
    }

    public authorize(input: LoginInput): Promise<any> {
        let params = new URLSearchParams();
        params.append("grant_type", this.settings.GRANT_TYPE);
        params.append("client_id", this.settings.CLIENT_ID);
        params.append("client_secret", this.settings.CLIENT_SECRET);
        //params.append('redirect_uri', this.redirectUri);
        params.append("username", input.username);
        params.append("password", input.password);

        var httpOptions = {
            headers: new HttpHeaders({
                "Content-Type":
                    "application/x-www-form-urlencoded; charset=utf-8",
            }),
        };
        const promise = this._httpClient
            .post<LoginModel>(this.baseUrl, params.toString(), httpOptions)
            .toPromise();

        return promise.then((response) => {
            localStorage.setItem("token", response.access_token);
            localStorage.setItem("baseUrl", this.settings.API_URL);
        });
    }
}
