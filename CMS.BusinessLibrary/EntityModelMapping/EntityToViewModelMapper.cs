
namespace CMS.BusinessLibrary.EntityModelMapping
{
    #region Namespace
    using CMS.BusinessLibrary.ViewModels;
    using CMS.Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    public static class EntityToViewModelMapper
    {
        public static ClaimViewModel Map(Claim entity)
        {
            if (entity == null)
                return null;

            return new ClaimViewModel
            {
                ClaimId = entity.ClaimId,
                FileNo = entity.FileNo,
                ClaimNo = entity.ClaimNo,
                CompanyId = entity.CompanyId,
                CompanyName = entity.Company.CompanyName,
                Company = entity.Company != null ? new CompanyViewModel { CompanyId = entity.Company.AccountId, CompanyName = entity.Company.CompanyName } : null,
                ContactId = entity.ContactId,
                ContactName = entity.Contact != null ? String.Format("{0} {1}", entity.Contact.FirstName, entity.Contact.LastName) : string.Empty,
                Contact = entity.Contact == null ? null : new ContactViewModel { ContactId = entity.Contact.ContactId, FirstName = entity.Contact.FirstName, LastName = entity.Contact.LastName, CompanyId = entity.Contact.CompanyId },
                ClaimantId = entity.ClaimantId,
                ClaimantName = entity.Claimant == null ? string.Empty : String.Format("{0} {1}", entity.Claimant.FirstName, entity.Claimant.LastName),
                Claimant = entity.Claimant == null ? null : new ContactViewModel { ContactId = entity.Contact.ContactId, FirstName = entity.Contact.FirstName, LastName = entity.Contact.LastName, CompanyId = entity.Contact.CompanyId },
                PolicyNo = entity.PolicyNo,
                PolicyEffectiveDate = entity.PolicyEffectiveDate != null ? entity.PolicyEffectiveDate.Value.Date.ToString("M/d/yyyy") : string.Empty,
                PolicyExpirationDate = entity.PolicyExpirationDate != null ? entity.PolicyExpirationDate.Value.Date.ToString("M/d/yyyy") : string.Empty,
                LoanNo = entity.LoanNo,
                Mortgagee = entity.Mortgagee,
                ReceivedDate = entity.ReceivedDate.Date.ToString("M/d/yyyy"),
                LossDate = entity.LossDate.Date.ToString("M/d/yyyy"),
                LossType = entity.LossType,
                AdjustmentType = entity.AdjustmentType,
                LossTotalUrgent = entity.LossTotalUrgent,
                MouldClaim = entity.MouldClaim,
                ManagerId = entity.ManagerId,
                ManagerName = entity.Manager != null ? String.Format("{0} {1}", entity.Manager.FirstName, entity.Manager.LastName) : string.Empty,
                Manager = entity.Manager == null ? null : new UserViewModel { UserId = entity.Manager.Id, FirstName = entity.Manager.FirstName, LastName = entity.Manager.LastName },
                AdjusterId = entity.AdjusterId,
                AdjusterName = entity.Adjuster != null ? String.Format("{0} {1}", entity.Adjuster.FirstName, entity.Adjuster.LastName) : string.Empty,
                Adjuster = entity.Adjuster == null ? null : new UserViewModel { UserId = entity.Adjuster.Id, FirstName = entity.Adjuster.FirstName, LastName = entity.Adjuster.LastName },
                Branch = entity.Branch,
                LossDescription = entity.LossDescription,
                Instruction = entity.Instruction,

                VinNo = entity.VinNo,
                Deductible = entity.Deductible,
                WindDeductible = entity.WindDeductible,
                Premium = entity.Premium,
                Building = entity.Building,
                Subrogation = entity.Subrogation,
                Salvage = entity.Salvage,
                BusinessPersonalProperty = entity.BusinessPersonalProperty,
                Contents = entity.Contents,
                Ale = entity.Ale,
                DetachedPrivateStructures = entity.DetachedPrivateStructures,
                CondominiumImprovements = entity.CondominiumImprovements,
                IndustrialHygenist = entity.IndustrialHygenist,
                AdjustingFee = Convert.ToString(entity.AdjustingFee),

                FirstContactDate = entity.FirstContactDate.Date.ToString("M/d/yyyy"),
                InspectionDate = entity.InspectionDate != null ? entity.InspectionDate.Value.Date.ToString("M/d/yyyy") : string.Empty,
                DueDate = entity.DueDate != null ? entity.DueDate.Value.Date.ToString("M/d/yyyy") : string.Empty,
                ClosedDate = entity.ClosedDate != null ? entity.ClosedDate.Value.Date.ToString("M/d/yyyy") : string.Empty,
                AcknowledgementMail = entity.AcknowledgementMail,
                LossNotice = entity.LossNotice,
                StatusId = entity.ClaimStatusId,
                Status = entity.ClaimStatus != null ? entity.ClaimStatus.Name : string.Empty
            };
        }

        public static IEnumerable<ClaimViewModel> Map(IEnumerable<Claim> entities)
        {
            if (entities == null || (entities != null && !entities.Any()))
                return null;

            return entities.Select(e =>
                new ClaimViewModel
                {
                    ClaimId = e.ClaimId,
                    FileNo = e.FileNo,
                    ClaimNo = e.ClaimNo,
                    CompanyId = e.CompanyId,
                    CompanyName = e.Company.CompanyName,
                    ContactId = e.ContactId,
                    ContactName = e.Contact != null ? String.Format("{0} {1}", e.Contact.FirstName, e.Contact.LastName) : string.Empty,
                    ClaimantId = e.ClaimantId,
                    ClaimantName = e.Claimant != null ? String.Format("{0} {1}", e.Claimant.FirstName, e.Claimant.LastName) : string.Empty,
                    PolicyNo = e.PolicyNo,
                    PolicyEffectiveDate = e.PolicyEffectiveDate != null ? e.PolicyEffectiveDate.Value.Date.ToString("M/d/yyyy") : string.Empty,
                    PolicyExpirationDate = e.PolicyExpirationDate != null ? e.PolicyExpirationDate.Value.Date.ToString("M/d/yyyy") : string.Empty,
                    LoanNo = e.LoanNo,
                    Mortgagee = e.Mortgagee,
                    ReceivedDate = e.ReceivedDate.Date.ToString("M/d/yyyy"),
                    LossDate = e.LossDate.Date.ToString("M/d/yyyy"),
                    LossType = e.LossType,
                    AdjustmentType = e.AdjustmentType,
                    LossTotalUrgent = e.LossTotalUrgent,
                    MouldClaim = e.MouldClaim,
                    ManagerId = e.ManagerId,
                    ManagerName = e.Manager != null ? String.Format("{0} {1}", e.Manager.FirstName, e.Manager.LastName) : string.Empty,
                    AdjusterId = e.AdjusterId,
                    AdjusterName = e.Adjuster != null ? String.Format("{0} {1}", e.Adjuster.FirstName, e.Adjuster.LastName) : string.Empty,
                    Branch = e.Branch,
                    LossDescription = e.LossDescription,
                    Instruction = e.Instruction,

                    VinNo = e.VinNo,
                    Deductible = e.Deductible,
                    WindDeductible = e.WindDeductible,
                    Premium = e.Premium,
                    Building = e.Building,
                    Subrogation = e.Subrogation,
                    Salvage = e.Salvage,
                    BusinessPersonalProperty = e.BusinessPersonalProperty,
                    Contents = e.Contents,
                    Ale = e.Ale,
                    DetachedPrivateStructures = e.DetachedPrivateStructures,
                    CondominiumImprovements = e.CondominiumImprovements,
                    IndustrialHygenist = e.IndustrialHygenist,
                    AdjustingFee = Convert.ToString(e.AdjustingFee),

                    FirstContactDate = e.FirstContactDate.Date.ToString("M/d/yyyy"),
                    InspectionDate = e.InspectionDate != null ? e.InspectionDate.Value.Date.ToString("M/d/yyyy") : string.Empty,
                    DueDate = e.DueDate != null ? e.DueDate.Value.Date.ToString("M/d/yyyy") : string.Empty,
                    ClosedDate = e.ClosedDate != null ? e.ClosedDate.Value.Date.ToString("M/d/yyyy") : string.Empty,
                    AcknowledgementMail = e.AcknowledgementMail,
                    LossNotice = e.LossNotice,
                    StatusId = e.ClaimStatusId,
                    Status = e.ClaimStatus != null ? e.ClaimStatus.Name : string.Empty
                });
        }

        public static IEnumerable<ClaimViewModel> ClaimsMap(IEnumerable<Claim> entities)
        {
            if (entities == null || (entities != null && !entities.Any()))
                return null;

            return entities.Select(e =>
                new ClaimViewModel
                {
                    ClaimId = e.ClaimId,
                    FileNo = e.FileNo,
                    ClaimNo = e.ClaimNo,
                    CompanyId = e.CompanyId,
                    CompanyName = e.Company.CompanyName,
                    ContactId = e.ContactId,
                    ContactName = e.Contact != null ? String.Format("{0} {1}", e.Contact.FirstName, e.Contact.LastName) : string.Empty,
                    ClaimantId = e.ClaimantId,
                    ClaimantName = e.Claimant != null ? String.Format("{0} {1}", e.Claimant.FirstName, e.Claimant.LastName) : string.Empty,
                    ReceivedDate = e.ReceivedDate != null ? e.ReceivedDate.Date.ToString("M/d/yyyy") : string.Empty,
                    LossDate = e.LossDate != null ? e.LossDate.Date.ToString("M/d/yyyy") : string.Empty,
                    AdjusterId = e.AdjusterId,
                    AdjusterName = e.Adjuster != null ? String.Format("{0} {1}", e.Adjuster.FirstName, e.Adjuster.LastName) : string.Empty,
                    Status = e.ClaimStatus != null ? e.ClaimStatus.Name : string.Empty,
                    IsActive = e.IsActive,
                    DueDate = e.DueDate != null ? e.DueDate.Value.ToString("M/d/yyyy") : string.Empty,
                    Days = (e.ClosedDate != null && e.CreatedOn != null) ? (e.ClosedDate.Value.Date - e.CreatedOn.Date).TotalDays : 0.0
                }).OrderByDescending(e => e.ReceivedDate);
        }

        public static ClaimNotesViewModel Map(ClaimNote entity)
        {
            if (entity == null)
                return null;

            return new ClaimNotesViewModel
            {
                NoteId = entity.NoteId,
                ClaimId = entity.ClaimId,
                ClaimNo = entity.ClaimDetails != null ? entity.ClaimDetails.ClaimNo : string.Empty,
                Title = entity.Title,
                Description = entity.Description,
                IsTask = entity.IsTask,
                Type = entity.IsTask ? "Task" : "Note",
                TaskEndDate = entity.TaskDueDate != null ? entity.TaskDueDate.Value.Date.ToString("M/d/yyyy") : string.Empty,
                CreatedDate = string.IsNullOrWhiteSpace(entity.CreatedDate) ? DateTime.Now.Date.ToString("M/d/yyyy") : entity.CreatedDate,
                AssignedTo = entity.AssignedToUser != null ? String.Format("{0} {1}", entity.AssignedToUser.FirstName, entity.AssignedToUser.LastName) : string.Empty,
                AssignedToUser = entity.AssignedToUser == null ? null : new UserViewModel { UserId = entity.AssignedToUser.Id, FirstName = entity.AssignedToUser.FirstName, LastName = entity.AssignedToUser.LastName },
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                LastModifiedBy = entity.LastModifiedBy,
                LastModifiedOn = entity.LastModifiedOn,
                IsActive = entity.IsActive
            };
        }

        public static IEnumerable<ClaimNotesViewModel> Map(IEnumerable<ClaimNote> entities)
        {
            if (entities == null || (entities != null && !entities.Any()))
                return null;
            return entities.Select(e =>
                new ClaimNotesViewModel
                {
                    NoteId = e.NoteId,
                    ClaimId = e.ClaimId,
                    ClaimNo = e.ClaimDetails != null ? e.ClaimDetails.ClaimNo : string.Empty,
                    CompanyId = e.ClaimDetails != null ? e.ClaimDetails.CompanyId : 0,
                    ContactId = e.ClaimDetails != null ? e.ClaimDetails.ContactId : 0,
                    Title = e.Title,
                    Description = e.Description,
                    IsTask = e.IsTask,
                    Type = e.IsTask ? "Task" : "Note",
                    TaskEndDate = e.TaskDueDate != null ? e.TaskDueDate.Value.Date.ToString("M/d/yyyy") : string.Empty,
                    CreatedDate = string.IsNullOrWhiteSpace(e.CreatedDate) ? DateTime.Now.Date.ToString("M/d/yyyy") : e.CreatedDate,
                    AssignedTo = e.AssignedToUser != null ? String.Format("{0} {1}", e.AssignedToUser.FirstName, e.AssignedToUser.LastName) : string.Empty,
                    AssignedToUser = e.AssignedToUser == null ? null : new UserViewModel { UserId = e.AssignedToUser.Id, FirstName = e.AssignedToUser.FirstName, LastName = e.AssignedToUser.LastName },
                    CreatedOn = e.CreatedOn,
                    LastModifiedBy = e.LastModifiedBy,
                    LastModifiedOn = e.LastModifiedOn,
                    IsActive = e.IsActive,
                    CreatedBy = e.CreatedByUser != null ? String.Format("{0} {1}", e.CreatedByUser.FirstName, e.CreatedByUser.LastName) : string.Empty,
                    CreatedByUser = e.CreatedByUser == null ? null : new UserViewModel { UserId = e.CreatedByUser.Id, FirstName = e.CreatedByUser.FirstName, LastName = e.CreatedByUser.LastName },
                    CompanyName = e.ClaimDetails.Company.CompanyName,
                    ContactName = e.ClaimDetails.Contact != null ? string.Format("{0} {1}", e.ClaimDetails.Contact.FirstName, e.ClaimDetails.Contact.LastName) : string.Empty,
                    DueDate = e.TaskDueDate != null ? e.TaskDueDate.Value.Date.ToString("M/d/yyyy") : string.Empty,
                    IsDuteDatetask = (e.TaskDueDate != null && e.TaskDueDate.Value.Date >= DateTime.Today) ? 1 : 0
                }).OrderByDescending(e => e.CreatedDate);
        }

        public static ClaimDocumentViewModel Map(ClaimDocumentMapping entity)
        {
            if (entity == null)
                return null;

            return new ClaimDocumentViewModel
            {
                DocumentId = entity.DocumentId,
                ClaimId = entity.ClaimId,
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

        public static IEnumerable<ClaimDocumentViewModel> Map(IEnumerable<ClaimDocumentMapping> entities)
        {
            if (entities == null || (entities != null && !entities.Any()))
                return null;

            return entities.Select(e =>
                new ClaimDocumentViewModel
                {
                    DocumentId = e.DocumentId,
                    ClaimId = e.ClaimId,
                    FileType = e.FileType,
                    FileName = e.FileName,
                    FileLocation = e.FileLocation,
                    FileDisplayName = e.FileDisplayName,
                    CreatedBy = e.CreatedBy,
                    CreatedOn = e.CreatedOn,
                    LastModifiedBy = e.LastModifiedBy,
                    LastModifiedOn = e.LastModifiedOn,
                    IsActive = e.IsActive
                }).OrderByDescending(e => e.LastModifiedOn).ThenBy(e => e.FileDisplayName).ToList();
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
        public static InvoiceDocumentViewModel Map(InvoiceDocumentMapping entity)
        {
            if (entity == null)
                return null;

            return new InvoiceDocumentViewModel
            {
                DocumentId = entity.DocumentId,
                UserId = entity.UserId,
                FileType = entity.FileType,
                FileName = entity.FileName,
                FileLocation = entity.FileLocation,
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn.ToString("yyyy/MM/dd hh:mm:ss"),
                LastModifiedBy = entity.LastModifiedBy,
                LastModifiedOn = entity.LastModifiedOn.ToString("yyyy/MM/dd hh:mm:ss"),
                IsActive = entity.IsActive,
                ClaimId=entity.ClaimId
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
                }).OrderByDescending(e => e.LastModifiedOn).ThenBy(e => e.FileDisplayName).ToList();
        }
        public static IEnumerable<InvoiceDocumentViewModel> Map(IEnumerable<InvoiceDocumentMapping> entities, string applicationUrl, string claimNo)
        {
            if (entities == null || (entities != null && !entities.Any()))
                return null;

            return entities.Select(e =>
                new InvoiceDocumentViewModel
                {
                    DocumentId = e.DocumentId,
                    UserId = e.UserId,
                    FileType = e.FileType,
                    FileName = e.FileName.Substring(e.FileName.ToString().LastIndexOf('\\')+1, (e.FileName.ToString().Length - e.FileName.ToString().LastIndexOf('\\')-1)),
                    FileLocation = applicationUrl + e.UserId + "_" + claimNo + "/" + e.FileName.Substring(e.FileName.ToString().LastIndexOf('\\') + 1, (e.FileName.ToString().Length - e.FileName.ToString().LastIndexOf('\\') - 1)),
                    CreatedBy = e.AspNetUser.FirstName +","+ e.AspNetUser.LastName,
                    CreatedOn = e.CreatedOn.ToString("yyyy/MM/dd hh:mm:ss"),
                    LastModifiedBy = e.LastModifiedBy,
                    LastModifiedOn = e.LastModifiedOn.ToString("yyyy/MM/dd hh:mm:ss"),
                    IsActive = e.IsActive,
                    ClaimId=e.ClaimId,
                }).OrderByDescending(e => e.CreatedOn).ToList();
        }

        
        public static AspNetUsersDocumentViewModel MapImage(IEnumerable<AspNetUsersDocumentMapping> entities)
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
                    FileLocation = e.FileLocation.Replace("\\", "/"),
                    FileDisplayName = e.FileDisplayName,
                    CreatedBy = e.CreatedBy,
                    CreatedOn = e.CreatedOn,
                    LastModifiedBy = e.LastModifiedBy,
                    LastModifiedOn = e.LastModifiedOn,
                    IsActive = e.IsActive
                }).OrderByDescending(e => e.LastModifiedOn).ToList().FirstOrDefault();
        }
        public static InvoiceDocumentViewModel MapImage(IEnumerable<InvoiceDocumentMapping> entities)
        {
            if (entities == null || (entities != null && !entities.Any()))
                return null;

            return entities.Select(e =>
                new InvoiceDocumentViewModel
                {
                    DocumentId = e.DocumentId,
                    UserId = e.UserId,
                    FileType = e.FileType,
                    FileName = e.FileName,
                    FileLocation = e.FileLocation.Replace("\\", "/"),
                    CreatedBy = e.CreatedBy,
                    CreatedOn = e.CreatedOn.ToString("yyyy/MM/dd hh:mm:ss"),
                    LastModifiedBy = e.LastModifiedBy,
                    LastModifiedOn = e.LastModifiedOn.ToString("yyyy/MM/dd hh:mm:ss"),
                    IsActive = e.IsActive,
                    ClaimId=e.ClaimId
                }).OrderByDescending(e => e.LastModifiedOn).ToList().FirstOrDefault();
        }

