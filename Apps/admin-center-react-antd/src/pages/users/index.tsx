import React, { useEffect, useRef, useState } from 'react';
import {
    ColumnHeightOutlined, DeleteOutlined, DownloadOutlined,
    DownOutlined, EditOutlined, EyeOutlined, FormatPainterOutlined, LineChartOutlined,
    MenuOutlined, MoreOutlined, PlusOutlined,
    ReloadOutlined,
    SearchOutlined, SlidersOutlined, SwapOutlined,
    UploadOutlined,
    UpOutlined,
    UserDeleteOutlined
} from '@ant-design/icons';
import {
    Alert,
    Button, Checkbox,
    Col, Drawer, Dropdown,
    Form,
    Input, MenuProps,
    message,
    Modal,
    Pagination,
    Popconfirm,
    Row,
    Select,
    Space,
    Table,
    theme, Tooltip,
    Upload
} from 'antd';
import { ColumnsType } from "antd/es/table";
import { TableRowSelection } from "antd/es/table/interface";
import DictSelect from "../../components/DictSelect";
import UserCreate from '../../components/users/create';
import UserEdit from '../../components/users/edit';
import { UserInfo, UserListeApi } from '../../apis/users/userApi';


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

const UserList: React.FC = () => {

    const [dataSource, setDataSource] = useState<UserInfo[]>([]);
    const [loading, setLoading] = useState(true);//loading
    const [isModalOpen, setIsModalOpen] = useState(false);//模态框
    const [selectedRowKeys, setSelectedRowKeys] = useState<React.Key[]>([]);
    const [selectCount, setSelectCount] = useState(0);//选中数量
    const [currentPage, setCurrentPage] = useState(1); // 当前页码
    const [total, setTotal] = useState(0); // 总条目数

    const fetchUserData = async (pageNumber: number, pageSize: number) => {
        const response = await UserListeApi(pageNumber, pageSize)

        if (response.code === 200) {
            setDataSource(response.data.items);
            setTotal(response.data.totalCount); // 设置总条目数
            setLoading(false);
        } else {
            message.error(response.message);
        }
    }

    useEffect(() => {

        fetchUserData(currentPage, 10);

    }, [currentPage]); // 当前页码变化时重新获取数据

    const columns = [
        {
            title: '账号',
            dataIndex: 'loginName',
            key: 'loginName',
        },
        {
            title: '昵称',
            dataIndex: 'nickName',
            key: 'nickName',
        },
        {
            title: '真实姓名',
            dataIndex: 'realName',
            key: 'realName',
        },
        {
            title: '电话',
            dataIndex: 'phoneNumber',
            key: 'phoneNumber',
        },
        {
            title: '操作',
            dataIndex: 'name',
            key: 'name',
            fixed: 'right',
            width: 250,
            render: () => {

                return (
                    <Space>
                        <a onClick={() => { setUserDescDrawer(true) }} > <EyeOutlined /> 查看</a>
                        <a onClick={() => { setUserCreateDrawer(true) }}> <EditOutlined /> 编辑</a>

                        <a style={{ color: "#ed4014" }}><DeleteOutlined /> 删除</a>
                        <a> 更多 <DownOutlined style={{ fontSize: "10px" }} /></a>
                    </Space>
                )
            }
        },
    ];


    const densityItems: MenuProps['items'] = [
        {
            key: '1',
            label: '默认',
        },
        {
            key: '2',
            label: '宽松',
        },
        {
            key: '3',
            label: '紧凑',
        },
    ];

    const selectNone = () => {
        setSelectedRowKeys([]);
        setSelectCount(0);
    };

    // rowSelection object indicates the need for row selection
    const rowSelection = {
        selectedRowKeys,
        onChange: (selectedRowKeys: React.Key[], selectedRows: any) => {
            console.log(`selectedRowKeys: ${selectedRowKeys}`, 'selectedRows: ', selectedRows);
            setSelectedRowKeys([...selectedRowKeys]);
            setSelectCount(selectedRows.length);
        },
        getCheckboxProps: (record: any) => ({
            disabled: record.name === 'Disabled User', // Column configuration not to be checked
            name: record.name,
        }),
    };

    // rowSelection.
    const [showDrawer, setShowDrawer] = useState(false);

    const [showUserCreateDrawer, setUserCreateDrawer] = useState(false);
    const [showUserDescDrawer, setUserDescDrawer] = useState(false);


    return (
        <div>
            <UserEdit formDisable={true} title='用户信息详情' open={showUserDescDrawer} onClose={() => setUserDescDrawer(false)}></UserEdit>

            <UserEdit formDisable={false} title='用户信息修改' open={showUserCreateDrawer} onClose={() => setUserCreateDrawer(false)} ></UserEdit>

            <AdvancedSearchForm />

            <Modal title="新增用户"
                open={isModalOpen}
                onOk={() => setIsModalOpen(false)}
                onCancel={() => setIsModalOpen(false)}
                width={'38%'}
                footer={[]}
                destroyOnClose>

                <UserCreate closeButtonClickedCallback={() => setIsModalOpen(false)} />
            </Modal>

            <Space style={{ display: "flex", justifyContent: "space-between", padding: "10px 0" }}>

                <Space align={"center"}>

                    <Button type="primary" onClick={() => {
                        setIsModalOpen(!isModalOpen)
                    }}><PlusOutlined />新增</Button>

                    <DictSelect dictUrl={"/account/status"} style={{ width: "130px" }} />

                    {selectCount > 0 &&
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
                    }


                </Space>

                <Space align={"center"} size={"small"}>
                    <Tooltip title="刷新">
                        <Button type={"text"} icon={<ReloadOutlined />} />
                    </Tooltip>

                    <Tooltip title="导出">
                        <Button type={"text"} icon={<DownloadOutlined />} />
                    </Tooltip>

                    <Tooltip title="密度">
                        <Dropdown menu={{ items: densityItems }} placement="bottom" arrow trigger={['click']}>
                            <Button type={"text"} icon={<ColumnHeightOutlined />} />
                        </Dropdown>
                    </Tooltip>

                    <Tooltip title="打印">
                        <Button type={"text"} icon={<FormatPainterOutlined />} />
                    </Tooltip>

                    <Tooltip title="列设置">
                        <Button type={"text"} icon={<SwapOutlined />} onClick={() => { setShowDrawer(true) }} />
                    </Tooltip>
                </Space>

            </Space>

            <Table
                loading={loading}
                dataSource={dataSource}
                columns={columns as ColumnsType}
                rowSelection={{
                    type: 'checkbox',
                    ...rowSelection,
                } as TableRowSelection<any>}
                rowKey={(record) => record.id}
                pagination={{
                    showQuickJumper: true,
                    current: currentPage,
                    total: total,
                    showTotal: (total) => `Total ${total} items`,
                    onChange: (page) => setCurrentPage(page), // 更新当前页码
                }}
            />
        </div>
    )
}
export default UserList