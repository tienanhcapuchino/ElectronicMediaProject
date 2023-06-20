import { Endpoint as api } from '~/api/helpers';
import axios from 'axios';
export const GET_POST_BY_ID = 'GET_POST_BY_ID';

export const getPostByID = (object: any) => async (dispatch: any) => {
    const query: string = `?id=${object.id}`;
    const res = await axios.get(`${api.getpost}${query}`).then((res) => {
        dispatch({
            type: GET_POST_BY_ID,
            payload: res.data,
        });
    });
};

export const getPostPagding = (object: any) => async (dispatch: any) => {
    return axios.post(`${api.getPostPagding}`, object).then((result) => result.data);
};
