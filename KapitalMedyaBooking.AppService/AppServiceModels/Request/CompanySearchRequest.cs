using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KapitalMedyaBooking.AppService.AppServiceModels.Request
{
    public class CompanySearchRequest : BaseSearchRequest
    {
        public string Name { get; set; }
    }
}
