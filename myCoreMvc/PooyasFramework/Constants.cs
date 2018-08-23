namespace PooyasFramework
{
    public enum Injection { Singleton, Transient };

    public enum TransactionResult { NotFound, Added, Updated, Deleted }

    public static class AuthConstants
    {
        public const string SchemeName = "Cookies";

        public const string Level0PolicyName = "Level0AndAbove";
        public const string Level1PolicyName = "Level1AndAbove";
        public const string Level2PolicyName = "Level2AndAbove";
        public const string Level3PolicyName = "Level3AndAbove";
    }
}
