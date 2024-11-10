using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        public User Create(UserCreateRequest user);
        public void Update(int id, UserUpdateRequest user);
        public void Delete(int id);
        public User GetById(int id);
        public List<User> GetAll();
    }
}
