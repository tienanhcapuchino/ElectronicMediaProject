/*********************************************************************
 * 
 * PROPRIETARY and CONFIDENTIAL
 * 
 * This is licensed from, and is trade secret of:
 * 
 *          Group 10 - PRN231 - SU23
 *          FPT University, Education and Training zone
 *          Hoa Lac Hi-tech Park, Km29, Thang Long Highway
 *          Ha Noi, Viet Nam
 *          
 * Refer to your License Agreement for restrictions on use,
 * duplication, or disclosure
 * 
 * RESTRICTED RIGHTS LEGEND
 * 
 * Use, duplication or disclosure is the
 * subject to restriction in Articles 736 and 738 of the 
 * 2005 Civil Code, the Intellectual Property Law and Decree 
 * No. 85/2011/ND-CP amending and supplementing a number of 
 * articles of Decree 100/ND-CP/2006 of the Government of Viet Nam
 * 
 * 
 * Copy right 2023 © PRN231 - SU23 - Group 10 ®. All Rights Reserved
 * 
 * Unpublished - All rights reserved under the copyright laws 
 * of the Government of Viet Nam
*********************************************************************/

import { createContext, useReducer } from 'react';
import axios from 'axios';
import Loading from '~/components/Loading';
import UserService from '../userService/indext';

// const isValidToken = (accessToken) => {
//     if (!accessToken) return false;

//     const decodedToken = jwtDecode(accessToken);
//     const currentTime = Date.now() / 1000;
//     return decodedToken.exp > currentTime;
// };
const initialState = {
    user: null,
    isInitialised: false,
    isAuthenticated: false,
};

const setSession = (accessToken: string) => {
    if (accessToken) {
        localStorage.setItem('accessToken', accessToken);
        axios.defaults.headers.common.Authorization = `Bearer ${accessToken}`;
    } else {
        localStorage.removeItem('accessToken');
        delete axios.defaults.headers.common.Authorization;
    }
};

const reducer = (state: any, action: any) => {
    switch (action.type) {
        case 'LOGIN': {
            const { user } = action.payload;
            return { ...state, isAuthenticated: true, user };
        }

        case 'LOGOUT': {
            return { ...state, isAuthenticated: false, user: null };
        }

        case 'REGISTER': {
            const { user } = action.payload;

            return { ...state, isAuthenticated: true, user };
        }

        default:
            return state;
    }
};

const AuthContext = createContext({
    method: 'JWT',
    login: (object: any) => {},
    logout: () => {},
    register: () => {},
});

export const AuthProvider = ({ children }: any) => {
    const [state, dispatch] = useReducer(reducer, initialState);

    const login = async (object: any) => {
        const response = await UserService.login(object);
        const { user } = response.data;
        dispatch({ type: 'LOGIN', payload: { user } });
    };

    const register = async (email: string, username: string, password: string) => {
        const response = await axios.post('/api/auth/register', { email, username, password });
        const { user } = response.data;

        dispatch({ type: 'REGISTER', payload: { user } });
    };

    const logout = () => {
        dispatch({ type: 'LOGOUT' });
    };

    // SHOW LOADER
    //if (state.isInitialised) return <Loading />;

    return (
        <AuthContext.Provider value={{ ...state, method: 'JWT', login, logout, register }}>
            {children}
        </AuthContext.Provider>
    );
};

export default AuthContext;
