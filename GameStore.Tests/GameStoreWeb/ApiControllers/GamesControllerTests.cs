using AutoMapper;
using GameStore.BLL.Entities;
using GameStore.BLL.Interfaces;
using GameStore.Tests.GameStoreBLL.Services;
using GameStore.Web.ApiControllers;
using GameStore.Web.ApiResources;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Xunit;

namespace GameStore.Tests.GameStoreWeb.ApiControllers
{
    public class GamesControllerTests
    {
        private Mock<IGameService> _mockGameService;
        private Mock<ICommentService> _mockCommentService;
        private Mock<IGenreService> _mockGenreService;
        private Mock<IMapper> _mockAutoMapper;

        public GamesControllerTests()
        {
            _mockGameService = new Mock<IGameService>();
            _mockCommentService = new Mock<ICommentService>();
            _mockGenreService = new Mock<IGenreService>();
            _mockAutoMapper = new Mock<IMapper>();
        }

        public static List<GetGameResource> GetTestGameResources()
        {
            return new List<GetGameResource>()
            {
                new GetGameResource { GameName = "Counter Strike", Description = "CS", PublisherName = "Publisher"},
                new GetGameResource { GameName = "Assasins Creed", Description = "AC", PublisherName = "Publisher"},
                new GetGameResource { GameName = "Grand Theft Auto", Description = "GTA", PublisherName = "Publisher"},
                new GetGameResource { GameName = "The Elder Scrolls: Skyrim", Description = "Skyrim", PublisherName = "Publisher"},
                new GetGameResource { GameName = "Mafia", Description = "Mafia", PublisherName = "Publisher"},
                new GetGameResource { GameName = "Dishonored", Description = "Dishonored", PublisherName = "Publisher"},
            };
        }

        [Fact]
        public void GetAllGames_ShouldCallGetAll()
        {
            // Arrange
            _mockGameService.Setup(s => s.GetAll()).Returns(It.IsAny<IEnumerable<Game>>());
            var gamesController = new GamesController(_mockGameService.Object, null,
                null, _mockAutoMapper.Object);

            // Act
            gamesController.GetAllGames();

            // Assert
            _mockGameService.Verify(s => s.GetAll(), Times.Once);
        }

        [Fact]
        public void GetAllGames_ShouldReturnOkWithResponseBody()
        {
            // Arrange
            _mockAutoMapper.Setup(m => m.Map<IEnumerable<Game>, IEnumerable<GetGameResource>>(
                It.IsAny<IEnumerable<Game>>()))
                .Returns(GetTestGameResources());

            var gamesController = new GamesController(_mockGameService.Object, null, 
                null, _mockAutoMapper.Object);

            // Act
            var actionResult = gamesController.GetAllGames();
            var contentResult = actionResult as OkNegotiatedContentResult<
                IEnumerable<GetGameResource>>;

            // Assert
            Assert.NotNull(contentResult.Content);
        }

