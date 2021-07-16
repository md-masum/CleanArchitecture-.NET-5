using System;

namespace Application.Common.Wrappers
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public int TotalItem { get; set; }

        public PagedResponse(T data, int pageNumber, int pageSize, int totalItem)
        {
            this.TotalItem = totalItem;
            this.TotalPage = (int)Math.Ceiling(totalItem / (double)pageSize);
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }
    }
}
