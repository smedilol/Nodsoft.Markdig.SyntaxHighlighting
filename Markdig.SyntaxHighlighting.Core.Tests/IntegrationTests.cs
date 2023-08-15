namespace Markdig.SyntaxHighlighting.Core.Tests;

public class IntegrationTests
{

    [Fact]
    public void ShouldUseDefaultRendererIfLanguageIsNotIndicated() {
        const string testString = @"
# This is a test

```
{
    ""jsonProperty"": 1
}
```";
        MarkdownPipeline pipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .UseSyntaxHighlighting()
            .Build();
        string html = Markdown.ToHtml(testString, pipeline);
        Assert.Contains("<pre><code>", html);
        Assert.Contains("jsonProperty", html);
        Assert.DoesNotContain("lang-", html);
    }

    [Fact]
    public void ShouldColorizeSyntaxWhenLanguageIsIndicated()
    {
        const string testString = @"
# This is a test

```json
{
    ""jsonProperty"": 1
}
```";
        MarkdownPipeline pipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .UseSyntaxHighlighting()
            .Build();
        string html = Markdown.ToHtml(testString, pipeline);
        Assert.Contains("<div", html);
        Assert.Contains("jsonProperty", html);
        Assert.Contains("lang-", html);
    }

}