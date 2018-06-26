using GameStore.BLL.Entities;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameStore.Tests.GameStoreBLL.Services
{
    public class GameServiceTests
    {
        private Mock<IGameService> _mockGameService;
        private Mock<IRepository<Game>> _mockGameRepository;


        public GameServiceTests()
        {
            _mockGameService = new Mock<IGameService>();
            _mockGameRepository = new Mock<IRepository<Game>>();
        }

        [Fact]
        public void Get_NullObjGiven_ShouldThrowArgumentNullException()
        {
            var gameService = new Service<Game>(_mockGameRepository.Object, null);
            gameService.Get(null, null);
            Assert.Throws<ArgumentNullException>(() => gameService.Get(null, null));
        }
    }
}
