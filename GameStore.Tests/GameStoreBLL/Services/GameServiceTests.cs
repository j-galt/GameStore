using GameStore.BLL.Entities;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Services;
using GameStore.BLL.Utilities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameStore.Tests.GameStoreBLL.Services
{
    public class GameServiceTests
    {
        private Mock<IRepository<Game>> _mockGameRepository;
        private Mock<IRepository<Genre>> _mockGenreRepository;
        private Mock<IRepository<PlatformType>> _mockPtRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private int invalidId = -1;

        public GameServiceTests()
        {
            _mockGameRepository = new Mock<IRepository<Game>>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockGenreRepository = new Mock<IRepository<Genre>>();
            _mockPtRepository = new Mock<IRepository<PlatformType>>();
        }

        public static List<Game> GetTestGames()
        {
            return new List<Game>()
            {
                new Game { GameId = 1, GameName = "Counter Strike", Description = "CS", PublisherId = 1},
                new Game { GameId = 2, GameName = "Assasins Creed", Description = "AC", PublisherId = 2},
                new Game { GameId = 3, GameName = "Grand Theft Auto", Description = "GTA", PublisherId = 3},
                new Game { GameId = 4, GameName = "The Elder Scrolls: Skyrim", Description = "Skyrim", PublisherId = 4},
                new Game { GameId = 5, GameName = "Mafia", Description = "Mafia", PublisherId = 5},
                new Game { GameId = 6, GameName = "Dishonored", Description = "Dishonored", PublisherId = 6},
            };
        }

        [Fact]
        public void Get_NullObjPassed_ShouldThrowArgumentNullException()
        {
            // Arrange
            var gameService = new GameService(_mockGameRepository.Object, null, null, null);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => gameService.Get(null));
        }

        [Fact]
        public void Get_InvalidIdPassed_ShouldThrowArgumentNullException()
        {
            // Arrange
            var gameService = new GameService(_mockGameRepository.Object, null, null, null);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => gameService.Get(invalidId));
        }

        [Fact]
        public void Get_GetTestData_ShouldCallGetWithIncludes()
        {
            // Arrange
            _mockGameRepository.Setup(repo => repo.GetWithIncludes(
                It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns(GetTestGames());
            var gameService = new GameService(_mockGameRepository.Object, null, null, null);

            // Act
            var result = gameService.Get(It.IsAny<Expression<Func<Game, bool>>>());

            // Assert
            _mockGameRepository.Verify(r => r.GetWithIncludes(It.IsAny<Expression<Func<Game, bool>>>()), 
                Times.Once());
        }

        [Fact]
        public void GetAll_GetTestData_ShouldReturnListOfGames()
        {
            // Arrange
            _mockGameRepository.Setup(r => r.GetAll()).Returns(GetTestGames());
            var gameService = new GameService(_mockGameRepository.Object, null, null, null);
            
            // Act
            var result = gameService.GetAll();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetAll_GetTestData_ShouldCallGetAll()
        {
            // Arrange
            _mockGameRepository.Setup(r => r.GetAll()).Returns(GetTestGames());
            var gameService = new GameService(_mockGameRepository.Object, null, null, null);

            // Act
            var result = gameService.GetAll();

            // Assert
            _mockGameRepository.Verify(r => r.GetAll(), Times.Once());
        }

        [Fact]
        public void Create_NullPassed_ShouldThrowArgumentNullException()
        {
            // Arrange
            var gameService = new GameService(_mockGameRepository.Object, 
                _mockUnitOfWork.Object, null, null);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => gameService.Create(null));
        }

        [Fact]
        public void Create_NewGameObjectPassed_ShouldCallAdd()
        {
            // Arrange
            _mockGameRepository.Setup(r => r.Add(It.IsAny<Game>()));
            var gameService = new GameService(_mockGameRepository.Object, 
                _mockUnitOfWork.Object, null, null);

            // Act
            gameService.Create(new Game());

            // Assert
            _mockGameRepository.Verify(r => r.Add(It.IsAny<Game>()), Times.Once());
        }

        [Fact]
        public void Edit_NullPassed_ShouldThrowArgumentNullException()
        {
            // Arrange
            var gameService = new GameService(_mockGameRepository.Object, null, null, null);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => gameService.Edit(It.IsAny<int>(), null));
        }

        [Fact]
        public void Edit_UpdatedGameObjectPassed_ShouldReturnGameThatEqualUpdatedGame()
        {
            // Arrange
            var updatedGame = new Game()
            {
                GameId = 1,
                GameName = "UpdatedName",
                Description = "UpdatedDescription"
            };

            _mockGameRepository.Setup(r => r.GetWithIncludes(It.IsAny<Expression<Func<Game, bool>>>(),
                It.IsAny<Expression<Func<Game, object>>>(),
                It.IsAny<Expression<Func<Game, object>>>()))
                .Returns(GetTestGames());

            var gameService = new GameService(_mockGameRepository.Object, _mockUnitOfWork.Object, 
                _mockPtRepository.Object, _mockGenreRepository.Object);

            // Act
            var result = gameService.Edit(It.IsAny<int>(), updatedGame);

            // Assert
            Assert.Equal(result, updatedGame, new GameComparer());
        }

        [Fact]
        public void GetGamesByGenre_NullPassed_ShouldThrowArgumentNullException()
        {
            // Arrange
            var gameService = new GameService(_mockGameRepository.Object, null, 
                null, _mockGenreRepository.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => gameService.GetGamesByGenre(null));
        }

        [Fact]
        public void GetGamesByGenre_GetTestData_ShouldCallFind()
        {
            // Arrange
            _mockGameRepository.Setup(r => r.Find(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns(new List<Game>());
            _mockGenreRepository.Setup(r => r.Find(It.IsAny<Expression<Func<Genre, bool>>>()))
                .Returns(new List<Genre>() { new Genre { GenreName = "FPS" } });

            var gameService = new GameService(_mockGameRepository.Object, null,
                null, _mockGenreRepository.Object);

            // Act 
            var result = gameService.GetGamesByGenre(It.IsAny<string>());

            // Assert
            _mockGameRepository.Verify(r => r.Find(It.IsAny<Expression<Func<Game, bool>>>()), Times.Once);
        }

        [Fact]
        public void GetGamesByPlatformTypes_TestDataPassed_ShouldCallGetWithIncludes()
        {
            // Arrange
            _mockGameRepository.Setup(r => r.GetWithIncludes(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns(new List<Game>());

            var gameService = new GameService(_mockGameRepository.Object, null, null, null);

            // Act 
            var result = gameService.GetGamesByPlatformTypes(new List<PlatformType>()
            {
                new PlatformType { Type = "PC" }
            });

            // Assert
            _mockGameRepository.Verify(r => r.GetWithIncludes(It.IsAny<Expression<Func<Game, bool>>>()), 
                Times.Once);
        }

        [Fact]
        public void Delete_NullPassed_ShouldThrowArgumentNullException()
        {
            // Arrange
            var gameService = new GameService(_mockGameRepository.Object, null, null, null);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => gameService.Delete(null));
        }

        [Fact]
        public void Delete_TestDataPassed_ShouldCallRemove()
        {
            // Arrange
            _mockGameRepository.Setup(r => r.Remove(It.IsAny<Game>()));
            var gameService = new GameService(_mockGameRepository.Object, 
                _mockUnitOfWork.Object, null, null);

            // Act 
            gameService.Delete(new Game());

            // Assert
            _mockGameRepository.Verify(r => r.Remove(It.IsAny<Game>()), Times.Once);
        }
    }
}
