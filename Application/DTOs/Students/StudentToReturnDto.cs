using Application.Common.Mappings;
using Domain.Entities;

namespace Application.DTOs.Students
{
    public class StudentToReturnDto : IMapFrom<Student>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte Age { get; set; }
        public string Address { get; set; }
    }
}
