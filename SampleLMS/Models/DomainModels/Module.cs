using System.ComponentModel.DataAnnotations;

namespace SampleLMS.Models.DomainModels
{
	public class Module
    {
        [Key]
        public int ModuleId { get; set; }

        [Required]
        public string ModuleName { get; set; } = string.Empty;

        public string ModuleContent { get; set; } = "Sample module content...";
        public string? ContentType { get; set; } = string.Empty; // You can use this to store the file type (e.g., PDF, Word, Quiz, etc.).

        // Add a property to store the uploaded file, you may use IFormFile or byte[] based on your requirements.
        // public IFormFile? UploadedFile { get; set; } // This is for handling file uploads.

        public ICollection<FilePath>? UploadedFilePaths { get; set; }// Store the paths to the uploaded files on the server.
        public ICollection<Course>? Courses { get; set; }
    }
}
