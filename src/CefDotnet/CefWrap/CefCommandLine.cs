using CefDotnet.CefApi.Types;
using CefDotnet.CefApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CefDotnet.CefWrap;

unsafe public class CefCommandLine
{
    public static string ToString(cef_command_line_t* p)
    {
        if (p->is_valid(p) != 1)
        {
            return "";
        }

        cef_string_t* str = p->get_command_line_string(p);
        string res = CefString.FromCefString(str);
        libcef.cef_string_userfree_utf16_free(str);

        return res;
    }
}
