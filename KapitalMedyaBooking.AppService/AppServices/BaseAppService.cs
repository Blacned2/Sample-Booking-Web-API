using AutoMapper;
using KapitalMedyaBooking.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KapitalMedyaBooking.AppService.AppServices
{
    public class BaseAppService
    {
        protected readonly KapitalDbContext DB;
        protected readonly IMapper Mapper;
        public BaseAppService(KapitalDbContext db, IMapper mapper)
        {
            this.DB = db;
            this.Mapper = mapper;
        }

    }
}