        [Fact]
        public void GetAllGames_GetAllReturnedNull_ShouldReturnNotFound()
        {
            // Arrange
            _mockGameService.Setup(s => s.GetAll()).Returns((IEnumerable<Game>)null);
            var gamesController = new GamesController(_mockGameService.Object, null,
                null, _mockAutoMapper.Object);

            // Act
            var actionResult = gamesController.GetAllGames();

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public void GetGame_ShouldReturnGameWithSameName()
        {
            // Arrange
            _mockGameService.Setup(s => s.Get(1)).Returns(GameServiceTests.GetTestGames().First());
            _mockAutoMapper.Setup(m => m.Map<Game, GetGameResource>(It.IsAny<Game>()))
                .Returns(GetTestGameResources().First());

            var gamesController = new GamesController(_mockGameService.Object, null,
                null, _mockAutoMapper.Object);

            // Act
            var actionResult = gamesController.GetGame(1);
            var contentResult = actionResult as OkNegotiatedContentResult<GetGameResource>;

            // Assert
            Assert.Equal(GetTestGameResources().First().GameName, contentResult.Content.GameName);
        }

        [Fact]
        public void GetGame_GetReturnsNull_ShouldReturnNotFound()
        {
            // Arrange
            var gamesController = new GamesController(_mockGameService.Object, null,
                null, _mockAutoMapper.Object);

            // Act
            var actionResult = gamesController.GetGame(It.IsAny<int>());

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public void CreateGame_CreateGameResourcePassed_ShouldReturnGameWithSameName()
        {
            // Arrange
            _mockAutoMapper.Setup(m => m.Map<CreateGameResource, Game>(It.IsAny<CreateGameResource>()))
                .Returns(GameServiceTests.GetTestGames().First());
            _mockAutoMapper.Setup(m => m.Map<Game, GetGameResource>(It.IsAny<Game>()))
                .Returns(GetTestGameResources().First());

            var gamesController = new GamesController(_mockGameService.Object, null,
                null, _mockAutoMapper.Object);

            // Act
            var actionResult = gamesController.CreateGame(new CreateGameResource());
            var contentResult = actionResult as OkNegotiatedContentResult<GetGameResource>;

            // Assert
            Assert.Equal(GetTestGameResources().First().GameName, contentResult.Content.GameName);
        }

        [Fact]
        public void CreateGame_NullPassed_ShouldReturnNotFound()
        {
            // Arrange
            var gamesController = new GamesController(_mockGameService.Object, null,
                null, _mockAutoMapper.Object);

            // Act
            var actionResult = gamesController.CreateGame(null);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public void CreateGame_ShouldCallCreate()
        {
            // Arrange
            _mockGameService.Setup(s => s.Create(It.IsAny<Game>()));
            var gamesController = new GamesController(_mockGameService.Object, null,
                null, _mockAutoMapper.Object);

            // Act
            var actionResult = gamesController.CreateGame(new CreateGameResource());

            // Assert
            _mockGameService.Verify(s => s.Create(It.IsAny<Game>()), Times.Once);
        }

        [Fact]
        public void UpdateGame_CreateGameResourcePassed_ShouldReturnGameWithSameName()
        {
            // Arrange
            _mockAutoMapper.Setup(m => m.Map<CreateGameResource, Game>(It.IsAny<CreateGameResource>()))
                .Returns(GameServiceTests.GetTestGames().First());
            _mockAutoMapper.Setup(m => m.Map<Game, GetGameResource>(It.IsAny<Game>()))
                .Returns(GetTestGameResources().First());

            var gamesController = new GamesController(_mockGameService.Object, null,
                null, _mockAutoMapper.Object);

            // Act
            var actionResult = gamesController.UpdateGame(It.IsAny<int>(), new CreateGameResource());
            var contentResult = actionResult as OkNegotiatedContentResult<GetGameResource>;

            // Assert
            Assert.Equal(GetTestGameResources().First().GameName, contentResult.Content.GameName);
        }

        [Fact]
        public void UpdateGame_NullPassed_ShouldReturnNotFound()
        {
            // Arrange
            var gamesController = new GamesController(_mockGameService.Object, null,
                null, _mockAutoMapper.Object);

            // Act
            var actionResult = gamesController.UpdateGame(It.IsAny<int>(), null);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public void UpdateGame_ShouldCallEdit()
        {
            // Arrange
            _mockGameService.Setup(s => s.Edit(It.IsAny<int>(), It.IsAny<Game>()));
            var gamesController = new GamesController(_mockGameService.Object, null,
                null, _mockAutoMapper.Object);

            // Act
            var actionResult = gamesController.UpdateGame(It.IsAny<int>(),
                new CreateGameResource());

            // Assert
            _mockGameService.Verify(s => s.Edit(It.IsAny<int>(), It.IsAny<Game>()), 
                Times.Once);
        }

        [Fact]
        public void DeleteGame_GetReturnsNull_ShouldReturnNotFound()
        {
            // Arrange
            var gamesController = new GamesController(_mockGameService.Object, null,
                null, _mockAutoMapper.Object);

            // Act
            var actionResult = gamesController.DeleteGame(It.IsAny<int>());

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public void DeleteGame_GameIdPassed_ShouldReturnDeletedGameId()
        {
            // Arrange
            _mockGameService.Setup(s => s.Get(1)).Returns(GameServiceTests.GetTestGames().First());
            var gamesController = new GamesController(_mockGameService.Object, null,
                null, _mockAutoMapper.Object);

            // Act
            var actionResult = gamesController.DeleteGame(1);
            var contentResult = actionResult as OkNegotiatedContentResult<int>;

            // Assert
            Assert.Equal(1, contentResult.Content);
        }

        [Fact]
        public void DeleteGame_ShouldCallDelete()
        {
            // Arrange
            _mockGameService.Setup(s => s.Delete(It.IsAny<Game>()));
            _mockGameService.Setup(s => s.Get(1)).Returns(GameServiceTests.GetTestGames().First());
            var gamesController = new GamesController(_mockGameService.Object, null,
                null, _mockAutoMapper.Object);

            // Act
            var actionResult = gamesController.DeleteGame(1);

            // Assert
            _mockGameService.Verify(s => s.Delete(It.IsAny<Game>()), Times.Once);
        }
    }
}
