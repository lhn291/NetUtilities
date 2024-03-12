using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace NetUtilities.Domain.Extensions
{
    public static class StringExtensions
    {
        public static string CutString(this string str, int startIndex, int length)
        {
            if (str == null)
            {
                return string.Empty;
            }

            if (startIndex >= str.Length)
            {
                return string.Empty;
            }

            int cutLength = length;

            if (startIndex + cutLength > str.Length)
            {
                cutLength = str.Length - startIndex;
            }

            return str.Substring(startIndex, cutLength);
        }

        public static string[] validImageExtensions = { ".png", ".jpg", ".gif" };

        public static bool IsFileImageValid(this string filePath)
        {
            string fileExtension = Path.GetExtension(filePath).ToLower();
            return validImageExtensions.Contains(fileExtension);
        }

        public static bool IsTextFile(this string filePath)
        {
            string extension = Path.GetExtension(filePath);
            return (extension.Equals(".log", StringComparison.CurrentCulture) || extension.Equals(".txt", StringComparison.CurrentCulture))
                && File.Exists(filePath);
        }

        public static bool IsJSON(this string text)
        {
            try
            {
                var obj = JsonConvert.DeserializeObject(text);
                return true;
            }
            catch (JsonReaderException)
            {
                return false;
            }
        }

        public static bool TryConvertToJSON(this string input, out string result)
        {
            try
            {
                int separatorIndex = input.IndexOf("{");

                if (separatorIndex >= 0)
                {
                    string nonConvertedSegment = input.Substring(0, separatorIndex).Trim();
                    string convertedSegment = input.Substring(separatorIndex).Trim();
                    int jsonEndIndex = convertedSegment.LastIndexOf("}");

                    if (jsonEndIndex >= 0)
                    {
                        string jsonPart = convertedSegment.Substring(0, jsonEndIndex + 1);
                        string nonJsonPart = convertedSegment.Substring(jsonEndIndex + 1).Trim();

                        try
                        {
                            dynamic parsedJson = JsonConvert.DeserializeObject(jsonPart);
                            string formattedJson = JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
                            convertedSegment = $"{formattedJson}\n{nonJsonPart}";
                        }
                        catch (Exception)
                        {
                            result = input;
                            return false;
                        }
                    }
                    else
                    {
                        result = input;
                        return false;
                    }

                    result = $"{nonConvertedSegment}\n{convertedSegment}";
                    return true;
                }
                else
                {
                    result = input;
                    return false;
                }
            }
            catch (Exception)
            {
                result = input;
                return false;
            }
        }
        public static string ExtendString(this string input, out bool isExtended)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                isExtended = false;
                return input;
            }

            string[] lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.TrimEntries);

            if (lines.Length > 1)
            {
                isExtended = true;
                return lines[0].Trim().Substring(0, Math.Min(lines[0].Trim().Length, 89)) + " ...";
            }
            else if (lines.Length == 1)
            {
                string firstLine = lines[0].Trim();
                if (firstLine.Length > 50)
                {
                    isExtended = true;
                    return lines[0].Trim().Substring(0, Math.Min(lines[0].Trim().Length, 89)) + " ...";
                }
                else
                {
                    isExtended = false;
                    return firstLine;
                }
            }
            else
            {
                isExtended = false;
                return input;
            }
        }

        public static bool TabJson(this string json, out string result, int tabSpace)
        {
            try
            {
                string[] lines = json.Split('\n');
                StringBuilder indentedJson = new StringBuilder();
                int indentationLevel = 0;

                foreach (string line in lines)
                {
                    if (line.Trim().StartsWith("}") || line.Trim().StartsWith("]"))
                    {
                        indentationLevel--;
                    }

                    indentedJson.Append(new string(' ', indentationLevel * tabSpace));
                    indentedJson.AppendLine(line.Trim());

                    if (line.Trim().EndsWith("{") || line.Trim().EndsWith("["))
                    {
                        indentationLevel++;
                    }
                }
                result = indentedJson.ToString();
                return true;
            }
            catch (JsonReaderException)
            {
                result = json;
                return false;
            }
        }

    }
}

