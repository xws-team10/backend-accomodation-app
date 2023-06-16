using account_service.Model;
using Grpc.Core;
using Microsoft.AspNetCore.Identity;

namespace account_service.ProtoServices
{
    public class GrpcGetUserIdService : GrpcGetUserId.GrpcGetUserIdBase
    {
        public readonly UserManager<User> _userManager;
        public GrpcGetUserIdService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public override async Task<UserIdResponse> GetUserId(UserIdRequest request, ServerCallContext context)
        {
            var response = new UserIdResponse();

            var user = await _userManager.FindByNameAsync(request.Username);

            response.UserId = user.Id.ToString();

            return await Task.FromResult(response);
        }
    }
}
