﻿using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthenticationService
    {
        //public User? UserAuthenticate(CredentialsRequest credentialsRequest);
        public string Authentication(CredentialsRequest credentialsRequest);
    }
}