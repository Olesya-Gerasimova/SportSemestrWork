using System;
using System.Threading.Tasks;
using ApiServer.Requests;
using ApiServer.Responses;

namespace ApiServer.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileResponse> GetProfileForUser(UserProfileRequest userProfileRequest);

        Task<UserProfileResponse> CreateUpdateProfileForUser(UserProfileCreateUpdateRequest createUpdateRequest);

    }
}
