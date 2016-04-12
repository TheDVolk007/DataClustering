using System.Collections.Generic;
using System.Linq;
using System.Text;
using Clustering;


namespace Thoughts_reader.Utils
{
    public class ClusteredDataResultWriter
    {
        public string WriteResults(List<KMeansEntity> data)
        {
            var clusters = data.Select(d => d.ClusterId).Distinct().ToList();

            var resultingMessage = new StringBuilder("Clustering Results:\r\n");
            foreach(var cluster in clusters)
            {
                resultingMessage.AppendFormat($"\r\nCluster {cluster}\r\n");
                foreach(var entity in data.Where(e => e.ClusterId == cluster))
                {
                    resultingMessage.AppendFormat($"{entity.Label}\r\n");
                }
            }

            return resultingMessage.ToString();
        }
    }
}