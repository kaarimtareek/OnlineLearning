namespace OnlineLearning.Constants
{
    public static class ConstantQuestionStatus
    {
        public const string DELETED = "DELETED";
        public const string ANSWERED = "ANSWERED";
        public const string NOT_ANSWERED = "NOT_ANSWERED";
        public static string[] ALL = new string[] { DELETED, ANSWERED, NOT_ANSWERED };
        public static bool IsActive(string status) => status != DELETED;
        public static bool IsAnswered(string status) => status == ANSWERED;
    }
}
