using System.Collections.Generic;


namespace Domain
{
    public class ExternalEntity
    {
        public Dictionary<string, double> Data { get; set; } = new Dictionary<string, double>();

        public string Label { get; set; }
    }
}