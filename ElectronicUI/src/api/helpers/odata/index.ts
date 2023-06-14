import { ODataSelector } from '~/model';

class ODataQueryBuilder {
    static toQuery(selector: ODataSelector): string {
        var query: string = '';
        if (selector.filter) {
            query += 'filter=' + encodeURIComponent(selector.filter) + '&';
        }
        if (selector.orderby) {
            query += 'filter=' + encodeURIComponent(selector.orderby) + '&';
        }
        if (selector.search) {
            query += 'filter=' + encodeURIComponent(selector.search) + '&';
        }
        if (selector.top) {
            query += 'filter=' + encodeURIComponent(selector.top) + '&';
        }
        if (selector.skip) {
            query += 'filter=' + encodeURIComponent(selector.skip) + '&';
        }
        if (selector.distinct) {
            query += 'filter=' + encodeURIComponent(selector.distinct) + '&';
        }
        if (query) {
            query = '?' + query;
        }
        return query;
    }
}

export default ODataQueryBuilder;
