using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Blog.Domain.Enumerations
{
    public abstract class Enumeration
    {
        public string Description { get; private set; }

        public int Id { get; private set; }

        protected Enumeration(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public override string ToString() => Description;

        public static bool Exists<T>(string description) where T : Enumeration
        {
            var enumeration = GetAll<T>().FirstOrDefault(x => x.Description.ToLower().Equals(description?.ToLower()));
            return enumeration != null;
        }

        private static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public static T FindByDescription<T>(string description) where T : Enumeration
        {
            var enumeration = GetAll<T>().FirstOrDefault(x => x.Description.ToLower() == description.ToLower());
            if (enumeration is null) return GetAll<T>().FirstOrDefault();
            return enumeration;
        }
    }
}
