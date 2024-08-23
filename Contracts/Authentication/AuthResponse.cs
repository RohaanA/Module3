﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Authentication
{
    public record AuthResponse (
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        string Token
        );
}
