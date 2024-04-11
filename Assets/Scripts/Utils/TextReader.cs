using System;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// This class provides methods for reading and manipulating text from TextAsset files.
/// I've used this to get strings separated by commas from a TextAsset file, by index or randomly.
/// </summary>
public class TextReader
{
    /// <summary>
    /// Returns a specific string from a TextAsset file, given its index.
    /// </summary>
    /// 
    /// <returns>The string at the given index in the TextAsset file.</returns>
    public static string GetStringFromFile(TextAsset file, int index)
    {
        string[] _result = ReadFromFile(file);

        return _result[index];
    }

    /// <summary>
    /// Returns a random string from a TextAsset file.
    /// </summary>
    ///
    /// <returns>A random string from the TextAsset file.</returns>
    public static string GetRandomStringFromFile(TextAsset file)
    {
        string[] _result = ReadFromFile(file);

        return _result[Random.Range(0, _result.Length)];
    }

    /// <summary>
    /// Reads a TextAsset file and returns its contents as an array of strings.
    /// </summary>
    ///     
    /// <returns>An array of strings containing the contents of the TextAsset file.</returns>
    public static string[] ReadFromFile(TextAsset file)
    {
        string _text = file.text;
        string[] _lines = _text.Split(new string[] { "," }, StringSplitOptions.None);

        return _lines;
    }

    /// <summary>
    /// Removes all newline characters and spaces from a string.
    /// </summary>
    /// 
    /// <returns>The trimmed string.</returns>
    public static string TrimString(string stringToTrim)
    {
        string _result = stringToTrim.Replace("\n", "").Replace(" ", "");

        return _result;
    }
}
