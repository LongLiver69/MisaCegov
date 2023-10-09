using AutoMapper;
using MISA.WEB05.CEGOV.Domain;

namespace MISA.WEB05.CEGOV.Application.Tests
{
    [TestFixture]
    public class AwardServiceTests
    {
        #region Fields
        private IAwardRepository _awardRepository;
        private IMapper _mapper;
        private IAwardManager _awardManager;
        private IExcelWorker _excelWorker;
        private AwardService _awardService;
        #endregion

        [SetUp]
        public void SetUp()
        {
            _awardRepository = Substitute.For<IAwardRepository>();
            _mapper = Substitute.For<IMapper>();
            _awardManager = Substitute.For<IAwardManager>();
            _excelWorker = Substitute.For<IExcelWorker>();
            _awardService = new AwardService(_awardRepository, _mapper, _awardManager, _excelWorker);
        }

        /// <summary>
        /// Test service xóa một bản ghi
        /// </summary>
        /// Created by: ntlong ( 24/07/2023 )
        [Test]
        public async Task DeleteAsync_CompleteId_Success()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            await _awardService.DeleteAsync(id);

            // Assert
            await _awardRepository.Received(1).GetByIdAsync(id);
            await _awardRepository.Received(1).DeleteAsync(id);
        }

        /// <summary>
        /// Test service xóa nhiều bản ghi trường hợp danh sách id rỗng
        /// </summary>
        /// Created by: ntlong ( 24/07/2023 )
        [Test]
        public async Task DeleteManyAsync_EmptyList_ThrowException()
        {
            // Arrange
            List<Guid> ids = new();
            var expectedMessage = "Không được truyền danh sách rỗng";

            // Act & Assert
            var exception = Assert.ThrowsAsync<Exception>(async () => await _awardService.DeleteManyAsync(ids));
            Assert.That(exception.Message, Is.EqualTo(expectedMessage));

            await _awardRepository.Received(0).DeleteManyAsync(ids);
        }

        /// <summary>
        /// Test service xóa nhiều bản ghi trường hợp không tìm thấy đủ id
        /// </summary>
        /// Created by: ntlong ( 24/07/2023 )
        [Test]
        public async Task DeleteManyAsync_IncompleteList_ThrowException()
        {
            // Arrange
            var expectedMessage = "Không thể xóa";

            // Tạo và thêm 5 id ngẫu nhiên không trùng vào list
            List<Guid> ids = new();
            for (int i = 0; i < 5; i++)
            {
                ids.Add(Guid.NewGuid());
            }

            // Tạo và thêm 4 award vào list
            List<Award> awards = new();
            for (int i = 0; i < 4; i++)
            {
                awards.Add(new Award());
            }

            _awardRepository.GetListByIdsAsync(ids).Returns(awards);

            // Act & Assert
            var exception = Assert.ThrowsAsync<Exception>(async () => await _awardService.DeleteManyAsync(ids));
            Assert.That(exception.Message, Is.EqualTo(expectedMessage));

            await _awardRepository.Received(1).GetListByIdsAsync(ids);
            await _awardRepository.Received(0).DeleteManyAsync(ids);
        }

        /// <summary>
        /// Test service xóa nhiều bản ghi trường hợp thành công
        /// </summary>
        /// Created by: ntlong ( 24/07/2023 )
        [Test]
        public async Task DeleteManyAsync_CompleteList_Success()
        {
            // Arrange

            // Tạo và thêm 5 id ngẫu nhiên không trùng vào list
            List<Guid> ids = new();
            for (int i = 0; i < 5; i++)
            {
                ids.Add(Guid.NewGuid());
            }

            // Tạo và thêm 5 award vào list
            List<Award> awards = new();
            for (int i = 0; i < 5; i++)
            {
                awards.Add(new Award());
            }

            _awardRepository.GetListByIdsAsync(ids).Returns(awards);

            // Act
            await _awardService.DeleteManyAsync(ids);

            // Assert
            await _awardRepository.Received(1).GetListByIdsAsync(ids);
            await _awardRepository.Received(1).DeleteManyAsync(ids);
        }
    }
}
