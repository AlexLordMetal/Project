using System;

namespace ServicesApp.ViewModels.ViewModels
{
    public class PageInfoViewModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }
        public int SkipItems
        {
            get { return (PageNumber - 1) * PageSize ; }
        }
    }
}