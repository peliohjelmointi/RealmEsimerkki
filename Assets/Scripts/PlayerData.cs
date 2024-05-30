using Realms;

public class PlayerData : RealmObject
{
    [PrimaryKey]
    public string Id { get; set; }

    public float X { get; set; }
    public float Y { get; set; }
}
