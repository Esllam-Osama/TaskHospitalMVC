using ContactDoctor.Models;
using Hospital.Models;
using Microsoft.EntityFrameworkCore;

namespace firstProjectMVC.Data
{
  public class ApplicationDbContext : DbContext
  {
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);
      optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=TaskHospital; Integrated Security=True;TrustServerCertificate=True");
    }
  }
}
