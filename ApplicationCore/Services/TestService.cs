using ApplicationCore.BaseService.Services;
using ApplicationCore.Entities;
using ApplicationCore.Repository;
using ApplicationCore.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class TestService : GenericService<Test>
    {
        private readonly IRepository<Test> _test;
        public TestService(IUnitOfWork unitOfWork, IRepository<Test> test) : base(unitOfWork)
        {
            _test = test;
        }

        public void AddTest(Test test)
        {
            _test.Create(test);
            
        }
    }
}
