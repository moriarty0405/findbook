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

        //创建Ajax链接
        private static TagBuilder CreateAjaxLinkTag(string innerHtml, int page, Func<int, string> pageUrl, string updateTargetId, string className = (string)null, string id = (string)null) {
             TagBuilder tag = new TagBuilder("a");
             tag.MergeAttribute("href", pageUrl(page));
             tag.MergeAttribute("data-ajax-mode", "replace");
             tag.MergeAttribute("data-ajax", "true");
             tag.MergeAttribute("data-ajax-update", "#" + updateTargetId);
             if(!String.IsNullOrEmpty(className))
                 tag.AddCssClass(className);
             if (!String.IsNullOrEmpty(id))
                 tag.MergeAttribute("id", id);
             tag.InnerHtml = innerHtml;
             return tag;
         }
    }

    //public static class PagingHelpers {
    //     <summary>
    //     生成Unobtrusive Ajax action link分页
    //     </summary>
    //     <param name="html"></param>
    //     <param name="pageInfo">分页信息</param>
    //     <param name="pageUrl">生成链接委托</param>
    //     <param name="leastCountToGenerateFirstLast">页面数量超过该数则生成“首页”“尾页”</param>
    //     <param name="updateTargetId">回调更新的页面元素id</param>
    //     <returns></returns>
    //    public static MvcHtmlString AjaxPageLinks(this HtmlHelper html, PageInfo pagingInfo, Func<int, string> pageUrl, int leastCountToGenerateFirstLast, string updateTargetId) {
    //        StringBuilder result = new StringBuilder();

    //        for (int i = 1; i <= pagingInfo.TotalPages; i++) {
    //            if (i == 1 && pagingInfo.TotalPages >= leastCountToGenerateFirstLast) {
    //                result.Append(CreateAjaxLinkTag("首页", i, pageUrl, updateTargetId, null, "pagination-first").ToString());
    //                result.Append(@"<a id=""pagination-pre""><<</a>");
    //            }

    //            if (i == 1) {
    //                result.Append(@"<div id=""pagination-div-container"">");
    //                result.Append(@"<ul id=""pagination-ul-container"">");
    //            }

    //            result.Append("<li>");
    //            result.Append(CreateAjaxLinkTag(i.ToString(), i, pageUrl, updateTargetId).ToString());
    //            result.Append("</li>");

    //            if (i == pagingInfo.TotalPages) {
    //                result.Append("</ul>");
    //                result.Append("</div>");
    //            }

    //            if (pagingInfo.TotalPages >= leastCountToGenerateFirstLast && i == pagingInfo.TotalPages) {
    //                result.Append(@"<a id=""pagination-next"">>></a>");
    //                result.Append(CreateAjaxLinkTag("末页", i, pageUrl, updateTargetId, null, "pagination-last").ToString());
    //            }
    //        }

    //        return MvcHtmlString.Create(result.ToString());
    //    }

    //    private static TagBuilder CreateAjaxLinkTag(string innerHtml, int page, Func<int, string> pageUrl, string updateTargetId, string className = (string)null, string id = (string)null) {
    //        TagBuilder tag = new TagBuilder("a");
    //        tag.MergeAttribute("href", pageUrl(page));
    //        tag.MergeAttribute("data-ajax-mode", "replace");
    //        tag.MergeAttribute("data-ajax", "true");
    //        tag.MergeAttribute("data-ajax-update", "#" + updateTargetId);
    //        if (!String.IsNullOrEmpty(className))
    //            tag.AddCssClass(className);
    //        if (!String.IsNullOrEmpty(id))
    //            tag.MergeAttribute("id", id);
    //        tag.InnerHtml = innerHtml;
    //        return tag;
    //    }
}