using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Wrappers;
using Application.DTOs.Students;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Students.Queries
{
    public class GetAllStudentQuery : IRequest<PagedResponse<IList<StudentToReturnDto>>>
    {
        public GetAllStudentQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllStudentQueryHandler : IRequestHandler<GetAllStudentQuery, PagedResponse<IList<StudentToReturnDto>>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        public GetAllStudentQueryHandler(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IList<StudentToReturnDto>>> Handle(GetAllStudentQuery request, CancellationToken cancellationToken)
        {
            var students = await _studentRepository.GetAllStudent(request.PageNumber, request.PageSize);
            var studentViewModels = _mapper.Map<IList<StudentToReturnDto>>(students.Data);
            return new PagedResponse<IList<StudentToReturnDto>>(studentViewModels, request.PageNumber, request.PageSize, students.TotalCount);
        }
    }
}
