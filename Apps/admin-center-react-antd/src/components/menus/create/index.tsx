import { Button, Cascader, Descriptions, Form, Input, InputNumber, message } from "antd";
import React, { useEffect, useState } from "react";
import style from "./menu-create.module.scss";
import { MenuCreateApi, MenuListWithChildrenApi, MenuListWithPaginationAndChildrenApi } from "../../../apis/menus/menuApi";
const MenuCreate: React.FC<{ submitOkCallback: () => void }> = (props) => {

    const [menuOptions, setMenuOptions] = useState<Option[]>([]);
    useEffect(() => {
        fetchOrgList();
    }, []);



    const fetchOrgList = async () => {
        const response = await MenuListWithChildrenApi();
        if (response.code === 200) {
            console.log("xxxxxxx");
            console.log(response.data);

            const menus = convertToAntdOptions(response.data);
            setMenuOptions(menus ?? []);
        }
    };

    const convertToAntdOptions = (data: any[]): any[] | null => {
        if (data == null || data == undefined) {
            return null;
        }
        return data.map((menu) => ({
            value: menu.id,
            label: menu.name,
            children: convertToAntdOptions(menu.children),
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
        var resp = await MenuCreateApi({
            name: values.name,
            menuType: 1,
            route: values.route,
            isLink: false,
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
                <Form.Item name="name" label="菜单名称" rules={[{ required: true, message: "菜单名称不能为空" }]}>
                    <Input />
                </Form.Item>
                <Form.Item name="menuType" label="菜单类型" rules={[{ required: true, message: "菜单类型不能为空" }]}>
                    <Input />
                </Form.Item>
                <Form.Item name="route" label="路由" rules={[{ required: true, message: "菜单名称不能为空" }]}>
                    <Input />
                </Form.Item>
                <Form.Item name="isLink" label="外链" rules={[{ required: true, message: "菜单名称不能为空" }]}>
                    <Input />
                </Form.Item>

                <Form.Item name="superior" label="上级菜单">
                    <Cascader
                        options={menuOptions}
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

export default MenuCreate