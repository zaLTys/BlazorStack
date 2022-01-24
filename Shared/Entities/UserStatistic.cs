﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorStack.Shared.Entities
{
    public class UserStatistic
    {
        public int Rank { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public int Battles { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }
    }
}
