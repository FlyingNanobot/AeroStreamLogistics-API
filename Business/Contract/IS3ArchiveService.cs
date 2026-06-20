namespace Business.Contract
{
    public interface IS3ArchiveService
    {
        Task<string> GetRawArchive(string key);
    }
}
