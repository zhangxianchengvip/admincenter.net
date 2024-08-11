using System;
using AdminCenter.Application.Features.Organizations.Dtos;
using AdminCenter.Domain;

namespace AdminCenter.Application.Features.Organizations.Extensions;

public static class OrganizationQueryExtension
{
    public static List<OrganizationWithChildrenDto> BuildOrganizationTree(this List<Organization> organizations)
    {
        var organizationDtos = new List<OrganizationWithChildrenDto>();
        var organizationDictionary = new Dictionary<Guid, OrganizationWithChildrenDto>();

        // 将所有菜单转换为 DTO 并填充到字典中以便快速查找
        foreach (var organization in organizations)
        {
            var organizationDto = organization.Adapt<OrganizationWithChildrenDto>();
            organizationDtos.Add(organizationDto);
            organizationDictionary[organization.Id] = organizationDto;
        }

        // 遍历 DTO 列表，为每个菜单设置子菜单
        foreach (var organizationDto in organizationDtos)
        {
            if (organizationDto.SuperiorId.HasValue && organizationDictionary.TryGetValue(organizationDto.SuperiorId.Value, out var parentMenuDto))
            {
                parentMenuDto.Children.Add(organizationDto);
            }
        }

        // 现在 menuDtos 包含了所有顶级菜单，每个顶级菜单都有其子菜单
        return organizationDtos.Where(m => !m.SuperiorId.HasValue).ToList();
    }
}
