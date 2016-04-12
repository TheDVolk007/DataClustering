using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Clustering
{
    public class Clusterer
    {
        private readonly int clusterCount;
        private readonly List<KMeansEntity> centroids;
        private readonly Random rnd;

        public Clusterer(int clusterCount)
        {
            this.clusterCount = clusterCount;
            centroids = new List<KMeansEntity>();
            rnd = new Random();
        }

        public void Cluster(List<KMeansEntity> data, Action actionPerStep)
        {
            InitRandom(data);

            var changeTracked = true;
            const long maxReClustering = long.MaxValue;
            long reClusteringCount = 0;

            while(changeTracked && reClusteringCount < maxReClustering)
            {
                reClusteringCount++;
                UpdateCentroids(data);
                changeTracked = UpdateClustering(data);

                actionPerStep?.Invoke();
            }

            //return clustered data?
        }

        private bool UpdateClustering(List<KMeansEntity> data)
        {
            var changed = false;

            Parallel.ForEach(data, entity =>
            {
                var closestCentroid = centroids.First();
                var distanceToClosestCentroid = CalculateDistanceToCentroid(entity, closestCentroid);
                foreach (var centroid in centroids)
                {
                    var distance = CalculateDistanceToCentroid(entity, centroid);
                    if (distance < distanceToClosestCentroid)
                    {
                        distanceToClosestCentroid = distance;
                        closestCentroid = centroid;
                    }
                }
                var previousCluster = entity.ClusterId;
                entity.ClusterId = closestCentroid.ClusterId;

                if (previousCluster != entity.ClusterId)
                {
                    changed = true;
                }
            });

            //foreach(var entity in data)
            //{
            //    var closestCentroid = centroids.First();
            //    var distanceToClosestCentroid = CalculateDistanceToCentroid(entity, closestCentroid);
            //    foreach(var centroid in centroids)
            //    {
            //        var distance = CalculateDistanceToCentroid(entity, centroid);
            //        if(distance < distanceToClosestCentroid)
            //        {
            //            distanceToClosestCentroid = distance;
            //            closestCentroid = centroid;
            //        }
            //    }
            //    var previousCluster = entity.ClusterId;
            //    entity.ClusterId = closestCentroid.ClusterId;

            //    if(previousCluster != entity.ClusterId)
            //    {
            //        changed = true;
            //    }
            //}

            return changed;
        }

        private void UpdateCentroids(List<KMeansEntity> data)
        {
            Parallel.ForEach(centroids, centroid =>
            {
                var pointsBelongingToCentroid = data
                    .Where(p => p.ClusterId == centroid.ClusterId)
                    .ToList();

                for (var i = 0; i < centroid.Arguments.Count; i++)
                {
                    centroid.Arguments[i] = pointsBelongingToCentroid
                        .Select(p => p.Arguments[i])
                        .Sum() / pointsBelongingToCentroid.Count;
                }
            });

            //foreach(var centroid in centroids)
            //{
            //    var pointsBelongingToCentroid = data
            //        .Where(p => p.ClusterId == centroid.ClusterId)
            //        .ToList();

            //    for(var i = 0; i < centroid.Arguments.Count; i++)
            //    {
            //        centroid.Arguments[i] = pointsBelongingToCentroid
            //            .Select(p => p.Arguments[i])
            //            .Sum() / pointsBelongingToCentroid.Count;
            //    }
            //}
        }

        private void InitRandom(List<KMeansEntity> data)
        {
            for(var i = 0; i < clusterCount; i++)
            {
                var randomlySelectedPoint = data[rnd.Next(0, data.Count)];
                var centroid = new KMeansEntity
                {
                    Label = $"centroid_{i}",
                    ClusterId = i
                };
                foreach(var argument in randomlySelectedPoint.Arguments)
                {
                    centroid.Arguments.Add(argument);
                }

                centroids.Add(centroid);
            }

            UpdateClustering(data);
        }

        private static double CalculateDistanceToCentroid(KMeansEntity entity, KMeansEntity centroid)
        {
            var squaredDistance = 0d;
            for (var i = 0; i < centroid.Arguments.Count; i++)
            {
                squaredDistance += Math.Pow(centroid.Arguments[i] - entity.Arguments[i], 2);
            }

            var distance = Math.Sqrt(squaredDistance);
            return distance;
        }
    }
}