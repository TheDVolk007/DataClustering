using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Iveonik.Stemmers;


namespace Thoughts_reader.Utils
{
    public static class WordBreaker
    {
        /*
        лемматизация?
        [10:43] 
        https://stackoverflow.com/questions/1371994/importing-external-module-in-ironpython         
        Importing external module in IronPython
        I'm currently working on an application written in C#, which I'm embedding IronPython in. I generally have no problems about it, but there's one thing that I don't know how to deal with. I want to
        [10:44] 
        Это для того, чтобы импортить питоновский NLTK        
        [10:47] 
        Хотя наверное в шарпе есть что-то для лемматизации, no idea

        стеммер портера

        Стемминг
        */
        public static List<string> BreakWords(string content)
        {
            var delimiterChars = new[] {' ', ',', '.', ':',
                '\t', ';', '\n', '\r', '?', '\\', '/', '-',
                '<', '>', ')', '(', '<', '>', '[', ']', '\'',
                '"', '|', '*', '&', '+', '\u00AB', '\u00BB'};
            var words = content.Split(delimiterChars);
            var wordsList = words.Select(word => word.Trim()).Where(w => !string.IsNullOrWhiteSpace(w)).ToList();

            var rusStemmer = new RussianStemmer();
            var engStemmer = new EnglishStemmer();
            var result = new List<string>();
            foreach(var word in wordsList)
            {
                var stemmedWord = Regex.IsMatch(word, "^[a-zA-Z0-9]*$") ? engStemmer.Stem(word) : rusStemmer.Stem(word);
                result.Add(stemmedWord);
            }

            return result;
        } 
    }
}