namespace GrandeNoctuleAPI_Main.Helpers.Exceptions;

public class AppException : Exception
{
    public int ErrorCode { get; set; }

    public AppException(string message, int errorCode, ILogger logger) : base(message)
    {
        logger.LogError($"[{errorCode}] " + message);
        ErrorCode = errorCode;
    }
}