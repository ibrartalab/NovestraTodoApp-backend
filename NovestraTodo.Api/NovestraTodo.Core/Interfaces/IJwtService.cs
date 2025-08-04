﻿using NovestraTodo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovestraTodo.Core.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(UserEntity user);
    }
}
