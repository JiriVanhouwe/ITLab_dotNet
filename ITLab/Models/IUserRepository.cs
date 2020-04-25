﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab.Models
{
    public interface IUserRepository
    {

        ItlabUser GetById();
        List<ItlabUser> GetAllUsers();
    }
}