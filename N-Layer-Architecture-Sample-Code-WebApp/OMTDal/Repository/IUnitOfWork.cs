using Microsoft.EntityFrameworkCore;
using OMTDal.Repository.Entity_Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace OMTDal.Repository
{
    public interface IUnitOfWork
    {
        void Commit();
        void CommitAsync();
        UserRepository UserRepository { get; }
    }
}
