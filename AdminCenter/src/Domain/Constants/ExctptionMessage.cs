using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminCenter.Domain.Constants;
public static class ExctptionMessage
{
    public const string UserNotFind = "用户不存在";
    public const string UserNameNull = "用户名称为空";
    public const string UserIdNull = "用户标识为空";
    public const string UserPasswordError = "用户密码错误";
    public const string UserPasswordNull = "用户密码为空";

    public const string RoleIdNull = "角色标识为空";
}
