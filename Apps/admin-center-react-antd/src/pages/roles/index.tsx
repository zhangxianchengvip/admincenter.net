import { ColumnHeightOutlined, DeleteOutlined, DownloadOutlined, DownOutlined, EditOutlined, EyeOutlined, FormatPainterOutlined, ReloadOutlined, SwapOutlined, UpOutlined } from "@ant-design/icons";
import { Button, Col, Form, Input, Modal, Row, Select, Space, Table, theme } from "antd";
import { ColumnsType } from "antd/es/table";
import { TableRowSelection } from "antd/es/table/interface";
import { useState, useEffect } from "react";
import RoleCreate from "../../components/roles/create";
import style from "./role-page.module.scss";
import { RoleList, RoleListApi } from "../../apis/roles/roleApi";

const { Option } = Select;
const AdvancedSearchForm = () => {
    const { token } = theme.useToken();
    const [form] = Form.useForm();
    const [expand, setExpand] = useState(false);

    const formStyle = {
        maxWidth: 'none',
        background: token.colorFillAlter,
        borderRadius: token.borderRadiusLG,
        padding: 24,
        marginBottom: '20px'

    };


    const getFields = () => {
        const count = expand ? 10 : 3;
        const children = [];
        for (let i = 0; i < count; i++) {
            children.push(
                <Col span={6} key={i}>
                    <Form.Item
                        name={`field-${i}`}
                        label={`Field ${i}`}
                        rules={[
                            {
                                required: true,
                                message: 'Input something!',
                            },
                        ]}
                    >
                        {i % 3 !== 1 ? (
                            <Input placeholder="placeholder" />
                        ) : (
                            <Select defaultValue="2">
                                <Option value="1">1</Option>
                                <Option value="2">
                                    nglonglong
                                </Option>
                            </Select>
                        )}
                    </Form.Item>
                </Col>,
            );
        }
        return children;
    };

    const onFinish = (values: any) => {
        console.log('Received values of form: ', values);
    };

    return (
        <Form form={form} name="advanced_search" style={formStyle} onFinish={onFinish}>
            <Row gutter={24}>

                {getFields()}

                <Col span={expand ? '12' : 6} style={{ textAlign: 'right' }}>
                    <Button type="primary" htmlType="submit">
                        搜索
                    </Button>
                    <Button
                        style={{ margin: '0 8px' }}
                        onClick={() => {
                            form.resetFields();
                        }}
                    >
                        重置
                    </Button>
                    <a
                        style={{ fontSize: 12 }}
                        onClick={() => {
                            setExpand(!expand);
                        }}
                    >
                        {expand ?
                            <> <UpOutlined /> 折叠 </> : <><DownOutlined /> 更多</>}
                    </a>
                </Col>
            </Row>
        </Form>
    );
};

const RolePage: React.FC = () => {
    const { token } = theme.useToken();
    const [dataSource, setDataSource] = useState<any[]>([]);
    const [loading, setLoading] = useState(true);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [selectedRowKeys, setSelectedRowKeys] = useState<React.Key[]>([]);
    const [selectCount, setSelectCount] = useState(0);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await RoleListApi(1, 10); // 假设默认获取第一页和每页显示10条记录
                setDataSource(response.data.items); // 假设返回的 data 对象包含一个 items 属性
            } catch (error) {
                console.error("Failed to fetch data:", error);
            } finally {
                setLoading(false);
            }
        };
        fetchData();
    }, []);


    const rowSelection = {
        selectedRowKeys,
        onChange: (selectedRowKeys: React.Key[], selectedRows: any[]) => {
            console.log(`selectedRowKeys: ${selectedRowKeys}`, 'selectedRows: ', selectedRows);
            setSelectedRowKeys([...selectedRowKeys]);
            setSelectCount(selectedRows.length);
        },
        getCheckboxProps: (record: any) => ({
            disabled: record.name === 'Disabled User', // 列配置不可被选中
            name: record.name,
        }),
    };

    const selectNone = () => {
        setSelectedRowKeys([]);
        setSelectCount(0);
    };

    const columns: ColumnsType<any> = [
        {
            title: '角色',
            dataIndex: 'name',
            key: 'name',
        },
        {
            title: '描述',
            dataIndex: 'description',
            key: 'description',
        },
        {
            title: '操作',
            key: 'actions',
            fixed: 'right',
            width: 250,
            render: () => (
                <Space>
                    <a><EyeOutlined /> 查看</a>
                    <a><EditOutlined /> 编辑</a>
                    <a style={{ color: "#ed4014" }}><DeleteOutlined /> 删除</a>
                    <a> 更多 <DownOutlined style={{ fontSize: "10px" }} /></a>
                </Space>
            ),
        },
    ];

    return (
        <div>
            <AdvancedSearchForm />

            <Modal
                title="角色添加"
                open={isModalOpen}
                onOk={() => setIsModalOpen(false)}
                onCancel={() => setIsModalOpen(false)}
                width={"30%"}
                footer={[]}
                destroyOnClose
            >
                <RoleCreate submitOkCallback={() => setIsModalOpen(false)} />
            </Modal>

            <Space className={style.space}>
                <Space align={"center"}>
                    <Button type="primary" onClick={() => setIsModalOpen(true)}>新增</Button>

                    {selectCount > 0 && (
                        <div style={{ border: "1px solid #abdcff", borderRadius: "6px", padding: "0 10px", margin: "-1px", background: "#f0faff" }}>
                            <Space>
                                <div>
                                    已选择 {selectCount} 项
                                </div>

                                <Button type="link" danger>
                                    <DeleteOutlined />全部删除
                                </Button>

                                <Button type="link" onClick={selectNone}>取消选择</Button>
                            </Space>
                        </div>
                    )}
                </Space>

                <Space align={"center"} size={"middle"}>
                    <ReloadOutlined />
                    <DownloadOutlined />
                    <ColumnHeightOutlined />
                    <FormatPainterOutlined />
                    <SwapOutlined />
                </Space>
            </Space>

            <Table
                loading={loading}
                dataSource={dataSource}
                columns={columns}
                rowSelection={{
                    type: 'checkbox',
                    ...rowSelection,
                } as TableRowSelection<any>}
                pagination={{
                    showQuickJumper: true,
                    defaultCurrent: 1,
                    total: dataSource.length, // 这里需要根据实际情况设置总数
                    showTotal: (total) => `共 ${total} 项`,
                }}
            />
        </div>
    );
};

export default RolePage;