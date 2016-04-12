using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clustering;
using Domain;
using Mappers;


namespace ClusteringPresentation
{
    public partial class Form1 : Form
    {
        private KPoint center;
        private readonly DataInitializer initializer;
        private List<KMeansEntity> data;
        private ChangeDetector changeDetector;
        private ConcurrentDictionary<int, Color> colorDictionary = new ConcurrentDictionary<int, Color>();
        private ConcurrentBag<KnownColor> names;
        private Random rnd;

        public Form1()
        {
            InitializeComponent();

            initializer = new DataInitializer();
            changeDetector = new ChangeDetector();
            var orderedNames = ((KnownColor[])Enum.GetValues(typeof(KnownColor))).ToList();
            orderedNames.Remove(KnownColor.Black);
            orderedNames.Remove(KnownColor.Transparent);
            orderedNames.Shuffle();
            names = new ConcurrentBag<KnownColor>(orderedNames);
            rnd = new Random();
        }

        private void MainCanvas_Paint(object sender, PaintEventArgs e)
        {
            var x0 = MainCanvas.Width / 2;
            var y0 = MainCanvas.Height / 2;
            e.Graphics.DrawLine(Pens.Black, new Point(x0, 0), new Point(x0, MainCanvas.Height));
            e.Graphics.DrawLine(Pens.Black, new Point(0, y0), new Point(MainCanvas.Width, y0));
            
            center = new KPoint {x = x0, y = y0};
        }

        private void DrawPoint(KMeansEntity entity, int size = 4)
        {
            var color = GetColor((int)entity.ClusterId);
            using(var graphic = MainCanvas.CreateGraphics())
            {
                var pt = new Point((int)entity.Arguments[0] + center.x, -(int)entity.Arguments[1] + center.y);
                using(Brush b = new SolidBrush(color))
                {
                    graphic.FillEllipse(b, pt.X - size / 2, pt.Y - size / 2, size, size);
                }
            }
        }

        private void initialize_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                var handedData = initializer.InitializeData();
                var mapper = new ClusterableEntityToKMeansEntityMapper();
                data = mapper.MapAllData(handedData);
                changeDetector.RegisterInitialData(data);

                var watch = new Stopwatch();
                watch.Start();
                //DrawData();
                watch.Stop();
                Debug.WriteLine(watch.ElapsedMilliseconds);
            });
        }

        private async void performKMeans_Click(object sender, EventArgs e)
        {
            informer.Text = " ";
            var task = Task.Run(() =>
            {
                var clusterer = new Clusterer(5);

                clusterer.Cluster(data, DrawChangedData);
            });
            await task;

            informer.Text = "Done!";
        }

        private Color GetColor(int clusterNum)
        {
            if(clusterNum == -1)
            {
                return Color.Black;
            }
            Color color;
            if(colorDictionary.TryGetValue(clusterNum, out color))
            {
                return color;
            }
            else
            {
                KnownColor randomColorName;
                names.TryTake(out randomColorName);
                var randomColor = Color.FromKnownColor(randomColorName);
                colorDictionary.TryAdd(clusterNum, randomColor);
                return randomColor;
            }
        }

        private void DrawData()
        {
            Parallel.ForEach(data, point =>
            {
                DrawPoint(point, 2);
            });

            //Thread.Sleep(10);
        }

        private void DrawChangedData()
        {
            var indeciesOfPointsToDraw = changeDetector.DetectChanges(data);

            Parallel.ForEach(indeciesOfPointsToDraw, i =>
            {
                DrawPoint(data[i], 2);
            });

            //foreach(var i in indeciesOfPointsToDraw)
            //{
            //    DrawPoint(data[i], 8);
            //}
        }
    }
}
