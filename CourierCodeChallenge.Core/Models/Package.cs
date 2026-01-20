using FluentValidation;

namespace CourierCodeChallenge.Core.Models
{
    public class Package
    {
        public string Id { get; }
        public string OfferCode { get; }
        public int Distance { get; }
        public int Weight { get; }
        public bool IsPackageValid { get; }
        public bool IsDelivered { get; set; }
        public double DeliveryTime { get; set; }
        public double DeliveryCost { get; set; }
        public double DiscountPrice { get; set; }

        public Package(string packageDetails)
        {
            IsPackageValid = true;
            var details = packageDetails.Split(' ');

            if (details.Length < 3 || details.Length > 4)
            {
                throw new FormatException("Entered input is not in correct format, please enter package details in this format, \n[Package_Name]<space>[Weight]<space>[Distance]<space>[Offer_Code]");
            }

            try
            {
                Id = details[0];
                Weight = Convert.ToInt32(details[1]);
                Distance = Convert.ToInt32(details[2]);
                OfferCode = details.Length == 4 ? details[3] : "";
            }
            catch (Exception)
            {
                throw new InvalidDataException("Distance & Weight should be a number, please enter package details in above format");
            }
        }
    }
}
