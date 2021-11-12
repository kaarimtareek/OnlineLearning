using Humanizer;

using OnlineLearning.Utilities.Stemmer;

using System.Collections.Generic;

namespace OnlineLearning.Utilities
{
    public static class StringNormalizer
    {
        public static string NormalizeWithStem(string value)
        {
            value = value.Trim().ToLower();
            value = value.Humanize(LetterCasing.LowerCase);
            value = value.Replace(' ', '-');
            var words = value.Split('-');
            List<string> stemmedWrods = new List<string>();
            foreach (var word in words)
            {
                EnglishPorter2Stemmer stemmer = new EnglishPorter2Stemmer();
                stemmedWrods.Add(stemmer.Stem(word).Value);
            }
            return string.Join("-", stemmedWrods.ToArray());
        }
        public static string Normalize(string value)
        {
            value = value.Trim().ToLower();
            value = value.Humanize(LetterCasing.LowerCase);
            value = value.Replace(' ', '-');
            return value;
        }
       
    }
}