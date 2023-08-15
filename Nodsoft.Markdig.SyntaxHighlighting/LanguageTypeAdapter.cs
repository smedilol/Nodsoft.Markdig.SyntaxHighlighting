using System.Text.RegularExpressions;
using ColorCode;

namespace Nodsoft.Markdig.SyntaxHighlighting;

public class LanguageTypeAdapter
{
	private readonly Dictionary<string, ILanguage> _languageMap = new()
	{
		{ "csharp", Languages.CSharp },
		{ "cplusplus", Languages.Cpp }
	};

	public ILanguage? Parse(string id, string? firstLine = null)
	{
		if (_languageMap.TryGetValue(id, out ILanguage? value))
		{
			return value;
		}

		if (!string.IsNullOrWhiteSpace(firstLine))
		{
			foreach (ILanguage? lang in Languages.All)
			{
				if (lang.FirstLinePattern is null)
				{
					continue;
				}

				Regex firstLineMatcher = new(lang.FirstLinePattern, RegexOptions.IgnoreCase);

				if (firstLineMatcher.IsMatch(firstLine))
				{
					return lang;
				}
			}
		}

		ILanguage? byIdCanidate = Languages.FindById(id);

		return byIdCanidate;
	}
}