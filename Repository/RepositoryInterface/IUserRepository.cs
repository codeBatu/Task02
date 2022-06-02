using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryInterface
{
 public   interface IUserRepository : ICrudRepository<UserTable, int, SignUpResponseMessageModel>
    {
        Task<SignUpResponseMessageModel> SignUp(UserTable userTable);

        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
    }
}
