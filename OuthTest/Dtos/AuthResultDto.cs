namespace OAuthTest.Dtos
{
    public class AuthResultDto
    {
        public string AccessToken { get; set; }

        public AuthResultDto(string accessToken)
        {
            AccessToken = accessToken;
        }
    }
}
