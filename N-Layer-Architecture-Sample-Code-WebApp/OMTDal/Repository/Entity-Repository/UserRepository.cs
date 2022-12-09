using OMTDal.Configuration;
using OMTDal.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OMTDal.Repository.Entity_Repository
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(Configuration.OmtContext context) : base(context)
        {

        }
    }
}
