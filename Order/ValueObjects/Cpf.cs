using System;

namespace Order
{
    public struct Cpf
    {
        private readonly string _value;
        private readonly string _onlyDigitsCpf;
        public readonly bool IsValid;

        private Cpf(string value)
        {
            _value = value;
            _onlyDigitsCpf = string.Empty;

            if (value == null)
            {
                IsValid = false;
                return;
            }

            var position = 0;
            var totalDigit1 = 0;
            var totalDigit2 = 0;
            var dv1 = 0;
            var dv2 = 0;

            bool sameDigits = true;
            var lastDigit = -1;

            foreach (var c in value)
            {
                if (char.IsDigit(c))
                {
                    _onlyDigitsCpf += c;

                    var digit = c - '0';
                    if (position != 0 && lastDigit != digit)
                    {
                        sameDigits = false;
                    }

                    lastDigit = digit;
                    if (position < 9)
                    {
                        totalDigit1 += digit * (10 - position);
                        totalDigit2 += digit * (11 - position);
                    }
                    else if (position == 9)
                    {
                        dv1 = digit;
                    }
                    else if (position == 10)
                    {
                        dv2 = digit;
                    }

                    position++;
                }
            }

            if (position > 11)
            {
                IsValid = false;
                return;
            }

            if (sameDigits)
            {
                IsValid = false;
                return;
            }

            var digito1 = totalDigit1 % 11;
            digito1 = digito1 < 2
                ? 0
                : 11 - digito1;

            if (dv1 != digito1)
            {
                IsValid = false;
                return;
            }

            totalDigit2 += digito1 * 2;
            var digito2 = totalDigit2 % 11;
            digito2 = digito2 < 2
                ? 0
                : 11 - digito2;

            IsValid = dv2 == digito2;
        }

        public static implicit operator Cpf(string value)
            => new Cpf(value);

        public override string ToString() => _value;

        public string CpfFormatado =>
            Convert.ToUInt64(_onlyDigitsCpf).ToString(@"000\.000\.000\-00");
    }
}
