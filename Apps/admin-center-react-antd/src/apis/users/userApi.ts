import { get, post } from "../apiClient";
import { ResponseData } from "../response";

export interface UserInfo {
    id: string,
    loginName: string,
    nickName: string,
    realName: string,
    email: string,
    phoneNumber: string
}


interface Organization {
    isSubsidiary: boolean;
    organizationId: string;
};

interface UserCreateCommand {
    loginName: string;
    realName: string;
    password: string;
    nickName: string;
    email: string;
    phoneNumber: string;
    roleIds: Array<string>;
    organizationIds: Array<Organization>;
};

export interface UserWithPaginationList {
    items: UserInfo[]
    pageNumber: number,
    totalPages: number,
    totalCount: number
}

//角色添加
export const UserCreateApi = async (data: UserCreateCommand): Promise<ResponseData<boolean>> => {
    try {
        const response = await post<ResponseData<boolean>>("/api/v1/Users", data);
        return response;
    } catch (error) {
        throw error;
    }
}


//用户列表
export const UserListeApi = async (pageNumber: number, pageSize: number): Promise<ResponseData<UserWithPaginationList>> => {
    try {
        const response = await get<ResponseData<UserWithPaginationList>>(`/api/v1/Users?pageNumber=${pageNumber}&pageSize=${pageSize}`);
        return response;
    } catch (error) {
        throw error;
    }
}





