using GameManager.Application.Services;
using GameManager.Domain.Contracts.Data;
using GameManager.Domain.Contracts.Data.Repositories;
using GameManager.Domain.Entities;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameManager.Application.IntegrationTests
{
    [TestFixture]
    public class GameServiceTests
    {
        private readonly string ID_GAME_1 = Guid.NewGuid().ToString();
        private readonly string ID_GAME_2 = Guid.NewGuid().ToString();
        private readonly string ID_GAME_3 = Guid.NewGuid().ToString();
        private readonly string ID_GAME_4 = Guid.NewGuid().ToString();

        private readonly string TITLE_GAME_1 = "GAME 1";
        private readonly string TITLE_GAME_2 = "GAME 2";
        private readonly string TITLE_GAME_3 = "GAME 3";
        private readonly string TITLE_GAME_4 = "GAME 4";

        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IGenericRepository<Game>> _gameRepository;
        private GameService _subject;

        private List<Game> _games;
        private Game _game1;
        private Game _newGame;

        [SetUp]
        public void Setup()
        {
            _game1 = new Game { Id = ID_GAME_1, Title = TITLE_GAME_1, Borrowed = false };
            _newGame = new Game { Title = "NEW GAME", Borrowed = false };

            _games = new List<Game>
            {
                _game1,
                new Game { Id = ID_GAME_2, Title = TITLE_GAME_2, Borrowed = false },
                new Game { Id = ID_GAME_3, Title = TITLE_GAME_3, Borrowed = true },
                new Game { Id = ID_GAME_4, Title = TITLE_GAME_4, Borrowed = false }
            };

            _unitOfWork = new Mock<IUnitOfWork>();
            _gameRepository = new Mock<IGenericRepository<Game>>();

            _gameRepository.Setup(x => x.GetAll()).Returns(_games);            
            _gameRepository.Setup(x => x.Get(ID_GAME_1)).Returns(_game1);
            _gameRepository.Setup(x => x.Update(_game1));
            _gameRepository.Setup(x => x.Add(_newGame));

            _unitOfWork.Setup(x => x.GameRepository).Returns(_gameRepository.Object);

            _subject = new GameService(_unitOfWork.Object);
        }

        [Test]
        public void GetAll()
        {
            var games = _subject.GetAll();

            Assert.AreEqual(games, _games);
        }

        [Test]
        public void GetAvailableGames()
        {
            _gameRepository.Setup(x => x.Get(It.IsAny<Func<Game, Boolean>>()))
                           .Returns(_games.Where(z => z.Borrowed == false).ToList());

            var games = _subject.GetAvailableGames();

            Assert.AreEqual(3, games.Count());
        }

        [Test]
        public void GetById()
        {
            var game1 = _subject.Get(ID_GAME_1);

            Assert.AreEqual(_game1, game1);
        }

        [Test]
        public void SaveUpdate()
        {
            _subject.Save(_game1);

            _gameRepository.Verify(x => x.Add(_game1), Times.Never);
            _gameRepository.Verify(x => x.Update(_game1), Times.Once);
            _unitOfWork.Verify(x => x.Save(), Times.Once);
        }

        [Test]
        public void SaveNew()
        {
            _subject.Save(_newGame);

            _gameRepository.Verify(x => x.Add(_newGame), Times.Once);
            _unitOfWork.Verify(x => x.Save(), Times.Once);
        }

        [Test]
        public void GetByTitle()
        {
            _gameRepository.Setup(x => x.Get(It.IsAny<Func<Game, Boolean>>()))
                           .Returns(new List<Game> { _game1 });

            var games = _subject.GetByTitle("1");

            Assert.AreEqual(1, games.Count());
        }

        [Test]
        public void GetByTitleWithNullTitle()
        {
            var games = _subject.GetByTitle(null);

            _gameRepository.Verify(x => x.GetAll(), Times.Once);

            Assert.AreEqual(4, games.Count());
        }

        [Test]
        public void Delete()
        {
            _subject.Delete(ID_GAME_1);

            _gameRepository.Verify(x => x.Delete(_game1), Times.Once);
            _unitOfWork.Verify(x => x.Save(), Times.Once);
        }
    }
}
