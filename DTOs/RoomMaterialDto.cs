namespace OnlineLearning.DTOs
{
    public class RoomMaterialDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public bool IsActive { get; set; }
    }
}