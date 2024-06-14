using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BethanysPieShop.TagHelpers
{
    public class EmailTagHelper: TagHelper // <email is how we will access this code
    {
        public string? Address { get; set; }
        public string? Content { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output) // this is to override by convention
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", "mailto:" + Address);
            output.Content.SetContent(Content);
        }

    }
}
