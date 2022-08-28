using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KapitalMedyaBooking.AppService.AppServiceModels.Dto
{
    public class CompanyListDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; } //?? Company Age, I think..
        public string Address { get; set; }
    }
}
