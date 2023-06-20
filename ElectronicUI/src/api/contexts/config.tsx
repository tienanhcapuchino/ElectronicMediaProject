import axios from 'axios';

const instance = axios.create();

// Interceptor để gán token vào các yêu cầu Axios
instance.interceptors.request.use(
    (config) => {
        const accessToken = localStorage.getItem('accessToken');
        if (accessToken) {
            config.headers['Authorization'] = `Bearer ${accessToken}`;
        }
        return config;
    },
    (error) => {
        return Promise.reject(error);
    },
);

export default instance;
