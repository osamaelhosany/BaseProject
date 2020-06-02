using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Services.FileSystem
{
    public interface IFileSystem
    {
        string GetFilePath(string filename);
        Task<Stream> OpenFileStream(string filename);
    }
}
