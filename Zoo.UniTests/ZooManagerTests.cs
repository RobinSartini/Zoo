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

    [Fact]
    [Trait("Requirement", "REQ-Z-006")]
    public void AddAnimal_FiftyFirstAnimal_ThrowsZooCapacityExceededException()
    {
        // Arrange
        var zoo = new ZooManager();
        for (int i = 1; i <= 50; i++)
        {
            zoo.AddAnimal(new Animal { Id = i, Name = f"Animal{i}", Category = AnimalCategory.Herbivore, Status = HealthStatus.Healthy });
        }
        var extraAnimal = new Animal { Id = 51, Name = "Extra", Category = AnimalCategory.Herbivore, Status = HealthStatus.Healthy };

        // Act
        var action = () => zoo.AddAnimal(extraAnimal);

        // Assert
        action.Should().Throw<ZooCapacityExceededException>()
            .WithMessage("Zoo capacity (50 animals) exceeded.");
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-006")]
    public void AddAnimal_FiftiethAnimal_Succeeds()
    {
        // Arrange
        var zoo = new ZooManager();
        for (int i = 1; i <= 49; i++)
        {
            zoo.AddAnimal(new Animal { Id = i, Name = f"Animal{i}", Category = AnimalCategory.Herbivore, Status = HealthStatus.Healthy });
        }
        var fiftiethAnimal = new Animal { Id = 50, Name = "Fiftieth", Category = AnimalCategory.Herbivore, Status = HealthStatus.Healthy };

        // Act
        var result = zoo.AddAnimal(fiftiethAnimal);

        // Assert
        result.Should().Be(50);
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-007")]
    public void TotalCapacityUsed_CriticalAnimal_ConsumesTwoSpaces()
    {
        // Arrange
        var zoo = new ZooManager();
        zoo.AddAnimal(new Animal { Id = 1, Name = "Simba", Category = AnimalCategory.Carnivore, Status = HealthStatus.Critical });

        // Act
        var result = zoo.TotalCapacityUsed;

        // Assert
        result.Should().Be(2);
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-007")]
    public void TotalCapacityUsed_OneHealthyAndOneCritical_ConsumesThreeSpaces()
    {
        // Arrange
        var zoo = new ZooManager();
        zoo.AddAnimal(new Animal { Id = 1, Name = "Simba", Category = AnimalCategory.Carnivore, Status = HealthStatus.Healthy });
        zoo.AddAnimal(new Animal { Id = 2, Name = "Dumbo", Category = AnimalCategory.Herbivore, Status = HealthStatus.Critical });

        // Act
        var result = zoo.TotalCapacityUsed;

        // Assert
        result.Should().Be(3);
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-007")]
    public void TotalCapacityUsed_TwoHealthyAnimals_ConsumesTwoSpaces()
    {
        // Arrange
        var zoo = new ZooManager();
        zoo.AddAnimal(new Animal { Id = 1, Name = "Simba", Category = AnimalCategory.Carnivore, Status = HealthStatus.Healthy });
        zoo.AddAnimal(new Animal { Id = 2, Name = "Dumbo", Category = AnimalCategory.Herbivore, Status = HealthStatus.Healthy });

        // Act
        var result = zoo.TotalCapacityUsed;

        // Assert
        result.Should().Be(2);
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-008")]
    public void CalculateDailyRation_CarnivoreHealthy_ReturnsFiveKg()
    {
        // Arrange
        var zoo = new ZooManager();
        zoo.AddAnimal(new Animal { Id = 1, Name = "Simba", Category = AnimalCategory.Carnivore, Status = HealthStatus.Healthy });

        // Act
        var result = zoo.CalculateDailyRation(1);

        // Assert
        result.Should().Be(5.0);
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-008")]
    public void CalculateDailyRation_HerbivoreHealthy_ReturnsTenKg()
    {
        // Arrange
        var zoo = new ZooManager();
        zoo.AddAnimal(new Animal { Id = 2, Name = "Dumbo", Category = AnimalCategory.Herbivore, Status = HealthStatus.Healthy });

        // Act
        var result = zoo.CalculateDailyRation(2);

        // Assert
        result.Should().Be(10.0);
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-008")]
    public void CalculateDailyRation_OmnivoreHealthy_ReturnsSevenKg()
    {
        // Arrange
        var zoo = new ZooManager();
        zoo.AddAnimal(new Animal { Id = 3, Name = "Baloo", Category = AnimalCategory.Omnivore, Status = HealthStatus.Healthy });

        // Act
        var result = zoo.CalculateDailyRation(3);

        // Assert
        result.Should().Be(7.0);
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-009")]
    public void CalculateDailyRation_CarnivoreSick_ReturnsThreePointFiveKg()
    {
        // Arrange
        var zoo = new ZooManager();
        zoo.AddAnimal(new Animal { Id = 1, Name = "Simba", Category = AnimalCategory.Carnivore, Status = HealthStatus.Sick });

        // Act
        var result = zoo.CalculateDailyRation(1);

        // Assert
        result.Should().Be(3.5);
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-009")]
    public void CalculateDailyRation_HerbivoreSick_ReturnsSevenKg()
    {
        // Arrange
        var zoo = new ZooManager();
        zoo.AddAnimal(new Animal { Id = 2, Name = "Dumbo", Category = AnimalCategory.Herbivore, Status = HealthStatus.Sick });

        // Act
        var result = zoo.CalculateDailyRation(2);

        // Assert
        result.Should().Be(7.0);
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-009")]
    public void CalculateDailyRation_OmnivoreSick_ReturnsFourPointNineKg()
    {
        // Arrange
        var zoo = new ZooManager();
        zoo.AddAnimal(new Animal { Id = 3, Name = "Baloo", Category = AnimalCategory.Omnivore, Status = HealthStatus.Sick });

        // Act
        var result = zoo.CalculateDailyRation(3);

        // Assert
        result.Should().Be(4.9);
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-010")]
    public void CalculateDailyCost_OneCarnivoreHealthy_ReturnsTwentyFiveEuros()
    {
        // Arrange
        var zoo = new ZooManager();
        zoo.AddAnimal(new Animal { Id = 1, Name = "Simba", Category = AnimalCategory.Carnivore, Status = HealthStatus.Healthy });

        // Act
        var result = zoo.CalculateDailyCost();

        // Assert
        result.Should().Be(25.0);
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-010")]
    public void CalculateDailyCost_MultipleAnimals_ReturnsSumOfCosts()
    {
        // Arrange
        var zoo = new ZooManager();
        zoo.AddAnimal(new Animal { Id = 1, Name = "Simba", Category = AnimalCategory.Carnivore, Status = HealthStatus.Healthy });
        zoo.AddAnimal(new Animal { Id = 2, Name = "Dumbo", Category = AnimalCategory.Herbivore, Status = HealthStatus.Healthy });
        zoo.AddAnimal(new Animal { Id = 3, Name = "Baloo", Category = AnimalCategory.Omnivore, Status = HealthStatus.Healthy });

        // Act
        var result = zoo.CalculateDailyCost();

        // Assert
        result.Should().Be(48.0);
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-010")]
    public void CalculateDailyCost_EmptyZoo_ReturnsZero()
    {
        // Arrange
        var zoo = new ZooManager();

        // Act
        var result = zoo.CalculateDailyCost();

        // Assert
        result.Should().Be(0.0);
    }
}
