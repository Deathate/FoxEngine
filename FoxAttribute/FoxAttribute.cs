
using System;

namespace FoxEngine
{
    //https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/attributes/general

    [AttributeUsage(AttributeTargets.Class)]
    public class GlobalAttribute : Attribute
    {
        public int index = -1;
    }
}

