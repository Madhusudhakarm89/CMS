
namespace CMS.Repository
{
    #region Namespace
    using CMS.Entity;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    #endregion

    #region Interface
    public interface IGlobalSearchRepository 
    {
        GlobalSearch GlobalSearch(string searchText);
    }
    #endregion



    #region Class

    public class GlobalSearch
    {
        public List<Account> Company { get; set; }
        public List<Claim> Claim { get; set; }
        public List<Contact> Contact { get; set; }
    }


    public sealed partial class GlobalSearchRepository : IGlobalSearchRepository
    {
       // public GlobalSearchRepository() : base(new CMSDBContext()) { }
        public GlobalSearch GlobalSearch(string searchText)
        {
            GlobalSearch abc = new Repository.GlobalSearch();
            CMSDBContext dbContext = new CMSDBContext();
            SqlParameter param1 = new SqlParameter("@searchKeyWord", searchText);
            SqlParameter param2 = new SqlParameter("@searchKeyWord", searchText);
            SqlParameter param3 = new SqlParameter("@searchKeyWord", searchText);
            abc.Company = dbContext.Database.SqlQuery<Account>( "exec usp_GlobalSearch @searchKeyWord", param1).ToList<Account>();
            abc.Contact = dbContext.Database.SqlQuery<Contact>("exec usp_GlobalSearch_Contact @searchKeyWord", param2).ToList<Contact>();
            abc.Claim = dbContext.Database.SqlQuery<Claim>("exec usp_GlobalSearch_Claim @searchKeyWord", param3).ToList<Claim>();
            return abc;// result1;
        }
    }
    #endregion
}
