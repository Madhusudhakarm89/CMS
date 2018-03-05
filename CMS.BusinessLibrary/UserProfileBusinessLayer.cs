using CMS.BusinessLibrary.ViewModels;
using CMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BusinessLibrary
{
    public interface IUserProfileBusinessLayer
    {
        Task<IEnumerable<UserProfileViewModel>> GetAllProfiles();
        Task<UserProfileViewModel> Find(string userId);
    }
    public sealed partial class UserProfileBusinessLayer : IUserProfileBusinessLayer
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileBusinessLayer()
        {
            this._userProfileRepository = new UserProfileRepository();
        }

        private IUserProfileRepository UserProfileRepository
        {
            get { return this._userProfileRepository; }
        }

        public async Task<IEnumerable<UserProfileViewModel>> GetAllProfiles()
        {
            return this.UserProfileRepository.AllRecords.Select(e =>
                new UserProfileViewModel
                {
                    Id = e.Id,
                    Name = e.Name
                });
        }

        public async Task<UserProfileViewModel> Find(string userId)
        {
            return this.UserProfileRepository.AllRecords.Where(e => e.Id == userId).Select(e =>
                new UserProfileViewModel
                {
                    Id = e.Id,
                    Name = e.Name
                }).FirstOrDefault();
        }
    }
}
