using Entities.Models;

namespace Contracts
{
    public interface IUserDetailRepository
    {
        Task<bool> ExistsUserDetailAsync(UserDetail userDetail, bool trackChanges);
        Task<UserDetail> GetUserDetailByUserIdAsync(string userId, bool trackChanges);
        void CreateUserDetail(UserDetail userDetail);
        void DeleteUserDetail(UserDetail userDetail);
    }
}
