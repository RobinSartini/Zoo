using FluentAssertions;
using Xunit;
using Zoo.Domain;

namespace Zoo.UnitTests;

public class ZooManagerTests
{
    [Fact]
    [Trait("Requirement", "REQ-Z-001")]
    [Trait("TestCase", "TC-001")]
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
    [Trait("TestCase", "TC-002")]
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
    [Trait("TestCase", "TC-003")]
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
    [Trait("TestCase", "TC-004")]
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
    [Trait("TestCase", "TC-005")]
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
    [Trait("TestCase", "TC-006")]
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
    [Trait("TestCase", "TC-007")]
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
    [Trait("TestCase", "TC-008")]
    public void AddAnimal_FiftyFirstAnimal_ThrowsZooCapacityExceededException()
    {
        // Arrange
        var zoo = new ZooManager();
        for (int i = 1; i <= 50; i++)
        {
            zoo.AddAnimal(new Animal { Id = i, Name = $"Animal{i}", Category = AnimalCategory.Herbivore, Status = HealthStatus.Healthy });
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
    [Trait("TestCase", "TC-009")]
    public void AddAnimal_FiftiethAnimal_Succeeds()
    {
        // Arrange
        var zoo = new ZooManager();
        for (int i = 1; i <= 49; i++)
        {
            zoo.AddAnimal(new Animal { Id = i, Name = $"Animal{i}", Category = AnimalCategory.Herbivore, Status = HealthStatus.Healthy });
        }
        var fiftiethAnimal = new Animal { Id = 50, Name = "Fiftieth", Category = AnimalCategory.Herbivore, Status = HealthStatus.Healthy };

        // Act
        var result = zoo.AddAnimal(fiftiethAnimal);

        // Assert
        result.Should().Be(50);
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-007")]
    [Trait("TestCase", "TC-010")]
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
    [Trait("TestCase", "TC-011")]
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
    [Trait("TestCase", "TC-012")]
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
    [Trait("TestCase", "TC-013")]
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
    [Trait("TestCase", "TC-014")]
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
    [Trait("TestCase", "TC-015")]
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
    [Trait("TestCase", "TC-016")]
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
    [Trait("TestCase", "TC-017")]
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
    [Trait("TestCase", "TC-018")]
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
    [Trait("TestCase", "TC-019")]
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
    [Trait("TestCase", "TC-020")]
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
    [Trait("Requirement", "REQ-Z-011")]
    [Trait("TestCase", "TC-021")]
    public void CalculateDailyCost_SickAnimal_IncludesVetFee()
    {
        // Arrange
        var zoo = new ZooManager();
        var sickCarnivore = new Animal { Id = 1, Name = "Simba", Category = AnimalCategory.Carnivore, Status = HealthStatus.Sick };
        zoo.AddAnimal(sickCarnivore);

        // Act
        var cost = zoo.CalculateDailyCost();
        var ration = zoo.CalculateDailyRation(1);

        // Assert
        cost.Should().Be(45.0); // 25 base + 20 vet fee
        ration.Should().Be(3.5); // 5 kg base - 30% = 3.5 kg
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-012")]
    [Trait("TestCase", "TC-022")]
    public void CalculateDailyCost_CriticalAnimal_IncludesVetFee()
    {
        // Arrange
        var zoo = new ZooManager();
        var criticalHerbivore = new Animal { Id = 2, Name = "Dumbo", Category = AnimalCategory.Herbivore, Status = HealthStatus.Critical };
        zoo.AddAnimal(criticalHerbivore);

        // Act
        var cost = zoo.CalculateDailyCost();
        var ration = zoo.CalculateDailyRation(2);

        // Assert
        cost.Should().Be(58.0); // 8 base + 50 vet fee
        ration.Should().Be(7.0); // 10 kg base - 30% = 7 kg
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-010")]
    [Trait("TestCase", "TC-023")]
    public void CalculateDailyCost_EmptyZoo_ReturnsZero()
    {
        // Arrange
        var zoo = new ZooManager();

        // Act
        var result = zoo.CalculateDailyCost();

        // Assert
        result.Should().Be(0.0);
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-013")]
    [Trait("TestCase", "TC-024")]
    public void GetCriticalAnimals_WithCriticalAnimals_ReturnsThem()
    {
        // Arrange
        var zoo = new ZooManager();
        var a1 = new Animal { Id = 1, Name = "Simba", Category = AnimalCategory.Carnivore, Status = HealthStatus.Critical };
        var a2 = new Animal { Id = 2, Name = "Dumbo", Category = AnimalCategory.Herbivore, Status = HealthStatus.Healthy };
        var a3 = new Animal { Id = 3, Name = "Baloo", Category = AnimalCategory.Omnivore, Status = HealthStatus.Critical };

        zoo.AddAnimal(a1);
        zoo.AddAnimal(a2);
        zoo.AddAnimal(a3);

        // Act
        var result = zoo.GetCriticalAnimals();

        // Assert
        result.Should().HaveCount(2)
            .And.Contain(a1)
            .And.Contain(a3)
            .And.NotContain(a2);
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-013")]
    [Trait("TestCase", "TC-025")]
    public void GetCriticalAnimals_NoCriticalAnimals_ReturnsEmptyList()
    {
        // Arrange
        var zoo = new ZooManager();
        zoo.AddAnimal(new Animal { Id = 1, Name = "Simba", Category = AnimalCategory.Carnivore, Status = HealthStatus.Healthy });
        zoo.AddAnimal(new Animal { Id = 2, Name = "Dumbo", Category = AnimalCategory.Herbivore, Status = HealthStatus.Sick });

        // Act
        var result = zoo.GetCriticalAnimals();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-014")]
    [Trait("TestCase", "TC-026")]
    public void RemoveAnimal_ExistingAnimal_ReturnsTrueAndDecrementsTotal()
    {
        // Arrange
        var zoo = new ZooManager();
        zoo.AddAnimal(new Animal { Id = 1, Name = "Simba", Category = AnimalCategory.Carnivore, Status = HealthStatus.Healthy });

        // Act
        var result = zoo.RemoveAnimal(1);

        // Assert
        result.Should().BeTrue();
        zoo.TotalAnimals.Should().Be(0);
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-015")]
    [Trait("TestCase", "TC-027")]
    public void RemoveAnimal_NonExistingAnimal_ReturnsFalse()
    {
        // Arrange
        var zoo = new ZooManager();

        // Act
        var result = zoo.RemoveAnimal(99);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-014")]
    [Trait("TestCase", "TC-028")]
    public void GetAnimal_AfterRemoval_ReturnsNull()
    {
        // Arrange
        var zoo = new ZooManager();
        zoo.AddAnimal(new Animal { Id = 1, Name = "Simba", Category = AnimalCategory.Carnivore, Status = HealthStatus.Healthy });
        zoo.RemoveAnimal(1);

        // Act
        var result = zoo.GetAnimal(1);

        // Assert
        result.Should().BeNull();
    }
}
