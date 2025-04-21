namespace OAuthTest.Dtos
{
    public class CustomerDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public CustomerDto(string id, string name)
        {
            Id = id;
            UserName = name;
        }

        public CustomerDto()
        {

        }
    }
}
