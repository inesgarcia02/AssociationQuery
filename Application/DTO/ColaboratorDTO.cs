namespace Application.DTO
{
    public class ColaboratorDTO
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }

        public ColaboratorDTO()
        {
        }

        public ColaboratorDTO(long id, string strName, string strEmail, string strStreet, string strPostalCode)
        {
            Id = id;
            Name = strName;
            Email = strEmail;
            Street = strStreet;
            PostalCode = strPostalCode;
        }
    }
}