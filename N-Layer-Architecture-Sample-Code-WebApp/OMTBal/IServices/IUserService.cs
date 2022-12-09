using OMTDal.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OMTBal.IServices
{
    public interface IUserService
    {
        User GetUserById(int id);
    }
}
