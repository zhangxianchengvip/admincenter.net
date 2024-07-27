import { Button, Cascader, Col, DatePicker, Drawer, Form, Input, Row, Select, Space } from 'antd';
import { useState } from 'react';

const { Option } = Select;

interface Option {
  value: string;
  label: string;
  children?: Option[];
}
const UserEdit: React.FC<
  {
    formDisable: boolean;
    title: string;
    open: boolean;
    onClose: () => void;
  }> = (props) => {

    const [disable, setIsDisable] = useState(props.formDisable);

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

    const displayRender = (labels: string[]) => labels[labels.length - 1];

    const onClose = () => {
      if (props.formDisable) {
        setIsDisable(true)
      }
      props.onClose()
    }

    return (
      <Drawer
        title={props.title}
        width={720}
        open={props.open}
        onClose={() => onClose()}
        extra={
          <Space>
            <Button onClick={onClose}>关闭</Button>
            <Button onClick={() => { setIsDisable(false) }} hidden={!disable} >编辑</Button>
            <Button onClick={props.onClose} type="primary" hidden={disable}>
              保存
            </Button>
          </Space>
        }
      >
        <Form layout="vertical" disabled={disable}>
          <Row gutter={16}>
            <Col span={12}>
              <Form.Item
                name="RealName"
                label="姓名"
                rules={[{ required: true, message: '请输入姓名' }]}
              >
                <Input placeholder="请输入姓名" />
              </Form.Item>
            </Col>
            <Col span={12}>
              <Form.Item
                name="NickName"
                label="昵称"
                rules={[{ required: true, message: '请输入昵称' }]}
              >
                <Input placeholder="请输入昵称" />
              </Form.Item>
            </Col>
          </Row>

          <Row gutter={16}>
            <Col span={12}>
              <Form.Item
                name="PhoneNumber"
                label="电话"
                rules={[{ required: false, message: '请输入电话' }]}
              >
                <Input placeholder="请输入电话" />
              </Form.Item>
            </Col>
            <Col span={12}>
              <Form.Item
                name="Email"
                label="邮箱"
                rules={[{ required: false, message: '请输入邮箱' }]}
              >
                <Input placeholder="请输入邮箱" />
              </Form.Item>
            </Col>
          </Row>
          <Row gutter={16}>
            <Col span={12}>
              <Form.Item
                name="Org"
                label="组织"
                rules={[{ required: true, message: '请输入组织' }]}
              >
                <Cascader
                  options={orgs}
                  expandTrigger="hover"
                  displayRender={displayRender}
                // onChange={onChange}
                />
              </Form.Item>
            </Col>
            <Col span={12}>
              <Form.Item
                name="Role"
                label="角色"
                rules={[{ required: true, message: '请输入角色' }]}
              >
                <Select placeholder="请输入角色">
                  <Option value="private">Private</Option>
                  <Option value="public">Public</Option>
                </Select>
              </Form.Item>
            </Col>
          </Row>
          <Row gutter={16}>
            <Col span={24}>
              <Form.Item
                name="description"
                label="描述"
                rules={[
                  {
                    required: false,
                    message: 'please enter url description',
                  },
                ]}
              >
                <Input.TextArea rows={4} placeholder="please enter url description" />
              </Form.Item>
            </Col>
          </Row>
        </Form>
      </Drawer>
    );
  };

export default UserEdit