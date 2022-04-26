using Microsoft.EntityFrameworkCore;

namespace RVezyTest
{
    [Keyless]
    public class Calendar
    {
        public int listing_id { get; set; }

        public DateTime date { get; set; }

        public bool available { get; set; }

        public decimal price { get; set; }
    }
}
