import { Button, Descriptions, Form, Input, InputNumber, message } from "antd";
import layout from "antd/es/layout";
import React from "react";
import style from "./role-create.module.scss";
import { RoleCreateApi } from "../../../apis/roles/roleApi";
import { BulbFilled } from "@ant-design/icons";
const RoleCreate: React.FC<{ submitOkCallback: () => void }> = (props) => {


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
        var resp = await RoleCreateApi({
            name: values.roleName,
            description: values.description
        })

        if (resp.code == 200 && resp.data != null && resp.data != undefined) {
            props.submitOkCallback();
        } else {
            message.error(resp.message)
        }
    };

    return (
        <>
            <Form
                {...layout}
                name="nest-messages"
                onFinish={onFinish}
                style={{ maxWidth: 430 }}
                validateMessages={validateMessages}
            >
                <Form.Item name="roleName" label="角色名称" rules={[{ required: true, message: "角色名称不能为空" }]}>
                    <Input />
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

export default RoleCreate