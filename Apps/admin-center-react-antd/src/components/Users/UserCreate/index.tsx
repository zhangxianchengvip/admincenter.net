import React, { useState } from 'react';
import {
    Button,
    Cascader,
    Form,
    Input,
    Select,
    SelectProps,

} from 'antd';
import styles from "./user-create.module.scss";


interface Option {
    value: string;
    label: string;
    children?: Option[];
}

const layout = {
    labelCol: { span: 8 },
    wrapperCol: { span: 16 },
};

const tailLayout = {
    wrapperCol: { offset: 8, span: 16 },
};

const UserCreate: React.FC<{ closeButtonClickedCallback: () => void; }> = (props) => {

    const [form] = Form.useForm();


    const onFinish = (values: any) => {
        console.log(values);
        onClose()
    };

    const onClose = () => {
        props.closeButtonClickedCallback()
    };
    const onReset = () => {
      form.resetFields()

    };

    const onFill = () => {
        form.setFieldsValue({ note: 'Hello world!', gender: 'male' });
    };

    const options: SelectProps['options'] = [];

    for (let i = 10; i < 36; i++) {
        options.push({
            label: i.toString(36) + i,
            value: i.toString(36) + i,
        });
    }
    const handleChange = (value: string[]) => {
        console.log(`selected ${value}`);
    };




    const orgs: Option[] = [
        {
            value: 'zhejiang',
            label: 'Zhejiang',
            children: [
                {
                    value: 'hangzhou',
                    label: 'Hangzhou',
                    children: [
                        {
                            value: 'xihu',
                            label: 'West Lake',
                        },
                    ],
                },
            ],
        },
        {
            value: 'jiangsu',
            label: 'Jiangsu',
            children: [
                {
                    value: 'nanjing',
                    label: 'Nanjing',
                    children: [
                        {
                            value: 'zhonghuamen',
                            label: 'Zhong Hua Men',
                        },
                    ],
                },
            ],
        },
    ];



    // const onChange: CascaderProps<Option>['onChange'] = (value: any, selectedOptions: any) => {
    //     console.log('Selected Options:', selectedOptions);
    //     console.log('Value:', value);
    // };
    // Just show the latest item.
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
                        options={orgs}
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