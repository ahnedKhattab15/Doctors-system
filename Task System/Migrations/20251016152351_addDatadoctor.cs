using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task_System.Migrations
{
    /// <inheritdoc />
    public partial class addDatadoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            INSERT INTO Doctors (Name, Specialization, Img) VALUES
            ('Dr. Ahmed Ali', 'Cardiology', 'doctor1.jpg'),
            ('Dr. Sara Omar', 'Dermatology', 'doctor2.jpg'),
            ('Dr. Mohamed Hassan', 'Neurology', 'doctor3.jpg'),
            ('Dr. Noha Mahmoud', 'Dentistry', 'doctor4.jpg'),
            ('Dr. Youssef Gamal', 'Ophthalmology', 'doctor5.jpg'),
            ('Dr. Laila Samir', 'Psychiatry', 'doctor6.jpg');");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
