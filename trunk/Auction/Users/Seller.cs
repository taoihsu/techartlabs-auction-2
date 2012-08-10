namespace Auction.Users
{
    public class Seller: User
    {
        internal Seller(string login, string firstName, string secondName)
            : base(login, firstName, secondName)
        {
        }
    }
}
