using CMS.BusinessLibrary.ViewModels;
using CMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BusinessLibrary
{
    public interface IContactTypeBusinessLayer
    {
        Task<IEnumerable<ContactTypeViewModel>> GetAllContactTypes();
    }
    public sealed partial class ContactTypeBusinessLayer : IContactTypeBusinessLayer
    {
        private readonly IContactTypeRepository _contactTypeRepository;

        public ContactTypeBusinessLayer()
        {
            this._contactTypeRepository = new ContactTypeRepository();
        }

        private IContactTypeRepository ContactTypeRepository
        {
            get { return this._contactTypeRepository; }
        }

        public async Task<IEnumerable<ContactTypeViewModel>> GetAllContactTypes()
        {
            return this.ContactTypeRepository.AllRecords.Select(e =>
                new ContactTypeViewModel
                {
                   ContactTypeId=e.ContactTypeId,
                   ContactTypeName=e.ContactTypeName
                });
        }
    }
}
