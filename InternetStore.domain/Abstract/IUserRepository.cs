﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternetStore.Domain.Entities;

namespace InternetStore.Domain.Abstract
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
    }
}
