using MongoDB.Bson;
using MongoDB.Driver;
using Network.Models.Drone;

public interface IDroneRepository
{
    Task<Drone> GetByIdAsync(ObjectId id);
}

public class DroneRepository : IDroneRepository
{
    private readonly IMongoCollection<Drone> _drones;

    public DroneRepository(IMongoDatabase database)
    {
        _drones = database.GetCollection<Drone>("drones");
    }

    public async Task<Drone> GetByIdAsync(ObjectId id)
    {
        return await _drones.Find(d => d.Id == id).FirstOrDefaultAsync();
    }
}