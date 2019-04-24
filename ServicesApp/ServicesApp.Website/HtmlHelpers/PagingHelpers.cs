using ServicesApp.ViewModels.ViewModels;
using System;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ServicesApp.Website.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PageInfoViewModel pageInfo, Func<int, string> pageUrl, string search=null)
        {
            StringBuilder result = new StringBuilder();            
            for (int count = 1; count <= pageInfo.TotalPages; count++)
            {
                TagBuilder ahref = new TagBuilder("a");
                string fullUrl = pageUrl(count);
                if (!String.IsNullOrWhiteSpace(search))
                {
                    fullUrl = $"{fullUrl}&Search={search}";
                }
                ahref.MergeAttribute("href", fullUrl);
                ahref.InnerHtml = count.ToString();
                
                TagBuilder li = new TagBuilder("li");
                li.InnerHtml = ahref.ToString();
                if (count == pageInfo.PageNumber)
                {
                    li.AddCssClass("active");
                }
                result.Append(li.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}