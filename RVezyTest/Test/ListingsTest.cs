using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RVezyTest.Test
{
    [TestClass]
    public class ListingsTest
    {
        [TestMethod]
        public void GetCSVPaginated()
        {
            int pageNumber = 1, pageSize = 3;
            var controller = new Controllers.ListingsController();
            var listings = controller.GetCSVPaginated(pageNumber, pageSize);
            Assert.IsNotNull(listings);
            Assert.IsTrue(listings.Count() == 3);
        }

        [TestMethod]
        public void GetCSVById()
        {
            int id = 1;
            var controller = new Controllers.ListingsController();
            var listing = controller.GetCSVById(id).First();
            Assert.IsNotNull(listing);
            Assert.AreEqual(id, listing.id);
            Assert.AreEqual("Queen Anne Apartment", listing.name);
        }

        [TestMethod]
        public void GetCSVPaginatedByPropertyType()
        {
            int pageNumber = 1, pageSize = 3;
            string property_type = "house";
            var controller = new Controllers.ListingsController();
            var listings = controller.GetCSVPaginatedByPropertyType(pageNumber, pageSize, property_type);
            Assert.IsNotNull(listings);
            Assert.IsTrue(listings.Count() == 1);
        }

        [TestMethod]
        public void List()
        {
            var controller = new Controllers.ListingsController();
            var listings = controller.List();
            Assert.IsNotNull(listings);
            Assert.IsTrue(listings.Count() >= 3);
        }

        [TestMethod]
        public void Get()
        {
            int id = 1;
            var controller = new Controllers.ListingsController();
            var listing = controller.Get(id).First();
            Assert.IsNotNull(listing);
            Assert.AreEqual(id, listing.id);
            Assert.AreEqual("Queen Anne Apartment", listing.name);
        }

        [TestMethod]
        public void Add()
        {
            var controller = new Controllers.ListingsController();
            controller.Add("http://www.test.com", "RVezy Test name", "RVezy Test description", "house");

            var listings = controller.List();
            var item = listings.Last();
            Assert.AreEqual(item.description, "RVezy Test description");
            controller.Delete(item.id);
        }

        [TestMethod]
        public void Update()
        {
            int id = 1;
            var controller = new Controllers.ListingsController();
            var item = controller.Get(id).First();
            item.description = "Test";
            controller.Update(id, item.listing_url, item.name, item.description, item.description);

            var updated = controller.Get(id).First();
            Assert.AreEqual(updated.description, "Test");

            item.description = "Enjoy Queen Anne Living";
            controller.Update(id, item.listing_url, item.name, item.description, item.description);
        }

        [TestMethod]
        public void Delete()
        {
            var controller = new Controllers.ListingsController();
            controller.Add("http://www.test.com", "RVezy Test name", "RVezy Test description", "house");
            var listings = controller.List();
            
            var item = listings.Last();
            controller.Delete(item.id);
            var delete_item = controller.Get(item.id);
            Assert.AreEqual(delete_item.Count(), 0);
        }

    }
}