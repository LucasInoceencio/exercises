using System;

namespace Order
{
    public class Person : EntityBase
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime BirthDate { get; private set; }
        public int Age { 
            get
            {
                var actualDate = DateTime.Now;
                var age = actualDate.Year - BirthDate.Year;

                if (actualDate.Month < BirthDate.Month)
                    age--;

                return age;
            }
        }
        public Cpf Cpf { get; private set; }
        public Email Email { get; private set; }

        public Person(string firstName, string lastName, DateTime birthDate, Cpf cpf, Email email)
        {
            FirstName = firstName.Trim();
            LastName = lastName.Trim();

            if (DateTime.Now.Year - birthDate.Year > 110)
                throw new OrderException("Idade superior a 110 anos não permitida!");

            if (!cpf.IsValid)
                throw new OrderException("Cpf inválido!");

            if (!email.IsValid)
                throw new OrderException("E-mail inválido!");

            BirthDate = birthDate;
            Cpf = cpf;
            Email = email;
            base.DateHourRegister = DateTime.Now;
        }

        public string FullName =>
            $"{FirstName} {LastName}";
    }
}
