using TheSolutionBrothers.Nfe.Infra.Crypto;
using TheSolutionBrothers.NFe.Domain.Features.Users;

namespace TheSolutionBrothers.NFe.Application.Features.Users
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public User Login(string username, string password)
        {
            password = password.GenerateHash();
            return _repository.GetByCredentials(username, password);
        }

        public User Add(User entity)
        {
            entity.Id = _repository.Save(entity).Id;

            return entity;
        }
    }
}
