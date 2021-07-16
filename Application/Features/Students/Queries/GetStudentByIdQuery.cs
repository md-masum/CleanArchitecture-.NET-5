using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Wrappers;
using Application.DTOs.Students;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Students.Queries
{
    public class GetStudentByIdQuery : IRequest<Response<StudentToReturnDto>>
    {
        public GetStudentByIdQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }

    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, Response<StudentToReturnDto>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        public GetStudentByIdQueryHandler(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<Response<StudentToReturnDto>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var students = await _studentRepository.GetStudentById(request.Id);
            var studentViewModels = _mapper.Map<StudentToReturnDto>(students);
            return new Response<StudentToReturnDto>(studentViewModels);
        }
    }
}
