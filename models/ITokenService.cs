using System;
using System.Collections.Generic;
using System.Security.Cryptography;

public interface ITokenService
{
    string CreateToken(string userId);
    string CreateUniqueiId();
}

public class TokenService : ITokenService
{
    public string CreateToken(string userId)
    {
        // Generate a secure random token
        const int tokenLength = 32; // Length of the token
        using (var rng = RandomNumberGenerator.Create())
        {
            byte[] tokenData = new byte[tokenLength];
            rng.GetBytes(tokenData);

            // Convert to a readable string using Base64
            string token = Convert.ToBase64String(tokenData);
            string res = "";
            for(int i = 0; i < 7; i++){
                res += token[i];
            }
            return res;
        }
    }
    public string CreateUniqueiId()
    {
        Guid guid = Guid.NewGuid();
        string guidString = guid.ToString();
        string res = "";
        for(int i = 0; i < 5; i++){
            res += guidString[i];
        }
        return res;
    }
}

public class TokenServiceProxy : ITokenService
{
    private readonly ITokenService _realTokenService;

    public TokenServiceProxy(ITokenService realTokenService)
    {
        _realTokenService = realTokenService;
    }

    public string CreateToken(string userId)
    {
        return _realTokenService.CreateToken(userId);
    }
    public string CreateUniqueiId()
    {
        return _realTokenService.CreateUniqueiId();
    }

}

