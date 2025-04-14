using CefDotnet.CefApi;
using CefDotnet.CefApi.Types;

namespace CefDotnet.CefWrap;

unsafe public class CefString
{
    public static string FromCefString(cef_string_t* str)
    {
        if (str == null)
        {
            return "";
        }

        return new string(str->str, 0, (int)str->length);
    }

    public static string[] FromCefStringList(nint list)
    {
        var size = libcef.cef_string_list_size(list);
        string[] res = new string[size];
        cef_string_t str;
        for (nuint i = 0; i < size; ++i)
        {
            libcef.cef_string_list_value(list, i, &str);
            res[i] = FromCefString(&str);
        }

        return res;
    }    
}
