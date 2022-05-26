namespace OnlineLearning.DTOs
{
    public class AnswerDto
    {
        public string AnswerDescription { get; set; }
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public bool IsAccepted { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
