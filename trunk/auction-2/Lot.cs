using System.Collections.Generic;
using System.Linq;

namespace auction_2
{
    public class Lot
    {
        public string Name { get; private set; }
        public string Discription { get; private set; }
        public byte[] Image { get; private set; }
        public List<Lot> Lots { get; private set; }
        public bool IsComplex {get { return Lots != null; }}

        public Lot(string name, string discription, byte[] image, params Lot[] lots )
        {
            Name = name;
            Discription = discription;
            Image = image;
            if (!lots.Any()) return;
            Lots = new List<Lot>();
            Lots.AddRange(lots);
        }
    }
}
