import instance from "./httpClinet";


export const get = <T>(url: string, params?: any): Promise<T> => instance.get(url, { params }).then(res => res.data);

export const post = <T>(url: string, data?: any): Promise<T> => instance.post(url, data).then(res => res.data);

export const put = <T>(url: string, data?: any): Promise<T> => instance.put(url, data).then(res => res.data);

export const del = <T>(url: string, data?: any): Promise<T> => instance.delete(url, { data }).then(res => res.data);