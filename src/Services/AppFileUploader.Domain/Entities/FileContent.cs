using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppFileUploader.Domain.Entities
{
    public class FileContent
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)] 
        public required string Name { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(150)] 
        public string? Description { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(350)] 
        public required string Filename { get; set; }
        public DateTime UploadTime { get; set; }
    }
}