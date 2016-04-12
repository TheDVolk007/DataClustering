using System;
using System.Collections.Generic;
using System.IO;
using DocumentClustering;
using Spire.Doc;


namespace Thoughts_reader.Utils
{
    public class FileReader
    {
        private readonly string path;
        
        public List<MyDocument> Documents { get; set; } = new List<MyDocument>();
        
        public FileReader(string path)
        {
            if(path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            this.path = path;
        }

        public List<MyDocument> PerformRead()
        {
            var directory = new DirectoryInfo(path);
            foreach(var file in directory.GetFiles("*.doc", SearchOption.AllDirectories))
            {
                var doc = new Document();
                try
                {
                    doc.LoadFromFile(file.FullName);
                    var myDoc = new MyDocument
                    {
                        FileName = file.FullName,
                        Content = doc.GetText()
                    };
                    myDoc.Words = WordBreaker.BreakWords(myDoc.Content);

                    Documents.Add(myDoc);
                }
                catch
                {
                    Application.exceptionsCount++;
                }
            }

            return Documents;
        }
    }
}