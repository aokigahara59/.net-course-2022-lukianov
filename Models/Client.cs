using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Client : Person
    {
        public string PhoneNumber { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (!(obj is Client)) return false;

            Client client = (Client) obj;
            return Name == client.Name 
                && LastName == client.LastName
                && PassportId == client.PassportId
                && PhoneNumber == client.PhoneNumber;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode()
                + LastName.GetHashCode()
                + PassportId.GetHashCode()
                + PhoneNumber.GetHashCode();
        }
    }
}
