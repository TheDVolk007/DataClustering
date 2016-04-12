using System.Collections.Generic;
using Clustering;
using Domain;


namespace ClusteringPresentation
{
    public class ChangeDetector
    {
        private List<KMeansEntity> data;

        public void RegisterInitialData(List<KMeansEntity> data)
        {
            this.data = new List<KMeansEntity>();
            foreach(var point in data)
            {
                var registeredPoint = new KMeansEntity
                {
                    ClusterId = point.ClusterId
                };

                this.data.Add(registeredPoint);
            }
        }

        public List<int> DetectChanges(List<KMeansEntity> data)
        {
            var indecies = new List<int>();
            for(var i = 0; i < data.Count; i++)
            {
                if(data[i].ClusterId != this.data[i].ClusterId)
                {
                    indecies.Add(i);
                }
            }

            RegisterInitialData(data);
            return indecies;
        } 
    }
}