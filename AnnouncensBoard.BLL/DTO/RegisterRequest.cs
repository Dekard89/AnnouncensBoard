using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnouncensBoard.BLL.DTO
{
    public record RegisterRequest
    {
        public string Username { get; set; } = String.Empty;

        public string Email { get; set; } = String.Empty;

        public string Password { get; set; } = String.Empty;

        public string ConfirmPassword { get; set; } = String.Empty;

        public string Phone { get; set; } = String.Empty;

        public string Birthday { get; set; }= String.Empty;


    }
}
