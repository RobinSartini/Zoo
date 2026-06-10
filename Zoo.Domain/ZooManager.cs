namespace Zoo.Domain;

public class ZooManager
{
    public const int MaxCapacity = 50;
    private readonly Dictionary<int, Animal> _animals = new();
    public int AddAnimal(Animal animal)
    {
        if (TotalCapacityUsed >= MaxCapacity) throw new ZooCapacityExceededException();
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(animal.Id);
        if (_animals.ContainsKey(animal.Id)) throw new DuplicateAnimalException(animal.Id);
        ArgumentException.ThrowIfNullOrEmpty(animal.Name);
        ArgumentNullException.ThrowIfNull(animal);
        _animals.Add(animal.Id, animal);
        return animal.Id;
    }

    public Animal? GetAnimal(int id)
    {
        var isNull = id <= 0 || !_animals.ContainsKey(id);
        return isNull ? null : _animals[id];
    }

    public int TotalAnimals => _animals.Count;
    public int TotalCapacityUsed => _animals.Sum(a => a.Value.Status == HealthStatus.Critical ? 2 : 1);

    public double GetRation(AnimalCategory category) => Convert.ToDouble(category);
    
    public double CalculateDailyRation(int animalId)
    {
        var animal = GetAnimal(animalId);
        
        ArgumentNullException.ThrowIfNull(animal);
        
        return GetRation(animal.Category);
    }
    public double CalculateDailyCost() => throw new NotImplementedException();
    public IReadOnlyList<Animal> GetCriticalAnimals() => throw new
        NotImplementedException();
    public bool RemoveAnimal(int id) => throw new NotImplementedException();
    public double CalculateMonthlyCost() => throw new NotImplementedException();
    public IReadOnlyList<Animal> GetAnimalsByCategory(AnimalCategory category) => throw new NotImplementedException();
}