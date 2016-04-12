using System.Collections.Generic;


namespace DocumentClustering
{
    public class MyDocument
    {
        public string FileName { get; set; }

        public string Content { get; set; }

        public List<string> Words { get; set; } = new List<string>();
    }
}