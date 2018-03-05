
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
    public interface IProvinceBusinessLayer
    {
        Task<IEnumerable<ProvinceViewModel>> GetAllProvinces();
        Task<ProvinceViewModel> Find(int provinceId);
        Task<IEnumerable<ProvinceViewModel>> Find(List<Expression<Func<Province, bool>>> filterPredicates);
        Task<ProvinceViewModel> Create(ProvinceViewModel item);
        Task<bool> Delete(int provinceId);
       
    }
        
    #endregion

    #region Class
    public sealed partial class ProvinceBusinessLayer : IProvinceBusinessLayer
    {
        private readonly IProvinceRepository _provinceRepository;


        public ProvinceBusinessLayer()
        {
            this._provinceRepository = new ProvinceRepository();
        }

        private IProvinceRepository ProvinceRepository
        {
            get { return this._provinceRepository; }
        }

        public async Task<IEnumerable<ProvinceViewModel>> GetAllProvinces()
        {
            return this.ProvinceRepository.AllRecords.Select(e =>
                new ProvinceViewModel
                {
                    ProvinceId = e.ProvinceId,
                    ProvinceName = e.ProvinceName,
                    CountryId=e.CountryId
                }).OrderBy(e => e.ProvinceName);
        }

        public async Task<ProvinceViewModel> Find(int provinceId)
        {
            var ProvinceInfo = this.ProvinceRepository.Find(provinceId);

            return new ProvinceViewModel
            {
                ProvinceId = ProvinceInfo.ProvinceId,
                ProvinceName = ProvinceInfo.ProvinceName
            };
        }

        public async Task<IEnumerable<ProvinceViewModel>> Find(List<Expression<Func<Province, bool>>> filterPredicates)
        {
            return this.ProvinceRepository.Find(filterPredicates).Select(e =>
                new ProvinceViewModel
                {
                    ProvinceId = e.ProvinceId,
                    ProvinceName = e.ProvinceName
                }).OrderBy(e => e.ProvinceName);
        }

        public async Task<ProvinceViewModel> Create(ProvinceViewModel province)
        {
            Province obj = new Province
            {
                ProvinceName = province.ProvinceName,
                CountryId = province.ProvinceId
            };


            var ProvinceInfo = this.ProvinceRepository.Add(obj);
            return new ProvinceViewModel
            {
                ProvinceName = ProvinceInfo.ProvinceName,
                ProvinceId = ProvinceInfo.ProvinceId
            };
            
            
        }


        public async Task<bool> Delete(int ProvinceId)
        {
            var ProvinceDetails = this.ProvinceRepository.Find(ProvinceId);

            if (ProvinceDetails != null)
            {

                var deletedProvince = this.ProvinceRepository.Delete(ProvinceDetails);

                
            }

            return false;
        }

    }

    #endregion
}
