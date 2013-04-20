using System;
using System.Text;
using System.Web.Mvc;
using findbook.WebUI.Models;

namespace findbook.WebUI.HtmlHelpers {
    public static class PagingHelpers {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
        PageInfo pagingInfo,
        Func<int, string> pageUrl) {
            StringBuilder result = new StringBuilder();

            int curr = pagingInfo.CurrentPage;

            //上一页
            TagBuilder formerTag = new TagBuilder("a");
            if (curr > 1) {
                formerTag.MergeAttribute("href", pageUrl(curr - 1));
            }

            if (curr == 1) {
                formerTag.AddCssClass("disable");
            }

            formerTag.InnerHtml = "<<";

            result.Append(formerTag.ToString());

            for (int i = 1; i <= pagingInfo.TotalPages; i++) {
                TagBuilder tag = new TagBuilder("a"); // Construct an <a> tag
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                    tag.AddCssClass("active");
                result.Append(tag.ToString());
            }

            //下一页
            TagBuilder nextTag = new TagBuilder("a");
            if (curr < pagingInfo.TotalPages) {
                nextTag.MergeAttribute("href", pageUrl(curr + 1));
            }

            if (curr == 1) {
                nextTag.AddCssClass("disable");
            }

            nextTag.InnerHtml = ">>";

            result.Append(nextTag.ToString());

            return MvcHtmlString.Create(result.ToString());
        }
    }
}