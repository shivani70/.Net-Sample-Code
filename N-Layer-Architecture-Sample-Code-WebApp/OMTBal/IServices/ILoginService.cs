using OMTDal.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OMTBal.IServices
{
    public interface ILoginService
    {
        string Register(string username, string password);
        (bool isLogin, User userdetail) Login(string EmailId, string password);
        (bool isEmail, string message) Email(string Email);
        string UpdatePassword(string EmailId, string password);


    }
}
