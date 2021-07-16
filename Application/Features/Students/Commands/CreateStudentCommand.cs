using System.Threading;
using System.Threading.Tasks;
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
    public class CreateStudentCommand : IRequest<Response<StudentToReturnDto>>, IMapFrom<Student>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte Age { get; set; }
        public string Address { get; set; }
    }

    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Response<StudentToReturnDto>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        public CreateStudentCommandHandler(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<Response<StudentToReturnDto>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.CreateStudent(_mapper.Map<Student>(request));
            return new Response<StudentToReturnDto>(_mapper.Map<StudentToReturnDto>(student));
        }
    }

    public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
    {
        private readonly IStudentRepository _studentRepository;
        public CreateStudentCommandValidator(IStudentRepository studentRepository)
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
                .EmailAddress().WithMessage("invalid email address")
                .MustAsync(IsUniqueEmail).WithMessage("{PropertyName} already exists.");
        }

        private async Task<bool> IsUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return await _studentRepository.IsUniqueEmailAsync(email);
        }
    }
}
