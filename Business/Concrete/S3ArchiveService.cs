using Business.Contract;
using Repository.Contract;

namespace Business.Concrete
{
    public class S3ArchiveService : IS3ArchiveService
    {
        private readonly IS3ArchiveRepository _repository;

        public S3ArchiveService(IS3ArchiveRepository repository)
        {
            _repository = repository;
        }

        public Task<string> GetRawArchive(string key)
            => _repository.GetRawArchive(key);
    }

}
