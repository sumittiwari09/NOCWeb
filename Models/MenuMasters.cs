using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class ShowMenuDropDown
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
    }
    public class MenuMasters
    {
        public int MenuId { get; set; }
        public string ControllerName { get; set; }
        public string ActionMethod { get; set; }
        public string MenuName { get; set; }
        public int Status { get; set; }
        public string PartyId { get; set; }
    }
    public class SubMenuMaster
    {
        public int SubMenuId { get; set; }
        public string ControllerName { get; set; }
        public string ActionMethod { get; set; }
        public int Status { get; set; }
        public int MenuId { get; set; }
        public string SubMenuName { get; set; }
        public string PartyId { get; set; }
        public string MenuName { get; set; }

    }
    public class RoleInformation
    {
        public int RoleinformationId { get; set; }
        public int DepartmentId { get; set; }
        public int GroupId { get; set; }
        public int RoleId { get; set; }
        public int Status { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
    }
    public class RoleMappingwithMenu
    {
        public int RoleMappingwithMenuid { get; set; }
        public int DepartmentId { get; set; }
        public int GroupId { get; set; }
        public int RoleId { get; set; }
        public int Status { get; set; }
        public int MenuId { get; set; }
        public string DepartmentName { get; set; }
        public string GroupName { get; set; }
        public string RoleName { get; set; }
        public string MenuName { get; set; }
    }
    public class MenuMappingwithSubmenu
    {
        public int MenuMappingwithSubmenuId { get; set; }
        public int DepartmentId { get; set; }
        public int GroupId { get; set; }
        public int RoleId { get; set; }
        public int Status { get; set; }
        public int MenuId { get; set; }
        public string DepartmentName { get; set; }
        public string GroupName { get; set; }
        public string RoleName { get; set; }
        public string MenuName { get; set; }
        public int SubMenuId { get; set; }
        public string SubMenuName { get; set; }
        public int InsertOperation { get; set; }
        public int UpdateOpertaion { get; set; }
        public int DeleteOperation { get; set; }
        public int ViewOperation { get; set; }
    }
    public class RoleMastertable
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int Status { get; set; }
        public string PartyId { get; set; }
    }
    public class MappingRoleWithDepartmentandGroup
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int GroupId { get; set; }
        public int RoleId { get; set; }
        public int levelId { get; set; }
        public int Status { get; set; }
        public string PartyId { get; set; }
        public string DepartmentName { get; set; }
        public string GroupName { get; set; }
        public string RoleName { get; set; }
    }
    public class UserPagingPermission
    {
        public int ID { get; set; }
        public string MenuName { get; set; }
        public string SubMenuName { get; set; }
        public int perInsert { get; set; }
        public int perDelete { get; set; }
        public int perEdit { get; set; }
        public int perView { get; set; }
        public int perStatus { get; set; }
        public int UserID { get; set; }
        public int GroupID { get; set; }
        public int? Inserting { get; set; }
        public int? Editing { get; set; }
        public int? Deleting { get; set; }
        public int? Viewing { get; set; }
        public int? Status { get; set; }
    }

}