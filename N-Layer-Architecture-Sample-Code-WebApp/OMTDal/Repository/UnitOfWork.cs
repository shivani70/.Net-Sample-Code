using OMTDal.Configuration;
using OMTDal.Repository.Entity_Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace OMTDal.Repository
{
    public sealed class UnitOfWork : IUnitOfWork 
    {
        private OmtContext _context;
        public UnitOfWork(OmtContext DbContext)
        {
            this._context = DbContext;
        }
        private UserRepository _userRepository;
        public void Commit()
        {
            try
            {
                this._context.SaveChanges();
            }
            catch(Exception ex)
            {

            }
        }
        public void CommitAsync()
        {
            try
            {
                this._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }
        public UserRepository UserRepository
        {
            get
            {
                if(this._userRepository == null)
                {
                    this._userRepository = new UserRepository(this._context);
                }
                return this._userRepository;
            }
        }
    }
}
