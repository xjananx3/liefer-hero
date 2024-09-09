using FakeItEasy;
using FluentAssertions;
using LieferHero.Controllers;
using LieferHero.Interfaces;
using LieferHero.Models;
using Microsoft.AspNetCore.Mvc;

namespace LieferHero.Tests.ControllerTests;

public class SpeiseControllerTests
{
    private SpeiseController _speiseController;
    private ISpeiseRepository _speiseRepository;
    private ISpeiseInBestellungRepository _speiseInBestellungRepository;
    private IPhotoService _photoService;
    
    public SpeiseControllerTests()
    {
        _speiseRepository = A.Fake<ISpeiseRepository>();
        _speiseInBestellungRepository = A.Fake<ISpeiseInBestellungRepository>();
        _photoService = A.Fake<IPhotoService>();

        _speiseController = new SpeiseController(_speiseRepository, _speiseInBestellungRepository, _photoService);
    }

    [Fact]
    public void SpieseController_Index_ReturnsSuccess()
    {
        // Arrange
        var speisen = A.Fake<IEnumerable<Speise>>();
        A.CallTo(() => _speiseRepository.GetAll()).Returns(speisen);

        // Act
        var result = _speiseController.Index();

        // Assert 
        result.Should().BeOfType<Task<IActionResult>>();

    }
}