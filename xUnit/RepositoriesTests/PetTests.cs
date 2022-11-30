/*
using Core.Context;
using Core.Entities;
using Core.Repositories.PetRepository;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace xUnit.RepositoriesTests;

public class PetTests
{
    private static Guid id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e");
    private static Mock<DbSet<Pet>> GetMockPets()
    {
        var mock = new Mock<DbSet<Pet>>();
        var pets = new List<Pet>()
        {
            new Pet() { Id = id, Name = "John"}
        };
        mock.Setup(m => m.FindAsync()).Returns(() => new ValueTask<Pet>(new Task<Pet>(() => { return pets[0]; })));
      
        return mock;
    }
    
    private static Mock<PetContext> GetMockContext()
    {
        var mock = new Mock<PetContext>();
        mock.Setup(m => m.Pets).Returns(() => GetMockPets().Object);
        return mock;
    }
    
    [Fact]
    public void ReadPetTest()
    {
        //Arrange
        //var dbContextMock = GetMockContext(); 
        //var productRepository = new PetRepository(dbContextMock.Object);

        //Act
        //var petResult = productRepository.ReadPetAsync(id); 
        var petResult = GetMockPets().Object.FindAsync();
  
        //Assert  
        Assert.NotNull(petResult);  
        Assert.IsAssignableFrom<Pet>(petResult); 
    }
}*/