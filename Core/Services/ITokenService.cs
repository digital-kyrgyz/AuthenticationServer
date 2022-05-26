﻿using Core.Configuration;
using Core.Dtos;
using Core.Entities;

namespace Core.Services
{
    public interface ITokenService
    {
        TokenDto CreateToken(UserApp user);
        ClientTokenDto CreateTokenByClient(Client client);
    }
}