using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminCenter.Domain.Enums;
public enum MenuTypeEnum
{
    /// <summary>
    /// 启用
    /// </summary>
    [Description("目录")]
    Dictionary = 1,

    /// <summary>
    /// 停用
    /// </summary>
    [Description("菜单")]
    Menum = 2,

    /// <summary>
    /// 停用
    /// </summary>
    [Description("按钮")]
    Button = 3,

}
