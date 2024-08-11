import { Button, Cascader, Descriptions, Form, Input, InputNumber, message } from "antd";
import React, { useEffect, useState } from "react";
import style from "./org-create.module.scss";
import { OrgCreateApi, OrgListWithChildrenApi } from "../../../apis/organizations/orgApi";
const OrganizaitonCreate: React.FC<{ submitOkCallback: () => void }> = (props) => {

    const [orgOptions, setOrgOptions] = useState<Option[]>([]);
    useEffect(() => {
        fetchOrgList();
    }, []);



    const fetchOrgList = async () => {
        const response = await OrgListWithChildrenApi();
        if (response.code === 200) {
            const orgs = convertToAntdOptions(response.data);
            setOrgOptions(orgs);
        }
    };

    const convertToAntdOptions = (data: any[]): any[] => {
        return data.map((org) => ({
            value: org.id,
            label: org.name,
            children: convertToAntdOptions(org.children),
        }));
    };


    const layout = {
        labelCol: { span: 8 },
        wrapperCol: { span: 16 },
    };

    /* eslint-disable no-template-curly-in-string */
    const validateMessages = {
        required: '${label} is required!',
        types: {
            email: '${label} is not a valid email!',
            number: '${label} is not a valid number!',
        },
        number: {
            range: '${label} must be between ${min} and ${max}',
        },
    };
    /* eslint-enable no-template-curly-in-string */

    const onFinish = async (values: any) => {
        var resp = await OrgCreateApi({
            name: values.name,
            code: values.code,
            description: values.description,
            superiorId: values.superior[values.superior.length - 1]
        })

        if (resp.code == 200 && resp.data != null && resp.data != undefined) {
            props.submitOkCallback();
        } else {
            message.error(resp.message)
        }
    };


    interface Option {
        value: string;
        label: string;
        children?: Option[];
    }
    // Just show the latest item.
    const displayRender = (labels: string[]) => labels[labels.length - 1];

    return (
        <>
            <Form
                {...layout}
                name="nest-messages"
                onFinish={onFinish}
                style={{ maxWidth: 430 }}
                validateMessages={validateMessages}
            >
                <Form.Item name="name" label="部门名称" rules={[{ required: true, message: "部门名称不能为空" }]}>
                    <Input />
                </Form.Item>
                <Form.Item name="code" label="部门编号" rules={[{ required: true, message: "部门名称不能为空" }]}>
                    <Input />
                </Form.Item>

                <Form.Item name="superior" label="上级部门">
                    <Cascader
                        options={orgOptions}
                        expandTrigger="hover"
                        displayRender={displayRender}
                        changeOnSelect={true}
                    />
                </Form.Item>

                <Form.Item name="description" label="描述">
                    <Input.TextArea rows={6} />
                </Form.Item>


                <div className={style.button}>
                    <Form.Item >
                        <Button type="default" htmlType="button" onClick={() => props.submitOkCallback()}>
                            关闭
                        </Button>
                    </Form.Item>
                    <Form.Item >
                        <Button type="primary" htmlType="submit">
                            保存
                        </Button>
                    </Form.Item>
                </div>
            </Form>
        </>
    )

}

export default OrganizaitonCreate