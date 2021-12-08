using System;
using System.Threading.Tasks;
using ApiServer.Requests;
using ApiServer.Responses;

namespace ApiServer.Interfaces
{
    public interface IPremierLeagueService
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
        Task<SignUpResponse> AddUser(SignUpRequest signUpRequest);
        
    }
}
