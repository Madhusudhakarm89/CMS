using CMS.BusinessLibrary.ViewModels;
using CMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BusinessLibrary.EntityModelMapping
{
    public static class UserEntityToViewModelMapper
    {
        public static UserViewModel Map(AspNetUser entity)
        {
            if (entity == null)
                return null;

            return new UserViewModel
            {
                UserId = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Phone = entity.PhoneNumber,
                Cell = entity.CellNumber,
                Department = entity.Department,
                CompanyName = entity.CompanyName,
                CompanyId = Convert.ToInt32(entity.CompanyName),
                Status = entity.Status,
                StatusId = Convert.ToInt32(entity.Status),
                UserTypeId=Convert.ToInt32(entity.UserType),
                ProfileTypeId=Convert.ToInt32(entity.UserProfile),
                Street = entity.Street,
                City = entity.City,
                CountryName = entity.Country.CountryName,
                CountryId = (int)entity.CountryId,
                ProvinceName = entity.Province.ProvinceName,
                ProvinceId = (int)entity.ProvinceId,
                PostalCode = entity.PostalCode,
                createdDate = entity.CreateDate != null ? entity.CreateDate.Date.ToString("yyyy/MM/dd") : string.Empty,
                ReceiveAlerts = entity.IsReceiveAlerts,
                
            };
        }

        public static IEnumerable<UserViewModel> Map(IEnumerable<AspNetUser> entities)
        {
            if (entities == null || (entities != null && !entities.Any()))
                return null;

            return entities.Select(e =>
                new UserViewModel
                {
                    UserId = e.Id,
                    UserName = e.UserName,
                    Email = e.Email,
                    Salutation = e.Salutation,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Department = e.Department,
                    CompanyName = e.CompanyName,
                    Status = e.Status == "0" ? "Inactive" : "Active",
                    createdDate = e.CreateDate != null ? e.CreateDate.Date.ToString("yyyy/MM/dd") : string.Empty,
                    UserTypeName = e.UserProfile == "1" ? "Standard User" : "System Administrator"

                }).OrderBy(e => e.createdDate);
        }

        public static AspNetUsersDocumentViewModel Map(AspNetUsersDocumentMapping entity)
        {
            if (entity == null)
                return null;

            return new AspNetUsersDocumentViewModel
            {
                DocumentId = entity.DocumentId,
                UserId = entity.UserId,
                FileType = entity.FileType,
                FileName = entity.FileName,
                FileLocation = entity.FileLocation,
                FileDisplayName = entity.FileDisplayName,
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                LastModifiedBy = entity.LastModifiedBy,
                LastModifiedOn = entity.LastModifiedOn,
                IsActive = entity.IsActive
            };
        }

        public static IEnumerable<AspNetUsersDocumentViewModel> Map(IEnumerable<AspNetUsersDocumentMapping> entities)
        {
            if (entities == null || (entities != null && !entities.Any()))
                return null;

            return entities.Select(e =>
                new AspNetUsersDocumentViewModel
                {
                    DocumentId = e.DocumentId,
                    UserId = e.UserId,
                    FileType = e.FileType,
                    FileName = e.FileName,
                    FileLocation = e.FileLocation,
                    FileDisplayName = e.FileDisplayName,
                    CreatedBy = e.CreatedBy,
                    CreatedOn = e.CreatedOn,
                    LastModifiedBy = e.LastModifiedBy,
                    LastModifiedOn = e.LastModifiedOn,
                    IsActive = e.IsActive
                }).OrderBy(e => e.FileDisplayName).ToList();
        }

    }
}
