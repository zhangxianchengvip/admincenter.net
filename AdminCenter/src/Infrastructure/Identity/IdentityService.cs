using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminCenter.Application.Common.Interfaces;
using AdminCenter.Application.Common.Models;

namespace AdminCenter.Infrastructure.Identity;
internal class IdentityService : IIdentityService
{
    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        await Task.CompletedTask;
        return true;
    }

    public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
    {
        await Task.CompletedTask;
        return (Result.Success(), "123");
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        await Task.CompletedTask;
        return Result.Success();
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        await Task.CompletedTask;

        return "123";
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        await Task.CompletedTask;

        return true;
    }
}
