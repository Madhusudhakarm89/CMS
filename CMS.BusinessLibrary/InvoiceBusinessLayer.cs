
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
    public interface IInvoiceBusinessLayer
    {
        Task<IEnumerable<InvoiceViewModel>> GetAllInvoices(string userId);
        Task<IEnumerable<InvoiceViewModel>> GetAllInvoices(string userId, int claimId);
        Task<InvoiceViewModel> Find(int invoiceId);
        Task<IEnumerable<InvoiceViewModel>> Find(IEnumerable<FilterParameterViewModel> viewModel);
        Task<InvoiceViewModel> Create(InvoiceViewModel viewModel);
        Task<InvoiceViewModel> Update(InvoiceViewModel viewModel);
        Task<bool> Delete(int invoiceId);
        IEnumerable<InvoiceViewModel> GetAllInvoicesforPdf(string userId, int claimId,int invoiceId);
    }
    #endregion

    #region Implemetation
    public sealed class InvoiceBusinessLayer : IInvoiceBusinessLayer
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapping_InvoiceTimelogRepository _invoiceTimelogRepository;

        public InvoiceBusinessLayer()
        {
            this._invoiceRepository = new InvoiceRepository();
            this._invoiceTimelogRepository = new Mapping_InvoiceTimelogRepository();
        }

        private IInvoiceRepository InvoiceRepository
        {
            get { return this._invoiceRepository; }
        }

        private IMapping_InvoiceTimelogRepository InvoiceTimelogRepository
        {
            get { return this._invoiceTimelogRepository; }
        }

        public async Task<IEnumerable<InvoiceViewModel>> GetAllInvoices(string userId)
        {
            return EntityToViewModelMapper.Map(this.InvoiceRepository.AllRecords);
        }

        public async Task<IEnumerable<InvoiceViewModel>> GetAllInvoices(string userId, int claimId)
        {
            List<Expression<Func<Invoice, bool>>> filterPredicates = new List<Expression<Func<Invoice, bool>>>();
            filterPredicates.Add(p => p.ClaimId == claimId);

            return EntityToViewModelMapper.Map(this.InvoiceRepository.Find(filterPredicates));
        }

        public IEnumerable<InvoiceViewModel> GetAllInvoicesforPdf(string userId, int claimId, int invoiceId)
        {
            List<Expression<Func<Invoice, bool>>> filterPredicates = new List<Expression<Func<Invoice, bool>>>();
            filterPredicates.Add(p => p.ClaimId == claimId);
            filterPredicates.Add(p => p.InvoiceId == invoiceId);

            return EntityToViewModelMapper.Map(this.InvoiceRepository.Find(filterPredicates));
        }


        public async Task<InvoiceViewModel> Find(int invoiceId)
        {
            return EntityToViewModelMapper.Map(this.InvoiceRepository.Find(invoiceId));
        }

        public async Task<IEnumerable<InvoiceViewModel>> Find(IEnumerable<FilterParameterViewModel> viewModel)
        {
            List<Expression<Func<Invoice, bool>>> filterPredicates = new List<Expression<Func<Invoice, bool>>>();
            foreach (FilterParameterViewModel model in viewModel)
            {
                foreach (int companyId in model.CompanyId)
                {
                    filterPredicates.Add(p => p.InvoiceClaim.CompanyId == companyId);
                }
            }

            return EntityToViewModelMapper.Map(this.InvoiceRepository.Find(filterPredicates));
        }

        public async Task<InvoiceViewModel> Create(InvoiceViewModel viewModel)
        {
            var newInvoice = ViewModelToEntityMapper.Map(viewModel);
            var invoice = this.InvoiceRepository.Add(newInvoice);

            if (invoice.InvoiceId > 0)
            {


                //if (newInvoice.InvoiceTimelogs != null)
                //{
                //    foreach (var timelog in newInvoice.InvoiceTimelogs)
                //    {
                //        var newTimelogMapping = this.InvoiceTimelogRepository.Add(timelog);
                //        if (newTimelogMapping.Id <= 0)
                //        {
                //            viewModel.HasError = true;
                //            break;
                //        }
                //    }
                //}

                viewModel.InvoiceId = invoice.InvoiceId;
            }
            else
            {
                viewModel.HasError = true;
            }

            return viewModel;
        }

        public async Task<InvoiceViewModel> Update(InvoiceViewModel viewModel)
        {
            var invoice = this.InvoiceRepository.Find(viewModel.InvoiceId);
            if (invoice != null && invoice.IsActive)
            {
                var lastModifiedDate = invoice.LastModifiedOn;
                ViewModelToEntityMapper.Map(viewModel, invoice);

                var invoiceTimelogs = invoice.InvoiceTimelogs;
                invoice.InvoiceTimelogs = null;

                invoice = this.InvoiceRepository.Update(invoice);

                if (lastModifiedDate < invoice.LastModifiedOn)
                {
                    if (invoiceTimelogs != null)
                    {
                        foreach (var timelog in invoiceTimelogs)
                        {
                            if (timelog.Id > 0)
                            {
                                var invoiceTimelog = this.InvoiceTimelogRepository.Find(timelog.Id);
                                if (invoiceTimelog != null && invoiceTimelog.IsActive)
                                {
                                    invoiceTimelog.Id = timelog.Id;
                                    invoiceTimelog.InvoiceId = timelog.InvoiceId;
                                    invoiceTimelog.TimelogId = timelog.TimelogId;
                                    invoiceTimelog.ServiceTotal = timelog.ServiceTotal;
                                    invoiceTimelog.LastModifiedBy = timelog.LastModifiedBy;
                                    invoiceTimelog.LastModifiedOn = timelog.LastModifiedOn;

                                    var newTimelogMapping = this.InvoiceTimelogRepository.Update(invoiceTimelog);
                                    if (newTimelogMapping.Id <= 0)
                                    {
                                        viewModel.HasError = true;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                var invoiceTimelog = this.InvoiceTimelogRepository.Add(timelog);
                                if (invoiceTimelog.Id <= 0)
                                {
                                    viewModel.HasError = true;
                                    break;
                                }
                            }
                        }
                    }

                    return viewModel;
                }
                else
                {
                    viewModel.HasError = true;
                }
            }

            return viewModel;
        }

        public async Task<bool> Delete(int invoiceId)
        {
            var invoiceDetails = this.InvoiceRepository.Find(invoiceId);

            if (invoiceDetails != null)
            {
                invoiceDetails.IsActive = false;
                var deletedInvoice = this.InvoiceRepository.Delete(invoiceDetails);

                if (!deletedInvoice.IsActive)
                    return true;
            }

            return false;
        }
    }
    #endregion
}
