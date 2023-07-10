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
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<UserIdentity> _userManager;
        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(EmailTemplateDataUpgradeService));
        public EmailTemplateDataUpgradeService(IEmailTemplateService emailTemplateService,
            IUserService userService, UserManager<UserIdentity> userManager)
        {
            _emailTemplateService = emailTemplateService;
            _userService = userService;
            _userManager = userManager;
        }
        public async Task UpgradeData()
        {
            await CreateSystemAccount();
            await InitialEmailTemplate();
        }

        private async Task InitialEmailTemplate()
        {
            try
            {
                _logger.Info("start to create email template!");
                List<EmailTemplate> emails = EmailTemplateConstants.GetAllEmailTemplateBuitIns();
                foreach (EmailTemplate email in emails)
                {
                    if (await _emailTemplateService.GetByIdAsync(email.Id) == null)
                    {
                        await _emailTemplateService.Add(email);
                    }
                }
                _logger.Info("finished to create email template!");
            }catch (Exception ex)
            {
                _logger.Error($"start to create email template: {ex.Message}");
            }
            
        }

        private async Task CreateSystemAccount()
        {
            _logger.Info("start to create system account user!");
            UserIdentity systemAccount = new UserIdentity()
            {
                Id = new Guid(EmailTemplateIdConstant.SystemAccountId).ToString(),
                FullName = "System account",
                Email = "systemaccount@gmail.com",
                UserName = "systemaccount",
                Dob = DateTime.Now,
                IsActived = true,
                Gender = Gender.Unknown,
                PhoneNumber = "noneedphone"
            };
            var user = await _userManager.FindByEmailAsync("systemaccount@gmail.com");
            if (user == null)
            {
                await _userManager.CreateAsync(systemAccount, "noneedpassword");
            }
            _logger.Info("finished to create system account user!");
        }
    }
}
