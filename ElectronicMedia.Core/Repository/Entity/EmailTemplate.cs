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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Repository.Entity
{
    public class EmailTemplate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public MailType MailType { get; set; }
        public string? Description { get; set; }
        public string MailTo { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public bool IsUsed { get; set; }
        [Column("ModifedBy")]
        public string ModifiedBy { get; set; }
        public virtual UserIdentity User { get; set; }
    }
    public enum MailType : byte
    {
        PostPublished = 1,
        CommentNotification = 2,
        PendingForApprovalPost = 3,
        AddNewUser = 4,
        ForgotPassword = 5
    }
}
