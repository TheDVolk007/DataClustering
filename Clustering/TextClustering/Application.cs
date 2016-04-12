using System;
using System.IO;
using Clustering;
using Mappers;
using Thoughts_reader.Utils;


namespace Thoughts_reader
{
    public static class Application
    {
        public static int exceptionsCount = 0;

        public static void Run()
        {
            //const string path = @"F:\thoughts\";
            while(true)
            {
                Console.WriteLine("Write path to file(s):");
                var path = Console.ReadLine();
                Console.WriteLine("Where to save results?");
                var savePath = Console.ReadLine();

                if(path != null)
                {
                    var fileReader = new FileReader(path);
                    var documents = fileReader.PerformRead();
                    var clusterableDocuments = new MyDocumentToClusterableEntityMapper().MapAllData(documents);
                    var kMeansDatas = new ClusterableEntityToKMeansEntityMapper().MapAllData(clusterableDocuments);

                    var clusterer = new Clusterer(7);
                    clusterer.Cluster(kMeansDatas, null);

                    var results = new ClusteredDataResultWriter().WriteResults(kMeansDatas);
                    if(savePath != null)
                    {
                        File.WriteAllText(savePath, results);
                    }
                }

                Console.WriteLine($"Amount of exceptions while reading: {exceptionsCount}");
                Console.Read();
            }
        }
    }
}