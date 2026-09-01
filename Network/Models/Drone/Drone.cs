using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Network.Models;

public class Drone
{
    [BsonId]
    public ObjectId Id {get; set;}
    public required string Name {get; set;}
    public required string SerialNumber {get; set;}
    public DroneStatus Status {get; set;}
    public DronePosiiton LastKnownPosition {get; set;} = new DronePosiiton {Lat= 0, Lng= 0, Alt= 0};
    public DateTime LastSeen {get; set;}
    public int BatteryPercent {get; set;}

    [BsonElement("createdAt")]
    public DateTime CreatedDate {get;set;}

}
