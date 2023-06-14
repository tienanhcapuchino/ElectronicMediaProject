export default class Endpoint {
    static get baseUrl(): string {
        if ((window as any).context && (window as any).context.api) {
            return `${(window as any).context.api}/api`;
        } else {
            return 'http://localhost:28586/api/';
        }
    }

    static get getpost(): string {
        return Endpoint.baseUrl + 'Post/id';
    }

    static get userLogin(): string {
        return Endpoint.baseUrl + 'User/login';
    }
}
