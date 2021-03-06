﻿// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using Softeq.NetKit.Payments.Data.Models.User;

namespace Softeq.NetKit.Payments.SQLRepository.Interfaces
{
    public interface IUserRepository : IRepository<User, Guid>
    {
    }
}