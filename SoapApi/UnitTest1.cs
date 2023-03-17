using ServiceReference1;
using System.Linq;

namespace SoapApi
{
    [TestClass]
    public class CountryRoad
    {
        // Global Variable
        private readonly ServiceReference1.CountryInfoServiceSoapTypeClient countryList =
            new ServiceReference1.CountryInfoServiceSoapTypeClient(ServiceReference1.CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);


        [TestMethod]
        public void CountryCodeList()
        {
            // Verify Country list of names by code is in Ascending Order
            var nameCountryCode = countryList.ListOfCountryNamesByCode();
            var nameCountryCodeAsc = nameCountryCode.OrderBy(isoCode => isoCode.sISOCode);

            Assert.IsTrue(Enumerable.SequenceEqual(nameCountryCode, nameCountryCodeAsc), "Country Code is not in Ascending Order");
        }

        [TestMethod]
        public void CountryCodeName()
        {
            // Country name exist in database
            var CountryName = countryList.CountryName("ZA");

            // Verify the code is not in database
            var SecondCountryName = countryList.CountryName("YU");

            // Verify the Country Name Exist in Database
            Assert.AreEqual("South Africa", CountryName, "Code is not existing in database");
            Assert.AreEqual("Country not found in the database", SecondCountryName, "Country is in the database");
        }

        [TestMethod]
        public void LastCountryNameCode()
        {
            // Gets the Country Code with Country Name
            var LastCountryRoad = countryList.ListOfCountryNamesByCode().Last();
            
            // Get The Country Name frome the Last Country Code
            var lastCountry = countryList.CountryName(LastCountryRoad.sISOCode);

            Assert.AreEqual(LastCountryRoad.sName, lastCountry, "");
        }

    }
}