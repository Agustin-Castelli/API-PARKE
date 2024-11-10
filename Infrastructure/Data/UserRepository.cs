using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public User Add(User user)
        {
            _context.Set<User>().Add(user);
            _context.SaveChanges();
            return user;
        }

        public void Update(User user)
        {
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context.Remove(user);
            _context.SaveChanges();
        }

        public void SaveChanges() { _context.SaveChanges(); }

        public User? GetById(int id)
        {
            return _context.Users.FirstOrDefault(p => p.Id == id);
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User? GetByUsername(string username)
        {
            return _context.Users.SingleOrDefault(c => c.Username == username);
        }
    }
}
