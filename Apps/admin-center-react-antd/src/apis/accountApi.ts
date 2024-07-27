import { createApi } from '@reduxjs/toolkit/query/react'
import myFetchQuery from "./myFetchQuery";
import { ResponseData } from './response';


export const accountApi = createApi({
    baseQuery: myFetchQuery,
    tagTypes: [],
    reducerPath: "accountApi",
    endpoints: (builder) => ({
        login: builder.mutation<ResponseData<object> & { jwt: string }, { account: string, password: string }>({
            query: (body) => ({
                url: "/Users/Login",
                method: 'POST',
                body,
            }),
        }),
        getAccountById: builder.query<ResponseData<object>, void>({
            query: (n) => `account/1`,
        }),

        getAccountPermissions: builder.query<ResponseData<object> & { permissions: string[] }, void>({
            query: (n) => `account/permissions`,
        }),
    }),
})

// Export hooks for usage in functional components
export const { useLoginMutation, useGetAccountByIdQuery, useGetAccountPermissionsQuery } = accountApi
