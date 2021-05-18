using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Users
{
    public interface IUserRepository
    {
        User Save(User entity);
        User GetByCredentials(string username, string password);
    }
}
