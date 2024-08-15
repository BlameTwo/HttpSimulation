using System.Collections.ObjectModel;

namespace HttpSimulation.Models.Enums;

public enum HttpBodyType : uint
{
    None = 1,
    FormData = 2,
    UrlEncoded = 3,
    Json = 4,
    Xml = 5,
    Raw = 6
}

public static class HttpBodyTypeHelper
{
    public static string GetContentType(HttpBodyType type)
    {
        switch (type)
        {
            case HttpBodyType.None:
                break;
            case HttpBodyType.FormData:
                return "multipart/form-data";
            case HttpBodyType.UrlEncoded:
                return "application/x-www-form-urlencoded";
            case HttpBodyType.Json:
                return "application/json";
            case HttpBodyType.Xml:
                return "application/xml";
            case HttpBodyType.Raw:
                return "text/plain";
            default:
                break;
        }
        return "raw";
    }

    public static string GetDisplay(HttpBodyType type)
    {
        switch (type)
        {
            case HttpBodyType.None:
                break;
            case HttpBodyType.FormData:
                return "form-data";
            case HttpBodyType.UrlEncoded:
                return "x-www-form-urlencoded";
            case HttpBodyType.Json:
                return "json";
            case HttpBodyType.Xml:
                return "xml";
            case HttpBodyType.Raw:
                return "raw";
            default:
                break;
        }
        return "raw";
    }

    public static HttpBodyType FromString(string contentType)
    {
        switch (contentType.ToLower())
        {
            case "form-data":
                return HttpBodyType.FormData;
            case "x-www-form-urlencoded":
                return HttpBodyType.UrlEncoded;
            case "json":
                return HttpBodyType.Json;
            case "xml":
                return HttpBodyType.Xml;
            case "plain":
                return HttpBodyType.Raw;
            default:
                return HttpBodyType.Raw;
        }
    }

    public static ObservableCollection<string> GetContentTypes()
    {
        return new ObservableCollection<string>()
        {
            "form-data",
            "x-www-form-urlencoded",
            "json",
            "xml",
            "raw"
        };
    }
}
