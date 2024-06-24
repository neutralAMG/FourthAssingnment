
using Microsoft.AspNetCore.Http;

namespace ForthAssignment.Core.Aplication.Utils.FileHandler
{
	public interface IFileHandler
	{
		string UploudFile(IFormFile file, string basePath,  Guid id);
		string UpdateFile(IFormFile file, Guid id, string basePath, string ImagUrl = "");
		void DeleteFile(Guid id, string basePath);
	}
}

