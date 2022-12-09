using OMTBal.IServices;
using OMTDal.Entity;
using OMTDal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OMTBal.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork _unitOfWork;
        public LoginService(IUnitOfWork UnitOfWork)
        {
            this._unitOfWork = UnitOfWork;
        }
        public string Register(string username, string password)
        {
            _unitOfWork.UserRepository.Add(new User() { Name="SurajMangal" });
            _unitOfWork.Commit();
            return String.Empty;
        }
        public string UpdatePassword(string EmailId, string password)
        {
            var FindUser = _unitOfWork.UserRepository.Where(x => x.Email == EmailId ).FirstOrDefault();
            if(FindUser != null)
            {
                FindUser.Password = password;   
                _unitOfWork.UserRepository.Update(FindUser);
                _unitOfWork.Commit();
            }
            return String.Empty;
        }
        public (bool isLogin,User userdetail) Login(string EmailId, string password)
        {
         var loginStatus =    _unitOfWork.UserRepository.Where(x => x.Email == EmailId && x.Password == password).FirstOrDefault();
            if (loginStatus !=null)
            {
                return (true, loginStatus);
            }
            return (false, null);
        }
        public (bool isEmail, string message) Email(string Email)
        {
            var loginStatus = _unitOfWork.UserRepository.Where(x => x.Email == Email);
            if (loginStatus.Count() == 0)
            {
                return (false, "Email is wrong.");
            }
            return (true, "Login success");
        }
        
    }
}
