using System;

namespace Helper
{
    public static class Generator
    {
        public static string GetNextId()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString().Substring(0,8);
        }
    }
}
