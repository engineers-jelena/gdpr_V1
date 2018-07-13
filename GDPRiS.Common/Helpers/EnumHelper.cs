using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GDPRiS.Common.Helpers
{
    public static class EnumHelper
    {
        public static string GetDescription(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                        as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static List<KeyValuePair<string, string>> GetListOfDescription<T>() where T : struct
        {
            Type t = typeof(T);

            if (!t.IsEnum)
                return null;

            Array values = System.Enum.GetValues(t);
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>(values.Length);

            foreach (var value in values)
            {
                string description = "";
                string name = Enum.GetName(t, value);
                if (name != null)
                {
                    FieldInfo field = t.GetField(name);
                    if (field != null)
                    {
                        DescriptionAttribute attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

                        if (attr != null)
                        {
                            description = attr.Description;

                        }
                    }
                }

                result.Add(new KeyValuePair<string, string>(name, string.IsNullOrEmpty(description) ? name : description));
            }

            return result;
        }
    }
}
