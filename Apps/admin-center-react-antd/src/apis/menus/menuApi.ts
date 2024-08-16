import { get, post } from "../apiClient";
import { ResponseData } from "../response";

interface MenuCreateCoommand {
  menuType: number;
  name: string;
  route: string;
  isLink: boolean;
  superiorId: string;
};


export interface MenuWithChildrenInfo {
  id: string;
  menuType: number;
  name: string;
  route: string;
  isLink: boolean;
  superiorId: string;
  children: MenuWithChildrenInfo[]
};


interface MenuListWithChildren {
  items: MenuWithChildrenInfo[]
  pageNumber: number,
  totalPages: number,
  totalCount: number,

}


export const MenuCreateApi = async (data: MenuCreateCoommand): Promise<ResponseData<boolean>> => {

  try {
    const response = await post<ResponseData<boolean>>(`/api/v1/Menus`, data);
    return response;
  } catch (error) {
    throw error;
  }
}

export const MenuListWithChildrenApi = async (): Promise<ResponseData<MenuWithChildrenInfo[]>> => {

  try {
    const response = await get<ResponseData<MenuWithChildrenInfo[]>>(`/api/v1/Menus/WithChildren`);
    return response;
  } catch (error) {
    throw error;
  }
}

export const MenuListWithPaginationAndChildrenApi = async (pageNumber: number, pageSize: number): Promise<ResponseData<MenuListWithChildren>> => {

  try {
    const response = await get<ResponseData<MenuListWithChildren>>(`/api/v1/Menus/WithPaginationAndChildren?PageNumber=${pageNumber}&PageSize=${pageSize}`);
    return response;
  } catch (error) {
    throw error;
  }
}