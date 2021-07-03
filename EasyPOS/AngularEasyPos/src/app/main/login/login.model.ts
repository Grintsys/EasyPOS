export class LoginModel {
    access_token: string
    expires_in: bigint
    token_type: string
    refresh_token: string
    scope: string
}

export class TenantModel {
    name: string;
    id: string;
}