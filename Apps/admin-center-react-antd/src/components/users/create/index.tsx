import React, { useEffect, useState } from 'react';
import {
    Button,
    Cascader,
    Form,
    Input,
    message,
    Select,
    SelectProps,
} from 'antd';
import styles from "./user-create.module.scss";
import { RoleListApi } from '../../../apis/roles/roleApi';
import { OrgListWithChildrenApi } from '../../../apis/organizations/orgApi';
import { UserCreateApi } from '../../../apis/users/userApi';

interface Option {
    value: string;
    label: string;
    children?: Option[];
}

const UserCreate: React.FC<{ closeButtonClickedCallback: () => void; }> = (props) => {

    const [form] = Form.useForm();
    const [options, setOptions] = useState<SelectProps['options']>([]);
    const [orgOptions, setOrgOptions] = useState<Option[]>([]);

    const layout = {
        labelCol: { span: 8 },
        wrapperCol: { span: 16 },
    };


    const fetchOrgList = async () => {
        const response = await OrgListWithChildrenApi();
        if (response.code === 200) {
            const newOrgOptions = convertToAntdOptions(response.data);
            setOrgOptions(newOrgOptions??[]);
        }
    };

    const fetchRoleListData = async () => {
        const response = await RoleListApi();
        if (response.code === 200) {
            const newOptions = response.data.map(role => ({
                label: role.name,
                value: role.id.toString()
            }));
            setOptions(newOptions);
        }
    };

    const convertToAntdOptions = (data: any[]): any[] | null => {
        if (data == null || data == undefined) {
            return null;
        }
        return data.map((org) => ({
            value: org.id,
            label: org.name,
            children: convertToAntdOptions(org.children),
        }));
    };

    useEffect(() => {
        fetchRoleListData();
        fetchOrgList();
    }, []);

    const onOrgChange = (value: any, selectOptions: any) => {
        console.log(value, selectOptions);
    }

    const onFinish = async (values: any) => {
        var org = values.org[values.org.length - 1]
        const response = await UserCreateApi({
            loginName: values.account,
            realName: values.realName,
            password: values.password,
            nickName: values.nickName,
            email: values.email,
            phoneNumber: values.phoneNumber,
            roleIds: values.role,
            organizationIds: [{ isSubsidiary: false, organizationId: org }]
        })

        if (response.code === 200) {
            message.success("创建成功");
            onClose()
        } else {
            message.error(response.message)
        }

    };

    const onClose = () => {
        props.closeButtonClickedCallback()
    };
    const onReset = () => {
        form.resetFields()
    };


    const handleChange = (value: string[]) => {
        console.log(`selected ${value}`);
    };

    const displayRender = (labels: string[]) => labels[labels.length - 1];


    return (
        <Form className={styles.container}
            preserve={false}
            {...layout}
            form={form}
            name="control-hooks"
            onFinish={onFinish}
            onReset={onReset}
            style={{ maxWidth: 600 }}

        >


            <div className={styles.item}>
                <Form.Item name="account" label="账号" rules={[{ required: true }]}  >
                    <Input />
                </Form.Item>
                <Form.Item name="realName" label="姓名" rules={[{ required: true }]}>
                    <Input />
                </Form.Item>
                <Form.Item name="phoneNumber" label="电话" rules={[{ required: false }]}>
                    <Input />
                </Form.Item>
                <Form.Item name="org" label="组织" rules={[{ required: true }]}>
                    <Cascader
                        options={orgOptions}
                        onChange={onOrgChange}
                        expandTrigger="hover"
                        displayRender={displayRender}
                        changeOnSelect={true}
                    />
                </Form.Item>
            </div>

            <div className={styles.item}>
                <Form.Item name="password" label="密码" rules={[{ required: true }]}>
                    <Input />
                </Form.Item>
                <Form.Item name="nickName" label="昵称" rules={[{ required: true }]}>
                    <Input />
                </Form.Item>
                <Form.Item name="email" label=" 邮箱" rules={[{ required: false }]}>
                    <Input />
                </Form.Item>
                <Form.Item name="role" label="角色" rules={[{ required: true }]}>
                    <Select
                        mode="multiple"
                        allowClear
                        style={{ width: '100%' }}
                        placeholder="请选择角色"
                        onChange={handleChange}
                        options={options}

                    />
                </Form.Item>
            </div>

            <div className={styles.button}>
                <Form.Item >
                    <Button type="default" htmlType="button" onClick={() => { onClose() }}>
                        取消
                    </Button>
                </Form.Item>
                <Form.Item >
                    <Button type="primary" htmlType="submit">
                        保存
                    </Button>
                </Form.Item>
            </div>


        </Form >
    );
};


export default UserCreate