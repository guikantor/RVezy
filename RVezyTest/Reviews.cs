﻿namespace RVezyTest
{
    public class Reviews
    {
        public int listing_id { get; set; }
        public int id { get; set; }
      
        public DateTime date { get; set; }

        public int reviewer_id { get; set; }
      
        public string reviewer_name { get; set; }

        public string comments { get; set; }
    }
}
