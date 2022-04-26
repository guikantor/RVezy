using Microsoft.AspNetCore.Mvc;

namespace RVezyTest.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ListingsController : ControllerBase
    {
        private static List<Listings> CSVFile()
        {
            var listings = new List<Listings>();
            string path = Path.Combine(Environment.CurrentDirectory, "listings.csv");
            StreamReader reader = new StreamReader(path);
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                string? line = reader.ReadLine();
                if (line == null)
                    continue;

                string[] parts = line.Split(';');
                listings.Add(new Listings()
                {
                    id = Convert.ToInt32(parts[0]),
                    listing_url = parts[1],
                    name = parts[2],
                    description = parts[3],
                    property_type = parts[4]

                });
            }

            return listings;
        }


        [HttpGet(Name = "GetCSVPaginated")]
        public IEnumerable<Listings> GetCSVPaginated(int pageNumber = 1, int pageSize = 3)
        {
            var listings = CSVFile();

            return listings.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        [HttpGet(Name = "GetCSVById")]
        public IEnumerable<Listings> GetCSVById(int id = 0)
        {
            var listings = CSVFile();
            return listings.Where(p => p.id == id);
        }

        [HttpGet(Name = "GetCSVPaginatedByPropertyType")]
        public IEnumerable<Listings> GetCSVPaginatedByPropertyType(int pageNumber = 1, int pageSize = 3, string property_type = "")
        {
            var listings = CSVFile();
            return listings.Where(p => p.property_type == property_type).Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        [HttpGet(Name = "List")]
        public IEnumerable<Listings> List()
        {
            var listings = new List<Listings>();
            using (var db = new DBContext.Context())
            {
                listings = db.Listings.ToList();
            }
            return listings.ToArray();
        }

        [HttpGet(Name = "Get")]
        public IEnumerable<Listings> Get(int id = 0)
        {
            var listings = new List<Listings>();
            using (var db = new DBContext.Context())
            {
                listings = db.Listings.Where(p => p.id == id).ToList();
            }
            return listings.ToArray();
        }

        [HttpPost(Name = "Add")]
        public void Add(string listing_url = "", string name = "", string description = "", string property_type = "")
        {
            var listings = new Listings()
            {
                listing_url = listing_url,
                name = name,
                description = description,
                property_type = property_type
            };

            using var db = new DBContext.Context();
            db.Listings.Add(listings);
            db.SaveChanges();
        }

        [HttpPost(Name = "Update")]
        public void Update(int id = 0, string listing_url = "", string name = "", string description = "", string property_type = "")
        {
            var listings = new Listings()
            {
                id = id,
                listing_url = listing_url,
                name = name,
                description = description,
                property_type = property_type
            };

            using var db = new DBContext.Context();
            db.Listings.Update(listings);
            db.SaveChanges();
        }

        [HttpPost(Name = "Delete")]
        public void Delete(int id = 0)
        {
            var listings = new Listings()
            {
                id = id
            };
            using var db = new DBContext.Context();
            db.Listings.Remove(listings);
            db.SaveChanges();
        }
    }
}
