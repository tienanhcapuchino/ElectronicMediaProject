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

using ElectronicMedia.Core.Repository.DataContext;
using ElectronicMedia.Core.Repository.Domains;
using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Services.Service
{
    public class EmailTemplateDataUpgradeService : IDataUpgradeService
    {
        private readonly ElectronicMediaDbContext _context;
        private readonly ILogger<EmailTemplateDataUpgradeService> _logger;
        public EmailTemplateDataUpgradeService(ElectronicMediaDbContext context,
            ILogger<EmailTemplateDataUpgradeService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task UpgradeData()
        {
            await InitialEmailTemplate();
        }

        private async Task InitialEmailTemplate()
        {
            var emails = await _context.EmailTemplates.ToListAsync();
            EmailTemplate email = new EmailTemplate()
            {
                Name = EmailTemplateNameConstant.CommentNotificationName,
            };
            _logger.LogInformation("start to create email template!");
        }
    }
}