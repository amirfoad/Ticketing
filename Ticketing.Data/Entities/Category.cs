using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Data.Common;

namespace Ticketing.Data.Entities
{
    public class Category: BaseAuditableEntity
    {
        public string Title { get; set; }

        #region Foreign Keys

        public int? ParentId { get; set; }


        #endregion



        #region Navigation Properties
        public Category? ParentCategory { get; set; }
        //public ICollection<Category?> ChildCategories { get; set; }

        public ICollection<Ticket>  Tickets { get; set; }

        #endregion
    }
}
