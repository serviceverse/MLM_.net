using System.Collections.Generic;

namespace MLM.Data
{
    public static class AppPermissions
    {
        // Configure your Modules and their available Actions here
        public static readonly Dictionary<string, List<string>> ConfiguredModules = new()
        {
            { "Users", new List<string> { "Add", "Edit", "Delete", "Get" } },
            { "Roles", new List<string> { "Add", "Edit", "Delete", "Get" } },
            { "AppModules", new List<string> { "Get" } },
            { "AppActions", new List<string> { "Get" } }
        };
    }
}
