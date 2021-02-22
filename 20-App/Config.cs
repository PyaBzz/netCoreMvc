using System.Collections.Generic;

namespace myCoreMvc.App
{
    public class Config
    {
        public Db Database { get; set; }

        public Auth Authentication { get; set; }

        public class Db
        {
            public Dictionary<string, string> ConnectionStr { get; set; }

            public Dictionary<string, string> ScriptPath { get; set; }
        }

        public class Auth
        {
            public string CookieName { get; set; }

            public int SessionLifeTime { get; set; }

            public bool ShowNavToUnknownUsers { get; set; }
        }
    }
}
