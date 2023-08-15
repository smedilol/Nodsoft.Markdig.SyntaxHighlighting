using System.Text;
using ColorCode;
using ColorCode.Styling;
using Markdig.Helpers;
using Markdig.Parsers;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace Nodsoft.Markdig.SyntaxHighlighting;

public class SyntaxHighlightingCodeBlockRenderer : HtmlObjectRenderer<CodeBlock>
{
	private readonly CodeBlockRenderer _underlyingRenderer;
	private readonly StyleDictionary? _customCss;

	public SyntaxHighlightingCodeBlockRenderer(CodeBlockRenderer? underlyingRenderer = null, StyleDictionary? customCss = null)
	{
		_underlyingRenderer = underlyingRenderer ?? new CodeBlockRenderer();
		_customCss = customCss;
	}

	protected override void Write(HtmlRenderer renderer, CodeBlock obj)
	{
		if (obj is not FencedCodeBlock { Parser: FencedCodeBlockParser { InfoPrefix: { } infoPrefix } } fencedCodeBlock)
		{
			_underlyingRenderer.Write(renderer, obj);
			return;
		}

		HtmlAttributes attributes = obj.TryGetAttributes() ?? new HtmlAttributes();

		string? languageMoniker = fencedCodeBlock.Info?.Replace(infoPrefix, string.Empty);

		if (string.IsNullOrEmpty(languageMoniker))
		{
			_underlyingRenderer.Write(renderer, obj);
			return;
		}

		attributes.AddClass($"lang-{languageMoniker}");
		attributes.Classes?.Remove($"language-{languageMoniker}");
		attributes.AddClass("editor-colors");

		string code = GetCode(obj, out string? firstLine);

		renderer
			.Write("<div")
			.WriteAttributes(attributes)
			.Write(">");

		string markup = ApplySyntaxHighlighting(languageMoniker, firstLine ?? "", code);

		renderer.WriteLine(markup);
		renderer.WriteLine("</div>");
	}

	private string ApplySyntaxHighlighting(string languageMoniker, string firstLine, string code)
	{
		LanguageTypeAdapter languageTypeAdapter = new();

		if (languageTypeAdapter.Parse(languageMoniker, firstLine) is not { } language)
		{
			// handle unrecognised language formats, e.g. when using mermaid diagrams
            return /*lang=html*/$@"<div class=""{languageMoniker}""><pre>{code}</pre></div>";
		}
        
		StyleDictionary? styleSheet = _customCss ?? StyleDictionary.DefaultDark;
		HtmlClassFormatter formatter = new(styleSheet);
		string colourizedCode = formatter.GetHtmlString(code, language);
		return colourizedCode;
	}

	private static string GetCode(LeafBlock obj, out string? firstLine)
	{
		StringBuilder code = new();
		firstLine = null;

		foreach (StringLine line in obj.Lines.Lines)
		{
			StringSlice slice = line.Slice;

			if (slice.Text is null)
			{
				continue;
			}

			string lineText = slice.Text.Substring(slice.Start, slice.Length);

			if (firstLine is null)
			{
				firstLine = lineText;
			} 
			else
			{
				code.AppendLine();
			}

			code.Append(lineText);
		}

		return code.ToString();
	}
}