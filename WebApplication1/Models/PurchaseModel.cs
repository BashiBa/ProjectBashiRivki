using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{

    public class PurchaseModel
    {
        public int PurchaseModelID { get; set; }
        public  Guid ApplicationUserId { get; set; }
        public bool IsDone { get; set; }
        public string CreditNum { get; set; }
        public string ValidityMonth { get; set; }
        public string ValidityYear { get; set; }

        public virtual ICollection<ProductModel> ProductModels { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}