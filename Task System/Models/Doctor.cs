using System.ComponentModel.DataAnnotations;

namespace Project_Task.Models
{
    public class Doctor
    {

        [Key]
        public int DoctorId { get; set; }
        public string? Name { get; set; }
        public string? Specialization { get; set; }
        public string? Img { get; set; }

    }
}
