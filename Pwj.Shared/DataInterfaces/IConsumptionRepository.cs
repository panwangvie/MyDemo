using Pwj.Shared.Dto;
using Pwj.Shared.HttpContact;
using Pwj.Shared.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwj.Shared.DataInterfaces
{
    /// <summary>
    /// 功能描述    ：IConsumptionRepository  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/2/4 17:23:24 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/2/4 17:23:24 
    /// </summary>
    public interface IConsumptionRepository<T>
    {
        Task<BaseResponse<IPagedList<T>>> GetAllListAsync(QueryParameters parameters);

        Task<BaseResponse<T>> GetAsync(int id);

        Task<BaseResponse> SaveAsync(T model);

        Task<BaseResponse> AddAsync(T model);

        Task<BaseResponse> DeleteAsync(int id);

        Task<BaseResponse> UpdateAsync(T model);
    }

    public interface IUserRepository : IConsumptionRepository<UserDto>
    {  












                   

        Task<BaseResponse<UserInfoDto>> LoginAsync(string account, string passWord);

        /// <summary>
        /// 获取用户的所属权限信息
        /// </summary>
        Task<BaseResponse> GetUserPermByAccountAsync(string account);

       // Task<BaseResponse<List<AuthItem>>> GetAuthListAsync();
    }

}
