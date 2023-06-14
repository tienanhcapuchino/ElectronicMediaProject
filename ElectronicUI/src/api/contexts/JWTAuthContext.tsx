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
