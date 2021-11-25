//using System;
//using Microsoft.AspNetCore.Authorization;
//using ApiServer.Requirements;
//using ApiServer.Helpers;
//using System.Threading.Tasks;

//namespace ApiServer.Handlers
//{
//    public class UserAuthenticationHandler : AuthorizationHandler<UserBlockedStatusRequirement>
//    {
//        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserBlockedStatusRequirement requirement)
//        {
//            var claim = context.User.FindFirst(c => c.Type == "IsBlocked" && c.Issuer == TokenHelper.Issuer);
//            if (!context.User.HasClaim(c => c.Type == "IsBlocked" && c.Issuer == TokenHelper.Issuer))
//            {
//                return Task.CompletedTask;
//            }
//            string value = context.User.FindFirst(c => c.Type == "IsBlocked" && c.Issuer == TokenHelper.Issuer).Value;
//            var userBlockedStatus = Convert.ToBoolean(value);
//            if (userBlockedStatus == requirement.IsBlocked)
//            {
//                context.Succeed(requirement);
//            }

//            return Task.CompletedTask;
//        }
//    }
//}
