using System;
using System.Collections.Generic;
using System.Security.Cryptography;

public interface ITokenService
{
    string CreateToken(string userId);
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
            return token;
        }
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
}

