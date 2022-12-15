namespace GeoPet.Helpers;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

// custom exception class for throwing application specific exceptions (e.g. for validation) 
// that can be caught and handled within the application
[ExcludeFromCodeCoverage]
public class InvalidException : Exception
{
    public InvalidException() : base() { }

    public InvalidException(string message) : base(message) { }

    public InvalidException(string message, params object[] args)
        : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}