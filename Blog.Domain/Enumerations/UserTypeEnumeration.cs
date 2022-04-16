namespace Blog.Domain.Enumerations
{
    public class UserTypeEnumeration : Enumeration
    {
        public static UserTypeEnumeration Administrator = new UserTypeEnumeration(1, "Administrator");
        public static UserTypeEnumeration Writer = new UserTypeEnumeration(2, "Writer");
        public static UserTypeEnumeration Reader = new UserTypeEnumeration(3, "Reader");

        public UserTypeEnumeration(int id, string description)
        : base(id, description)
        { }
    }
}
