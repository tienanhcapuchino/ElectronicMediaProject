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
using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Services.Service
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly ElectronicMediaDbContext _context;
        public EmailTemplateService(ElectronicMediaDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(EmailTemplate entity, bool saveChange = true)
        {
            await _context.EmailTemplates.AddAsync(entity);
            bool result = true;
            if (saveChange)
            {
                result = await _context.SaveChangesAsync() > 0;
            }
            return result;
        }

        public async Task<bool> Delete(Guid id, bool saveChange = true)
        {
            var entity = await GetByIdAsync(id);
            _context.EmailTemplates.Remove(entity);
            bool result = true;
            if (saveChange)
            {
                result = await _context.SaveChangesAsync() > 0;
            }
            return result;
        }

        public Task<IEnumerable<EmailTemplate>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<EmailTemplate>> GetAllWithPaging(PageRequestBody requestBody)
        {
            var posts = await _context.EmailTemplates.ToListAsync();
            var result = QueryData<EmailTemplate>.QueryForModel(requestBody, posts).ToList();
            return PagedList<EmailTemplate>.ToPagedList(result, requestBody.Page, requestBody.Top);
        }

        public async Task<EmailTemplate> GetByIdAsync(Guid id)
        {
            var result = await _context.EmailTemplates.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<bool> Update(EmailTemplate entity, bool saveChange = true)
        {
            _context.EmailTemplates.Update(entity);
            bool result = true;
            if (saveChange)
            {
                result = await _context.SaveChangesAsync() > 0;
            }
            return result;
        }
    }
}
