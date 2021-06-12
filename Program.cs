using System;
using Azure;
using Azure.Core;
using Azure.Communication.Identity;

namespace AccessTokensQuickstart
{
    class Program
    {
        private static readonly string endpoint = "<Endpoint>";
        private static readonly string accessKey = "<AccessKey>";
        // 作成したユーザーを削除する場合は以下を true
        private static readonly bool isDeleteUser = false;

        static async System.Threading.Tasks.Task Main(string[] args)
        {
            // 認証
            var client = new CommunicationIdentityClient(new Uri(endpoint), new AzureKeyCredential(accessKey));

            // ID作成とToken発行
            var identityAndTokenResponse = await client.CreateUserAndTokenAsync(scopes: new[] { CommunicationTokenScope.VoIP });
            var user = identityAndTokenResponse.Value.User;
            var token = identityAndTokenResponse.Value.AccessToken.Token;

            Console.WriteLine($"{user.Id}\n");
            Console.WriteLine(token);

            // ID削除
            if (isDeleteUser)
            {
                await client.DeleteUserAsync(user);
                Console.WriteLine($"\nDelete ID: {user.Id}");
            }
        }
    }
}