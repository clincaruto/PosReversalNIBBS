using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosReversalNIBBS_API.Models.DTO
{
    public class FileUploadDto
    {
        [Key]
        public Guid BatchId { get; set; }

        //[NotMapped]
        //public IFormFile File { get; set; }
        public string FileName { get; set; }
        //public string? FileDescription { get; set; }
       // public string FileExtension { get; set; }
        // public long FileSizeInBytes { get; set; }
       // public string FilePath { get; set; }
        public DateTime DateUploaded { get; set; }
        public double TotalTransaction { get; set; }
        public double TotalAmount { get; set; }
        public string Status { get; set; }
    }
}
