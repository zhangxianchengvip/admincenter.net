using System.ComponentModel;

namespace AdminCenter.Domain;

public enum StatusEnum
{
    /// <summary>
    /// 启用
    /// </summary>
    [Description("启用")]
    Enable = 1,

    /// <summary>
    /// 停用
    /// </summary>
    [Description("停用")]
    Disable = 2,

    /// <summary>
    /// 已删除
    /// </summary>
    [Description("已删除")]
    Deleted = 3
}
