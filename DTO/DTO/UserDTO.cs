using Model;
using Repository.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTO
{
    public class UserDTO 
    {
        private readonly IUserRepository _userRepository;

        public UserDTO(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            return await _userRepository.Authenticate(model);
        }

        public async Task<SignUpResponseMessageModel> Delete(int id)
        {
            return await _userRepository.Delete(id);
        }

        public IEnumerable<UserTable> GetAll()
        {
            return _userRepository.GetAll();
        }

        public async Task<UserTable> GetById(int id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<SignUpResponseMessageModel> SignUp(UserTable userTable)
        {
            return await _userRepository.SignUp(userTable);
        }

        public async Task<SignUpResponseMessageModel> Update(int id, UserTable entity)
        {
            return await _userRepository.Update(id, entity);
        }
    }
}
