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
 * Copy right 2023 - PRN231 - SU23 - Group 10. All Rights Reserved
 * 
 * Unpublished - All rights reserved under the copyright laws 
 * of the Government of Viet Nam
*********************************************************************/

using ElectronicMedia.Core.Common.Extension;
using ElectronicMedia.Core.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Repository.Models
{
    public class UserProfileModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Dob { get; set; }
        public Gender? Gender { get; set; }
        public string Image
        {
            get
            {
                if (string.IsNullOrEmpty(this.Image))
                {
                    return Convert.ToBase64String(CommonService.InitAvatarUser());
                }
                else
                {
                    return this.Image;
                }
            }
            set
            {
                if (!string.IsNullOrEmpty(this.Image))
                {
                    this.Image = value;
                }
                else
                {
                    this.Image = Convert.ToBase64String(CommonService.InitAvatarUser());
                }
            }
        }
    }
}
