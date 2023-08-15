using ColorCode;

namespace Nodsoft.Markdig.SyntaxHighlighting.Tests;

public class LanguageTypeAdapterTests {
    private const string AspxCsFirstLine = "<%@ Page Language=\"C#\" %>";

    [Theory]
    [InlineData("csharp", "c#", null)]
    [InlineData("cplusplus", "cpp", null)]
    [InlineData("css", "css", null)]
    [InlineData("aspx", "aspx(c#)", AspxCsFirstLine)]
    [InlineData("javascript", "javascript", "var myVar = 1;")]
    public void CanParse(string inputLanguage, string expectedId, string firstLine) {
        LanguageTypeAdapter adapter = new();
        ILanguage? result = adapter.Parse(inputLanguage, firstLine);
        Assert.Equal(expectedId, result?.Id);
    }

    [Theory]
//    [InlineData(null)]
    [InlineData("fubar")]
    public void CanNotParse(string inputLanguage) {
        LanguageTypeAdapter adapter = new();
        ILanguage? result = adapter.Parse(inputLanguage);
        Assert.Null(result);
    }
}