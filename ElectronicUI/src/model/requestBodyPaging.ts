
type requestBodyPaging = {
    page: number,
    top: number,
    skip: number,
    searchText?: string,
    searchByColumn?:[],
    orderBy: orderByRequest,
    filter: null,
    additionalFilters: null,
};

interface orderByRequest {
    orderByDesc: boolean,
    orderByKeyWord: string,
}

export default requestBodyPaging;