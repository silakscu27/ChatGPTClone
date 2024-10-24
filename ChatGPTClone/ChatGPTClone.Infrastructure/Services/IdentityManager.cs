﻿using System.Web;
using ChatGPTClone.Application.Common.Interfaces;
using ChatGPTClone.Application.Common.Models.Identity;
using ChatGPTClone.Application.Common.Models.Jwt;
using ChatGPTClone.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace ChatGPTClone.Infrastructure.Services
{
    public class IdentityManager : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtService _jwtService;

        public IdentityManager(UserManager<AppUser> userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<bool> AuthenticateAsync(IdentityAuthenticateRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null) return false;
            return await _userManager.CheckPasswordAsync(user, request.Password);

        }

        public async Task<IdentityLoginResponse> LoginAsync(IdentityLoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            var roles = await _userManager.GetRolesAsync(user);

            var jwtRequest = new JwtGenerateTokenRequest(user.Id, user.Email, roles);

            var jwtResponse = new JwtGenerateTokenResponse(jwtRequest);

            return new IdentityLoginResponse(jwtResponse.Token, jwtResponse.ExpiresAt);
        }

        Task<bool> IIdentityService.AuthenticateAsync(IdentityAuthenticateRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<bool> IIdentityService.CheckEmailExistAsync(string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<IdentityLoginResponse> IIdentityService.LoginAsync(IdentityLoginRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
