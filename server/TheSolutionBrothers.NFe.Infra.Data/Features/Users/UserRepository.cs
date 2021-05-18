using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Users;
using TheSolutionBrothers.NFe.Infra.Data.Contexts;

namespace TheSolutionBrothers.NFe.Infra.Data.Features.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly ContextNfe _context;

        public UserRepository(ContextNfe context)
        {
            _context = context;
        }

        public User GetByCredentials(string username, string password)
        {
            return _context.Users.Where(u => u.Name.Equals(username) && u.Password.Equals(password)).FirstOrDefault() ?? throw new InvalidCredentialsException(); ;
        }

        public User Save(User entity)
        {
            entity = _context.Users.Add(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
