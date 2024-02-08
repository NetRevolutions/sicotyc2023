namespace Entities.Enum
{
    public static class EnumExtensions
    {
        public static string GetStringValue(this object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var type = value.GetType();

            if (!type.IsEnum)
            {
                throw new ArgumentException($"{type} no es un tipo de enumeración.");
            }

            var fieldInfo = type.GetField(value.ToString());

            if (fieldInfo == null)
            {
                return value.ToString();
            }

            var attribute = (StringValueAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(StringValueAttribute));

            return attribute == null ? value.ToString() : attribute.Value;
        }
    }
}
