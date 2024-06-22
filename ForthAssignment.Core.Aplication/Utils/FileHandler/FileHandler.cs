
using Microsoft.AspNetCore.Http;

namespace ForthAssignment.Core.Aplication.Utils.FileHandler
{
	public class FileHandler : IFileHandler
	{
		public string UploudFile(IFormFile file, string basePath, Guid id)
		{
			string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}

			Guid guid = Guid.NewGuid();
			FileInfo fileInfo = new FileInfo(file.Name);
			string fileName = guid + fileInfo.Extension;
			string fileNameWithPath = Path.Combine(path, fileName); 
			
			using(FileStream stream = new(fileNameWithPath, FileMode.Create))
			{
				file.CopyTo(stream);
			}
			return $"{basePath}/{file.Name}";

		}	

		public string UpdateFile(IFormFile file, Guid id, string basePath,  string ImagUrl = "" )
		{
			if(file is null)
			{
				return ImagUrl;
			}
			string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}

			Guid guid = Guid.NewGuid();
			FileInfo fileInfo = new FileInfo(file.Name);
			string fileName = guid + fileInfo.Extension;
			string fileNameWithPath = Path.Combine(path, fileName);

			using (FileStream stream = new(fileNameWithPath, FileMode.Create))
			{
				file.CopyTo(stream);
			}
			string[] oldImagUrl = ImagUrl.Split('/');
			string oldImgName = oldImagUrl[^1];
			string CompleteOldPath = Path.Combine(path, oldImgName);

			if (System.IO.File.Exists(CompleteOldPath))
			{
				System.IO.File.Delete(CompleteOldPath);
			}
			return $"{basePath}/{file.Name}";
		}

		public void DeleteFile(string basePath)
		{
			string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

			if (Directory.Exists(path))
			{
				DirectoryInfo directoryInfo = new(path);
				foreach (FileInfo file in directoryInfo.GetFiles())
				{
					file.Delete();
				}

				foreach(DirectoryInfo folder in directoryInfo.GetDirectories())
				{
					folder.Delete(true);
				}
			}
		}

	}
}
