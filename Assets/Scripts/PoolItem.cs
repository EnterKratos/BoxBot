namespace DefaultNamespace
{
    public class PoolItem<T>
    {
        public bool Available { get; set; } = true;
        public T Item { get; set; }
    }
}