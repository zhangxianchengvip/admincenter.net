import { post } from "../apiClient";
import { ResponseData } from "../response";
import { UserInfo } from "./userApi";

interface LoginRequest {
  loginName: string;
  password: string;
}

interface LoginResponse {
  token: string;
  user: UserInfo;
}



/**
* 登录接口
* @param data - 登录所需的数据
* @returns 返回登录结果
*/
export const loginApi = async (data: LoginRequest): Promise<ResponseData<LoginResponse>> => {
  try {
    const response = await post<ResponseData<LoginResponse>>('/api/v1/Login/Account', data);
    return response;
  } catch (error) {
    throw error;
  }
};