﻿using System.Collections.Generic;
using System;

namespace OnlineLearning.Models.InputModels
{
    public class AddRoomInputModel
    {
        public string RoomName { get; set; }
        public string RoomDescription { get; set; }
        public decimal Price { get; set; }
        public DateTime? ExpectedEndDate { get; set; }
        public DateTime StartDate { get; set; }
        public List<string> Interests { get; set; }
    }
}
