
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
    public interface ITimeLogBusinessLayer
    {
        Task<IEnumerable<TimeLogViewModel>> GetAllTimeLogs(string userId, int claimId);
        Task<TimeLogViewModel> Find(int timelogId);
        Task<IEnumerable<TimeLogViewModel>> Find(List<Expression<Func<TimeLog, bool>>> filterPredicates);
        Task<TimeLogViewModel> Create(TimeLogViewModel viewModel);
        Task<TimeLogViewModel> Update(TimeLogViewModel viewModel);

        Task<TimeLogViewModel> IsBilledUpdate(TimeLogViewModel viewModel);
        Task<bool> Delete(int timelogId);
        IEnumerable<TimeLogViewModel> GetAllTimeLogsforPdf(string userId, int claimId, int timelogId);
    }
    #endregion

    #region Implemetation
    public sealed class TimeLogBusinessLayer : ITimeLogBusinessLayer
    {
        private readonly ITimeLogRepository _timelogRepository;


        public TimeLogBusinessLayer()
        {
            this._timelogRepository = new TimeLogRepository();
        }

        private ITimeLogRepository TimeLogRepository
        {
            get { return this._timelogRepository; }
        }

        public async Task<IEnumerable<TimeLogViewModel>> GetAllTimeLogs(string userId, int claimId)
        {
            List<Expression<Func<TimeLog, bool>>> filterPredicates = new List<Expression<Func<TimeLog, bool>>>();
            filterPredicates.Add(p => p.ClaimId == claimId);

            return EntityToViewModelMapper.Map(this.TimeLogRepository.Find(filterPredicates));
        }

        public IEnumerable<TimeLogViewModel> GetAllTimeLogsforPdf(string userId, int claimId,int timelogId)
        {
            List<Expression<Func<TimeLog, bool>>> filterPredicates = new List<Expression<Func<TimeLog, bool>>>();
            filterPredicates.Add(p => p.ClaimId == claimId);
            filterPredicates.Add(p => p.TimeLogId == timelogId);

            return EntityToViewModelMapper.Map(this.TimeLogRepository.Find(filterPredicates));
        }

        public async Task<TimeLogViewModel> Find(int timelogId)
        {
            return EntityToViewModelMapper.Map(this.TimeLogRepository.Find(timelogId));
        }

        public async Task<IEnumerable<TimeLogViewModel>> Find(List<Expression<Func<TimeLog, bool>>> filterPredicates)
        {
            return EntityToViewModelMapper.Map(this.TimeLogRepository.Find(filterPredicates));
        }

        public async Task<TimeLogViewModel> Create(TimeLogViewModel viewModel)
        {
            var timelog = this.TimeLogRepository.Add(ViewModelToEntityMapper.Map(viewModel));
            if (timelog.TimeLogId > 0)
            {
                viewModel.TimeLogId = timelog.TimeLogId;
            }
            else
            {
                viewModel.HasError = true;
            }

            return viewModel;
        }

        public async Task<TimeLogViewModel> Update(TimeLogViewModel viewModel)
        {
            var timelog = this.TimeLogRepository.Find(viewModel.TimeLogId);
            if (timelog != null && timelog.IsActive)
            {
                var lastModifiedDate = timelog.LastModifiedOn;
                timelog = this.TimeLogRepository.Update(ViewModelToEntityMapper.Map(viewModel, timelog));

                if (lastModifiedDate < timelog.LastModifiedOn)
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

        public async Task<TimeLogViewModel> IsBilledUpdate(TimeLogViewModel viewModel)
        {

            List<Expression<Func<TimeLog, bool>>> filterPredicates = new List<Expression<Func<TimeLog, bool>>>();
            filterPredicates.Add(p => p.IsActive == true);
            filterPredicates.Add(p => p.ClaimId == viewModel.ClaimId);
            filterPredicates.Add(p => p.TimeLogId == viewModel.TimeLogId);

            //return EntityToViewModelMapper.Map(this.TimeLogRepository.Find(filterPredicates).ToList());

            // EntityToViewModelMapper.Map(this.TimeLogRepository.Find(filterPredicates));

            var timelog = this.TimeLogRepository.Find(filterPredicates).FirstOrDefault();
            if (timelog != null )
            {
                var lastModifiedDate = timelog.LastModifiedOn;
                timelog = this.TimeLogRepository.Update(ViewModelToEntityMapper.IsBilledMap(viewModel, timelog));

                if (lastModifiedDate < timelog.LastModifiedOn)
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


        public async Task<bool> Delete(int timelogId)
        {
            var timelogDetails = this.TimeLogRepository.Find(timelogId);

            if (timelogDetails != null)
            {
                timelogDetails.IsActive = false;
                var deletedTimeLog = this.TimeLogRepository.Delete(timelogDetails);

                if (!deletedTimeLog.IsActive)
                    return true;
            }

            return false;
        }
    }
    #endregion
}
