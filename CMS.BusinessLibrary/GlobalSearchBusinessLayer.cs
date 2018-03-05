
using System;
using System.Reflection;
namespace CMS.BusinessLibrary
{
    #region Namespace
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
    public interface IGlobalSearchBusinessLayer
    {
        Task<GlobalSearchViewModel> GlobalSearch(string searchText);

    }

    #endregion

    #region Class


    public sealed partial class GlobalSearchBusinessLayer : IGlobalSearchBusinessLayer
    {
        private readonly IGlobalSearchRepository _GlobalSearchRepository;


        public GlobalSearchBusinessLayer()
        {
            this._GlobalSearchRepository = new GlobalSearchRepository();
        }

        private IGlobalSearchRepository GlobalSearchRepository
        {
            get { return this._GlobalSearchRepository; }
        }

        public async Task<GlobalSearchViewModel> GlobalSearch(string searchText)
        {
            GlobalSearchViewModel objGlobalSearchViewModel = new GlobalSearchViewModel();
            CompanyViewModel obj = new CompanyViewModel();
            var objGlobalSearch = this.GlobalSearchRepository.GlobalSearch(searchText);

            if (objGlobalSearch != null)
            {
                if (objGlobalSearch.Company.Any())
                {
                    objGlobalSearchViewModel.CompanyViewModel = objGlobalSearch.Company.Select(e =>
                        new CompanyViewModel
                        {
                            CompanyId = e.AccountId,
                            CompanyName = e.CompanyName,
                            Phone = e.Phone,
                            Type = e.Type,
                            AlternatePhone = e.AlternatePhone,
                            ContactEmailId = e.EmailId,
                            KeyContact = e.KeyContact ?? 0


                        }).ToList();
                }
                if (objGlobalSearch.Contact.Any())
                {
                    objGlobalSearchViewModel.ContactViewModel = objGlobalSearch.Contact.Select(e =>
                   new ContactViewModel
                   {
                       ContactId = e.ContactId,
                       FirstName = e.FirstName,
                       LastName = e.LastName,
                       CompanyId = e.CompanyId,

                       CompanyName = e.Company != null ? e.Company.CompanyName : string.Empty,
                       ContactTypeId = e.ContactTypeId,
                       ContactTypeName = e.ContactType!=null ? e.ContactType.ContactTypeName : string.Empty,
                       OwnerId = e.OwnerId,
                       OwnerName = e.Owner !=null ? e.Owner.UserName:string.Empty,
                       Email = e.EmailId,
                       Phone = e.Phone,
                       Cell = e.Cell,
                       // IsKeyContact = e.IsKeyContact,
                       Street = e.Street,
                       City = e.City,
                       StateId = e.ProvinceId,
                       StateName = e.Province!=null ? e.Province.ProvinceName: string.Empty,
                       CountryId = e.CountryId,
                       CountryName = e.Country != null ? e.Country.CountryName : string.Empty,
                       PostalCode = e.Postal


                   }).ToList();
                }

                if ( objGlobalSearch.Claim.Any())
                {
                    objGlobalSearchViewModel.ClaimViewModel = objGlobalSearch.Claim.Select(e =>
                   new ClaimViewModel
                   {
                       ClaimId = e.ClaimId,
                       FileNo = e.FileNo,
                       ClaimNo = e.ClaimNo,
                       CompanyId = e.CompanyId,
                       CompanyName = e.Company !=null ?e.Company.CompanyName:string.Empty,
                       ContactId = e.ContactId,
                       ContactName = e.Contact != null ? String.Format("{0} {1}", e.Contact.FirstName, e.Contact.LastName) : string.Empty,
                       ClaimantId = e.ClaimantId,
                       ClaimantName = e.Claimant != null ? String.Format("{0} {1}", e.Claimant.FirstName, e.Claimant.LastName) : string.Empty,
                       ReceivedDate = e.ReceivedDate.Date.ToString("M/d/yyyy"),
                       AdjusterId = e.AdjusterId,
                       AdjusterName = e.Adjuster != null ? String.Format("{0} {1}", e.Adjuster.FirstName, e.Adjuster.LastName) : string.Empty,
                       Status = e.ClaimStatus != null ? e.ClaimStatus.Name : string.Empty


                   }).ToList();
                }
                //ContactViewModel = CopyClass.CopyObject<ContactViewModel>(objGlobalSearch.Contact, new ContactViewModel();
                //ClaimViewModel = CopyClass.CopyObject<ClaimViewModel>(e.Claim, new ClaimViewModel())
            }
            //new GlobalSearchViewModel
            //{
            //   //ClaimId = e.ClaimId,
            //    CompanyViewModel = CopyClass.CopyObject<CompanyViewModel>(e.Company, obj),
            //    ContactViewModel = CopyClass.CopyObject<ContactViewModel>(e.Contact, new ContactViewModel()),
            //    ClaimViewModel = CopyClass.CopyObject<ClaimViewModel>(e.Claim, new ClaimViewModel())
            //    ////FileNo = e.FileNo,
            //   //ClaimNo = e.ClaimNo,
            //   //CompanyId = e.CompanyId,
            //   //CompanyName = e.Company.CompanyName,
            //   //ContactId = e.ContactId,
            //   //ContactName = e.Contact != null ? String.Format("{0} {1}", e.Contact.FirstName, e.Contact.LastName) : string.Empty,
            //   //ClaimantId = e.ClaimantId,
            //   //ClaimantName = e.Claimant != null ? String.Format("{0} {1}", e.Claimant.FirstName, e.Claimant.LastName) : string.Empty,
            //   //ReceivedDate = e.ReceivedDate.Date.ToString("M/d/yyyy"),
            //   //AdjusterId = e.AdjusterId,
            //   //AdjusterName = e.Adjuster != null ? String.Format("{0} {1}", e.Adjuster.FirstName, e.Adjuster.LastName) : string.Empty,
            //   //Status = e.ClaimStatus != null ? e.ClaimStatus.Status : string.Empty

            //});
            return objGlobalSearchViewModel;
        }

    }

    #endregion
}

public class CopyClass
{
    /// <summary>
    /// Copy an object to destination object, only matching fields will be copied
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sourceObject">An object with matching fields of the destination object</param>
    /// <param name="destObject">Destination object, must already be created</param>
    public static T CopyObject<T>(object sourceObject, T destObject)
    {

        //  Get the type of each object
        Type sourceType = sourceObject.GetType();
        Type targetType = destObject.GetType();

        //  Loop through the source properties
        foreach (PropertyInfo p in sourceType.GetProperties())
        {
            //  Get the matching property in the destination object
            PropertyInfo targetObj = targetType.GetProperty(p.Name);
            //  If there is none, skip
            if (targetObj == null)
                continue;

            //  Set the value in the destination
            targetObj.SetValue(destObject, p.GetValue(sourceObject, null), null);
        }

        return destObject;
    }
}