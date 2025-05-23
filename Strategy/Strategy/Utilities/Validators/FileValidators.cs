using Strategy.Utilities.Enums;

namespace Strategy.Utilities.Validators
{
    public static class FileValidators
    {
        public static bool ValidateType(this IFormFile file, string type)
        {
            return file.ContentType.Contains(type);
        }

        public static bool ValidateSize(this IFormFile file, FileType type, int size)
        {
            switch (type)
            {
                case FileType.KB:
                    return file.Length <= size * 1024;
                case FileType.MB:
                    return file.Length <= size * 1024 * 1024;
                case FileType.GB:
                    return file.Length <= size * 1024 * 1024 * 1024;

            }
            return false;
        }


        private static string _getPath(params string[] folders)
        {
            string path = string.Empty;
            for (int i = 0; i < folders.Length; i++)
            {
                path = Path.Combine(path, folders[i]);
            }
            return path;
        }


        public static async Task<string> CreateFileAsync(this IFormFile file, params string[] folders)
        {
            string filename = string.Concat(Guid.NewGuid().ToString(), file.FileName);

            string path = _getPath(folders);

            path = Path.Combine(path, filename);

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }
            return filename;
        }
        public static void Delete(this string fileName, params string[] folders)
        {
            string path = _getPath(folders);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

    }
}

