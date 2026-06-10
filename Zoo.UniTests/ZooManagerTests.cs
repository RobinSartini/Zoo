using FluentAssertions;
using Xunit;
using Zoo.Domain;

namespace Zoo.UnitTests;

public class ZooManagerTests
{
    [Fact]
    [Trait("Requirement", "REQ-Z-001")]
    public void AddAnimal_CarnivoreHealthy_ReturnsId()
    {
        // Arrange
        var zoo = new ZooManager();
        var animal = new Animal
        {
            Id = 1,
            Name = "Simba",
            Category = AnimalCategory.Carnivore,
            Status = HealthStatus.Healthy
        };

        // Act
        var result = zoo.AddAnimal(animal);

        // Assert
        result.Should().Be(1);
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-001")]
    public void AddAnimal_HerbivoreHealthy_ReturnsId()
    {
        // Arrange
        var zoo = new ZooManager();
        var animal = new Animal
        {
            Id = 2,
            Name = "Dumbo",
            Category = AnimalCategory.Herbivore,
            Status = HealthStatus.Healthy
        };

        // Act
        var result = zoo.AddAnimal(animal);

        // Assert
        result.Should().Be(2);
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-002")]
    public void GetAnimal_ExistingAnimal_ReturnsAnimal()
    {
        // Arrange
        var zoo = new ZooManager();
        var animal = new Animal
        {
            Id = 1,
            Name = "Simba",
            Category = AnimalCategory.Carnivore,
            Status = HealthStatus.Healthy
        };
        zoo.AddAnimal(animal);

        // Act
        var result = zoo.GetAnimal(1);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(1);
        result.Name.Should().Be("Simba");
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-003")]
    public void GetAnimal_NonExistingAnimal_ReturnsNull()
    {
        // Arrange
        var zoo = new ZooManager();

        // Act
        var result = zoo.GetAnimal(99);

        // Assert
        result.Should().BeNull();
    }
}
