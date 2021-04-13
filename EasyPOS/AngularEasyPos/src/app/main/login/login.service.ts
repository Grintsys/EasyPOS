import { HttpClient, HttpHeaders } from "@angular/common/http";
import { AppSettings } from "../../app-settings/app-settings.model";
import { AppSettingsService } from "../../app-settings/app-settings.service";
import { LoginInput } from "./login.input";
import { LoginModel } from "./login.model";

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
        _appSettingService.getSettings().then(response => this.settings = response);
        this.baseUrl = `${this.settings.API_URL}/connect/token`;
    }

    public authorize(data: LoginInput): Promise<any> {
        var httpOptions = {
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                "client_id": this.settings.CLIENT_ID,
                "client_secret": this.settings.CLIENT_SECRET,
                "grant_type": this.settings.GRANT_TYPE,
            }),
        };

        const promise = this._httpClient.post<LoginModel>(this.baseUrl, data, httpOptions).toPromise();

        return promise.then(response => 
        {
            localStorage.setItem("token", response.access_token)
        });
    }
}