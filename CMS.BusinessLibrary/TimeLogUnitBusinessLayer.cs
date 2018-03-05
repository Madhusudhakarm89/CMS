
namespace CMS.BusinessLibrary
{
    #region Namespace
    using CMS.BusinessLibrary.EntityModelMapping;
    using CMS.BusinessLibrary.ViewModels;
    using CMS.Entity;
    using CMS.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    #endregion

    #region Interface
    public interface ITimeLogUnitBusinessLayer
    {
        Task<IEnumerable<TimeLogUnitViewModel>> GetAllTimeLogUnits();
        Task<TimeLogUnitViewModel> Find(int unitId);
        Task<IEnumerable<TimeLogUnitViewModel>> Find(List<Expression<Func<TimeLogUnit, bool>>> filterPredicate);
        Task<TimeLogUnitViewModel> Create(TimeLogUnitViewModel model);
        Task<TimeLogUnitViewModel> Update(TimeLogUnitViewModel model);

        Task<bool> Delete(int unitId);
    }

    #endregion

    #region Class
    public sealed partial class TimeLogUnitBusinessLayer : ITimeLogUnitBusinessLayer
    {
        private readonly ITimeLogUnitRepository _timeLogUnitRepository;


        public TimeLogUnitBusinessLayer()
        {
            this._timeLogUnitRepository = new TimeLogUnitRepository();
        }

        private ITimeLogUnitRepository TimeLogUnitRepository
        {
            get { return this._timeLogUnitRepository; }
        }

        public async Task<IEnumerable<TimeLogUnitViewModel>> GetAllTimeLogUnits()
        {
            return EntityToViewModelMapper.Map(this.TimeLogUnitRepository.AllRecords);
        }

        public async Task<TimeLogUnitViewModel> Find(int unitId)
        {
            return EntityToViewModelMapper.Map(this.TimeLogUnitRepository.Find(unitId));
        }

        public async Task<IEnumerable<TimeLogUnitViewModel>> Find(List<Expression<Func<TimeLogUnit, bool>>> filterPredicate)
        {
            return EntityToViewModelMapper.Map(this.TimeLogUnitRepository.Find(filterPredicate));
        }

        public async Task<TimeLogUnitViewModel> Create(TimeLogUnitViewModel model)
        {
            var timelogUnit = this.TimeLogUnitRepository.Add(ViewModelToEntityMapper.Map(model));
            if (timelogUnit.Id > 0)
            {
                model.UnitId = timelogUnit.Id;
            }
            else
            {
                model.HasError = true;
            }

            return model;
        }

        public async Task<TimeLogUnitViewModel> Update(TimeLogUnitViewModel model)
        {
            var timelogUnit = this.TimeLogUnitRepository.Find(model.UnitId);
            if (timelogUnit != null)
            {
                var lastModifiedDate = timelogUnit.LastModifiedDate;
                timelogUnit = this.TimeLogUnitRepository.Update(ViewModelToEntityMapper.Map(model, timelogUnit));

                if (lastModifiedDate < timelogUnit.LastModifiedDate)
                {
                    return model;
                }
                else
                {
                    model.HasError = true;
                }
            }

            return model;
        }


        public async Task<bool> Delete(int unitId)
        {
            var timelogUnitDetails = this.TimeLogUnitRepository.Find(unitId);

            if (timelogUnitDetails != null)
            {
                timelogUnitDetails.IsActive = false;
                var deletedTimeLogUnit = this.TimeLogUnitRepository.Delete(timelogUnitDetails);

                if (!deletedTimeLogUnit.IsActive)
                    return true;
            }

            return false;
        }
    }
    #endregion
}