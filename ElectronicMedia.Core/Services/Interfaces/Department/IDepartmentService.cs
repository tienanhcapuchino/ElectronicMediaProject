using ElectronicMedia.Core.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Services.Interfaces
{
    public interface IDepartmentService : ICoreRepository<Department>
    {
        Task<bool> AssignMemberToDepartment(Guid depId, List<Guid> membersId);
        Task<bool> AssignLeader(Guid depId, Guid leaderId);
    }
}
