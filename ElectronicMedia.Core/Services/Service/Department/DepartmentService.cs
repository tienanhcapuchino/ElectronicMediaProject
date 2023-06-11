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
    public class DepartmentService : IDepartmentService
    {
        private readonly IUserService _userService;
        private readonly ElectronicMediaDbContext _dbContext;
        public DepartmentService(IUserService userService, ElectronicMediaDbContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
        }

        public async Task<bool> Add(Department entity, bool saveChange = true)
        {
            await _dbContext.Departments.AddAsync(entity);
            bool result = true;
            if (saveChange)
            {
                result = await _dbContext.SaveChangesAsync() > 0;
            }
            return await Task.FromResult(result);
        }

        public async Task<bool> AssignLeader(Guid depId, Guid leaderId)
        {
            var user = await _userService.GetByIdAsync(leaderId);
            if (user.Role != RoleType.Leader) throw new Exception("user is not a leader to assign!");
            if (user.DepartmentId == null || user.DepartmentId != depId)
            {
                user.DepartmentId = depId;
                await _userService.Update(user, false);
            }
            bool result = await _dbContext.SaveChangesAsync() > 0;
            return await Task.FromResult(result);
        }

        public async Task<bool> AssignMemberToDepartment(Guid depId, List<Guid> membersId)
        {
            var members = await _userService.GetUsersByIds(membersId);
            if (members != null && members.Count > 0)
            {
                foreach (var item in members)
                {
                    if (item.Role != RoleType.Writer)
                    {
                        throw new Exception("You only can assign writer to become member of department!");
                    }
                    if (item.DepartmentId == null || item.DepartmentId != depId)
                    {
                        item.DepartmentId = depId;
                        await _userService.Update(item, false);
                    }
                }
            }
            bool result = await _dbContext.SaveChangesAsync() > 0;
            return await Task.FromResult(result);
        }

        public async Task<bool> Delete(Guid id, bool saveChange = true)
        {
            var users = await _dbContext.Users.Where(x => x.DepartmentId == id).ToListAsync();
            if (users != null && users.Any())
            {
                foreach (var item in users)
                {
                    item.DepartmentId = null;
                    await _userService.Update(item);
                }
            }
            bool result = true;
            _dbContext.Departments.Remove(await GetByIdAsync(id));
            if (saveChange) result = await _dbContext.SaveChangesAsync() > 0;
            return result;
        }

        public Task<IEnumerable<Department>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<Department>> GetAllWithPaging(PageRequestBody requestBody)
        {
            var departments = await _dbContext.Departments.ToListAsync();
            var result = QueryData<Department>.QueryForModel(requestBody, departments).ToList();
            return PagedList<Department>.ToPagedList(result, requestBody.Page, requestBody.Top);
        }

        public async Task<Department> GetByIdAsync(Guid id)
        {
            var result = await _dbContext.Departments.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<bool> Update(Department entity, bool saveChange = true)
        {
            _dbContext.Departments.Update(entity);
            bool result = true;
            if (saveChange)
            {
                result = await _dbContext.SaveChangesAsync() > 0;
            }
            return await Task.FromResult(result);
        }
    }
}
