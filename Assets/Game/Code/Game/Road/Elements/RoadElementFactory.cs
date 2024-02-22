public class RoadElementFactory
{
    public RoadElementModel Create(RoadElementEntry entry)
    {
        return new RoadElementModel(entry);
    }
}