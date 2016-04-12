namespace KMeans
{
    public interface IKMeans
    {
        int ClustersQuantity { get; set; }

        void PerformClustering();

        void SetData<T>(T data);
    }
}