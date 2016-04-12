using System;
using System.Collections.Generic;
using System.Linq;


namespace DocumentClustering
{
    public class WordWeightCalculator
    {
        //TODO: normalize tf*idf?
        private Dictionary<string, double> wordIdfs;
        private List<string> words;

        public void CalculateWordIdfs(List<MyDocument> documents)
        {
            var totalNumberOfDocuments = documents.Count;

            words = documents.SelectMany(d => d.Words).Distinct().OrderBy(w => w).ToList();

            wordIdfs = new Dictionary<string, double>();
            foreach(var word in words)
            {
                if(wordIdfs.ContainsKey(word))
                    continue;

                var documentsWithSpecifiedWord = documents.Where(d => d.Words.Contains(word)).ToList().Count;
                var idf = Math.Log((totalNumberOfDocuments / (1 + documentsWithSpecifiedWord)), 2);
                wordIdfs.Add(word, idf);
            }
        }

        public Dictionary<string, double> CalculateTfIdfs(MyDocument document)
        {
            var tfIdfs = new Dictionary<string, double>();
            foreach(var word in words)
            {
                if(document.Words.Contains(word))
                {
                    var wordCount = document.Words.Count(w => w == word);
                    var tf = 1.0 * wordCount / document.Words.Count;
                    var tfIdf = tf * wordIdfs[word];

                    tfIdfs.Add(word, tfIdf);
                }
                else
                {
                    tfIdfs.Add(word, 0);
                }
            }

            return tfIdfs;
        }
    }
}