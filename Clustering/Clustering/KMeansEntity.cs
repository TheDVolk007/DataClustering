using System.Collections.Generic;
using System.Dynamic;
using System.Reflection.Emit;


namespace Clustering
{
    public class KMeansEntity
    {
        public List<double> Arguments { get; set; } = new List<double>();

        public string Label { get; set; }

        public long ClusterId { get; set; } = -1;
    }
}