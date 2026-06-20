using Amazon;
using Amazon.S3;
using Repository.Contract;

namespace Repository.Concrete
{
    public class S3ArchiveRepository : IS3ArchiveRepository
    {
        private readonly IAmazonS3 _s3;
        private readonly string _bucket;

        public S3ArchiveRepository(string accessKey, string secretKey, string bucket, string region)
        {
            _s3 = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.GetBySystemName(region));
            _bucket = bucket;
        }

        public async Task<string?> GetRawArchive(string key)
        {
            try
            {
                var response = await _s3.GetObjectAsync(_bucket, key);
                using var reader = new StreamReader(response.ResponseStream);
                return await reader.ReadToEndAsync();
            }
            catch (AmazonS3Exception ex) when (ex.ErrorCode == "NoSuchKey" || ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Key not found in bucket
                return null;
            }
        }
    }
}
