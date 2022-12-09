using OMTBal.IServices;
using OMTDal.Entity;
using OMTDal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OMTBal.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork UnitOfWork)
        {
            this._unitOfWork = UnitOfWork;
        }
        public User GetUserById(int id)
        {
            return _unitOfWork.UserRepository.Where(d => d.Id == id).FirstOrDefault();
        }
    }
}
