using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace WebCarInspection.Helpers
{
    public class DateTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Content.SetContent($"Current date: {DateTime.Now:dd.MM.yyyy}");
        }
    }
}
