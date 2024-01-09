﻿using AutoMapper;
using Dictionary.Api.Application.Interfaces.Repositories;
using Dictionary.Common.Infrastructure;
using Dictionary.Common.Infrastructure.Exceptions;
using Dictionary.Common.Models.Queries;
using Dictionary.Common.Models.RequestModels;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Api.Application.Features.Commands.User;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var dbUser= await _userRepository.GetSingleAsync(x=>x.EmailAddress == request.EmailAddress);
        if (dbUser == null)
            throw new DatabaseValidationException("User not found!");

        var pass=PasswordEncryptor.Encrypt(request.Password);
        if (dbUser.Password!=pass)
            throw new DatabaseValidationException("Password is wrong!");

        if (!dbUser.EmailConfirmed)
            throw new DatabaseValidationException("Email address is not confirmed yet!");

        var result=_mapper.Map<LoginUserViewModel>(dbUser);

        var claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier,dbUser.Id.ToString()),
            new Claim(ClaimTypes.Email,dbUser.EmailAddress),
            new Claim(ClaimTypes.Name,dbUser.UserName),
            new Claim(ClaimTypes.GivenName,dbUser.FirstName),
            new Claim(ClaimTypes.Surname,dbUser.LastName),
        };

        result.Token = GenerateToken(claims);

        return result;
    }

    private string GenerateToken(Claim[] claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthConfig:Secret"]));
        var creds= new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
        var expiry= DateTime.Now.AddDays(10);

        var token= new JwtSecurityToken(claims:claims,
                                               expires:expiry,
                                               signingCredentials:creds,
                                               notBefore:DateTime.Now);

        return new JwtSecurityTokenHandler().WriteToken(token);

    }

}
