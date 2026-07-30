using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.PageList
{
    public class PageList<T>
    {

        public List<T> List { get; }
        public int Page {  get;  }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages => List.Count / PageSize;
        public bool HasNextPage => Page * PageSize <= TotalCount;
        public bool HasPreviousPage => Page > 1;
        public PageList(List<T> list, int page, int pageSize, int totalCount)
        {
            List = list;
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
    }
}
