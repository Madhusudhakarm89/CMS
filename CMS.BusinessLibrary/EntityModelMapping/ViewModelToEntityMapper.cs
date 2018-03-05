
namespace CMS.BusinessLibrary.EntityModelMapping
{
    #region Namespaces
    using CMS.BusinessLibrary.ViewModels;
    using CMS.Entity;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    #endregion

    public static class ViewModelToEntityMapper
    {
        public static ClaimNote Map(ClaimNotesViewModel viewModel)
        {
            if (viewModel == null)
                return null;

            return new ClaimNote
            {
                NoteId = viewModel.NoteId,
                ClaimId = viewModel.ClaimId,
                Title = viewModel.Title,
                Description = viewModel.Description,
                IsTask = !string.IsNullOrWhiteSpace(viewModel.Type) && viewModel.Type.Equals("Task", StringComparison.InvariantCultureIgnoreCase) ? true : false,
                TaskDueDate = !string.IsNullOrWhiteSpace(viewModel.Type) && viewModel.Type.Equals("Task", StringComparison.InvariantCultureIgnoreCase) ? DateTime.ParseExact(viewModel.TaskEndDate, "M/d/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null,
                AssignedTo = viewModel.AssignedToUser != null ? viewModel.AssignedToUser.UserId : null,
                CreatedDate = !string.IsNullOrWhiteSpace(viewModel.CreatedDate) ? viewModel.CreatedDate : DateTime.Now.Date.ToString("M/d/yyyy"),
                CreatedBy = viewModel.CreatedBy,
                CreatedOn = viewModel.CreatedOn,
                LastModifiedBy = viewModel.LastModifiedBy,
                LastModifiedOn = viewModel.LastModifiedOn,
                IsActive = viewModel.IsActive
            };
        }

        public static ClaimNote Map(ClaimNotesViewModel viewModel, ClaimNote entity)
        {
            if (viewModel == null || entity == null)
                return null;

            entity.NoteId = viewModel.NoteId;
            entity.ClaimId = viewModel.ClaimId;
            entity.Title = viewModel.Title;
            entity.Description = viewModel.Description;
            entity.IsTask = !string.IsNullOrWhiteSpace(viewModel.Type) && viewModel.Type.Equals("Task", StringComparison.InvariantCultureIgnoreCase) ? true : false;
            entity.TaskDueDate = !string.IsNullOrWhiteSpace(viewModel.Type) && viewModel.Type.Equals("Task", StringComparison.InvariantCultureIgnoreCase) ? DateTime.ParseExact(viewModel.TaskEndDate, "M/d/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
            entity.AssignedTo = viewModel.AssignedToUser != null ? viewModel.AssignedToUser.UserId : null;
            entity.CreatedDate = !string.IsNullOrWhiteSpace(viewModel.CreatedDate) ? viewModel.CreatedDate : DateTime.Now.Date.ToString("M/d/yyyy");
            entity.CreatedBy = viewModel.CreatedBy;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.LastModifiedBy = viewModel.LastModifiedBy;
            entity.LastModifiedOn = viewModel.LastModifiedOn;
            entity.IsActive = viewModel.IsActive;

            return entity;
        }

        public static ClaimDocumentMapping Map(ClaimDocumentViewModel viewModel)
        {
            if (viewModel == null)
                return null;

            return new ClaimDocumentMapping
            {
                DocumentId = viewModel.DocumentId,
                ClaimId = viewModel.ClaimId,
                FileType = viewModel.FileType,
                FileName = viewModel.FileName,
                FileLocation = viewModel.FileLocation,
                FileDisplayName = viewModel.FileDisplayName,
                CreatedBy = viewModel.CreatedBy,
                CreatedOn = viewModel.CreatedOn,
                LastModifiedBy = viewModel.LastModifiedBy,
                LastModifiedOn = viewModel.LastModifiedOn,
                IsActive = viewModel.IsActive
            };
        }

        public static ClaimDocumentMapping Map(ClaimDocumentViewModel viewModel, ClaimDocumentMapping entity)
        {
            if (viewModel == null || entity == null)
                return null;

            entity.DocumentId = viewModel.DocumentId;
            entity.ClaimId = viewModel.ClaimId;
            entity.FileType = viewModel.FileType;
            entity.FileName = viewModel.FileName;
            entity.FileLocation = viewModel.FileLocation;
            entity.FileDisplayName = viewModel.FileDisplayName;
            entity.CreatedBy = viewModel.CreatedBy;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.LastModifiedBy = viewModel.LastModifiedBy;
            entity.LastModifiedOn = viewModel.LastModifiedOn;
            entity.IsActive = viewModel.IsActive;

            return entity;
        }

        public static AspNetUsersDocumentMapping Map(AspNetUsersDocumentViewModel viewModel)
        {
            if (viewModel == null)
                return null;

            return new AspNetUsersDocumentMapping
            {
                DocumentId = viewModel.DocumentId,
                UserId = viewModel.UserId,
                FileType = viewModel.FileType,
                FileName = viewModel.FileName,
                FileLocation = viewModel.FileLocation,
                FileDisplayName = viewModel.FileDisplayName,
                CreatedBy = viewModel.CreatedBy,
                CreatedOn = viewModel.CreatedOn,
                LastModifiedBy = viewModel.LastModifiedBy,
                LastModifiedOn = viewModel.LastModifiedOn,
                IsActive = viewModel.IsActive
            };
        }

        public static AspNetUsersDocumentMapping Map(AspNetUsersDocumentViewModel viewModel, AspNetUsersDocumentMapping entity)
        {
            if (viewModel == null || entity == null)
                return null;

            entity.DocumentId = viewModel.DocumentId;
            entity.UserId = viewModel.UserId;
            entity.FileType = viewModel.FileType;
            entity.FileName = viewModel.FileName;
            entity.FileLocation = viewModel.FileLocation;
            entity.FileDisplayName = viewModel.FileDisplayName;
            entity.CreatedBy = viewModel.CreatedBy;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.LastModifiedBy = viewModel.LastModifiedBy;
            entity.LastModifiedOn = viewModel.LastModifiedOn;
            entity.IsActive = viewModel.IsActive;

            return entity;
        }
        public static InvoiceDocumentMapping Map(InvoiceDocumentViewModel viewModel)
        {
            if (viewModel == null)
                return null;

            return new InvoiceDocumentMapping
            {
                DocumentId = viewModel.DocumentId,
                UserId = viewModel.UserId,
                FileType = viewModel.FileType,
                FileName = viewModel.FileName,
                FileLocation = viewModel.FileLocation,
                CreatedBy = viewModel.CreatedBy,
                CreatedOn = Convert.ToDateTime(viewModel.CreatedOn),
                LastModifiedBy = viewModel.LastModifiedBy,
                LastModifiedOn = Convert.ToDateTime(viewModel.LastModifiedOn),
                IsActive = viewModel.IsActive,
                ClaimId=viewModel.ClaimId
            };
        }

        public static InvoiceDocumentMapping Map(InvoiceDocumentViewModel viewModel, InvoiceDocumentMapping entity)
        {
            if (viewModel == null || entity == null)
                return null;

            entity.DocumentId = viewModel.DocumentId;
            entity.UserId = viewModel.UserId;
            entity.FileType = viewModel.FileType;
            entity.FileName = viewModel.FileName;
            entity.FileLocation = viewModel.FileLocation;
            entity.CreatedBy = viewModel.CreatedBy;
            entity.CreatedOn = Convert.ToDateTime(viewModel.CreatedOn);
            entity.LastModifiedBy = viewModel.LastModifiedBy;
            entity.LastModifiedOn = Convert.ToDateTime(viewModel.LastModifiedOn);
            entity.IsActive = viewModel.IsActive;
            entity.ClaimId = viewModel.ClaimId;

            return entity;
        }
        public static Status Map(ClaimStatusViewModel viewModel)
        {
            if (viewModel == null)
                return null;

            return new Status
            {
                Id = viewModel.ClaimStatusId,
                Name = viewModel.Status,
                SortOrder = viewModel.SortOrder,
                IsActive = viewModel.IsActive
            };
        }

        public static Status Map(ClaimStatusViewModel viewModel, Status entity)
        {
            if (viewModel == null || entity == null)
                return null;

            entity.Id = viewModel.ClaimStatusId;
            entity.Name = viewModel.Status;
            entity.SortOrder = viewModel.SortOrder;
            entity.IsActive = viewModel.IsActive;

            return entity;
        }

        public static TimeLog Map(TimeLogViewModel viewModel)
        {
            if (viewModel == null)
                return null;

            return new TimeLog
            {
                ClaimId = viewModel.ClaimId,
                ServiceItemId = viewModel.ServiceItemId,
                Quantity = viewModel.Quantity,
                HoursSpent = viewModel.HoursSpent,
                Comment = viewModel.Comment,
                TaskDate = !string.IsNullOrWhiteSpace(viewModel.TaskDate) ? DateTime.ParseExact(viewModel.TaskDate, "M/d/yyyy", CultureInfo.InvariantCulture) : DateTime.Now,
                LoggedOn = !string.IsNullOrWhiteSpace(viewModel.LoggedOn) ? DateTime.ParseExact(viewModel.LoggedOn, "M/d/yyyy", CultureInfo.InvariantCulture) : DateTime.Now,
                AdjusterId = viewModel.Adjuster != null ? viewModel.Adjuster.UserId : null,
                CreatedBy = viewModel.CreatedBy,
                CreatedOn = viewModel.CreatedOn,
                LastModifiedBy = viewModel.LastModifiedBy,
                LastModifiedOn = viewModel.LastModifiedOn,
                IsActive = viewModel.IsActive
            };
        }

        public static TimeLog Map(TimeLogViewModel viewModel, TimeLog entity)
        {
            if (viewModel == null || entity == null)
                return null;

            entity.ClaimId = viewModel.ClaimId;
            entity.ServiceItemId = viewModel.ServiceItemId;
            entity.Quantity = viewModel.Quantity;
            entity.HoursSpent = viewModel.HoursSpent;
            entity.Comment = viewModel.Comment;
            entity.TaskDate = !string.IsNullOrWhiteSpace(viewModel.TaskDate) ? DateTime.ParseExact(viewModel.TaskDate, "M/d/yyyy", CultureInfo.InvariantCulture) : DateTime.Now.Date;
            entity.LoggedOn = !string.IsNullOrWhiteSpace(viewModel.LoggedOn) ? DateTime.ParseExact(viewModel.LoggedOn, "M/d/yyyy", CultureInfo.InvariantCulture) : DateTime.Now.Date;
            entity.AdjusterId = viewModel.Adjuster != null ? viewModel.Adjuster.UserId : null;
            entity.LastModifiedBy = viewModel.LastModifiedBy;
            entity.LastModifiedOn = viewModel.LastModifiedOn;
            entity.IsActive = viewModel.IsActive;
           

            return entity;
        }

        public static ServiceItem Map(ServiceItemViewModel viewModel)
        {
            if (viewModel == null)
                return null;

            return new ServiceItem
            {
                ServiceItemId = viewModel.ServiceItemId,
                ServiceItemName = viewModel.ServiceItemName,
                ServiceItemDescription = viewModel.ServiceItemDescription,
                ServiceCategoryId = viewModel.ServiceCategoryId,
                IsHourBased = viewModel.IsHourBased,
                DefaultQuantity = viewModel.DefaultQuantity,
                DefaultFee = viewModel.DefaultFee,
                MinimumFee = viewModel.MinimumFee,
                CreatedDate = viewModel.CreatedOn,
                CreatedBy = viewModel.CreatedBy,
                LastModifiedDate = viewModel.LastModifiedOn,
                LastModifiedBy = viewModel.LastModifiedBy,
                IsActive = viewModel.IsActive
            };
        }

        public static ServiceItem Map(ServiceItemViewModel viewModel, ServiceItem entity)
        {
            if (viewModel == null || entity == null)
                return null;

            entity.ServiceItemId = viewModel.ServiceItemId;
            entity.ServiceItemName = viewModel.ServiceItemName;
            entity.ServiceItemDescription = viewModel.ServiceItemDescription;
            entity.ServiceCategoryId = viewModel.ServiceCategoryId;
            entity.IsHourBased = viewModel.IsHourBased;
            entity.DefaultQuantity = viewModel.DefaultQuantity;
            entity.DefaultFee = viewModel.DefaultFee;
            entity.MinimumFee = viewModel.MinimumFee;
            entity.CreatedDate = viewModel.CreatedOn;
            entity.CreatedBy = viewModel.CreatedBy;
            entity.LastModifiedDate = viewModel.LastModifiedOn;
            entity.LastModifiedBy = viewModel.LastModifiedBy;
            entity.IsActive = viewModel.IsActive;

            return entity;
        }

        public static ServiceCategory Map(ServiceCategoryViewModel viewModel)
        {
            if (viewModel == null)
                return null;

            return new ServiceCategory
            {
                ServiceCategoryId = viewModel.ServiceCategoryId,
                ServiceCategoryName = viewModel.ServiceCategoryName,
                SortOrder = viewModel.SortOrder,
                IsActive = viewModel.IsActive
            };
        }

        public static ServiceCategory Map(ServiceCategoryViewModel viewModel, ServiceCategory entity)
        {
            if (viewModel == null || entity == null)
                return null;

            entity.ServiceCategoryId = viewModel.ServiceCategoryId;
            entity.ServiceCategoryName = viewModel.ServiceCategoryName;
            entity.SortOrder = viewModel.SortOrder;
            entity.IsActive = viewModel.IsActive;

            return entity;
        }

        public static TypeOfLoss Map(TypeOfLossViewModel viewModel)
        {
            if (viewModel == null)
                return null;

            return new TypeOfLoss
            {
                Name = viewModel.LossTypeName,
                Description = null,
                SortOrder = viewModel.SortOrder,
                CreatedBy= viewModel.CreatedBy,
                CreatedDate= viewModel.CreatedOn,
                LastModifiedBy = viewModel.LastModifiedBy,
                LastModifiedDate = viewModel.LastModifiedOn,                
                IsActive = viewModel.IsActive,
              
            };
        }

        public static TypeOfLoss Map(TypeOfLossViewModel viewModel, TypeOfLoss entity)
        {
            if (viewModel == null || entity == null)
                return null;

            entity.Id = viewModel.LossTypeId;
            entity.Name = viewModel.LossTypeName;
            entity.SortOrder = viewModel.SortOrder;
             entity.LastModifiedBy = viewModel.LastModifiedBy;
             entity.LastModifiedDate = viewModel.LastModifiedOn;  
            entity.IsActive = viewModel.IsActive;

            return entity;
        }

        public static TimeLogUnit Map(TimeLogUnitViewModel viewModel)
        {
            if (viewModel == null)
                return null;

            return new TimeLogUnit
            {
                Id = viewModel.UnitId,
                Name = viewModel.UnitName,
                SortOrder = viewModel.SortOrder,
                IsActive = viewModel.IsActive
            };
        }
        public static TimeLogUnit Map(TimeLogUnitViewModel viewModel, TimeLogUnit entity)
        {
            if (viewModel == null || entity == null)
                return null;

            entity.Id = viewModel.UnitId;
            entity.Name = viewModel.UnitName;
            entity.SortOrder = viewModel.SortOrder;
            entity.IsActive = viewModel.IsActive;

            return entity;
        }

        public static FileNameCode Map(FileNameViewModel viewModel)
        {
            if (viewModel == null)
                return null;

            return new FileNameCode
            {
                Id = viewModel.FileNameId,
                LocationName = viewModel.LocationName,
                FileNumberPrefix = viewModel.FileNumberPrefix,
                CreatedBy = viewModel.CreatedBy,
                CreatedOn = viewModel.CreatedOn,
                LastModifiedBy = viewModel.LastModifiedBy,
                LastModifiedOn = viewModel.LastModifiedOn,
                IsActive = viewModel.IsActive
            };
        }
        public static FileNameCode Map(FileNameViewModel viewModel, FileNameCode entity)
        {
            if (viewModel == null || entity == null)
                return null;

            entity.Id = viewModel.FileNameId;
            entity.LocationName = viewModel.LocationName;
            entity.FileNumberPrefix = viewModel.FileNumberPrefix;
            entity.CreatedBy = viewModel.CreatedBy;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.LastModifiedBy = viewModel.LastModifiedBy;
            entity.LastModifiedOn = viewModel.LastModifiedOn;
            entity.IsActive = viewModel.IsActive;

            return entity;
        }

        public static Invoice Map(InvoiceViewModel viewModel)
        {
            if (viewModel == null)
                return null;

            return new Invoice
            {
                InvoiceId = viewModel.InvoiceId,
                ClaimId = viewModel.ClaimId,
                InvoiceNumber = viewModel.InvoiceNumber,
                InvoiceDate = !string.IsNullOrWhiteSpace(viewModel.InvoiceDate) ? DateTime.ParseExact(viewModel.InvoiceDate, "M/d/yyyy", CultureInfo.InvariantCulture) : DateTime.Now,
                DueDate = !string.IsNullOrWhiteSpace(viewModel.DueDate) ? DateTime.ParseExact(viewModel.DueDate, "M/d/yyyy", CultureInfo.InvariantCulture) : DateTime.Now,
                InvoiceTotal = viewModel.InvoiceTotal,
                CreatedBy = viewModel.CreatedBy,
                CreatedOn = viewModel.CreatedOn,
                LastModifiedBy = viewModel.LastModifiedBy,
                LastModifiedOn = viewModel.LastModifiedOn,
                IsActive = viewModel.IsActive,

                InvoiceTimelogs = viewModel.TimeLogs == null
                                    ? null
                                    : viewModel.TimeLogs.Select(e => new Mapping_InvoiceTimelog
                                    {
                                        Id = e.InvoiceTimelogId,
                                        InvoiceId = viewModel.InvoiceId,
                                        TimelogId = e.TimeLogId,
                                        ServiceTotal = e.TotalAmount,
                                        CreatedBy = viewModel.CreatedBy,
                                        CreatedOn = viewModel.CreatedOn,
                                        LastModifiedBy = viewModel.LastModifiedBy,
                                        LastModifiedOn = viewModel.LastModifiedOn,
                                        IsActive = viewModel.IsActive
                                       
                                           
                                    }).ToList()
            };
        }
        public static Invoice Map(InvoiceViewModel viewModel, Invoice entity)
        {
            if (viewModel == null || entity == null)
                return null;

            entity.InvoiceId = viewModel.InvoiceId;
            entity.ClaimId = viewModel.ClaimId;
            entity.InvoiceNumber = viewModel.InvoiceNumber;
            entity.InvoiceDate = !string.IsNullOrWhiteSpace(viewModel.InvoiceDate) ? DateTime.ParseExact(viewModel.InvoiceDate, "M/d/yyyy", CultureInfo.InvariantCulture) : DateTime.Now;
            entity.DueDate = !string.IsNullOrWhiteSpace(viewModel.DueDate) ? DateTime.ParseExact(viewModel.DueDate, "M/d/yyyy", CultureInfo.InvariantCulture) : DateTime.Now;
            entity.InvoiceTotal = viewModel.InvoiceTotal;
            entity.CreatedBy = viewModel.CreatedBy;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.LastModifiedBy = viewModel.LastModifiedBy;
            entity.LastModifiedOn = viewModel.LastModifiedOn;
            entity.IsActive = viewModel.IsActive;

            entity.InvoiceTimelogs = viewModel.TimeLogs == null
                                ? null
                                : viewModel.TimeLogs.Select(e => new Mapping_InvoiceTimelog
                                {
                                    Id = e.InvoiceTimelogId,
                                    InvoiceId = viewModel.InvoiceId,
                                    TimelogId = e.TimeLogId,
                                    ServiceTotal = e.TotalAmount,
                                    CreatedBy = viewModel.CreatedBy,
                                    CreatedOn = viewModel.CreatedOn,
                                    LastModifiedBy = viewModel.LastModifiedBy,
                                    LastModifiedOn = viewModel.LastModifiedOn,
                                    IsActive = viewModel.IsActive
                                }).ToList();

            return entity;
        }

        public static TaxSetting Map(TaxSettingViewModel viewModel)
        {
            if (viewModel == null)
                return null;

            return new TaxSetting
            {
                TaxRate = viewModel.TaxRate,
                Id = viewModel.Id,
                CountryId = viewModel.CountryId,
                ProvinceId = viewModel.ProvinceId,
                CreatedDate = viewModel.CreatedOn,
                CreatedBy = viewModel.CreatedBy,
                LastModifiedDate = viewModel.LastModifiedOn,
                LastModifiedBy = viewModel.LastModifiedBy,
                IsActive = viewModel.IsActive
            };
        }

        public static TaxSetting Map(TaxSettingViewModel viewModel, TaxSetting entity)
        {
            if (viewModel == null || entity == null)
                return null;

            entity.TaxRate = viewModel.TaxRate;
            entity.Id = viewModel.Id;
            entity.CountryId = viewModel.CountryId;
            entity.ProvinceId = viewModel.ProvinceId;
            entity.CreatedDate = viewModel.CreatedOn;
            entity.CreatedBy = viewModel.CreatedBy;
            entity.LastModifiedDate = viewModel.LastModifiedOn;
            entity.LastModifiedBy = viewModel.LastModifiedBy;
            entity.IsActive = viewModel.IsActive;

            return entity;
        }

        public static TimeLog IsBilledMap(TimeLogViewModel viewModel, TimeLog entity)
        {
            if (viewModel == null || entity == null)
                return null;
            entity.LastModifiedBy = viewModel.LastModifiedBy;
            entity.LastModifiedOn = DateTime.Now;
            entity.IsBilled = viewModel.IsBilled;

            return entity;
        }
    }
}
