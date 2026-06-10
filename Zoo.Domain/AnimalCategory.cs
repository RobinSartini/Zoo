namespace Zoo.Domain;

public sealed class AnimalCategory
{
    public static readonly AnimalCategory Carnivore = new(nameof(Carnivore), ration: 5,  cost: 25);
    public static readonly AnimalCategory Herbivore = new(nameof(Herbivore), ration: 10, cost: 8);
    public static readonly AnimalCategory Omnivore  = new(nameof(Omnivore),  ration: 7,  cost: 15);

    public string Name   { get; }
    public double Ration { get; }
    public double Cost   { get; }

    private AnimalCategory(string name, double ration, double cost)
        => (Name, Ration, Cost) = (name, ration, cost);
}