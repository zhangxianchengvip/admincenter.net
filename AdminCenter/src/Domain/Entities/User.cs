using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;
using AdminCenter.Domain.Constants;
using Ardalis.GuardClauses;
namespace AdminCenter.Domain;

/// <summary>
/// 用户
/// </summary>
public class User : AggregateRoot<Guid>
{
    /// <summary>
    /// 账号
    /// </summary>
    public string LoginName { get; init; } = default!;

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; private set; } = default!;

    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string? NickName { get; set; }

    /// <summary>
    /// 真实名称
    /// </summary>
    public string RealName { get; private set; } = default!;

    /// <summary>
    /// 用户角色
    /// </summary>
    public ICollection<UserRole> UserRoles { get; set; } = [];

    /// <summary>
    /// 状态
    /// </summary>
    public StatusEnum Status { get; set; }

    private User() { }

    public User(
        [NotNull] Guid id,
        [NotNull] string loginName,
        [NotNull] string password,
        [NotNull] string realName,
        string? nickName = null,
        string? phoneNumber = null,
        string? email = null) : base(id)
    {
        NickName = nickName;
        PhoneNumber = phoneNumber;
        Email = email;
        Status = StatusEnum.Enable;

        //校验名称为空，或者赋值
        RealName = Guard.Against.NullOrWhiteSpace
        (
            input: realName,
            parameterName: nameof(realName),
            exceptionCreator: () => new AdminBusinessException(ExctptionMessage.UserNameNull)
        );

        //校验登录名称为空，或者赋值
        LoginName = Guard.Against.NullOrWhiteSpace
        (
            input: loginName,
            parameterName: nameof(loginName),
            exceptionCreator: () => new AdminBusinessException(ExctptionMessage.UserNameNull)
        );

        //校验密码为空，或者赋值
        Password = HashPassword(Guard.Against.NullOrWhiteSpace
        (
            input: password,
            parameterName: nameof(password),
            exceptionCreator: () => new AdminBusinessException(ExctptionMessage.UserNameNull)
        ));

        UserRoles = [];
    }

    /// <summary>
    /// 更新密码
    /// </summary>
    /// <param name="password"></param>
    public User UpdatePassword([NotNull] string password)
    {
        //校验密码为空，或者赋值
        Password = HashPassword(Guard.Against.NullOrWhiteSpace
        (
            input: password,
            parameterName: nameof(password),
            exceptionCreator: () => new AdminBusinessException(ExctptionMessage.UserPasswordNull)
        ));

        //添加用户密码修改事件
        AddDomainEvent(new UserPasswordUpdateEvent { UserId = Id });

        return this;
    }

    /// <summary>
    /// 密码hash加密
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    private string HashPassword([NotNull] string password)
    {
        //校验密码为空
        Guard.Against.NullOrWhiteSpace
        (
            input: password,
            parameterName: nameof(password),
            exceptionCreator: () => new AdminBusinessException(ExctptionMessage.UserPasswordNull)
        );

        // 生成一个随机的盐
        using var rng = RandomNumberGenerator.Create();
        byte[] salt = new byte[32]; // 生成一个32字节的盐
        rng.GetBytes(salt);

        // 使用HMAC-SHA256算法创建一个哈希对象
        using var hmac = new HMACSHA256(salt);

        // 计算密码的哈希值
        byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        // 将盐和哈希值组合成一个单一的字节数组
        byte[] saltAndHash = new byte[salt.Length + hash.Length];
        Buffer.BlockCopy(salt, 0, saltAndHash, 0, salt.Length);
        Buffer.BlockCopy(hash, 0, saltAndHash, salt.Length, hash.Length);

        // 将组合后的字节数组转换为Base64字符串
        return Convert.ToBase64String(saltAndHash);
    }

    /// <summary>
    /// 校验密码
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public bool ValidatePassword([NotNull] string password)
    {
        //校验密码为空
        Guard.Against.NullOrWhiteSpace
        (
            input: password,
            parameterName: nameof(password),
            exceptionCreator: () => new AdminBusinessException(ExctptionMessage.UserPasswordNull)
        );

        byte[] saltAndHash = Convert.FromBase64String(Password);
        byte[] salt = new byte[32];
        byte[] hash = new byte[saltAndHash.Length - 32];

        // 从存储的哈希值中分离出盐和哈希值
        Buffer.BlockCopy(saltAndHash, 0, salt, 0, 32);
        Buffer.BlockCopy(saltAndHash, 32, hash, 0, hash.Length);

        // 使用相同的盐重新计算密码的哈希值
        using var hmac = new HMACSHA256(salt);
        byte[] testHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        // 比较原始哈希值和新计算的哈希值
        return testHash.SequenceEqual(hash);
    }

    /// <summary>
    /// 更新角色
    /// </summary>
    /// <param name="roleList"></param>
    /// <returns></returns>
    public User UpdateRoleRange(List<Guid> roleList)
    {
        UserRoles = roleList.Select(roleId => new UserRole(userId: Id, roleId: roleId)).ToList();

        return this;
    }

    /// <summary>
    /// 更新真实名称
    /// </summary>
    /// <param name="realName"></param>
    /// <returns></returns>
    public User UpdateRealName([NotNull] string realName)
    {
        //校验名称为空，或者赋值
        RealName = Guard.Against.NullOrWhiteSpace
        (
           input: realName,
           parameterName: nameof(realName),
           exceptionCreator: () => new AdminBusinessException("用户名称不能为空!")
        );

        return this;
    }
}
