using System.Collections.Generic;
using System.Linq;
using DocumentClustering;
using Domain;


namespace Mappers
{
    public class MyDocumentToClusterableEntityMapper
    {
        public List<ExternalEntity> MapAllData(List<MyDocument> documents)
        {
            var weightCalculator = new WordWeightCalculator();
            weightCalculator.CalculateWordIdfs(documents);

            return documents.Select(document => new ExternalEntity
            {
                Label = document.FileName, Data = weightCalculator.CalculateTfIdfs(document)
            }).ToList();
        }
    }
}