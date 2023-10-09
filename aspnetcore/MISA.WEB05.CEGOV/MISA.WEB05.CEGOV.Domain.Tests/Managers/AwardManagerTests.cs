using NSubstitute;
using System;

namespace MISA.WEB05.CEGOV.Domain.Tests
{
    [TestFixture]
    public class AwardManagerTests
    {
        #region Fields
        private IAwardRepository _awardRepository;
        private AwardManager _awardManager;
        #endregion

        #region Methods
        [SetUp]
        public void Setup()
        {
            _awardRepository = Substitute.For<IAwardRepository>();
            _awardManager = new AwardManager(_awardRepository);
        }

        /// <summary>
        /// Test trường hợp Id đã tồn tại
        /// </summary>
        /// <returns>Trả về ConflictException</returns>
        /// Created by: ntlong ( 25/07/2023 )
        [Test]
        public async Task CheckAwardExistById_ExistAward_ConflictException()
        {
            // Arrange
            var id = Guid.NewGuid();
            _awardRepository.FindByIdAsync(id).Returns(new Award());
            var expectedMessage = "Id đã tồn tại";

            // Act & Assert
            var exception = Assert.ThrowsAsync<ConflictException>(async () => await _awardManager.CheckAwardExistById(id));
            Assert.That(exception.Message, Is.EqualTo(expectedMessage));

            await _awardRepository.Received(1).FindByIdAsync(id);
        }

        /// <summary>
        /// Test trường hợp Id chưa tồn tại
        /// </summary>
        /// <returns>Thành công</returns>
        /// Created by: ntlong ( 25/07/2023 )
        [Test]
        public async Task CheckAwardExistById_NotExistAward_Success()
        {
            // Arrange
            var id = Guid.NewGuid();
            _awardRepository.FindByIdAsync(id).Returns(Task.FromResult<Award?>(null));

            // Act
            await _awardManager.CheckAwardExistById(id);

            // Assert
            await _awardRepository.Received(1).FindByIdAsync(id);
        }

        /// <summary>
        /// Test trường hợp mã danh hiệu đã tồn tại
        /// </summary>
        /// <returns>Trả về ConflictException</returns>
        /// Created by: ntlong ( 25/07/2023 )
        [Test]
        public async Task CheckAwardExistByCode_ExistAward_ConflictException()
        {
            // Arrange
            var code = "testtest";
            var oldCode = "abcxyz";
            _awardRepository.FindByCodeAsync(code).Returns(new Award() { AwardCode = code });

            // Act & Assert
            Assert.ThrowsAsync<ConflictException>(async () => await _awardManager.CheckAwardExistByCode(code, oldCode));

            await _awardRepository.Received(1).FindByCodeAsync(code);
        }

        /// <summary>
        /// Test trường hợp mã danh hiệu chưa tồn tại
        /// </summary>
        /// <returns>Thành công</returns>
        /// Created by: ntlong ( 25/07/2023 )
        [Test]
        public async Task CheckAwardExistByCode_NotExistAward_Success()
        {
            // Arrange
            var code = "testtest";
            var oldCode = "abcxyz";
            _awardRepository.FindByCodeAsync(code).Returns(Task.FromResult<Award?>(null));

            // Act
            await _awardManager.CheckAwardExistByCode(code, oldCode);

            // Assert
            await _awardRepository.Received(1).FindByCodeAsync(code);
        }

        #endregion
    }
}
