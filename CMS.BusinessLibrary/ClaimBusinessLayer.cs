
namespace CMS.BusinessLibrary
{
    #region Namespace
    using CMS.BusinessLibrary.EntityModelMapping;
    using CMS.BusinessLibrary.ViewModels;
    using CMS.Entity;
    using CMS.Repository;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    #endregion

    #region Interface
    public interface IClaimBusinessLayer
    {
        Task<IEnumerable<ClaimViewModel>> GetAllClaims(string userId);
        Task<ClaimViewModel> Find(int claimId);
        Task<IEnumerable<ClaimViewModel>> Find(List<Expression<Func<Claim, bool>>> filterPredicates);
        Task<ClaimViewModel> Create(ClaimViewModel viewModel);
        Task<ClaimViewModel> Update(ClaimViewModel viewModel);
        Task<bool> Delete(int claimId);
        Task<IEnumerable<ClaimViewModel>> GetAllClaimsDueDays(string userId, bool isDueToday);
        Task<IEnumerable<ClaimViewModel>> GetOpenClaims();
        Task<IEnumerable<ClaimViewModel>> GetAverageDaysOpenTasks();
        Task<IEnumerable<ClaimViewModel>> GetOverdueClaims(string userId);
    }
    #endregion

    #region Class
    public sealed partial class ClaimBusinessLayer : IClaimBusinessLayer
    {
        private readonly IClaimRepository _claimRepository;

        public ClaimBusinessLayer()
        {
            this._claimRepository = new ClaimRepository();
        }

        private IClaimRepository ClaimRepository
        {
            get { return this._claimRepository; }
        }

        public async Task<IEnumerable<ClaimViewModel>> GetAllClaims(string userId)
        {
            List<Expression<Func<Claim, bool>>> filterPredicates = new List<Expression<Func<Claim, bool>>>();
            filterPredicates.Add(p => p.CreatedBy == userId);
            filterPredicates.Add(p => p.IsActive == true);

            return this.ClaimRepository.Find(filterPredicates).ToList().Select(e =>
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
                    ReceivedDate = e.ReceivedDate.Date.ToString("M/d/yyyy"),
                    LossDate = e.LossDate.Date.ToString("M/d/yyyy"),
                    AdjusterId = e.AdjusterId,
                    AdjusterName = e.Adjuster != null ? String.Format("{0} {1}", e.Adjuster.FirstName, e.Adjuster.LastName) : string.Empty,
                    Status = e.ClaimStatus != null ? e.ClaimStatus.Name : string.Empty,
                    IsActive = e.IsActive,
                    PolicyNo=e.PolicyNo,
                    LossType=e.LossType
                });
        }

