namespace roundhouse.infrastructure.extensions
{
    public static class StringExtensions
    {
        public static string format_using(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static string to_lower(this string input)
        {
            return string.IsNullOrEmpty(input) ? string.Empty : input.ToLower();
        }

        public static string to_upper(this string input)
        {
            return string.IsNullOrEmpty(input) ? string.Empty : input.ToUpper();
        }
    }
}