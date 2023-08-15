[![GitHub issues](https://img.shields.io/github/issues/Nodsoft/Nodsoft.Markdig.SyntaxHighlighting.svg?style=flat-square)](https://github.com/Nodsoft/Nodsoft.Markdig.SyntaxHighlighting/issues)
[![GitHub forks](https://img.shields.io/github/forks/Nodsoft/Nodsoft.Markdig.SyntaxHighlighting.svg?style=flat-square)](https://github.com/Nodsoft/Nodsoft.Markdig.SyntaxHighlighting/network)
[![GitHub stars](https://img.shields.io/github/stars/Nodsoft/Nodsoft.Markdig.SyntaxHighlighting.svg?style=flat-square)](https://github.com/Nodsoft/Nodsoft.Markdig.SyntaxHighlighting/stargazers)
[![GitHub license](https://img.shields.io/github/license/Nodsoft/Nodsoft.Markdig.SyntaxHighlighting.svg?style=flat-square)](https://raw.githubusercontent.com/Nodsoft/Nodsoft.Markdig.SyntaxHighlighting/master/LICENSE.md)
[![NuGet](https://img.shields.io/nuget/dt/Nodsoft.Markdig.SyntaxHighlighting.svg?style=flat-square)](https://www.nuget.org/packages/Nodsoft.Markdig.SyntaxHighlighting/)
[![NuGet](https://img.shields.io/nuget/v/Nodsoft.Markdig.SyntaxHighlighting.svg?style=flat-square)](https://www.nuget.org/packages/Nodsoft.Markdig.SyntaxHighlighting/)

# Nodsoft.Markdig.SyntaxHighlighting
### Syntax Highlighting extension for Markdig
An extension that adds Syntax Highlighting, also known as code colourization, to a [Markdig](xoofx/markdig) pipeline through the power of [ColorCode](CommunityToolkit/ColorCode-Universal). By simply adding this extension to your pipeline, you can add colour and style to your source code in markdown files.

#### PSA
This project is a fork of Richard Slater's [Markdig.SyntaxHighlighting](https://github.com/RichardSlater/Markdig.SyntaxHighlighting). It has been ported to .NET 6.0 by our team, and will continue to be supported for the foreseeable future as we use this lib for our downstream projects.

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