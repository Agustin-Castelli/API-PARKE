using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Create(UserCreateRequest user)
        {
            var newObj = new User();

            newObj.Username = user.Username;
            newObj.Password = user.Password;
            newObj.Rol = user.Rol;

            return _userRepository.Add(newObj);
        }

        public void Update(int id, UserUpdateRequest user)
        {
            var obj = _userRepository.GetById(id);

            if (obj == null)
            {
                throw new NotFoundException(nameof(User), id);
            }

            if (obj.Username != string.Empty) obj.Username = user.Username;
            if (obj.Password != string.Empty) obj.Password = user.Password;
            obj.Rol = user.Rol;

            _userRepository.Update(obj);
        }

        public void Delete(int id)
        {
            var user = _userRepository.GetById(id);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), id);
            }

            _userRepository.Delete(user);
        }

        public User GetById(int id)
        {
            var obj = _userRepository.GetById(id);

            if (obj == null)
            {
                throw new NotFoundException(nameof(User), id);
            }

            else
            {
                return obj;
            }
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll();
        }
    }
}
