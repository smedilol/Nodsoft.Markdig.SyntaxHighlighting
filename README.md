[![GitHub issues](https://img.shields.io/github/issues/Nodsoft/Markdig.SyntaxHighlighting.Core.svg?style=flat-square)](https://github.com/Nodsoft/Markdig.SyntaxHighlighting.Core/issues)
[![GitHub forks](https://img.shields.io/github/forks/Nodsoft/Markdig.SyntaxHighlighting.Core.svg?style=flat-square)](https://github.com/Nodsoft/Markdig.SyntaxHighlighting.Core/network)
[![GitHub stars](https://img.shields.io/github/stars/Nodsoft/Markdig.SyntaxHighlighting.Core.svg?style=flat-square)](https://github.com/Nodsoft/Markdig.SyntaxHighlighting.Core/stargazers)
[![GitHub license](https://img.shields.io/github/license/Nodsoft/Markdig.SyntaxHighlighting.Core.svg?style=flat-square)](https://raw.githubusercontent.com/Nodsoft/Markdig.SyntaxHighlighting.Core/master/LICENSE.md)
[![NuGet](https://img.shields.io/nuget/dt/Markdig.SyntaxHighlighting.Core.svg?style=flat-square)](https://www.nuget.org/packages/Markdig.SyntaxHighlighting.Core/)
[![NuGet](https://img.shields.io/nuget/v/Markdig.SyntaxHighlighting.Core.svg?style=flat-square)](https://www.nuget.org/packages/Markdig.SyntaxHighlighting.Core/)

# Markdig.SyntaxHighlighting.Core 
### Syntax Highlighting extension for Markdig
An extension that adds Syntax Highlighting, also known as code colourization, to a [Markdig](xoofx/markdig) pipeline through the power of [ColorCode](CommunityToolkit/ColorCode-Universal). By simply adding this extension to your pipeline, you can add colour and style to your source code:
## Demonstration
### Before
```
namespace Amido.VersionDashboard.Web.Domain 
{
    public interface IConfigProvider 
    {
        string GetSetting(string appSetting);
    }
}
```

### After
```csharp
namespace Amido.VersionDashboard.Web.Domain 
{
    public interface IConfigProvider 
    {
        string GetSetting(string appSetting);
    }
}
```
## Usage 
Simply import the nuget package, add a using statement for `Markdig.SyntaxHighlighting` and add to your pipeline:
```csharp
var pipeline = new MarkdownPipelineBuilder()
    .UseAdvancedExtensions()
    .UseSyntaxHighlighting()
    .Build();
```