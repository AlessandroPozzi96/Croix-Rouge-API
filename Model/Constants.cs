using System;
namespace CroixRouge.Model
{
    public class Constants
    {
        public static class Roles
        {
            public const string Admin = "ADMIN";
            public const string User = "USER";
            public const string SuperUser = "SUPERUSER";
        }

        public static class Paging
        {
            public const int PAGE_SIZE = 20;
            public const int PAGE_INDEX = 0;
        }
    }
}
