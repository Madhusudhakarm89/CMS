
namespace ClaimManagementSystem.Controllers
{
    #region Namespace
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Http;
    using System.Threading.Tasks;
    using System.Web.Http.Description;
    using Microsoft.AspNet.Identity;
    using NLog;

    using CMS.BusinessLibrary;
    using CMS.BusinessLibrary.ViewModels;
    using System.Net.Http;
    using System.Web;
    using System.IO;
    #endregion

    public class TypeOfLossController : BaseApiController
    {
        private readonly ITypeOfLossBusinessLayer BusinessLayer;

        public TypeOfLossController()
        {
            this.BusinessLayer = new TypeOfLossBusinessLayer();
        }

        [HttpGet]
        public async Task<IEnumerable<TypeOfLossViewModel>> GetAllTypeOfLoss()
        {
            try
            {
                IEnumerable<TypeOfLossViewModel> lossTypes = await this.BusinessLayer.GetAllTypeOfLoss();
                return lossTypes;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<TypeOfLossViewModel> Find(int id)
        {
            try
            {
                TypeOfLossViewModel lossTypes = await this.BusinessLayer.Find(id);
                return lossTypes;
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<TypeOfLossViewModel> Save(TypeOfLossViewModel lossTypeData)
        {
            try
            {
                lossTypeData.CreatedBy = User.Identity.GetUserId();
                lossTypeData.CreatedOn = DateTime.Now;
                lossTypeData.LastModifiedBy = User.Identity.GetUserId();
                lossTypeData.LastModifiedOn = DateTime.Now;
                lossTypeData.IsActive = true;

                return await this.BusinessLayer.Create(lossTypeData);
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<TypeOfLossViewModel> Update(TypeOfLossViewModel lTypedata)
        {
            try
            {
                lTypedata.LastModifiedBy = User.Identity.GetUserId();
                lTypedata.LastModifiedOn = DateTime.Now;
                lTypedata.IsActive = true;

                return await this.BusinessLayer.Update(lTypedata);
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }



        [HttpPost]
        public async Task<bool> Delete(int id)
        {
            try
            {
                return await this.BusinessLayer.Delete(id);
            }
            catch (Exception ex)
            {
                this.ExceptionLogger.Log(LogLevel.Error, ex);
                throw ex;
            }
        }


    }
}