        public static ClaimStatusViewModel Map(Status entity)
        {
            if (entity == null)
                return null;

            return new ClaimStatusViewModel
            {
                ClaimStatusId = entity.Id,
                Status = entity.Name,
                SortOrder = entity.SortOrder,
                IsActive = entity.IsActive
            };
        }

        public static UserViewModel Map(AspNetUser entity)
        {
            if (entity == null)
                return null;

            return new UserViewModel
            {
                UserId = entity.Id,
                FirstName = entity.FirstName != null ? entity.FirstName : string.Empty,
                LastName = entity.LastName != null ? entity.LastName : string.Empty,
                Email = entity.Email != null ? entity.Email : string.Empty,
                Phone = entity.PhoneNumber != null ? entity.PhoneNumber : string.Empty,
                Cell = entity.CellNumber != null ? entity.CellNumber : string.Empty,
                Department = entity.Department != null ? entity.Department : string.Empty,
                CompanyName = entity.CompanyName != null ? entity.CompanyName : string.Empty,
                //CompanyId = Convert.ToInt32(entity.CompanyName),
                Status = entity.Status != null ? entity.Status : string.Empty,
                StatusId = entity.Status != null ? Convert.ToInt32(entity.Status) : 0,
                UserTypeName = entity.UserType != null ? entity.UserType : string.Empty,
                ProfileType = entity.UserProfile != null ? entity.UserProfile : string.Empty,
                //UserTypeId = Convert.ToInt32(entity.UserType),
                // ProfileTypeId = Convert.ToInt32(entity.UserProfile),
                Street = entity.Street != null ? entity.Street : string.Empty,
                City = entity.City != null ? entity.City : string.Empty,
                CountryName = entity.Country != null ? (entity.Country.CountryName != null ? entity.Country.CountryName : string.Empty) : string.Empty,
                CountryId = entity.CountryId != null ? (int)entity.CountryId : 0,
                ProvinceName = entity.Province != null ? (entity.Province.ProvinceName != null ? entity.Province.ProvinceName : string.Empty) : string.Empty,
                ProvinceId = entity.ProvinceId != null ? (int)entity.ProvinceId : 0,
                PostalCode = entity.PostalCode != null ? entity.PostalCode : string.Empty,
                CreatedDate = entity.CreateDate != null ? entity.CreateDate.Date.ToString("yyyy/MM/dd") : string.Empty,
                ReceiveAlerts = entity.IsReceiveAlerts,
                LastModifiedDate = entity.LastModifiedDate != null ? entity.LastModifiedDate.Value.Date.ToString("yyyy/MM/dd") : string.Empty

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
                    CreatedDate = e.CreateDate != null ? e.CreateDate.Date.ToString("yyyy/MM/dd") : string.Empty,
                    UserTypeName = e.UserProfile

                }).OrderBy(e => e.CreatedDate);
        }
        public static IEnumerable<ClaimStatusViewModel> Map(IEnumerable<Status> entities)
        {
            if (entities == null || (entities != null && !entities.Any()))
                return null;

            return entities.Select(e =>
                new ClaimStatusViewModel
                {
                    ClaimStatusId = e.Id,
                    Status = e.Name,
                    SortOrder = e.SortOrder,
                    IsActive = e.IsActive
                }).OrderBy(e => e.SortOrder).ThenBy(e => e.Status).ToList();
        }

        public static CompanyViewModel Map(Account entity)
        {
            if (entity == null)
                return null;

            return new CompanyViewModel
            {
                CompanyId = entity.AccountId,
                CompanyName = entity.CompanyName,
                Type = entity.Type,
                ContactEmailId = entity.EmailId,
                Phone = entity.Phone,
                AlternatePhone = entity.AlternatePhone,
                Fax = entity.Fax,
                Extension = entity.Extension,
                DefaultAdjuster = entity.DefaultAdjuster,
                KeyContact = entity.KeyContact ?? 0,
                Unit = entity.Unit,
                Street = entity.Street,
                City = entity.City,
                ProvinceId = entity.ProvinceId,
                ProvinceName = entity.Province.ProvinceName,
                CountryId = entity.CountryId,
                CountryName = entity.Country.CountryName,
                Postal = entity.Postal,
                Status = entity.Status
            };
        }

        public static IEnumerable<CompanyViewModel> Map(IEnumerable<Account> entities)
        {
            if (entities == null || (entities != null && !entities.Any()))
                return null;

            return entities.Select(e =>
                new CompanyViewModel
                {
                    CompanyId = e.AccountId,
                    CompanyName = e.CompanyName,
                    Type = e.Type,
                    ContactEmailId = e.EmailId,
                    Phone = e.Phone,
                    AlternatePhone = e.AlternatePhone,
                    Fax = e.Fax,
                    Extension = e.Extension,
                    DefaultAdjuster = e.DefaultAdjuster,
                    KeyContact = e.KeyContact ?? 0,
                    Unit = e.Unit,
                    Street = e.Street,
                    City = e.City,
                    ProvinceId = e.ProvinceId,
                    ProvinceName = e.Province.ProvinceName,
                    CountryId = e.CountryId,
                    CountryName = e.Country.CountryName,
                    Postal = e.Postal,
                    Status = e.Status
                }).OrderBy(e => e.CompanyName).ToList();
        }

        public static ContactViewModel Map(Contact entity)
        {
            if (entity == null)
                return null;

            return new ContactViewModel
            {
                ContactId = entity.ContactId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                CompanyId = entity.CompanyId,
                CompanyName = entity.Company.CompanyName,
                ContactTypeId = entity.ContactTypeId,
                ContactTypeName = entity.ContactType.ContactTypeName,
                OwnerId = entity.OwnerId,
                OwnerName = entity.Owner.UserName,
                Email = entity.EmailId,
                Phone = entity.Phone,
                Cell = entity.Cell,
                IsKeyContact = entity.IsKeyContact,
                Street = entity.Street,
                City = entity.City,
                StateId = entity.ProvinceId,
                StateName = entity.Province.ProvinceName,
                CountryId = entity.CountryId,
                CountryName = entity.Country.CountryName,
                PostalCode = entity.Postal,
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                LastModifiedBy = entity.LastModifiedBy,
                LastModifiedOn = entity.LastModifiedOn,
                IsActive = entity.IsActive

            };
        }

        public static IEnumerable<ContactViewModel> Map(IEnumerable<Contact> entities)
        {
            if (entities == null || (entities != null && !entities.Any()))
                return null;

            return entities.Select(e =>
                new ContactViewModel
                {
                    ContactId = e.ContactId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    CompanyId = e.CompanyId,
                    CompanyName = e.Company.CompanyName,
                    ContactTypeId = e.ContactTypeId,
                    ContactTypeName = e.ContactType.ContactTypeName,
                    OwnerId = e.OwnerId,
                    OwnerName = e.Owner.UserName,
                    Email = e.EmailId,
                    Phone = e.Phone,
                    Cell = e.Cell,
                    IsKeyContact = e.IsKeyContact,
                    Street = e.Street,
                    City = e.City,
                    StateId = e.ProvinceId,
                    StateName = e.Province.ProvinceName,
                    CountryId = e.CountryId,
                    CountryName = e.Country.CountryName,
                    PostalCode = e.Postal,
                    CreatedBy = e.CreatedBy,
                    CreatedOn = e.CreatedOn,
                    LastModifiedBy = e.LastModifiedBy,
                    LastModifiedOn = e.LastModifiedOn,
                    IsActive = e.IsActive
                }).OrderBy(e => e.ContactName).ToList();
        }

        public static TimeLogViewModel Map(TimeLog entity)
        {
            if (entity == null)
                return null;

            return new TimeLogViewModel
            {
                TimeLogId = entity.TimeLogId,
                ClaimId = entity.ClaimId,
                FileNo = entity.TimeLogClaim != null ? entity.TimeLogClaim.FileNo : string.Empty,
                ClaimNo = entity.TimeLogClaim != null ? entity.TimeLogClaim.ClaimNo : string.Empty,
                CompanyId = entity.TimeLogClaim != null ? entity.TimeLogClaim.CompanyId : 0,
                CompanyName = entity.TimeLogClaim != null ? entity.TimeLogClaim.Company != null ? entity.TimeLogClaim.Company.CompanyName : string.Empty : string.Empty,
                ServiceItemId = entity.ServiceItemId,
                ServiceItemName = entity.TimeLogServiceItem != null ? entity.TimeLogServiceItem.ServiceItemName : string.Empty,
                ServiceRate = entity.TimeLogServiceItem != null ? entity.TimeLogServiceItem.DefaultFee : 0.0M,
                Quantity = entity.Quantity,
                HoursSpent = entity.HoursSpent,
                Comment = entity.Comment,
                TaskDate = entity.TaskDate.Date.ToString("M/d/yyyy"),
                LoggedOn = entity.LoggedOn.Date.ToString("M/d/yyyy"),
                AdjusterId = !string.IsNullOrWhiteSpace(entity.AdjusterId) ? entity.AdjusterId : string.Empty,
                AdjusterName = entity.Adjuster != null ? String.Format("{0} {1}", entity.Adjuster.FirstName, entity.Adjuster.LastName) : string.Empty,
                Adjuster = entity.Adjuster == null ? null : new UserViewModel { UserId = entity.Adjuster.Id, FirstName = entity.Adjuster.FirstName, LastName = entity.Adjuster.LastName },
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                LastModifiedBy = entity.LastModifiedBy,
                LastModifiedOn = entity.LastModifiedOn,
                IsActive = entity.IsActive
            };
        }

        public static IEnumerable<TimeLogViewModel> Map(IEnumerable<TimeLog> entities)
        {
            if (entities == null || (entities != null && !entities.Any()))
                return null;

            return entities.Select(e =>
                new TimeLogViewModel
                {
                    TimeLogId = e.TimeLogId,
                    ClaimId = e.ClaimId,
                    FileNo = e.TimeLogClaim != null ? e.TimeLogClaim.FileNo : string.Empty,
                    ClaimNo = e.TimeLogClaim != null ? e.TimeLogClaim.ClaimNo : string.Empty,
                    CompanyId = e.TimeLogClaim != null ? e.TimeLogClaim.CompanyId : 0,
                    CompanyName = e.TimeLogClaim != null ? e.TimeLogClaim.Company != null ? e.TimeLogClaim.Company.CompanyName : string.Empty : string.Empty,
                    ServiceItemId = e.ServiceItemId,
                    ServiceItemName = e.TimeLogServiceItem != null ? e.TimeLogServiceItem.ServiceItemName : string.Empty,
                    ServiceRate = e.TimeLogServiceItem != null ? e.TimeLogServiceItem.DefaultFee : 0.0M,
                    Quantity = e.Quantity,
                    HoursSpent = e.HoursSpent,
                    Comment = e.Comment,
                    TaskDate = e.TaskDate.Date.ToString("M/d/yyyy"),
                    LoggedOn = e.LoggedOn.Date.ToString("M/d/yyyy"),
                    AdjusterId = !string.IsNullOrWhiteSpace(e.AdjusterId) ? e.AdjusterId : string.Empty,
                    AdjusterName = e.Adjuster != null ? String.Format("{0} {1}", e.Adjuster.FirstName, e.Adjuster.LastName) : string.Empty,
                    Adjuster = e.Adjuster == null ? null : new UserViewModel { UserId = e.Adjuster.Id, FirstName = e.Adjuster.FirstName, LastName = e.Adjuster.LastName },
                    CreatedBy = e.CreatedBy,
                    CreatedOn = e.CreatedOn,
                    LastModifiedBy = e.LastModifiedBy,
                    LastModifiedOn = e.LastModifiedOn,
                    IsActive = e.IsActive,
                    IsBilled=e.IsBilled
                    
                }).OrderByDescending(e => e.CreatedOn);
        }

        public static ServiceItemViewModel Map(ServiceItem entity)
        {
            if (entity == null)
                return null;

            return new ServiceItemViewModel
            {
                ServiceItemId = entity.ServiceItemId,
                ServiceItemName = entity.ServiceItemName,
                ServiceItemDescription = entity.ServiceItemDescription,
                ServiceCategoryId = entity.ServiceCategoryId,
                ServiceCategoryName = entity.Category != null ? entity.Category.ServiceCategoryName : string.Empty,
                IsHourBased = entity.IsHourBased,
                DefaultQuantity = entity.DefaultQuantity,
                DefaultFee = entity.DefaultFee,
                MinimumFee = entity.MinimumFee,
                CreatedOn = entity.CreatedDate,
                CreatedBy = entity.CreatedBy,
                LastModifiedOn = entity.LastModifiedDate,
                LastModifiedBy = entity.LastModifiedBy,
                IsActive = entity.IsActive
            };
        }

        public static IEnumerable<ServiceItemViewModel> Map(IEnumerable<ServiceItem> entities)
        {
            if (entities == null || (entities != null && !entities.Any()))
                return null;

            return entities.Select(e =>
                new ServiceItemViewModel
                {
                    ServiceItemId = e.ServiceItemId,
                    ServiceItemName = e.ServiceItemName,
                    ServiceItemDescription = e.ServiceItemDescription,
                    ServiceCategoryId = e.ServiceCategoryId,
                    ServiceCategoryName = e.Category != null ? e.Category.ServiceCategoryName : string.Empty,
                    IsHourBased = e.IsHourBased,
                    DefaultQuantity = e.DefaultQuantity,
                    DefaultFee = e.DefaultFee,
                    MinimumFee = e.MinimumFee,
                    CreatedOn = e.CreatedDate,
                    CreatedBy = e.CreatedBy,
                    LastModifiedOn = e.LastModifiedDate,
                    LastModifiedBy = e.LastModifiedBy,
                    IsActive = e.IsActive
                }).OrderByDescending(e => e.ServiceItemName).ToList();
        }

        public static TaxSettingViewModel Map(TaxSetting entity)
        {
            if (entity == null)
                return null;

            return new TaxSettingViewModel
            {
                TaxRate = entity.TaxRate,
                Id = entity.Id,
                CountryId = entity.CountryId,
                ProvinceId = entity.ProvinceId,
                CountryName = entity.Country != null ? entity.Country.CountryName : string.Empty,
                StateName = entity.State != null ? entity.State.ProvinceName : string.Empty,
                CreatedOn = entity.CreatedDate,
                CreatedBy = entity.CreatedBy,
                LastModifiedOn = entity.LastModifiedDate,
                LastModifiedBy = entity.LastModifiedBy,
                IsActive = entity.IsActive
            };
        }

        public static IEnumerable<TaxSettingViewModel> Map(IEnumerable<TaxSetting> entities)
        {
            if (entities == null || (entities != null && !entities.Any()))
                return null;

            return entities.Select(e =>
                new TaxSettingViewModel
                {
                    Id = e.Id,
                    CountryId = e.CountryId,
                    ProvinceId = e.ProvinceId,
                    TaxRate = e.TaxRate,
                    CountryName = e.Country != null ? e.Country.CountryName : string.Empty,
                    StateName = e.State != null ? e.State.ProvinceName : string.Empty,
                    CreatedOn = e.CreatedDate,
                    CreatedBy = e.CreatedBy,
                    LastModifiedOn = e.LastModifiedDate,
                    LastModifiedBy = e.LastModifiedBy,
                    IsActive = e.IsActive
                }).OrderBy(e => e.CountryName).ThenBy(e => e.StateName).ToList();
        }

        public static ServiceCategoryViewModel Map(ServiceCategory entity)
        {
            if (entity == null)
                return null;

            return new ServiceCategoryViewModel
            {
                ServiceCategoryId = entity.ServiceCategoryId,
                ServiceCategoryName = entity.ServiceCategoryName,
                SortOrder = entity.SortOrder,
                IsActive = entity.IsActive
            };
        }

        public static IEnumerable<ServiceCategoryViewModel> Map(IEnumerable<ServiceCategory> entities)
        {
            if (entities == null || (entities != null && !entities.Any()))
                return null;

            return entities.Select(e =>
                new ServiceCategoryViewModel
                {
                    ServiceCategoryId = e.ServiceCategoryId,
                    ServiceCategoryName = e.ServiceCategoryName,
                    SortOrder = e.SortOrder,
                    IsActive = e.IsActive
                }).OrderBy(e => e.SortOrder).ThenBy(e => e.ServiceCategoryName).ToList();
        }

        public static TypeOfLossViewModel Map(TypeOfLoss entity)
        {
            if (entity == null)
                return null;

            return new TypeOfLossViewModel
            {
                LossTypeId = entity.Id,
                LossTypeName = entity.Name,
                SortOrder = entity.SortOrder,
                IsActive = entity.IsActive
            };
        }
        public static IEnumerable<TypeOfLossViewModel> Map(IEnumerable<TypeOfLoss> entities)
        {
            if (entities == null || (entities != null && !entities.Any()))
                return null;

            return entities.Select(e =>
                new TypeOfLossViewModel
                {
                    LossTypeId = e.Id,
                    LossTypeName = e.Name,
                    SortOrder = e.SortOrder,
                    IsActive = e.IsActive
                }).OrderBy(e => e.SortOrder).ThenBy(e => e.LossTypeName).ToList();
        }

        public static TimeLogUnitViewModel Map(TimeLogUnit entity)
        {
            if (entity == null)
                return null;

            return new TimeLogUnitViewModel
            {
                UnitId = entity.Id,
                UnitName = entity.Name,
                SortOrder = entity.SortOrder,
                IsActive = entity.IsActive
            };
        }
        public static IEnumerable<TimeLogUnitViewModel> Map(IEnumerable<TimeLogUnit> entities)
        {
            if (entities == null || (entities != null && !entities.Any()))
                return null;

            return entities.Select(e =>
                new TimeLogUnitViewModel
                {
                    UnitId = e.Id,
                    UnitName = e.Name,
                    SortOrder = e.SortOrder,
                    IsActive = e.IsActive
                }).OrderBy(e => e.SortOrder).ThenBy(e => e.UnitName).ToList();
        }

        public static FileNameViewModel Map(FileNameCode entity)
        {
            if (entity == null)
                return null;

            return new FileNameViewModel
            {
                FileNameId = entity.Id,
                LocationName = entity.LocationName,
                FileNumberPrefix = entity.FileNumberPrefix,
                SortOrder = entity.SortOrder,
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                LastModifiedBy = entity.LastModifiedBy,
                LastModifiedOn = entity.LastModifiedOn,
                IsActive = entity.IsActive
            };
        }
        public static IEnumerable<FileNameViewModel> Map(IEnumerable<FileNameCode> entities)
        {
            if (entities == null || (entities != null && !entities.Any()))
                return null;

            return entities.Select(e =>
                new FileNameViewModel
                {
                    FileNameId = e.Id,
                    LocationName = e.LocationName,
                    FileNumberPrefix = e.FileNumberPrefix,
                    SortOrder = e.SortOrder,
                    CreatedBy = e.CreatedBy,
                    CreatedOn = e.CreatedOn,
                    LastModifiedBy = e.LastModifiedBy,
                    LastModifiedOn = e.LastModifiedOn,
                    IsActive = e.IsActive
                }).OrderBy(e => e.SortOrder).ThenBy(e => e.LocationName).ToList();
        }

        public static InvoiceViewModel Map(Invoice entity)
        {
            if (entity == null)
                return null;

            return new InvoiceViewModel
            {
                InvoiceId = entity.InvoiceId,
                ClaimId = entity.ClaimId,
                FileNumber = entity.InvoiceClaim != null ? entity.InvoiceClaim.FileNo : string.Empty,
                ClaimNumber = entity.InvoiceClaim != null ? entity.InvoiceClaim.ClaimNo : string.Empty,
                PolicyNumber = entity.InvoiceClaim != null ? entity.InvoiceClaim.PolicyNo : string.Empty,
                LossType = entity.InvoiceClaim != null ? entity.InvoiceClaim.LossType : string.Empty,
                CompanyId = entity.InvoiceClaim != null ? entity.InvoiceClaim.CompanyId : 0,
                CompanyName = entity.InvoiceClaim != null
                                    ? entity.InvoiceClaim.Company != null ? entity.InvoiceClaim.Company.CompanyName : string.Empty
                                    : string.Empty,
                ClaimantId = entity.InvoiceClaim != null ? entity.InvoiceClaim.ClaimantId : 0,
                ClaimantName = entity.InvoiceClaim != null
                                    ? entity.InvoiceClaim.Claimant != null ? String.Format("{0} {1}", entity.InvoiceClaim.Claimant.FirstName, entity.InvoiceClaim.Claimant.LastName) : string.Empty
                                    : string.Empty,
                AdjusterId = entity.InvoiceClaim != null ? entity.InvoiceClaim.AdjusterId : string.Empty,
                AdjusterName = entity.InvoiceClaim != null
                                    ? entity.InvoiceClaim.Adjuster != null ? String.Format("{0} {1}", entity.InvoiceClaim.Adjuster.FirstName, entity.InvoiceClaim.Adjuster.LastName) : string.Empty
                                    : string.Empty,
                InvoiceNumber = entity.InvoiceNumber,
                InvoiceDate = entity.InvoiceDate.Date.ToString("M/d/yyyy"),
                DueDate = entity.DueDate.Date.ToString("M/d/yyyy"),
                InvoiceTotal = entity.InvoiceTotal,
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                LastModifiedBy = entity.LastModifiedBy,
                LastModifiedOn = entity.LastModifiedOn,
                IsActive = entity.IsActive,

                TimeLogs = entity.InvoiceTimelogs == null ? null : entity.InvoiceTimelogs.Select(e => new TimeLogViewModel
                {
                    InvoiceTimelogId = e.Id,
                    TimeLogId = e.Timelog != null ? e.Timelog.TimeLogId : 0,
                    ServiceItemId = e.Timelog != null ? e.Timelog.ServiceItemId : 0,
                    ServiceItemName = e.Timelog != null
                                        ? e.Timelog.TimeLogServiceItem != null ? e.Timelog.TimeLogServiceItem.ServiceItemName : string.Empty
                                        : string.Empty,
                    Quantity = e.Timelog != null ? e.Timelog.Quantity : 0.0M,
                    ServiceRate = e.Timelog != null
                                        ? e.Timelog.TimeLogServiceItem != null ? e.Timelog.TimeLogServiceItem.DefaultFee : 0.0M
                                        : 0.0M,
                    Comment = e.Timelog != null ? e.Timelog.Comment : string.Empty,
                    AdjusterName = e.Timelog != null
                                        ? e.Timelog.Adjuster != null ? String.Format("{0} {1}", e.Timelog.Adjuster.FirstName, e.Timelog.Adjuster.LastName) : string.Empty
                                        : string.Empty,
                    TotalAmount = e.ServiceTotal
                  //  IsBilled= e.IsBilled
                    
                }).ToList()
            };
        }
        public static IEnumerable<InvoiceViewModel> Map(IEnumerable<Invoice> entities)
        {
            if (entities == null || (entities != null && !entities.Any()))
                return null;

            return entities.Select(x =>
                new InvoiceViewModel
                {
                    InvoiceId = x.InvoiceId,
                    ClaimId = x.ClaimId,
                    FileNumber = x.InvoiceClaim != null ? x.InvoiceClaim.FileNo : string.Empty,
                    ClaimNumber = x.InvoiceClaim != null ? x.InvoiceClaim.ClaimNo : string.Empty,
                    PolicyNumber = x.InvoiceClaim != null ? x.InvoiceClaim.PolicyNo : string.Empty,
                    LossType = x.InvoiceClaim != null ? x.InvoiceClaim.LossType : string.Empty,
                    CompanyId = x.InvoiceClaim != null ? x.InvoiceClaim.CompanyId : 0,
                    CompanyName = x.InvoiceClaim != null
                                        ? x.InvoiceClaim.Company != null ? x.InvoiceClaim.Company.CompanyName : string.Empty
                                        : string.Empty,
                    ClaimantId = x.InvoiceClaim != null ? x.InvoiceClaim.ClaimantId : 0,
                    ClaimantName = x.InvoiceClaim != null
                                        ? x.InvoiceClaim.Claimant != null ? String.Format("{0} {1}", x.InvoiceClaim.Claimant.FirstName, x.InvoiceClaim.Claimant.LastName) : string.Empty
                                        : string.Empty,
                    AdjusterId = x.InvoiceClaim != null ? x.InvoiceClaim.AdjusterId : string.Empty,
                    AdjusterName = x.InvoiceClaim != null 
                                        ? x.InvoiceClaim.Adjuster != null ? String.Format("{0} {1}", x.InvoiceClaim.Adjuster.FirstName, x.InvoiceClaim.Adjuster.LastName) : string.Empty
                                        : string.Empty,
                    InvoiceNumber = x.InvoiceNumber,
                    InvoiceDate = x.InvoiceDate.Date.ToString("M/d/yyyy"),
                    DueDate = x.DueDate.Date.ToString("M/d/yyyy"),
                    InvoiceTotal = x.InvoiceTotal,
                    CreatedBy = x.CreatedBy,
                    CreatedOn = x.CreatedOn,
                    LastModifiedBy = x.LastModifiedBy,
                    LastModifiedOn = x.LastModifiedOn,
                    IsActive = x.IsActive,
                    LostDate=x.InvoiceClaim != null? x.InvoiceClaim.LossDate.Date.ToString("M/d/yyyy"):string.Empty,
                    Street=x.InvoiceClaim !=null? x.InvoiceClaim.Company.Street:"",
                    City=x.InvoiceClaim !=null? x.InvoiceClaim.Company.City:"",
                    ProvinceName=x.InvoiceClaim !=null ? x.InvoiceClaim.Company.Province.ProvinceName:"",
                    CountryName = x.InvoiceClaim != null ? x.InvoiceClaim.Company.Country.CountryName : "",
                    PostalCode=x.InvoiceClaim !=null? x.InvoiceClaim.Company.Postal:"",
                    ContactName=x.InvoiceClaim != null ? (x.InvoiceClaim.Contact.LastName + " , "+ x.InvoiceClaim.Contact.FirstName):"",
                    TimeLogs = x.InvoiceTimelogs == null ? null : x.InvoiceTimelogs.Select(e => new TimeLogViewModel
                    {
                        InvoiceTimelogId = e.Id,
                        TimeLogId = e.Timelog != null ? e.Timelog.TimeLogId : 0,
                        ServiceItemId = e.Timelog != null ? e.Timelog.ServiceItemId : 0,
                        ServiceItemName = e.Timelog != null
                                            ? e.Timelog.TimeLogServiceItem != null ? e.Timelog.TimeLogServiceItem.ServiceItemName : string.Empty
                                            : string.Empty,
                        Quantity = e.Timelog != null ? e.Timelog.Quantity : 0.0M,
                        ServiceRate = e.Timelog != null
                                            ? e.Timelog.TimeLogServiceItem != null ? e.Timelog.TimeLogServiceItem.DefaultFee : 0.0M
                                            : 0.0M,
                        Comment = e.Timelog != null ? e.Timelog.Comment : string.Empty,
                        AdjusterName = e.Timelog != null
                                            ? e.Timelog.Adjuster != null ? String.Format("{0} {1}", e.Timelog.Adjuster.FirstName, e.Timelog.Adjuster.LastName) : string.Empty
                                            : string.Empty,
                        TotalAmount = e.ServiceTotal
                    }).ToList()
                }).OrderBy(e => e.DueDate).ToList();
        }

        public static IEnumerable<AlertViewModel> Map(IEnumerable<Alert> entities)
        {
            if (entities == null || (entities != null && !entities.Any()))
                return null;

            return entities.Select(e =>
                new AlertViewModel
                {
                    AlertId = e.AlertId,
                    Description = e.Description,
                    Title = e.Title,
                    AlertBy = e.AlertByUser != null ? String.Format("{0} {1}", e.AlertByUser.FirstName, e.AlertByUser.LastName) : string.Empty,
                    AlertTo = e.AlertByUser != null ? String.Format("{0} {1}", e.AlertToUser.FirstName, e.AlertToUser.LastName) : string.Empty,
                    IsRead = e.IsRead,
                    Status = e.IsActive == true ? "Active" : "Inactive",
                    CreatedOn = e.CreatedOn != null ? e.CreatedOn.Value.Date.ToString("yyyy/MM/dd") : string.Empty,
                    LastModifiedOn = e.LastModifiedOn != null ? e.LastModifiedOn.Value.Date.ToString("yyyy/MM/dd") : string.Empty
                }).ToList();
        }
        public static AlertViewModel Map(Alert entity)
        {
            if (entity == null)
                return null;

            return new AlertViewModel
            {
                AlertId = entity.AlertId,
                Description = entity.Description,
                Title = entity.Title,
                AlertBy = entity.AlertByUser != null ? String.Format("{0} {1}", entity.AlertByUser.FirstName, entity.AlertByUser.LastName) : string.Empty,
                AlertTo = entity.AlertByUser != null ? String.Format("{0} {1}", entity.AlertToUser.FirstName, entity.AlertToUser.LastName) : string.Empty,
                IsRead = entity.IsRead,
                Status = entity.IsActive == true ? "Active" : "Inactive",
                CreatedOn = entity.CreatedOn != null ? entity.CreatedOn.Value.Date.ToString("yyyy/MM/dd") : string.Empty,
                LastModifiedOn = entity.LastModifiedOn != null ? entity.LastModifiedOn.Value.Date.ToString("yyyy/MM/dd") : string.Empty
            };
        }

      
    }
}
