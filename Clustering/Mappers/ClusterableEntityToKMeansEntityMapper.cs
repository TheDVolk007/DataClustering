using System.Collections.Generic;
using System.Linq;
using Clustering;
using Domain;


namespace Mappers
{
    public class ClusterableEntityToKMeansEntityMapper
    {
        public List<KMeansEntity> MapAllData(List<ExternalEntity> entities)
        {
            var arguments = entities.SelectMany(e => e.Data.Select(kvp => kvp.Key)).Distinct().ToList();

            //var argumentsList = new List<string>();
            //foreach(var argumentPair in entities.SelectMany(entity => entity.Data.Where(argumentPair => !argumentsList.Contains(argumentPair.Key))))
            //{
            //    argumentsList.Add(argumentPair.Key);
            //}

            var result = new List<KMeansEntity>();
            foreach(var entity in entities)
            {
                var mappedEntity = new KMeansEntity
                {
                    Label = entity.Label,
                    Arguments = new List<double>()
                };

                foreach(var argument in arguments)
                {
                    double value;
                    mappedEntity.Arguments.Add(entity.Data.TryGetValue(argument, out value) ? value : 0);
                }

                result.Add(mappedEntity);
            }

            return result;
        }
    }
}