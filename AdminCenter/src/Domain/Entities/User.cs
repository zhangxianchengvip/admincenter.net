using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;
using Ardalis.GuardClauses;
namespace AdminCenter.Domain;

/// <summary>
/// 用户
/// </summary>
public class User : IAggregateRoot<Guid>
{
    /// <summary>
    /// 账号
    /// </summary>
    public string LoginName { get; init; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; private set; }

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
    public string RealName { get; private set; }

    /// <summary>
    /// 用户角色
    /// </summary>
    public ICollection<UserRole> UserRoles { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public StatusEnum Status { get; set; }

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
        RealName = Guard.Against.NullOrWhiteSpace(realName, nameof(realName));
        LoginName = Guard.Against.NullOrWhiteSpace(loginName, nameof(loginName));
        Password = HashPassword(Guard.Against.NullOrWhiteSpace(password, nameof(password)));
        UserRoles = [];
    }

    /// <summary>
    /// 更新密码
    /// </summary>
    /// <param name="password"></param>
    public User UpdatePassword([NotNull] string password)
    {
        Password = HashPassword(Guard.Against.NullOrWhiteSpace(password, nameof(password)));

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
        ArgumentException.ThrowIfNullOrWhiteSpace(password);

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
        ArgumentException.ThrowIfNullOrWhiteSpace(password, nameof(password));

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
        UserRoles = roleList.Select(roleId => new UserRole
        (
          userId: Id,
          roleId: roleId
        )).ToList();

        return this;
    }

    /// <summary>
    /// 更新真实名称
    /// </summary>
    /// <param name="realName"></param>
    /// <returns></returns>
    public User UpdateRealName([NotNull] string realName)
    {
        RealName = Guard.Against.NullOrWhiteSpace
        (
           input: realName,
           parameterName: nameof(realName),
           exceptionCreator: () => new Exception()
        );

        return this;
    }
}
