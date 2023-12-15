using System;
using System.IO;
using Markdig;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Xunit;

namespace Nodsoft.Markdig.SyntaxHighlighting.Tests
{

    public class SyntaxHighlightingExtensionsTests
    {
        private class FakeRenderer : TextRendererBase<FakeRenderer>
        {
            public FakeRenderer(TextWriter writer) : base(writer)
            {
            }
        }

        [Fact]
        public void CodeBlockRendererReplaced()
        {
            var extension = new SyntaxHighlightingExtension();
            var writer = new StringWriter();
            var markdownRenderer = new HtmlRenderer(writer);

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
            var extension = new SyntaxHighlightingExtension();
            extension.Setup(new MarkdownPipelineBuilder());
        }

        [Fact]
        public void PipelineChangedIfHtmlRenderer()
        {
            var extension = new SyntaxHighlightingExtension();
            var writer = new StringWriter();
            var markdownRenderer = new HtmlRenderer(writer);
            markdownRenderer.ObjectRenderers.RemoveAll(x => true);
            extension.Setup(null, markdownRenderer);
            Assert.Single(markdownRenderer.ObjectRenderers);
        }

        [Fact]
        public void PipelineChangedIfHtmlRendererUsingExtensionMethod()
        {
            var pipelineBuilder = new MarkdownPipelineBuilder();
            pipelineBuilder.UseSyntaxHighlighting();
            MarkdownPipeline pipeline = pipelineBuilder.Build();
            var writer = new StringWriter();
            var markdownRenderer = new HtmlRenderer(writer);
            pipeline.Setup(markdownRenderer);
            SyntaxHighlightingCodeBlockRenderer renderer = markdownRenderer.ObjectRenderers.FindExact<SyntaxHighlightingCodeBlockRenderer>();
            Assert.NotNull(renderer);
        }

        [Fact]
        public void PipelineIntactIfNotHtmlRenderer()
        {
            var extension = new SyntaxHighlightingExtension();
            var writer = new StringWriter();
            var markdownRenderer = new FakeRenderer(writer);
            int oldRendererCount = markdownRenderer.ObjectRenderers.Count;
            extension.Setup(null, markdownRenderer);
            Assert.Equal(oldRendererCount, markdownRenderer.ObjectRenderers.Count);
        }

        [Fact]
        public void ThrowsIfRendererIsNull()
        {
            var extension = new SyntaxHighlightingExtension();
            Assert.Throws<ArgumentNullException>((Action)_ExtensionSetup);
            return;

            void _ExtensionSetup() => extension.Setup(null, null);
        }
    }
}