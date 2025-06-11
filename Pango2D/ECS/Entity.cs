namespace Pango2D.ECS
{
    public struct Entity
    {
        public int Id { get; }
        internal Entity(int id)
        {
            Id = id;
        }
    }
}
