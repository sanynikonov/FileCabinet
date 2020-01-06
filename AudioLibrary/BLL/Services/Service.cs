using AutoMapper;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Service : IDisposable
    {
        protected IUnitOfWork unit;
        protected IMapper mapper;

        public Service(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public void Dispose()
        {
            unit.Dispose();
        }
    }
}
