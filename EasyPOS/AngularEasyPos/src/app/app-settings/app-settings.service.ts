import { Injectable } from '@angular/core';
import 'rxjs/operators';

import { HttpClient } from '@angular/common/http';
import { exception } from 'console';
import { AppSettings } from './app-settings.model'

const SETTINGS_LOCATION = "../assets/appsettings.json";

@Injectable()
export class AppSettingsService {
    settings: AppSettings;
    
    constructor(private http: HttpClient) { }
    
    async getSettings(): Promise<AppSettings> {
        await this.http.get<AppSettings>(SETTINGS_LOCATION)
        .toPromise()
        .then(
            (settings) => {
                this.settings = settings;
            },
            (error) => {
                console.log("Promise rejected with " + JSON.stringify(error));
                throw new exception("Error")
            }
        );

        return this.settings; 
    }
}