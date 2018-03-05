
namespace CMS.BusinessLibrary.ViewModels
{
    #region Namespace
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    #endregion

    public class ClaimViewModel : BaseViewModel
    {
        public int ClaimId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public string FileNo { get; set; }
        public string ClaimNo { get; set; }
        public int ClaimantId { get; set; }
        public string ClaimantName { get; set; }
        public string PolicyNo { get; set; }
        public string PolicyEffectiveDate { get; set; }
        public string PolicyExpirationDate { get; set; }
        public string LoanNo { get; set; }
        public string Mortgagee { get; set; }
        public string ReceivedDate { get; set; }
        public string LossDate { get; set; }
        public string LossType { get; set; }
        public string AdjustmentType { get; set; }
        public bool LossTotalUrgent { get; set; }
        public bool MouldClaim { get; set; }
        public string ManagerId { get; set; }
        public string ManagerName { get; set; }
        public string AdjusterId { get; set; }
        public string AdjusterName { get; set; }
        public string Branch { get; set; }
        public string LossDescription { get; set; }
        public string Instruction { get; set; }

        public string VinNo { get; set; }
        public string Deductible { get; set; }
        public string WindDeductible { get; set; }
        public string Premium { get; set; }
        public string Building { get; set; }
        public string Subrogation { get; set; }
        public string Salvage { get; set; }
        public string BusinessPersonalProperty { get; set; }
        public string Contents { get; set; }
        public string Ale { get; set; }
        public string DetachedPrivateStructures { get; set; }
        public string CondominiumImprovements { get; set; }
        public string IndustrialHygenist { get; set; }
        public string AdjustingFee { get; set; }

        public string FirstContactDate { get; set; }
        public string InspectionDate { get; set; }
        public string DueDate { get; set; }
        public string ClosedDate { get; set; }
        public bool AcknowledgementMail { get; set; }
        public bool LossNotice { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }

        public CompanyViewModel Company { get; set; }
        public ContactViewModel Contact { get; set; }
        public ContactViewModel Claimant { get; set; }
        public UserViewModel Adjuster { get; set; }
        public UserViewModel Manager { get; set; }

        public double Days { get; set; }
    }
}