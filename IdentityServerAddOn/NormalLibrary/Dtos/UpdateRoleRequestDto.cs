﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NormalLibrary.Dtos
{
    public class UpdateRoleRequestDto
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
