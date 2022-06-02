using DTO.DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
  public  class UserService
    {
        private readonly UserDTO _userDTO;

        public UserService(UserDTO userDTO)
        {
            _userDTO = userDTO;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            return await _userDTO.Authenticate(model);
        }

        public async Task<SignUpResponseMessageModel> Delete(int id)
        {
            return await _userDTO.Delete(id);
        }

        public IEnumerable<UserTable> GetAll()
        {
            return _userDTO.GetAll();
        }

        public async Task<UserTable> GetById(int id)
        {
            return await _userDTO.GetById(id);
        }

        public async Task<SignUpResponseMessageModel> SignUp(UserTable userTable)
        {
            return await _userDTO.SignUp(userTable);
        }

        public async Task<SignUpResponseMessageModel> Update(int id, UserTable entity)
        {
            return await _userDTO.Update(id, entity);
        }
    }
}
