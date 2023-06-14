import { IFunc1 } from '~/util';
import { Endpoint as api } from '../helpers';
import axios from 'axios';

export class PostService {
    public static getPost: IFunc1<any, Promise<any>> = (object) => {
        const query: string = `?id=${object.id}`;
        return axios.get(`${api.getpost}${query}`).then((result) => result.data);
    };
}

export default PostService;
