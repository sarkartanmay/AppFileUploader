using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppFileUploader.Domain.Entities
{
    public class FileContent
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Filename { get; set; }
        public DateTime UploadTime { get; set; }
    }
}