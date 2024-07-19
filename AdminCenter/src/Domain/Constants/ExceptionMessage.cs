namespace AdminCenter.Domain.Constants;
public static class ExceptionMessage
{
    //用户
    public const string UserExist = "用户已存在";
    public const string UserNotExist = "用户不存在";
    public const string UserNameNull = "用户名称为空";
    public const string UserIdNull = "用户标识为空";
    public const string UserPasswordError = "用户密码错误";
    public const string UserPasswordNull = "用户密码为空";
    public const string UserDeleteError = "用户不能删除自己";
    public const string UserRoleListNull = "用户角色为空";
    public const string UserOrgListNull = "用户组织为空";
    public const string IdNull = "标识为空";

    //角色
    public const string RoleIdNull = "角色表示为空";
    public const string RoleNameNull = "角色名称为空";
    public const string RoleExist = "角色已存在";
    public const string RoleNotExist = "角色不存在";

    //组织
    public const string OrganizationNotExist = "组织不存在";
    public const string OrganizationNameNull = "组织名称为空";
    public const string OrganizationCodeNull = "组织编号为空";
    public const string OrganizationCodeExist = "组织编号已存在";

    //职位
    public const string PositionNameNull = "职位名称为空";
    public const string PositionCodeNull = "职位编号为空";
    public const string OPositionCodeExist = "职位编号已存在";

}
