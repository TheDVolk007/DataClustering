using System;
using System.Collections.Generic;
using Domain;


namespace ClusteringPresentation
{
    public class DataInitializer
    {
        private const int BoundX = 457;
        private const int BoundY = 299;

        //private const int BoundX = 299;
        //private const int BoundY = 299;

        //private const int BoundX = 956;
        //private const int BoundY = 500;

        private List<ExternalEntity> data;

        public List<ExternalEntity> InitializeData()
        {
            var rnd = new Random();
            if (false)
            {
                const int amountOfDataPoints = 550200; //rnd.Next(25000, 31000);

                data = new List<ExternalEntity>(amountOfDataPoints);

                for(var i = 0; i < amountOfDataPoints; i++)
                {
                    var point = new ExternalEntity();
                    point.Data.Add("x", rnd.Next(-BoundX, BoundX + 1));
                    point.Data.Add("y", rnd.Next(-BoundY, BoundY + 1));

                    data.Add(point);
                }

                return data;
            }
            else if(true)
            {
                const int amountOfData = BoundX * BoundY * 4;
                data = new List<ExternalEntity>(amountOfData);
                for(var i = -BoundX; i <= BoundX; i++)
                {
                    for(var j = -BoundY; j <= BoundY; j++)
                    {
                        var point = new ExternalEntity();
                        point.Data.Add("x", i);
                        point.Data.Add("y", j);

                        data.Add(point);
                    }
                }

                return data;
            }
            else
            {
                data = new List<ExternalEntity>();
                for (var i = -BoundX; i <= BoundX; i += 8)
                {
                    for (var j = -BoundY; j <= BoundY; j += 8)
                    {
                        var point = new ExternalEntity();
                        point.Data.Add("x", i);
                        point.Data.Add("y", j);

                        data.Add(point);
                    }
                }

                return data;
            }
        } 
    }
}