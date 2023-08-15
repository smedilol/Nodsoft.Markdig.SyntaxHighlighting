using Markdig;
using Markdig.Renderers;
using Markdig.Renderers.Html;

namespace Nodsoft.Markdig.SyntaxHighlighting.Tests;

public class SyntaxHighlightingExtensionsTests
{
	private class FakeRenderer : TextRendererBase<FakeRenderer>
	{
		public FakeRenderer(TextWriter writer) : base(writer) { }
	}

	[Fact]
	public void CodeBlockRendererReplaced()
	{
		SyntaxHighlightingExtension extension = new();
		StringWriter writer = new();
		HtmlRenderer markdownRenderer = new(writer);

		int oldRendererCount = markdownRenderer.ObjectRenderers.Count;

		Assert.Single(markdownRenderer.ObjectRenderers.FindAll(x => x.GetType() == typeof(CodeBlockRenderer)));

		extension.Setup(null, markdownRenderer);

		Assert.Empty(markdownRenderer.ObjectRenderers.FindAll(x => x.GetType() == typeof(CodeBlockRenderer)));
		Assert.Single(markdownRenderer.ObjectRenderers.FindAll(x => x.GetType() == typeof(SyntaxHighlightingCodeBlockRenderer))
);

		Assert.Equal(oldRendererCount, markdownRenderer.ObjectRenderers.Count);
	}

	[Fact]
	public void DoesntThrowWhenSetupPipeline()
	{
		SyntaxHighlightingExtension extension = new();
		extension.Setup(new());
	}

	[Fact]
	public void PipelineChangedIfHtmlRenderer()
	{
		SyntaxHighlightingExtension extension = new();
		StringWriter writer = new();
		HtmlRenderer markdownRenderer = new(writer);
		markdownRenderer.ObjectRenderers.RemoveAll(x => true);
		extension.Setup(null, markdownRenderer);
		Assert.Single(markdownRenderer.ObjectRenderers);
	}

	[Fact]
	public void PipelineChangedIfHtmlRendererUsingExtensionMethod()
	{
		MarkdownPipelineBuilder pipelineBuilder = new();
		pipelineBuilder.UseSyntaxHighlighting();
		MarkdownPipeline pipeline = pipelineBuilder.Build();
		StringWriter writer = new();
		HtmlRenderer markdownRenderer = new(writer);
		pipeline.Setup(markdownRenderer);
		SyntaxHighlightingCodeBlockRenderer? renderer = markdownRenderer.ObjectRenderers.FindExact<SyntaxHighlightingCodeBlockRenderer>();
		Assert.NotNull(renderer);
	}

	[Fact]
	public void PipelineIntactIfNotHtmlRenderer()
	{
		SyntaxHighlightingExtension extension = new();
		StringWriter writer = new();
		FakeRenderer markdownRenderer = new(writer);
		int oldRendererCount = markdownRenderer.ObjectRenderers.Count;
		extension.Setup(null, markdownRenderer);
		Assert.Equal(oldRendererCount, markdownRenderer.ObjectRenderers.Count);
	}

	[Fact]
	public void ThrowsIfRendererIsNull()
	{
		SyntaxHighlightingExtension extension = new();
		Assert.Throws<ArgumentNullException>((Action)_ExtensionSetup);
		return;

		void _ExtensionSetup() => extension.Setup(null!, null!);
	}
}