        public async Task<ClaimViewModel> Find(int claimId)
        {
            var claimInfo = this.ClaimRepository.Find(claimId);

            if (claimInfo == null)
            {
                return new ClaimViewModel();
            }

            return new ClaimViewModel
            {
                ClaimId = claimInfo.ClaimId,
                FileNo = claimInfo.FileNo,
                ClaimNo = claimInfo.ClaimNo,
                CompanyId = claimInfo.CompanyId,
                CompanyName = claimInfo.Company.CompanyName,
                Company = new CompanyViewModel{
                 CompanyId = claimInfo.Company.AccountId,
                 CompanyName = claimInfo.Company.CompanyName,
                },
                ContactId = claimInfo.ContactId,
                ContactName = claimInfo.Contact != null ? String.Format("{0} {1}", claimInfo.Contact.FirstName, claimInfo.Contact.LastName) : string.Empty,
                Contact = new ContactViewModel {
                    ContactId = claimInfo.Contact.ContactId,
                    FirstName = claimInfo.Contact.FirstName,
                    LastName = claimInfo.Contact.LastName,
                    CompanyId = claimInfo.Contact.CompanyId
                },
                ClaimantId = claimInfo.ClaimantId,
                ClaimantName = claimInfo.Claimant != null ? String.Format("{0} {1}", claimInfo.Claimant.FirstName, claimInfo.Claimant.LastName) : string.Empty,
                Claimant = new ContactViewModel{
                    ContactId = claimInfo.Contact.ContactId,
                    FirstName = claimInfo.Contact.FirstName,
                    LastName = claimInfo.Contact.LastName,
                    CompanyId = claimInfo.Contact.CompanyId
                },
                PolicyNo = claimInfo.PolicyNo,
                PolicyEffectiveDate = claimInfo.PolicyEffectiveDate != null ? claimInfo.PolicyEffectiveDate.Value.Date.ToString("M/d/yyyy") : string.Empty,
                PolicyExpirationDate = claimInfo.PolicyExpirationDate != null ? claimInfo.PolicyExpirationDate.Value.Date.ToString("M/d/yyyy") : string.Empty,
                LoanNo = claimInfo.LoanNo,
                Mortgagee = claimInfo.Mortgagee,
                ReceivedDate = claimInfo.ReceivedDate.Date.ToString("M/d/yyyy"),
                LossDate = claimInfo.LossDate.Date.ToString("M/d/yyyy"),
                LossType = claimInfo.LossType,
                AdjustmentType = claimInfo.AdjustmentType,
                LossTotalUrgent = claimInfo.LossTotalUrgent,
                MouldClaim = claimInfo.MouldClaim,
                ManagerId = claimInfo.ManagerId,
                ManagerName = claimInfo.Manager != null ? String.Format("{0} {1}", claimInfo.Manager.FirstName, claimInfo.Manager.LastName) : string.Empty,
                Manager = new UserViewModel{
                    UserId = claimInfo.Manager.Id,
                    FirstName = claimInfo.Manager.FirstName,
                    LastName = claimInfo.Manager.LastName
                },
                AdjusterId = claimInfo.AdjusterId,
                AdjusterName = claimInfo.Adjuster != null ? String.Format("{0} {1}", claimInfo.Adjuster.FirstName, claimInfo.Adjuster.LastName) : string.Empty,
                Adjuster = new UserViewModel
                {
                    UserId = claimInfo.Adjuster.Id,
                    FirstName = claimInfo.Adjuster.FirstName,
                    LastName = claimInfo.Adjuster.LastName
                },
                Branch = claimInfo.Branch,
                LossDescription = claimInfo.LossDescription,
                Instruction = claimInfo.Instruction,

                VinNo = claimInfo.VinNo,
                Deductible = claimInfo.Deductible,
                WindDeductible = claimInfo.WindDeductible,
                Premium = claimInfo.Premium,
                Building = claimInfo.Building,
                Subrogation = claimInfo.Subrogation,
                Salvage = claimInfo.Salvage,
                BusinessPersonalProperty = claimInfo.BusinessPersonalProperty,
                Contents = claimInfo.Contents,
                Ale = claimInfo.Ale,
                DetachedPrivateStructures = claimInfo.DetachedPrivateStructures,
                CondominiumImprovements = claimInfo.CondominiumImprovements,
                IndustrialHygenist = claimInfo.IndustrialHygenist,
                AdjustingFee = Convert.ToString(claimInfo.AdjustingFee),

                FirstContactDate = claimInfo.FirstContactDate.Date.ToString("M/d/yyyy"),
                InspectionDate = claimInfo.InspectionDate != null ? claimInfo.InspectionDate.Value.Date.ToString("M/d/yyyy") : string.Empty,
                DueDate = claimInfo.DueDate != null ? claimInfo.DueDate.Value.Date.ToString("M/d/yyyy") : string.Empty,
                ClosedDate = claimInfo.ClosedDate != null ? claimInfo.ClosedDate.Value.Date.ToString("M/d/yyyy") : string.Empty,
                AcknowledgementMail = claimInfo.AcknowledgementMail,
                LossNotice = claimInfo.LossNotice,
                StatusId = claimInfo.ClaimStatusId,
                Status = claimInfo.ClaimStatus != null ? claimInfo.ClaimStatus.Name : string.Empty
            };
        }

