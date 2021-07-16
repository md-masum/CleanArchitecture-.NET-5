// using System.Threading.Tasks;
// using Application.Features.Students.Commands;
// using Application.Features.Students.Queries;
// using Microsoft.AspNetCore.Mvc;
//
// namespace WebApi.Controllers.V0
// {
//     [ApiVersion("0.0")]
//     public class StudentController : BaseApiController
//     {
//         [HttpGet]
//         public async Task<IActionResult> GetAllStudent([FromQuery] GetAllStudentParams studentParams)
//         {
//             var result = await Mediator.Send(new GetAllStudentQuery(studentParams.PageNumber, studentParams.PageSize));
//             return Ok(result.Data);
//         }
//         
//         [HttpGet("{id}")]
//         public async Task<IActionResult> GetStudentById(int id)
//         {
//             var result = await Mediator.Send(new GetStudentByIdQuery(id));
//             return Ok(result.Data);
//         }
//         
//         [HttpPost]
//         public async Task<IActionResult> CreateStudent(CreateStudentCommand command)
//         {
//             var result = await Mediator.Send(command);
//             return Ok(result.Data);
//         }
//         
//         [HttpPut]
//         public async Task<IActionResult> UpdateStudent(int id, UpdateStudentCommand command)
//         {
//             if (id != command.Id)
//             {
//                 return BadRequest();
//             }
//
//             var result = await Mediator.Send(command);
//             return Ok(result.Data);
//         }
//         
//         [HttpDelete]
//         public async Task<IActionResult> DeleteStudent(int id)
//         {
//             var result = await Mediator.Send(new DeleteStudentCommand(id));
//             return Ok(result.Data);
//         }
//
//
//     }
// }
