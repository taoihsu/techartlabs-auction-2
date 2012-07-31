namespace auction_2
{
    public class Person
    {
        public string Login { get; private set; }
        public string FirstName { get; private set; }
        public string SecondName { get; private set; }
        public string FullName { get { return FirstName + " " + SecondName; } }
        public Person(string login, string firstName, string secondName)
        {
            Login = login;
            FirstName = firstName;
            SecondName = secondName;
        }

        public new string ToString()
        {
            return FullName;
        }

    }
}
