﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        public User Add(User user);
        public void Update(User user);
        public void Delete(User user);
        public void SaveChanges();
        public User GetById(int id);
        public List<User> GetAll();
        public User? GetByUsername(string username);
    }
}
