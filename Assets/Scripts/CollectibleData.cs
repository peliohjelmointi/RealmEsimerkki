using Realms;

public class CollectibleData : RealmObject
{
    [PrimaryKey]
    public string Id { get; set; }

    public bool IsCollected { get; set; }
}
