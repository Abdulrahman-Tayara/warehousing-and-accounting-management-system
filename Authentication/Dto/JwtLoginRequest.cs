namespace Authentication.Dto;

public class JwtLoginRequest
{
    public string Username { get; set; }
    
    public string Password { get; set; }
}