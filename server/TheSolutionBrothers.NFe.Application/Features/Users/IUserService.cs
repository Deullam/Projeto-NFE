using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Domain.Features.Users;

namespace TheSolutionBrothers.NFe.Application.Features.Users
{
    public interface IUserService
    {

        User Add(User entity);
        User Login(string username, string password);

    }
}