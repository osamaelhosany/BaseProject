using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace BaseProject.Services.FileSystem
{
    public class FileSystem : IFileSystem
    {
        public string GetFilePath(string filename)
        {
            return Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, filename);
        }
        public async Task<Stream> OpenFileStream(string filename)
        {
            var file = await Xamarin.Essentials.FileSystem.OpenAppPackageFileAsync(filename);
            return file;
        }
    }
}
