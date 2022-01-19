using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfChance.Client
{
    public class UserManagerResponse
    {
        public string Message { get; set; }
        public string Token { get; set; } = null;
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
