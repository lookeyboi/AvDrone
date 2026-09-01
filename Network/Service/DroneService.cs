using MongoDB.Bson;
using Network.Models.Response;

public interface IDroneService
{
    Task<DroneResponse> GetDroneByIdAsync(string id);
}

public class DroneService : IDroneService
{
    private readonly IDroneRepository _droneRepository;

    public DroneService(IDroneRepository droneRepository)
    {
        _droneRepository = droneRepository;
    }

    public async Task<DroneResponse> GetDroneByIdAsync(string id)
    {
        if (!ObjectId.TryParse(id, out var objectId))
        {
            throw new ArgumentException("Invalid drone id format.");
        }

        var drone = await _droneRepository.GetByIdAsync(objectId) ?? throw new KeyNotFoundException($"Drone with id '{id}' was not found.");
        return new DroneResponse
        {
            Id = drone.Id.ToString(),
            Name = drone.Name,
            SerialNumber = drone.SerialNumber,
            Status = drone.Status.ToString(),
        };
    }
}