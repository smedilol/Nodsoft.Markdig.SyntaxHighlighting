using System.Text;
using Markdig.Helpers;
using Markdig.Parsers;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using NSubstitute;

namespace Markdig.SyntaxHighlighting.Core.Tests;

public class SyntaxHighlightingCodeBlockRendererTests
{
	private const string ScriptBlock = @"```csharp
var desktop = Environment.SpecialFolder.DesktopDirectory;
```";

	private static FencedCodeBlock GetFencedCodeBlock(string language = "language-csharp") 
		=> new(new FencedCodeBlockParser())
		{
			Info = language,
			Lines = new(ScriptBlock)
		};

	[Fact]
	public void ConstructorDoesNotThrow()
	{
		SyntaxHighlightingCodeBlockRenderer renderer = new();
		Assert.NotNull(renderer);
	}

	[Fact]
	public void DivWritten()
	{
		CodeBlockRenderer? underlyingRenderer = Substitute.For<CodeBlockRenderer>();

		underlyingRenderer
			.Write(Arg.Any<HtmlRenderer>(), Arg.Any<CodeBlock>());

		SyntaxHighlightingCodeBlockRenderer renderer = new(underlyingRenderer);
		StringBuilder builder = new();
		HtmlRenderer markdownRenderer = new(new StringWriter(builder));
		FencedCodeBlock codeBlock = GetFencedCodeBlock();
		renderer.Write(markdownRenderer, codeBlock);
		Assert.Contains("<div", builder.ToString());
		Assert.Contains("</div>", builder.ToString());
	}

	[Fact]
	public void DivWrittenUnrecognisedLanguage()
	{
		CodeBlockRenderer? underlyingRenderer = Substitute.For<CodeBlockRenderer>();

		underlyingRenderer
			.Write(Arg.Any<HtmlRenderer>(), Arg.Any<CodeBlock>());

		SyntaxHighlightingCodeBlockRenderer renderer = new(underlyingRenderer);
		StringBuilder builder = new();
		HtmlRenderer markdownRenderer = new(new StringWriter(builder));
		FencedCodeBlock codeBlock = GetFencedCodeBlock("language-made-up-language");
		renderer.Write(markdownRenderer, codeBlock);
		Assert.Contains("<div", builder.ToString());
		Assert.Contains("</div>", builder.ToString());
	}

	[Fact]
	public void EditorColorsCssClassAdded()
	{
		CodeBlockRenderer? underlyingRenderer = Substitute.For<CodeBlockRenderer>();

		underlyingRenderer
			.Write(Arg.Any<HtmlRenderer>(), Arg.Any<CodeBlock>());

		SyntaxHighlightingCodeBlockRenderer renderer = new(underlyingRenderer);
		StringBuilder builder = new();
		HtmlRenderer markdownRenderer = new(new StringWriter(builder));
		FencedCodeBlock codeBlock = GetFencedCodeBlock();
		renderer.Write(markdownRenderer, codeBlock);
		Assert.Contains("editor-colors", builder.ToString());
	}

	[Fact]
	public void LangCssClassAdded()
	{
		CodeBlockRenderer? underlyingRenderer = Substitute.For<CodeBlockRenderer>();

		underlyingRenderer
			.Write(Arg.Any<HtmlRenderer>(), Arg.Any<CodeBlock>());

		SyntaxHighlightingCodeBlockRenderer renderer = new(underlyingRenderer);
		StringBuilder builder = new();
		HtmlRenderer markdownRenderer = new(new StringWriter(builder));
		FencedCodeBlock codeBlock = GetFencedCodeBlock();
		renderer.Write(markdownRenderer, codeBlock);
		Assert.Contains("lang-csharp", builder.ToString());
	}

	[Fact]
	public void UnderlyingRendererCalledIfNotFencedCodeBlock()
	{
		CodeBlockRenderer? underlyingRenderer = Substitute.For<CodeBlockRenderer>();
    
		SyntaxHighlightingCodeBlockRenderer renderer = new(underlyingRenderer);
		StringWriter writer = new();
		HtmlRenderer markdownRenderer = new(writer);
		CodeBlock codeBlock = new(new IndentedCodeBlockParser());
		renderer.Write(markdownRenderer, codeBlock);
		
		underlyingRenderer.Received().Write(Arg.Any<HtmlRenderer>(), Arg.Any<CodeBlock>());
	}

	[Fact]
	public void WritesOutCode()
	{
		CodeBlockRenderer? underlyingRenderer = Substitute.For<CodeBlockRenderer>();

		underlyingRenderer
			.Write(Arg.Any<HtmlRenderer>(), Arg.Any<CodeBlock>());

		SyntaxHighlightingCodeBlockRenderer renderer = new(underlyingRenderer);
		StringBuilder builder = new();
		HtmlRenderer markdownRenderer = new(new StringWriter(builder));
		FencedCodeBlock codeBlock = GetFencedCodeBlock();
		renderer.Write(markdownRenderer, codeBlock);
		Assert.Contains("var", builder.ToString());
	}

//	[Fact]
//	public void WritesOutColouredCode()
//	{
//		CodeBlockRenderer? underlyingRenderer = Substitute.For<CodeBlockRenderer>();
//
//		underlyingRenderer
//			.Write(Arg.Any<HtmlRenderer>(), Arg.Any<CodeBlock>());
//
//		SyntaxHighlightingCodeBlockRenderer renderer = new(underlyingRenderer);
//		StringBuilder builder = new();
//		HtmlRenderer markdownRenderer = new(new StringWriter(builder));
//		FencedCodeBlock codeBlock = GetFencedCodeBlock();
//		renderer.Write(markdownRenderer, codeBlock);
//		Assert.Contains(/*lang=html*/@"<span style=""color:Blue;"">var</span>", builder.ToString());
//	}
}