        public async Task<IEnumerable<ClaimViewModel>> Find(List<Expression<Func<Claim, bool>>> filterPredicates)
        {
            return this.ClaimRepository.Find(filterPredicates).Select(e =>
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

        public async Task<ClaimViewModel> Create(ClaimViewModel viewModel)
        {
            var claim = new Claim
            {
                FileNo = string.Format("{0}-{1}-{2:000000}"
                                        , viewModel.Company != null && !string.IsNullOrWhiteSpace(viewModel.Company.CompanyName) && viewModel.Company.CompanyName.Length > 3
                                                        ? viewModel.Company.CompanyName.Substring(0, 3) : "COM"
                                        , DateTime.Now.Year
                                        , (new Random()).Next(1, 999999)),
                ClaimNo = viewModel.ClaimNo,
                CompanyId = viewModel.Company.CompanyId,
                ContactId = viewModel.Contact.ContactId,
                ClaimantId = viewModel.Claimant.ContactId,
                PolicyNo = viewModel.PolicyNo,
                PolicyEffectiveDate = !string.IsNullOrWhiteSpace(viewModel.PolicyEffectiveDate) ? DateTime.ParseExact(viewModel.PolicyEffectiveDate, "M/d/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null,
                PolicyExpirationDate = !string.IsNullOrWhiteSpace(viewModel.PolicyExpirationDate) ? DateTime.ParseExact(viewModel.PolicyExpirationDate, "M/d/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null,
                LoanNo = viewModel.LoanNo,
                Mortgagee = viewModel.Mortgagee,
                ReceivedDate = !string.IsNullOrWhiteSpace(viewModel.ReceivedDate) ? DateTime.ParseExact(viewModel.ReceivedDate, "M/d/yyyy", CultureInfo.InvariantCulture) : DateTime.Now,
                LossDate = !string.IsNullOrWhiteSpace(viewModel.LossDate) ? DateTime.ParseExact(viewModel.LossDate, "M/d/yyyy", CultureInfo.InvariantCulture) : DateTime.Now,
                LossType = viewModel.LossType,
                AdjustmentType = viewModel.AdjustmentType,
                LossTotalUrgent = viewModel.LossTotalUrgent,
                MouldClaim = viewModel.MouldClaim,
                ManagerId = viewModel.Manager.UserId,
                AdjusterId = viewModel.Adjuster.UserId,
                Branch = viewModel.Branch,
                LossDescription = viewModel.LossDescription,
                Instruction = viewModel.Instruction,
                VinNo = viewModel.VinNo,
                Deductible = viewModel.Deductible,
                WindDeductible = viewModel.WindDeductible,
                Premium = viewModel.Premium,
                Building = viewModel.Building,
                Subrogation = viewModel.Subrogation,
                Salvage = viewModel.Salvage,
                BusinessPersonalProperty = viewModel.BusinessPersonalProperty,
                Contents = viewModel.Contents,
                Ale = viewModel.Ale,
                DetachedPrivateStructures = viewModel.DetachedPrivateStructures,
                CondominiumImprovements = viewModel.CondominiumImprovements,
                IndustrialHygenist = viewModel.IndustrialHygenist,
                AdjustingFee = !string.IsNullOrWhiteSpace(viewModel.AdjustingFee) ? Convert.ToDecimal(viewModel.AdjustingFee) : (decimal?)null,
                FirstContactDate = !string.IsNullOrWhiteSpace(viewModel.FirstContactDate) ? DateTime.ParseExact(viewModel.FirstContactDate, "M/d/yyyy", CultureInfo.InvariantCulture) : DateTime.Now,
                InspectionDate = !string.IsNullOrWhiteSpace(viewModel.InspectionDate) ? DateTime.ParseExact(viewModel.InspectionDate, "M/d/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null,
                DueDate = !string.IsNullOrWhiteSpace(viewModel.DueDate) ? DateTime.ParseExact(viewModel.DueDate, "M/d/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null,
                AcknowledgementMail = viewModel.AcknowledgementMail,
                LossNotice = viewModel.LossNotice,
                ClaimStatusId = viewModel.StatusId,
                CreatedBy = viewModel.CreatedBy,
                CreatedOn = viewModel.CreatedOn,
                LastModifiedBy = viewModel.LastModifiedBy,
                LastModifiedOn = viewModel.LastModifiedOn,
                IsActive = viewModel.IsActive
            };

            claim = this.ClaimRepository.Add(claim);

            if (claim.ClaimId > 0)
            {
                viewModel.ClaimId = claim.ClaimId;
                viewModel.FileNo = claim.FileNo;
            }
            else
            {
                viewModel.HasError = true;
            }

            return viewModel;
        }

        public async Task<ClaimViewModel> Update(ClaimViewModel viewModel)
        {
            var claimInfo = this.ClaimRepository.Find(viewModel.ClaimId);
            if (claimInfo != null && claimInfo.IsActive)
            {
                var lastModifiedDate = claimInfo.LastModifiedOn;

                claimInfo.CompanyId = viewModel.Company.CompanyId;
                claimInfo.ContactId = viewModel.Contact.ContactId;
                claimInfo.ClaimNo = viewModel.ClaimNo;
                claimInfo.ClaimantId = viewModel.Claimant.ContactId;
                claimInfo.PolicyNo = viewModel.PolicyNo;
                claimInfo.PolicyEffectiveDate = !string.IsNullOrWhiteSpace(viewModel.PolicyEffectiveDate) ? DateTime.ParseExact(viewModel.PolicyEffectiveDate, "M/d/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                claimInfo.PolicyExpirationDate = !string.IsNullOrWhiteSpace(viewModel.PolicyExpirationDate) ? DateTime.ParseExact(viewModel.PolicyExpirationDate, "M/d/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                claimInfo.LoanNo = viewModel.LoanNo;
                claimInfo.Mortgagee = viewModel.Mortgagee;
                claimInfo.ReceivedDate = !string.IsNullOrWhiteSpace(viewModel.ReceivedDate) ? DateTime.ParseExact(viewModel.ReceivedDate, "M/d/yyyy", CultureInfo.InvariantCulture) : DateTime.Now;
                claimInfo.LossDate = !string.IsNullOrWhiteSpace(viewModel.LossDate) ? DateTime.ParseExact(viewModel.LossDate, "M/d/yyyy", CultureInfo.InvariantCulture) : DateTime.Now;
                claimInfo.LossType = viewModel.LossType;
                claimInfo.AdjustmentType = viewModel.AdjustmentType;
                claimInfo.LossTotalUrgent = viewModel.LossTotalUrgent;
                claimInfo.MouldClaim = viewModel.MouldClaim;
                claimInfo.ManagerId = viewModel.Manager.UserId;
                claimInfo.AdjusterId = viewModel.Adjuster.UserId;
                claimInfo.Branch = viewModel.Branch;
                claimInfo.LossDescription = viewModel.LossDescription;
                claimInfo.Instruction = viewModel.Instruction;
                claimInfo.VinNo = viewModel.VinNo;
                claimInfo.Deductible = viewModel.Deductible;
                claimInfo.WindDeductible = viewModel.WindDeductible;
                claimInfo.Premium = viewModel.Premium;
                claimInfo.Building = viewModel.Building;
                claimInfo.Subrogation = viewModel.Subrogation;
                claimInfo.Salvage = viewModel.Salvage;
                claimInfo.BusinessPersonalProperty = viewModel.BusinessPersonalProperty;
                claimInfo.Contents = viewModel.Contents;
                claimInfo.Ale = viewModel.Ale;
                claimInfo.DetachedPrivateStructures = viewModel.DetachedPrivateStructures;
                claimInfo.CondominiumImprovements = viewModel.CondominiumImprovements;
                claimInfo.IndustrialHygenist = viewModel.IndustrialHygenist;
                claimInfo.AdjustingFee = !string.IsNullOrWhiteSpace(viewModel.AdjustingFee) ? Convert.ToDecimal(viewModel.AdjustingFee) : (decimal?)null;
                claimInfo.FirstContactDate = !string.IsNullOrWhiteSpace(viewModel.FirstContactDate) ? DateTime.ParseExact(viewModel.FirstContactDate, "M/d/yyyy", CultureInfo.InvariantCulture) : DateTime.Now;
                claimInfo.InspectionDate = !string.IsNullOrWhiteSpace(viewModel.InspectionDate) ? DateTime.ParseExact(viewModel.InspectionDate, "M/d/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                claimInfo.DueDate = !string.IsNullOrWhiteSpace(viewModel.DueDate) ? DateTime.ParseExact(viewModel.DueDate, "M/d/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                claimInfo.AcknowledgementMail = viewModel.AcknowledgementMail;
                claimInfo.LossNotice = viewModel.LossNotice;
                claimInfo.ClaimStatusId = viewModel.StatusId;
                claimInfo.LastModifiedBy = viewModel.LastModifiedBy;
                claimInfo.LastModifiedOn = viewModel.LastModifiedOn;
                claimInfo.IsActive = viewModel.IsActive;

                claimInfo = this.ClaimRepository.Update(claimInfo);

                if (lastModifiedDate < claimInfo.LastModifiedOn)
                {
                    return viewModel;
                }
                else
                {
                    viewModel.HasError = true;
                }
            }

            return viewModel;
        }

        public async Task<bool> Delete(int claimId)
        {
            var claimDetails = this.ClaimRepository.Find(claimId);

            if (claimDetails != null)
            {
                claimDetails.IsActive = false;
                var deletedClaim = this.ClaimRepository.Delete(claimDetails);

                if (!deletedClaim.IsActive)
                    return true;
            }

            return false;
        }



        public async Task<IEnumerable<ClaimViewModel>> GetAllClaimsDueDays(string userId, bool isDueToday)
        {
            List<Expression<Func<Claim, bool>>> filterPredicates = new List<Expression<Func<Claim, bool>>>();
            filterPredicates.Add(p => p.AdjusterId == userId);
            if (isDueToday)
            {
                filterPredicates.Add(p => p.DueDate.Value == DateTime.Today);
            }
            else
            {
                DateTime dueIn7Days= DateTime.Today.AddDays(7);
                filterPredicates.Add(p => p.DueDate.Value > DateTime.Today);
                filterPredicates.Add(p => p.DueDate.Value <= dueIn7Days);
            }
             
            return this.ClaimRepository.Find(filterPredicates).ToList().Select(e =>
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
                    ReceivedDate = e.ReceivedDate.Date.ToString("M/d/yyyy"),
                    DueDate = e.DueDate.Value.ToString("M/d/yyyy"),
                    AdjusterId = e.AdjusterId,
                    AdjusterName = e.Adjuster != null ? String.Format("{0} {1}", e.Adjuster.FirstName, e.Adjuster.LastName) : string.Empty,
                    Status = e.ClaimStatus != null ? e.ClaimStatus.Name : string.Empty,
                    IsActive = e.IsActive
                }).OrderByDescending(e=>e.ReceivedDate);
        }

        public async Task<IEnumerable<ClaimViewModel>> GetOpenClaims()
        {
            List<Expression<Func<Claim, bool>>> filterPredicates = new List<Expression<Func<Claim, bool>>>();
            filterPredicates.Add(p => p.ClaimStatusId == 1);

            return EntityToViewModelMapper.ClaimsMap(this.ClaimRepository.Find(filterPredicates).ToList());
        }

        public async Task<IEnumerable<ClaimViewModel>> GetAverageDaysOpenTasks()
        {
            List<Expression<Func<Claim, bool>>> filterPredicates = new List<Expression<Func<Claim, bool>>>();
            filterPredicates.Add(p => p.ClaimStatusId == 2);

            return EntityToViewModelMapper.ClaimsMap(this.ClaimRepository.Find(filterPredicates).ToList()); 
        }

        public async Task<IEnumerable<ClaimViewModel>> GetOverdueClaims(string userId)
        {
            List<Expression<Func<Claim, bool>>> filterPredicates = new List<Expression<Func<Claim, bool>>>();
            filterPredicates.Add(p => p.AdjusterId == userId);
            filterPredicates.Add(p => p.DueDate < DateTime.Today);
            return EntityToViewModelMapper.ClaimsMap(this.ClaimRepository.Find(filterPredicates).ToList()); 
        }

    }

    #endregion
}
