using Microsoft.EntityFrameworkCore;
using Project_Task.Models;

namespace Task_System.Data_Acsess
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-EEU926S;Initial Catalog= System1 ;Integrated Security=True;Connect Timeout=30;Encrypt=True;" +
               "Trust Server Certificate=True;");
        }
    }
}
