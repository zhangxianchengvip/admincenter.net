﻿using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;
using AdminCenter.Domain.Constants;
using AdminCenter.Domain.Entities;
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
    /// 用户职位
    /// </summary>
    public ICollection<UserPosition> UserPositions { get; set; } = [];

    /// <summary>
    /// 用户组织
    /// </summary>
    public ICollection<UserOrganization> UserOrganizations { get; set; } = [];

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
        [NotNull] List<Guid> roles,
        [NotNull] List<(Guid organizationId, bool isSubsidiary)> organizations,
        string? nickName = null,
        string? phoneNumber = null,
        string? email = null) : base(id)
    {
        //校验名称为空，或者赋值
        if (string.IsNullOrWhiteSpace(loginName))
        {
            throw new BusinessException(ExceptionMessage.UserNameNull);
        }

        //如果realName为空，则抛出异常
        if (string.IsNullOrWhiteSpace(realName))
        {
            throw new BusinessException(ExceptionMessage.UserNameNull);
        }

        //校验密码为空，或者赋值
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new BusinessException(ExceptionMessage.UserPasswordNull);
        }

        //校验角色列表为空
        if (roles.Count == 0)
        {
            throw new BusinessException(ExceptionMessage.UserRoleListNull);
        }

        //校验组织列表为空
        if (organizations.Count == 0)
        {
            throw new BusinessException(ExceptionMessage.UserRoleListNull);
        }

        //校验通过，设置值
        Email = email;
        RealName = realName;
        NickName = nickName;
        LoginName = loginName;
        PhoneNumber = phoneNumber;
        Status = StatusEnum.Enable;
        Password = HashPassword(password);
        UserRoles = roles.Select(roleId => new UserRole { RoleId = roleId, UserId = Id }).ToList();
        UserOrganizations = organizations.Select(organization => new UserOrganization { UserId = Id, OrganizationId = organization.organizationId, IsSubsidiary = organization.isSubsidiary }).ToList();
    }

    /// <summary>
    /// 更新密码
    /// </summary>
    public User UpdatePassword([NotNull] string password)
    {
        //校验密码为空
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new BusinessException(ExceptionMessage.UserPasswordNull);
        }

        Password = HashPassword(password);

        //添加用户密码修改事件
        AddDomainEvent(new UserPasswordUpdateEvent { UserId = Id });

        return this;
    }

    /// <summary>
    /// 密码hash加密
    /// </summary>
    private string HashPassword([NotNull] string password)
    {
        //校验密码为空
        Guard.Against.NullOrWhiteSpace
        (
            input: password,
            parameterName: nameof(password),
            exceptionCreator: () => new BusinessException(ExceptionMessage.UserPasswordNull)
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
    public bool ValidatePassword([NotNull] string password)
    {
        //校验密码为空
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new BusinessException(ExceptionMessage.UserPasswordNull);
        }

        // 解码密码
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
    public User UpdateRoleRange(List<Guid> roleList)
    {
        //校验角色列表为空
        if (roleList.Count == 0)
        {
            throw new BusinessException(ExceptionMessage.UserRoleListNull);
        }

        UserRoles = roleList
        .Select(roleId => new UserRole { RoleId = roleId, UserId = Id })
        .ToList();

        return this;
    }

    /// <summary>
    /// 更新真实名称
    /// </summary>
    public User UpdateRealName([NotNull] string realName)
    {
        //校验名称为空，或者赋值
        if (string.IsNullOrWhiteSpace(realName))
        {
            throw new BusinessException(ExceptionMessage.UserNameNull);
        }
        RealName = realName;

        return this;
    }

    /// <summary>
    /// 更新用户组织
    /// </summary>
    public User UpdateOrganizationRange(List<(Guid organizationId, bool isSubsidiary)> organizationList)
    {
        //校验组织列表为空
        if (organizationList.Count == 0)
        {
            throw new BusinessException(ExceptionMessage.UserRoleListNull);
        }
        
        UserOrganizations = organizationList
        .Select(organization => new UserOrganization { UserId = Id, OrganizationId = organization.organizationId, IsSubsidiary = organization.isSubsidiary })
        .ToList();

        return this;
    }
}
