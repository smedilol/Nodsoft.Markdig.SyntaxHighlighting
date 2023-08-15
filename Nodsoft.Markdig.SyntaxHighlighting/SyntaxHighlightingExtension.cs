using ColorCode.Styling;
using Markdig;
using Markdig.Renderers;
using Markdig.Renderers.Html;

namespace Nodsoft.Markdig.SyntaxHighlighting;

public class SyntaxHighlightingExtension : IMarkdownExtension
{
	private readonly StyleDictionary? _customCss;

	public SyntaxHighlightingExtension(StyleDictionary? customCss = null)
	{
		_customCss = customCss;
	}

	public void Setup(MarkdownPipelineBuilder pipeline) { }

	public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer? renderer)
	{
		ArgumentNullException.ThrowIfNull(renderer);

		if (renderer is not TextRendererBase<HtmlRenderer> htmlRenderer)
		{
			return;
		}

		CodeBlockRenderer? originalCodeBlockRenderer = htmlRenderer.ObjectRenderers.FindExact<CodeBlockRenderer>();

		if (originalCodeBlockRenderer is not null)
		{
			htmlRenderer.ObjectRenderers.Remove(originalCodeBlockRenderer);
		}

		htmlRenderer.ObjectRenderers.AddIfNotAlready(new SyntaxHighlightingCodeBlockRenderer(originalCodeBlockRenderer, _customCss));
	}
}