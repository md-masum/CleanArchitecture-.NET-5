using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Wrappers;
using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Features.Students.Commands
{
    public class DeleteStudentCommand : IRequest<Response<int>>
    {
        public DeleteStudentCommand(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }

    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, Response<int>>
    {
        private readonly IStudentRepository _studentRepository;
        public DeleteStudentCommandHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<Response<int>> Handle(DeleteStudentCommand command, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetStudentById(command.Id);
            if (student == null) throw new ApiException($"Student Not Found.");
            await _studentRepository.DeleteStudent(student);
            return new Response<int>(student.Id);
        }
    }
}
