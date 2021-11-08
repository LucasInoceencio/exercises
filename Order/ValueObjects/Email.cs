using System.Text.RegularExpressions;

namespace Order
{
    public struct Email
    {
        private readonly string _value;
        public readonly bool IsValid;

        private Email(string value)
        {
            _value = value.Trim();

            if (string.IsNullOrEmpty(_value))
            {
                IsValid = false;
                return;
            }

            IsValid = Regex.IsMatch(_value, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

            if (!IsValid)
                return;
        }

        public static implicit operator Email(string value)
            => new Email(value);

        public override string ToString() => _value;
    }
}
