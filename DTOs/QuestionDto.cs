using System.Collections.Generic;

namespace OnlineLearning.DTOs
{
    public class QuestionDto
    {
        public int RoomId { get; set; }
        public string QuestionTitle { get; set; }
        public int Id { get; set; }
        public string QuestionDescription { get; set; }
        public bool IsAnswered { get; set; }
        public string StatusId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }
}
