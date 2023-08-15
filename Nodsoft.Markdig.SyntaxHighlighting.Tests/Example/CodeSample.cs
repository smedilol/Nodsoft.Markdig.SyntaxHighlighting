using System.Reflection;

namespace Nodsoft.Markdig.SyntaxHighlighting.Tests.Example;

public class CodeSample
{
//	[Fact]
//	public void CodeSampleWorks()
//	{
//		string? codebase = Assembly.GetExecutingAssembly().GetName().CodeBase;
//		string? directory = Path.GetDirectoryName(codebase);
//
//		if (directory is null)
//		{
//			throw new NullReferenceException("appPath came back null.");
//		}
//
//		string appPath = new Uri(directory).LocalPath;
//		string folder = Path.Combine(appPath, "Example");
//		string inputMarkdown = Path.Combine(folder, "README.md");
//		string referenceFile = Path.Combine(folder, "expected.html");
//		string expectedHtml = File.ReadAllText(referenceFile);
//		string markdown = File.ReadAllText(inputMarkdown);
//
//		MarkdownPipeline pipeline = new MarkdownPipelineBuilder()
//			.UseAdvancedExtensions()
//			.UseSyntaxHighlighting()
//			.Build();
//
//		string html = Markdown.ToHtml(markdown, pipeline);
//
//		string actualHtml = File.ReadAllText(Path.Combine(folder, "_template.html"))
//			.Replace("{{{this}}}", html);
//
//		actualHtml = actualHtml.Replace("\r\n", "\n").Replace("\n", "\r\n");
//		expectedHtml = expectedHtml.Replace("\r\n", "\n").Replace("\n", "\r\n");
//		File.WriteAllText(Path.Combine(folder, "actual.html"), actualHtml);
//		Assert.Equal(expectedHtml, actualHtml);
//	}
}