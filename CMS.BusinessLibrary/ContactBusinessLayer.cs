
namespace CMS.BusinessLibrary
{
    #region Namespace
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
    public interface IContactBusinessLayer
    {
        Task<IEnumerable<ContactViewModel>> GetAllContacts(string userId);
        Task<ContactViewModel> Find(int contactId);
        Task<IEnumerable<ContactViewModel>> Find(IEnumerable<ContactViewModel> viewModel);
        Task<ContactViewModel> Create(ContactViewModel viewModel);
        Task<ContactViewModel> Update(ContactViewModel viewModel);
        Task<bool> Delete(int contactId);
    }
    #endregion

    #region Class
    public sealed partial class ContactBusinessLayer : IContactBusinessLayer
    {
        private readonly IContactRepository _contactRepository;

        public ContactBusinessLayer()
        {
            this._contactRepository = new ContactRepository();
        }

        private IContactRepository ContactRepository
        {
            get { return this._contactRepository; }
        }

        public async Task<IEnumerable<ContactViewModel>> GetAllContacts(string userId)
        {
            return this.ContactRepository.AllRecords.Select(e =>
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
                    // IsKeyContact = e.IsKeyContact,
                    Street = e.Street,
                    City = e.City,
                    StateId = e.ProvinceId,
                    StateName = e.Province.ProvinceName,
                    CountryId = e.CountryId,
                    CountryName = e.Country.CountryName,
                    PostalCode = e.Postal
                }).OrderBy(e => e.FirstName).ThenBy(e => e.LastName);
        }

        public async Task<ContactViewModel> Find(int contactId)
        {
            var contactInfo = this.ContactRepository.Find(contactId);

            return new ContactViewModel
            {
                ContactId = contactInfo.ContactId,
                FirstName = contactInfo.FirstName,
                LastName = contactInfo.LastName,
                CompanyId = contactInfo.CompanyId,
                CompanyName = contactInfo.Company.CompanyName,
                ContactTypeId = contactInfo.ContactTypeId,
                ContactTypeName = contactInfo.ContactType.ContactTypeName,
                OwnerId = contactInfo.OwnerId,
                OwnerName = contactInfo.Owner.UserName,
                Email = contactInfo.EmailId,
                Phone = contactInfo.Phone,
                Cell = contactInfo.Cell,
                IsKeyContact = contactInfo.IsKeyContact,
                Street = contactInfo.Street,
                City = contactInfo.City,
                StateId = contactInfo.ProvinceId,
                StateName = contactInfo.Province.ProvinceName,
                CountryId = contactInfo.CountryId,
                CountryName = contactInfo.Country.CountryName,
                PostalCode = contactInfo.Postal
            };
        }

        public async Task<IEnumerable<ContactViewModel>> Find(IEnumerable<ContactViewModel> searchViewModel)
        {
            IEnumerable<ContactViewModel> searchResult = null;

            if (searchViewModel != null && searchViewModel.Any())
            {
                List<Expression<Func<Contact, bool>>> filterPredicates = new List<Expression<Func<Contact, bool>>>();

                foreach (ContactViewModel viewModel in searchViewModel)
                {
                    var parameter = Expression.Parameter(typeof(Contact), "p");
                    
                    foreach (var field in viewModel.GetType().GetProperties())
                    {
                        if (typeof(Contact).GetProperty(field.Name) != null && !field.DeclaringType.Name.Equals("BaseViewModel", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var fieldValue = field.GetValue(viewModel, null);
                            if (fieldValue != null && !string.IsNullOrWhiteSpace(Convert.ToString(fieldValue))
                                    && !(field.Name.EndsWith("id", StringComparison.InvariantCultureIgnoreCase) && fieldValue.ToString().Trim().Equals("0")))
                            {
                                var propertyReference = Expression.Property(Expression.Convert(parameter, typeof(Contact)), field.Name);
                                var lambdaExpression = Expression.Lambda<Func<Contact, bool>>(Expression.Equal(propertyReference, Expression.Constant(fieldValue)), parameter);
                                filterPredicates.Add(lambdaExpression);
                            }
                        }
                    }
                }

                searchResult = this.ContactRepository.Find(filterPredicates).Select(e =>
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
                                        PostalCode = e.Postal
                                    });
            }

            return searchResult.OrderBy(e => e.ContactName);
        }

        public async Task<ContactViewModel> Create(ContactViewModel viewModel)
        {
            var contact = new Contact
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                CompanyId = viewModel.CompanyId,
                ContactTypeId = viewModel.ContactTypeId,
                EmailId = viewModel.Email,
                Phone = viewModel.Phone,
                Cell = viewModel.Cell,
                OwnerId = viewModel.OwnerId,
                IsKeyContact = viewModel.IsKeyContact,
                Street = viewModel.Street,
                City = viewModel.City,
                ProvinceId = viewModel.StateId,
                CountryId = viewModel.CountryId,
                Postal = viewModel.PostalCode,
                CreatedBy = viewModel.CreatedBy,
                CreatedOn = viewModel.CreatedOn,
                LastModifiedBy = viewModel.LastModifiedBy,
                LastModifiedOn = viewModel.LastModifiedOn,
                IsActive = viewModel.IsActive
            };

            contact = this.ContactRepository.Add(contact);

            if (contact.ContactId > 0)
            {
                viewModel.ContactId = contact.ContactId;
            }
            else
            {
                viewModel.HasError = true;
            }

            return viewModel;
        }

        public async Task<ContactViewModel> Update(ContactViewModel viewModel)
        {
            var contact = this.ContactRepository.Find(viewModel.ContactId);
            if (contact != null && contact.IsActive)
            {
                var lastModifiedDate = contact.LastModifiedOn;

                contact.FirstName = viewModel.FirstName;
                contact.LastName = viewModel.LastName;
                contact.CompanyId = viewModel.CompanyId;
                contact.ContactTypeId = viewModel.ContactTypeId;
                contact.Phone = viewModel.Phone;
                contact.Cell = viewModel.Cell;
                contact.OwnerId = viewModel.OwnerId;
                contact.IsKeyContact = viewModel.IsKeyContact;
                contact.Street = viewModel.Street;
                contact.City = viewModel.City;
                contact.ProvinceId = viewModel.StateId;
                contact.CountryId = viewModel.CountryId;
                contact.Postal = viewModel.PostalCode;
                contact.LastModifiedBy = viewModel.LastModifiedBy;
                contact.LastModifiedOn = viewModel.LastModifiedOn;
                contact.IsActive = viewModel.IsActive;

                contact = this.ContactRepository.Update(contact);

                if (lastModifiedDate < contact.LastModifiedOn)
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

        public async Task<bool> Delete(int contactId)
        {
            var contact = this.ContactRepository.Find(contactId);
            if (contact != null)
            {
                contact.IsActive = false;
                contact = this.ContactRepository.Delete(contact);

                if (!contact.IsActive)
                    return true;
            }

            return false;
        }
    }
    #endregion
}
