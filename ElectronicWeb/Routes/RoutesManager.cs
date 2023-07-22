﻿/*********************************************************************
 * 
 * PROPRIETARY and CONFIDENTIAL
 * 
 * This is licensed from, and is trade secret of:
 * 
 *          Group 10 - PRN231 - SU23
 *          FPT University, Education and Training zone
 *          Hoa Lac Hi-tech Park, Km29, Thang Long Highway
 *          Ha Noi, Viet Nam
 *          
 * Refer to your License Agreement for restrictions on use,
 * duplication, or disclosure
 * 
 * RESTRICTED RIGHTS LEGEND
 * 
 * Use, duplication or disclosure is the
 * subject to restriction in Articles 736 and 738 of the 
 * 2005 Civil Code, the Intellectual Property Law and Decree 
 * No. 85/2011/ND-CP amending and supplementing a number of 
 * articles of Decree 100/ND-CP/2006 of the Government of Viet Nam
 * 
 * 
 * Copy right 2023 © PRN231 - SU23 - Group 10 ®. All Rights Reserved
 * 
 * Unpublished - All rights reserved under the copyright laws 
 * of the Government of Viet Nam
*********************************************************************/

namespace ElectronicWeb.Routes
{
    public static class RoutesManager
    {
        private const string LocalHostDomain = "http://localhost:5243/api/";
        public static string GetUrlPattern(EndPoint endpoint)
        {
            return LocalHostDomain  + endpoint;
        }
        #region post
        public static string GetPostsWithPaging = $"{GetUrlPattern(EndPoint.Post)}/page";
        public static string GetPostById = $"{GetUrlPattern(EndPoint.Post)}/";
        public static string UpdatePost = $"{GetUrlPattern(EndPoint.Post)}/update";
        #endregion

        #region user
        public static string GetUerssWithPaging = $"{GetUrlPattern(EndPoint.User)}/getall";
        public static string UpdateRole = $"{GetUrlPattern(EndPoint.User)}/roleupdate";
        public static string Deactivate = $"{GetUrlPattern(EndPoint.User)}/deactivate";
        public static string AddNewUser = $"{GetUrlPattern(EndPoint.User)}/adduser";
        public static string ExportUsers = $"{GetUrlPattern(EndPoint.User)}/export";
        #endregion

        #region department
        public static string GetAllDepartments = $"{GetUrlPattern(EndPoint.Department)}/page";
        public static string AddDepartment = $"{GetUrlPattern(EndPoint.Department)}/add";
        public static string UpdateDepartment = $"{GetUrlPattern(EndPoint.Department)}/update";
        public static string DeleteDepartment = $"{GetUrlPattern(EndPoint.Department)}/delete";
        public static string ViewDetailDepartment = $"{GetUrlPattern(EndPoint.Department)}/detail";
        public static string KickMember = $"{GetUrlPattern(EndPoint.Department)}/kick";
        public static string AssignMember = $"{GetUrlPattern(EndPoint.Department)}/assignmember";
        public static string GetMembers = $"{GetUrlPattern(EndPoint.Department)}/members";
        public static string GetLeaders = $"{GetUrlPattern(EndPoint.Department)}/leaders";
        #endregion

        #region category
        public static string GetAllCategory = $"{GetUrlPattern(EndPoint.Category)}/category";
        public static string GetAllSubCategory = $"{GetUrlPattern(EndPoint.Category)}/subcategory/getall";
        #endregion
    }

    public enum EndPoint
    {
        Post,
        User,
        Category,
        Department,
    }
}
