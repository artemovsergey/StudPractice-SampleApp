using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SampleApp.RazorPage.TagHelpers
{
    [HtmlTargetElement(Attributes = "if")]
    public class IfTagHelper : TagHelper
    {
        [HtmlAttributeName("if")]
        public bool RenderContent { get; set; } = true;
        public override void Process(
        TagHelperContext context, TagHelperOutput output)
        {
            if (RenderContent == false)
            {
                output.TagName = null;
                output.SuppressOutput();
            }
        }
        public override int Order => int.MinValue;
    }
}
