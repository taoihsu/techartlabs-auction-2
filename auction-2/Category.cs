namespace auction_2
{
    public class Category
    {
        public string Name { get; private set; }
        public double Restriction { get; private set; }

        public Category(string name, double value = 0)
        {
            Name = name;
            Restriction = value;
        }


    }
}
