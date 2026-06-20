using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Contract
{
    public interface IS3ArchiveRepository
    {
        Task<string> GetRawArchive(string key);
    }
}
