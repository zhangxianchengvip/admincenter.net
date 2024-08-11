
import { get, post, put, del } from "../apiClient";
import { ResponseData } from "../response";


interface RoleAddCommand {
    name: string;
    order: number;
    description: string | null;
}

interface RoleUpdateCommand {
    id: string
    name: string
    order: number
    description: string | null
}

export interface RoleInfo {
    id: string
    name: string
    description: string | null
}

export interface RoleList {
    items: RoleInfo[]
    pageNumber: number,
    totalPages: number,
    totalCount: number,

}



//角色添加
export const RoleCreateApi = async (data: RoleAddCommand): Promise<ResponseData<RoleInfo>> => {
    try {
        const response = await post<ResponseData<RoleInfo>>("/api/v1/Roles", data);
        return response;
    } catch (error) {
        throw error;
    }
}

//角色修改
export const RoleUpdateApi = async (data: RoleUpdateCommand, roleId: string): Promise<ResponseData<RoleInfo>> => {
    try {
        const response = await put<ResponseData<RoleInfo>>("/api/v1/Roles/" + roleId, data);
        return response;
    } catch (error) {
        throw error;
    }
}


//角色删除

export const RoleDeleteApi = async (roleId: string): Promise<ResponseData<boolean>> => {

    try {
        const response = await del<ResponseData<boolean>>("/api/v1/Roles/" + roleId);
        return response;
    } catch (error) {
        throw error;
    }
}

//分页查询
export const RoleListWithPaginationApi = async (pageNumber: number, pageSize: number): Promise<ResponseData<RoleList>> => {

    try {
        const response = await get<ResponseData<RoleList>>(`/api/v1/Roles/WithPagination?PageNumber=${pageNumber}&PageSize=${pageSize}`);
        return response;
    } catch (error) {
        throw error;
    }
}


//全部角色
export const RoleListApi = async (): Promise<ResponseData<RoleInfo[]>> => {

    try {
        const response = await get<ResponseData<RoleInfo[]>>("/api/v1/Roles");
        return response;
    } catch (error) {
        throw error;
    }
}
