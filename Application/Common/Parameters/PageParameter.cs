using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Parameters
{
    public class PageParameter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PageParameter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public PageParameter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}
