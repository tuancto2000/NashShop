using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashShop_ViewModel
{
    public class PagingRequest
    {
        public string Bearer { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
