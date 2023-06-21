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
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IUserService _userService;
        private readonly ILogger<EmailTemplateDataUpgradeService> _logger;
        public EmailTemplateDataUpgradeService(ILogger<EmailTemplateDataUpgradeService> logger,
            IEmailTemplateService emailTemplateService,
            IUserService userService)
        {
            _logger = logger;
            _emailTemplateService = emailTemplateService;
            _userService = userService;
        }
        public async Task UpgradeData()
        {
            await CreateSystemAccount();
            await InitialEmailTemplate();
        }

        private async Task InitialEmailTemplate()
        {
            _logger.LogInformation("start to create email template!");
            List<EmailTemplate> emails = EmailTemplateConstants.GetAllEmailTemplateBuitIns();
            foreach (EmailTemplate email in emails)
            {
                if (await _emailTemplateService.GetByIdAsync(email.Id) == null)
                {
                    await _emailTemplateService.Add(email);
                }
            }
            _logger.LogInformation("finished to create email template!");
        }

        private async Task CreateSystemAccount()
        {
            _logger.LogInformation("start to create system account user!");
            User systemAccount = new User()
            {
                Id = new Guid(EmailTemplateIdConstant.SystemAccountId),
                FullName = "System account",
                Email = "systemaccount@gmail.com",
                UserName = "systemaccount",
                Password = "noneedpassword",
                Dob = DateTime.Now,
                Role = RoleType.Admin,
                IsActived = true,
                Gender = Gender.Unknown,
                PhoneNumber = "noneedphone"
            };
            var userEntity = await _userService.GetByIdAsync(systemAccount.Id);
            if (userEntity == null)
            {
                await _userService.Add(systemAccount);
            }
            _logger.LogInformation("finished to create system account user!");
        }
    }
}
