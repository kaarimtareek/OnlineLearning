﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Models
{
    public class RoomRoadMap : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string RoadMap { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        public virtual Room Room { get; set; }
    }
}
