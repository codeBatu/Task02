using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model;
using Repository.Models;
using Repository.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly SmartPulse01DbContext _smartPulse01DbContext;
        private readonly IConfiguration _configuration;
        public UserRepository(SmartPulse01DbContext smartPulse01DbContext)
        {
            _smartPulse01DbContext = smartPulse01DbContext;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            SignInResponseMessageModel signInResponseMessageModel = new();
            signInResponseMessageModel.IsSucces = true;
            signInResponseMessageModel.Message = "Succes";

            var user = _smartPulse01DbContext.UserTables.SingleOrDefault(x => x.UserName == model.Username && x.Password == model.Password);

            if (user is null)
            {
                signInResponseMessageModel.IsSucces = true;

                signInResponseMessageModel.Message = "Hatalı Giriş";
                throw new Exception(signInResponseMessageModel.Message);
            }
            var token = generateJwtToken(user);
            var userToken = new AuthenticateResponse(user, token);
            Console.WriteLine(userToken.Id);
            return userToken;
        }

        public IEnumerable<UserTable> GetAll()
        {
            return _smartPulse01DbContext.UserTables.ToList();
        }

        public async Task<UserTable> GetById(int id)
        {
            return await _smartPulse01DbContext.UserTables.FindAsync(id);
        }

        public async Task<SignUpResponseMessageModel> SignUp(UserTable userTable)
        {
            SignUpResponseMessageModel signUpResponseMessageModel = new SignUpResponseMessageModel();
            signUpResponseMessageModel.IsSucces = true;
            signUpResponseMessageModel.Message = "Register is Succes";

            var result = _smartPulse01DbContext.UserTables.SingleOrDefault(x => x.Email == userTable.Email);
            if (result is not null)
            {
                signUpResponseMessageModel.IsSucces = false;
                signUpResponseMessageModel.Message = "Kayıtlı Mail";
                throw new Exception($"  Error Message : { signUpResponseMessageModel.Message}");
            }
            _smartPulse01DbContext.Add(userTable);

            var saveDataResponseCode = await _smartPulse01DbContext.SaveChangesAsync();
            if (saveDataResponseCode != 1)
            {
                signUpResponseMessageModel.IsSucces = false;
                signUpResponseMessageModel.Message = "Not Save Value on Db";
                throw new Exception($"  Error Message : { signUpResponseMessageModel.Message}");
            }

            return signUpResponseMessageModel;
        }

        private string generateJwtToken(UserTable user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public async Task<SignUpResponseMessageModel> Update(int id, UserTable entity)
        {
            SignUpResponseMessageModel blogServiceMessageModel = new();
            blogServiceMessageModel.IsSucces = true;

            blogServiceMessageModel.Message = "Succes";
            var result = await validResultById(id);
            if (result.Email == entity.Email || result.UserName == entity.UserName || result.Password == entity.Password)
            {
                result.Email = entity.Email;
                result.UserName = entity.UserName;
                result.Password = entity.Password;


                return await saveData();

            }
            result.Email = entity.Email;
            result.UserName = entity.UserName;
            result.Password = entity.Password;
            return await saveData();
        }

        private async Task<SignUpResponseMessageModel> saveData()
        {
            SignUpResponseMessageModel blogServiceMessageModel = new();
            var saveDataResponseCode = await _smartPulse01DbContext.SaveChangesAsync();
            if (saveDataResponseCode != 1)
            {
                blogServiceMessageModel.IsSucces = false;
                blogServiceMessageModel.Message = "Not Save Value on Db";
                throw new Exception($"  Error Message : { blogServiceMessageModel.Message}");
            }

            return blogServiceMessageModel;
        }
        private async Task<UserTable> validResultById(int id)
        {
            SignUpResponseMessageModel blogServiceMessageModel = new();
            var result = await _smartPulse01DbContext.UserTables.FindAsync(id);
            if (result is null)
            {
                blogServiceMessageModel.IsSucces = false;
                blogServiceMessageModel.Message = "Gçersi Id";
                throw new Exception($"  Error Message : { blogServiceMessageModel.Message}");
            }
            return result;
        }

        public async Task<SignUpResponseMessageModel> Delete(int id)
        {
            SignUpResponseMessageModel blogServiceMessageModel = new();
            blogServiceMessageModel.IsSucces = true;

            blogServiceMessageModel.Message = "Succes";
            var result = await validResultById(id);
            _smartPulse01DbContext.Remove<UserTable>(result);

            return await saveData();
        }
    }
}