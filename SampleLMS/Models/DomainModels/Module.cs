﻿using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SampleLMS.Models.DomainModels
{
    public class Module
    {
        [Key]
        public int ModuleId { get; set; }

        [Required]
        public string ModuleName { get; set; } = string.Empty;
        public string? ContentType { get; set; } = string.Empty; // You can use this to store the file type (e.g., PDF, Word, Quiz, etc.).
        
        // Add a property to store the uploaded file, you may use IFormFile or byte[] based on your requirements.
        public IFormFile? UploadedFile { get; set; } // This is for handling file uploads.
        
        public string? FilePath { get; set; } // Store the path to the uploaded file on the server.

        public int CourseId { get; set; }
        
        public Course? Course { get; set; }
    }
}