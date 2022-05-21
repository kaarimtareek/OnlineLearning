using System;
using System.Collections.Generic;

namespace OnlineLearning.DTOs
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StatusId { get; set; }
        public RoomStatusDto Status { get; set; }
        public decimal Price { get; set; }
        public bool IsPublic { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? ExpectedEndDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public IEnumerable<InterestDto> Interests { get; set; }
    }
}
