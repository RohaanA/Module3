﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Authentication
{
  public interface IJWTTokenGenerator
    {
        string GenerateToken(Guid userId, string firstName, string lastName);
        string GenerateToken(User user, string role);
    }
}
