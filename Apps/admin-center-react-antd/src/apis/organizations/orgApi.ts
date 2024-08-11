import { del, get, post, put } from "../apiClient";
import { ResponseData } from "../response";

interface OrgCreateCommand {
    name: string;
    code: string;
    description: string;
    superiorId: string | null;
};


interface OrgInfo {
    id: string;
    name: string;
    description: string;
    code: string;
    superiorId: string | null;
};

interface OrgWithChildrenInfo {
    id: string;
    name: string;
    description: string;
    code: string;
    superiorId: string | null;
    children: OrgWithChildrenInfo[]
};


interface OrgUpdateCommand {
    id: string;
    name: string;
    code: string;
    description: string;
    superiorId: string | null;
    status: number;
};
interface OrgList {
    items: OrgInfo[]
    pageNumber: number,
    totalPages: number,
    totalCount: number,

}
interface OrgListWithChildren {
    items: OrgWithChildrenInfo[]
    pageNumber: number,
    totalPages: number,
    totalCount: number,

}


//角色添加
export const OrgCreateApi = async (data: OrgCreateCommand): Promise<ResponseData<boolean>> => {
    try {
        const response = await post<ResponseData<boolean>>("/api/v1/Organizations", data);
        return response;
    } catch (error) {
        throw error;
    }
}

//角色修改
export const OrgUpdateApi = async (data: OrgUpdateCommand, orgId: string): Promise<ResponseData<boolean>> => {
    try {
        const response = await put<ResponseData<boolean>>("/api/v1/Organizations/" + orgId, data);
        return response;
    } catch (error) {
        throw error;
    }
}


//角色删除

export const OrgDeleteApi = async (roleId: string): Promise<ResponseData<boolean>> => {

    try {
        const response = await del<ResponseData<boolean>>("/api/v1/Organizations/" + roleId);
        return response;
    } catch (error) {
        throw error;
    }
}

//组织分页查询
export const OrgListWithPaginationBySuperiorIdApi = async (pageNumber: number, pageSize: number, SuperiorId: string | null): Promise<ResponseData<OrgList>> => {

    try {
        const response = await get<ResponseData<OrgList>>(`/api/v1/Organizations/WithPaginationBySuperiorId?PageNumber=${pageNumber}&PageSize=${pageSize} ${SuperiorId == null ? "" : "&SuperiorId=" + SuperiorId}`);
        return response;
    } catch (error) {
        throw error;
    }
}
//组织查询
export const OrgListBySuperiorIdApi = async (SuperiorId: string | null): Promise<ResponseData<OrgInfo[]>> => {

    try {
        const response = await get<ResponseData<OrgInfo[]>>(`/api/v1/Organizations ${SuperiorId == null ? "" : "?SuperiorId=" + SuperiorId}`);
        return response;
    } catch (error) {
        throw error;
    }
}

//组织及其下级查询
export const OrgListWithChildrenApi = async (): Promise<ResponseData<OrgWithChildrenInfo[]>> => {

    try {
        const response = await get<ResponseData<OrgWithChildrenInfo[]>>(`/api/v1/Organizations/WithChildren`);
        return response;
    } catch (error) {
        throw error;
    }
}


export const OrgListWithPaginationAndChildrenApi = async (pageNumber: number, pageSize: number): Promise<ResponseData<OrgListWithChildren>> => {

    try {
        const response = await get<ResponseData<OrgListWithChildren>>(`/api/v1/Organizations/WithPaginationAndChildren?PageNumber=${pageNumber}&PageSize=${pageSize}`);
        return response;
    } catch (error) {
        throw error;
    }
}

