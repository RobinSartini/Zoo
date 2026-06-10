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

    [Fact]
    [Trait("Requirement", "REQ-Z-004")]
    public void TotalAnimals_EmptyZoo_ReturnsZero()
    {
        // Arrange
        var zoo = new ZooManager();

        // Act
        var result = zoo.TotalAnimals;

        // Assert
        result.Should().Be(0);
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-004")]
    public void TotalAnimals_AfterTwoAdditions_ReturnsTwo()
    {
        // Arrange
        var zoo = new ZooManager();
        zoo.AddAnimal(new Animal { Id = 1, Name = "Simba", Category = AnimalCategory.Carnivore });
        zoo.AddAnimal(new Animal { Id = 2, Name = "Dumbo", Category = AnimalCategory.Herbivore });

        // Act
        var result = zoo.TotalAnimals;

        // Assert
        result.Should().Be(2);
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-005")]
    public void AddAnimal_DuplicateId_ThrowsDuplicateAnimalException()
    {
        // Arrange
        var zoo = new ZooManager();
        zoo.AddAnimal(new Animal { Id = 1, Name = "Simba", Category = AnimalCategory.Carnivore });
        var duplicateAnimal = new Animal { Id = 1, Name = "Nala", Category = AnimalCategory.Carnivore };

        // Act
        var action = () => zoo.AddAnimal(duplicateAnimal);

        // Assert
        action.Should().Throw<DuplicateAnimalException>()
            .WithMessage("An animal with id 1 already exists.");
    }
}
