
namespace CMS.BusinessLibrary
{
    #region Namespace
    using CMS.BusinessLibrary.EntityModelMapping;
    using CMS.BusinessLibrary.ViewModels;
    using CMS.Entity;
    using CMS.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    #endregion

    #region Interface
    public interface IUserBusinessLayer
    {
        Task<IEnumerable<UserViewModel>> GetAllUsers();
        Task<UserViewModel> Find(string userID);
        Task<IEnumerable<UserViewModel>> Find(List<Expression<Func<AspNetUser, bool>>> filterPredicates);
        Task<bool> Delete(string id);
        Task<UserViewModel> Update(UserViewModel viewModel);

    }

    #endregion

    #region Class
    public sealed partial class UserBusinessLayer : IUserBusinessLayer
    {
        private readonly IUserRepository _userRepository;


        public UserBusinessLayer()
        {
            this._userRepository = new UserRepository();
        }

        private IUserRepository UserRepository
        {
            get { return this._userRepository; }
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsers()
        {
            return EntityToViewModelMapper.Map(this._userRepository.AllRecords.ToList());
        }

        public async Task<UserViewModel> Find(string userID)
        {
            return EntityToViewModelMapper.Map(this._userRepository.Find(userID));
        }

        public async Task<IEnumerable<UserViewModel>> Find(List<Expression<Func<AspNetUser, bool>>> filterPredicates)
        {

            return EntityToViewModelMapper.Map(this.UserRepository.Find(filterPredicates).ToList());
        }

        
        public async Task<UserViewModel> Update(UserViewModel updateModel)
        {
            var userDetails = this._userRepository.Find(updateModel.UserId);
            if (userDetails != null)
            {
                 userDetails.Id=updateModel.UserId;
                 userDetails.UserName = updateModel.Email;
                 userDetails.Salutation = "mr";
                 userDetails.FirstName = updateModel.FirstName;
                 userDetails.LastName = updateModel.LastName;
                 userDetails.Email = updateModel.Email;
                 userDetails.IsReceiveAlerts = updateModel.ReceiveAlerts;
                 userDetails.Street = updateModel.Street;
                 userDetails.City = updateModel.City;
                 if( updateModel.ProvinceId==0)
                 {
                     userDetails.ProvinceId = null;
                 }
                 else
                 {
                     userDetails.ProvinceId = updateModel.ProvinceId;
                 }
                 if (updateModel.CountryId == 0)
                 {
                     userDetails.CountryId = null;
                 }
                 else
                 {
                     userDetails.CountryId = updateModel.CountryId;
                 }
                 userDetails.PostalCode = updateModel.PostalCode;
                 userDetails.Department = updateModel.Department;
                 userDetails.UserType = updateModel.UserTypeName;
                 userDetails.UserProfile = updateModel.ProfileType;
                 userDetails.CompanyName = updateModel.CompanyName;
                 userDetails.Status = Convert.ToString(updateModel.StatusId);
                 userDetails.PhoneNumber = updateModel.Phone;
                 userDetails.CellNumber = updateModel.Cell;
                 userDetails.LastModifiedDate = DateTime.Now;
                userDetails = this._userRepository.Update(userDetails);

                return new UserViewModel
                {
                        UserId=userDetails.Id,
                };
            }
            else
            {
                updateModel.UserId = "";
            }

            return updateModel;
        }

        public async Task<bool> Delete(string id)
        {
            var userDetails = this._userRepository.Find(id);
            if (userDetails != null)
            {
                userDetails.Id = id;
                userDetails.Status ="0"; //0 for inactive
                userDetails.IsActive = false;
                userDetails = this._userRepository.Update(userDetails);

                if(userDetails != null)
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
            return false;
        }

    }

    #endregion
}
