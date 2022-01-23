namespace BlazorStack.Shared
{
    public class UnitModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int HitPoints { get; set; } = 100;
        public int PointCost { get; set;}
    }
}
