// apiClient.ts
import axios, { AxiosRequestConfig, AxiosResponse, AxiosError, InternalAxiosRequestConfig, AxiosHeaders } from 'axios';

interface ApiConfig extends AxiosRequestConfig {
    // 可以在这里添加特定于 API 的配置
}

// 基础 URL 和其他配置
const instance = axios.create({
    baseURL: 'https://api.admin.hello-developer.cn',
    timeout: 10000,
    headers: { 'Content-Type': 'application/json' }
});


// 添加请求拦截器
instance.interceptors.request.use(
    (config: InternalAxiosRequestConfig<any>): InternalAxiosRequestConfig<any> => {
        // 在发送请求之前做一些事情，比如添加 token
        const token = localStorage.getItem('token');
        if (token) {
            // 确保 headers 存在
            if (!config.headers) {
                config.headers = {} as AxiosHeaders;
            }
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    (error: AxiosError): Promise<AxiosError> => {
        // 对请求错误做些什么
        return Promise.reject(error);
    }
);

// 添加响应拦截器
instance.interceptors.response.use(
    (response: AxiosResponse): AxiosResponse => {
        // 对响应数据做点什么
        return response;
    },
    (error: AxiosError): Promise<AxiosError> => {
        // 对响应错误做点什么
        if (error.response) {
            // The request was made and the server responded with a status code
            // that falls out of the range of 2xx
            console.error('Error response:', error.response.data);
        } else if (error.request) {
            // The request was made but no response was received
            console.error('No response received:', error.request);
        } else {
            // Something happened in setting up the request that triggered an Error
            console.error('Error setting up request:', error.message);
        }
        console.error('Error config:', error.config);
        return Promise.reject(error);
    }
);

export default instance;
