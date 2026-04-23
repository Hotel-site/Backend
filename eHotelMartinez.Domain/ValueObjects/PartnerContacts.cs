using eHotelMartinez.Domain.Models.Base;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace eHotelMartinez.Domain.ValueObjects
{
    public class PartnerContacts
    {
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? BookingUrl { get; set; }

        public PartnerContacts() { }

        public PartnerContacts(string? phone, string? email, string? bookingUrl)
        {
            Phone = phone;
            Email = email;
            BookingUrl = bookingUrl;
        }

        public ResponseMsg Validate()
        {
            if (!string.IsNullOrEmpty(Phone))
            {
                var phoneTrimmed = Phone!.Trim();
                var phoneRegex = new Regex(@"^[0-9+\-\s()]{5,30}$");
                if (!phoneRegex.IsMatch(phoneTrimmed))
                    return new ResponseMsg { IsSuccess = false, Message = "Phone contains invalid characters or length is out of range (5-30)." };
            }

            if (!string.IsNullOrEmpty(Email))
            {
                try
                {
                    _ = new MailAddress(Email);
                }
                catch (Exception)
                {
                    return new ResponseMsg { IsSuccess = false, Message = "Email is not in a valid format." };
                }
            }

            if (!string.IsNullOrEmpty(BookingUrl))
            {
                if (!Uri.TryCreate(BookingUrl, UriKind.Absolute, out _))
                    return new ResponseMsg { IsSuccess = false, Message = "Booking URL is not a valid HTTP/HTTPS URL." };
            }

            return new ResponseMsg { IsSuccess = true, Message = "Validation successful." };
        }

        public static bool TryCreate(string? phone, string? email, string? bookingUrl, out PartnerContacts? contact, out ResponseMsg response)
        {
            contact = new PartnerContacts(phone, email, bookingUrl);
            response = contact.Validate();
            if (!response.IsSuccess)
            {
                contact = null;
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            var phoneOut = string.IsNullOrWhiteSpace(Phone) ? "-" : Phone!;
            var emailOut = string.IsNullOrWhiteSpace(Email) ? "-" : Email!;
            var bookingOut = string.IsNullOrWhiteSpace(BookingUrl) ? "-" : BookingUrl!;
            return $"{phoneOut}; {emailOut}; {bookingOut}";
        }
    }
}
