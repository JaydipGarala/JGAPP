﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGAPP.Model
{
    public class LoginModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public record LoginResponse
    { 
        public string? Email { get; set; }
        public string? UId { get; set; }
        public string? RefreshToken { get; set; }
        public string? IDToken { get; set; }
    }
}
