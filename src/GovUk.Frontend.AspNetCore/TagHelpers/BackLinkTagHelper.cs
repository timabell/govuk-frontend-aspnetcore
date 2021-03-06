﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUk.Frontend.AspNetCore.TagHelpers
{
    [HtmlTargetElement("govuk-back-link", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class BackLinkTagHelper : LinkTagHelperBase
    {
        public BackLinkTagHelper(IGovUkHtmlGenerator htmlGenerator, IUrlHelperFactory urlHelperFactory)
            : base(htmlGenerator, urlHelperFactory)
        {
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            IHtmlContent content = new HtmlString("Back");

            if (output.TagMode == TagMode.StartTagAndEndTag)
            {
                content = await output.GetChildContentAsync();
            }

            var href = ResolveHref();

            var tagBuilder = Generator.GenerateBackLink(href, content, output.Attributes.ToAttributesDictionary());

            output.TagName = tagBuilder.TagName;
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.Clear();
            output.MergeAttributes(tagBuilder);
            output.Content.SetHtmlContent(tagBuilder.InnerHtml);
        }
    }
}