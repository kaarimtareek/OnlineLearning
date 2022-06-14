namespace OnlineLearning.DTOs
{
    public class InviteDto
    {
        public int Id { get; set; }
        public string StatusId { get; set; }
        public string OwnerName { get; set; }
        public string OwnerId { get; set; }
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public string UserId { get; set; }
    }
}
