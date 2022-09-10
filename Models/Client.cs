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
            int code = 0;
            if (Name != null)
            {
                code += Name.GetHashCode();
            }

            if (LastName != null)
            {
                code += LastName.GetHashCode();
            }

            if (PhoneNumber != null)
            {
                code += PhoneNumber.GetHashCode();
            }

            if (PassportId != 0)
            {
                code += PassportId.GetHashCode();
            }

            return code;
        }
    }
}
