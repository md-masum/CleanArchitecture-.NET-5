using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Mappings;
using Application.Common.Wrappers;
using Application.DTOs.Students;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Students.Commands
{
    public class UpdateStudentCommand : IRequest<Response<StudentToReturnDto>>, IMapFrom<Student>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte Age { get; set; }
        public string Address { get; set; }
    }

    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, Response<StudentToReturnDto>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        public UpdateStudentCommandHandler(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<Response<StudentToReturnDto>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetStudentById(request.Id);
            if (student is null)
            {
                throw new ApiException($"Student Not Found.");
            }

            var updatedStudent = await _studentRepository.UpdateStudent(_mapper.Map<Student>(request));
            return new Response<StudentToReturnDto>(_mapper.Map<StudentToReturnDto>(updatedStudent));
        }
    }

    public class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
    {
        private readonly IStudentRepository _studentRepository;
        public UpdateStudentCommandValidator(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;

            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .EmailAddress().WithMessage("invalid email address").CustomAsync(IsUniqueEmailForUpdate);
        }
        private async Task IsUniqueEmailForUpdate(string email, ValidationContext<UpdateStudentCommand> context, CancellationToken cancellationToken)
        {
            var result = await _studentRepository.IsUniqueEmailForUpdateAsync(context.InstanceToValidate.Id, email);
            if (!result)
            {
                context.AddFailure("email already exist");
            }
        }
    }
}
