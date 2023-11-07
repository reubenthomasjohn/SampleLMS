using SampleLMS.Models.DomainModels;
using System.ComponentModel.DataAnnotations;

namespace SampleLMS.Models.DTOs.Module
{
	public class ModuleDetailedView
	{
		[Required]
		public string ModuleName { get; set; } = string.Empty;

		public string ModuleContent { get; set; } = "Sample module content...";
		public string? ContentType { get; set; } = string.Empty; // You can use this to store the file type (e.g., PDF, Word, Quiz, etc.).

        // Add a property to store the uploaded file, you may use IFormFile or byte[] based on your requirements.
        // public IFormFile? UploadedFile { get; set; } // This is for handling file uploads.

        public IEnumerable<FilePath>? uploadedFilePaths { get; set; } // Store the paths to the uploaded file on the server.
    }
}
