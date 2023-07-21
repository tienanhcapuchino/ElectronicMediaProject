/*********************************************************************
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

using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Services.Service
{
    public class UserExcelService : IExcelService<UserIdentity>
    {
        private const string SHEET_NAME = "Users";
        public DataTable ExportToExcel(List<UserIdentity> values)
        {
            DataTable dt = new DataTable(SHEET_NAME);
            dt.Columns.AddRange(new DataColumn[8]
            {
                new DataColumn("No"),
                new DataColumn("FullName"),
                new DataColumn("Dob"),
                new DataColumn("IsActive"),
                new DataColumn("Department"),
                new DataColumn("Username"),
                new DataColumn("Email"),
                new DataColumn("Phonenumber"),
            });
            int i = 1;
            foreach (UserIdentity user in values)
            {
                dt.Rows.Add(
                    i,
                    user.FullName,
                    user.Dob.ToString("dd/MM/yyyy"),
                    user.IsActived ? "Actived" : "Deactived",
                    user.Department != null ? user.Department.Name : "None",
                    user.UserName,
                    user.Email,
                    user.PhoneNumber
                    );
                i++;
            }
            return dt;
        }
    }
}
