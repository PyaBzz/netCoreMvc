using System.Collections.Generic;

namespace myCoreMvc.App
{
    public class Config
    {
        public Db Data { get; set; }
        public Auth Authentication { get; set; }
        public class Db
        {
            public Dictionary<string, string> ConnectionStr { get; set; }
            public Paths Path { get; set; }
            public class Paths
            {
                public Dictionary<string, string> Script { get; set; }
                public Dictionary<string, string> XmlSource { get; set; }
            }
        }
        public class Auth
        {
            public string CookieName { get; set; }
            public int SessionLifeTime { get; set; }
            public bool ShowNavToUnknownUsers { get; set; }
        }
    }
}
