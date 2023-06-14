import { IFunc1 } from '~/util';
import { Endpoint as api } from '../helpers';
import axios from 'axios';

export class UserService {
    public static login: IFunc1<any, Promise<any>> = (object) => {
        return axios.post(`${api.userLogin}`, object).then((result) => result.data);
    };
}

export default UserService;
