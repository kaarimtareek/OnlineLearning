namespace OnlineLearning.Models.InputModels
{
    public class AddUserInterestInputModel
    {
        public string InterestId { get; set; }
        public string Interest { get; set; }
        public bool IgnoreSimilarity { get; set; }
    }
}
