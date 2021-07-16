using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Features.Students;
using Application.Features.Students.Commands;
using Application.Features.Students.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;

namespace WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class StudentController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllStudent([FromQuery]GetAllStudentParams studentParams)
        {

            return Ok(await Mediator.Send(new GetAllStudentQuery(studentParams.PageNumber, studentParams.PageSize)));
        }
        
        [HttpGet("{id}", Name = "GetStudentById")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            return Ok(await Mediator.Send(new GetStudentByIdQuery(id)));
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateStudent(CreateStudentCommand command)
        {
            var result = await Mediator.Send(command);
            return CreatedAtRoute("GetStudentById", new {id = result.Data.Id}, result);
        }
        
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateStudent(int id, UpdateStudentCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            return Ok(await Mediator.Send(new DeleteStudentCommand(id)));
        }
    }
}
