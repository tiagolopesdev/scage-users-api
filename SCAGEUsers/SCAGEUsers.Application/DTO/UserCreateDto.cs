﻿
using SCAGEUsers.Application.VO;

namespace SCAGEUsers.Application.DTO
{
    public class UserCreateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Sex Sex { get; set; }
    }
}
