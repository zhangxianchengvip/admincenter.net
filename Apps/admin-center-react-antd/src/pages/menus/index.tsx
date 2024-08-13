import {
    ColumnHeightOutlined, DeleteOutlined, DownloadOutlined, DownOutlined, EditOutlined, EyeOutlined,
    FormatPainterOutlined, ReloadOutlined, SwapOutlined
} from "@ant-design/icons";
import { Button, message, Modal, Space, Table } from "antd";
import { ColumnsType } from "antd/es/table";
import { TableRowSelection } from "antd/es/table/interface";
import { useState, useEffect } from "react";
import { RoleInfo } from "../../apis/roles/roleApi";
import { OrgDeleteApi, OrgListWithPaginationAndChildrenApi, OrgUpdateApi } from "../../apis/organizations/orgApi";
import MenuCreate from "../../components/menus/create";
import style from "./menu-page.module.scss";
import { MenuListWithPaginationAndChildrenApi, MenuWithChildrenInfo } from "../../apis/menus/menuApi";
const MenuPage: React.FC = () => {
    const [dataSource, setDataSource] = useState<MenuWithChildrenInfo[]>([]); // 数据
    const [loading, setLoading] = useState(true); // loading
    const [isModalOpen, setIsModalOpen] = useState(false); // 模态框
    const [selectedRowKeys, setSelectedRowKeys] = useState<React.Key[]>([]);
    const [selectCount, setSelectCount] = useState(0); // 选中数量
    const [currentPage, setCurrentPage] = useState(1); // 当前页码
    const [total, setTotal] = useState(0); // 总条目数
    const [expandedRows, setExpandedRows] = useState<string[]>([]); // 保存当前展开的行
    const [expandedTopLevelRow, setExpandedTopLevelRow] = useState<string | null>(null);

    const fetchOrgList = async (pageNumber: number, pageSize: number, superiorId: string | null = null) => {
        try {
            const response = await MenuListWithPaginationAndChildrenApi(pageNumber, pageSize);
            console.log(response.data.items);
            setDataSource(response.data.items);
            setTotal(response.data.totalCount); // 设置总条目数
        } catch (error) {
            console.error("获取数据失败:", error);
        } finally {
            setLoading(false);
        }
    };

    const onExpand = (expanded: boolean, record: any) => {
        console.log("展开", record);

        if (record.superiorId === null) { // 判断是否为顶级节点
            console.log("顶级节点", record);
            if (expanded) {
                setExpandedTopLevelRow(record.id); // 记录当前展开的顶级行
                setExpandedRows([record.id]); // 只展开当前顶级行
            } else {
                setExpandedTopLevelRow(null); // 取消记录顶级行
                setExpandedRows([]); // 折叠所有行
            }
        } else {
            // 如果是子节点，更新展开的行
            const newExpandedRows = expanded
                ? [...expandedRows, record.id]
                : expandedRows.filter(id => id !== record.id);

            setExpandedRows(newExpandedRows);
        }
    };

    useEffect(() => {
        fetchOrgList(currentPage, 10);
    }, [currentPage]); // 当前页码变化时重新获取数据

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

    // 删除角色
    const handleDelete = async (id: string) => {
        const response = await OrgDeleteApi(id);

        if (response.code === 200) {
            fetchOrgList(currentPage, 10); // 删除角色后重新获取数据
            message.success("删除成功");
        } else {
            message.error("删除失败");
        }
    }

    // 编辑角色
    const handleEdit = async (id: string, name: string, code: string, description: string, status: number, superiorId: string) => {
        const response = await OrgUpdateApi({
            id,
            name,
            code,
            description,
            superiorId,
            status
        }, id)
    }

    const columns: ColumnsType<any> = [
        {
            title: '菜单名称',
            dataIndex: 'name',
            key: 'name',
        },
        {
            title: '路由',
            dataIndex: 'route',
            key: 'route',
        },
        {
            title: '状态',
            dataIndex: 'status',
            key: 'status',
        },
        {
            title: '操作',
            key: 'actions', 
            fixed: 'right',
            width: 250,
            render: (record) => (
                <Space>
                    <a><EyeOutlined /> 查看</a>
                    <a><EditOutlined /> 编辑</a>
                    <a style={{ color: "#ed4014" }} onClick={() => handleDelete(record.id)}><DeleteOutlined /> 删除</a>
                    <a> 更多 <DownOutlined style={{ fontSize: "10px" }} /></a>
                </Space>
            ),
        },
    ];

    return (
        <div>
            <Modal
                title="菜单添加"
                open={isModalOpen}
                onOk={() => {
                    fetchOrgList(currentPage, 10); // 关闭模态框后重新获取数据
                    setIsModalOpen(false);
                }}
                onCancel={() => {
                    fetchOrgList(currentPage, 10); // 关闭模态框后重新获取数据
                    setIsModalOpen(false);
                }}
                width={"30%"}
                footer={[]}
                destroyOnClose
            >
                <MenuCreate submitOkCallback={() => {
                    fetchOrgList(currentPage, 10); // 创建角色后重新获取数据
                    setIsModalOpen(false);
                }} />
            </Modal>

            <Space className={style.space}>
                <Space align={"center"}>
                    <Button type="primary" onClick={() => setIsModalOpen(true)}>
                        新增
                    </Button>

                    {selectCount > 0 && (
                        <div style={{ border: "1px solid #abdcff", borderRadius: "6px", padding: "0 10px", margin: "-1px", background: "#f0faff" }}>
                            <Space>
                                <div>
                                    已选择 {selectCount} 项
                                </div>

                                <Button type="link" danger>
                                    <DeleteOutlined /> 全部删除
                                </Button>

                                <Button type="link" onClick={selectNone}>
                                    取消选择
                                </Button>
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
                rowKey={(record) => record.id}
                pagination={{
                    showQuickJumper: true,
                    current: currentPage,
                    onChange: (page) => setCurrentPage(page), // 更新当前页码
                    total,
                    showTotal: (total) => `共 ${total} 项`,
                }}
                expandable={{
                    expandedRowKeys: expandedRows, // 控制展开的行
                    onExpand: onExpand, // 处理展开/折叠的逻辑
                    childrenColumnName: 'children', // 自动处理children字段作为子项
                }}
            />
        </div>
    );
};

export default MenuPage;
