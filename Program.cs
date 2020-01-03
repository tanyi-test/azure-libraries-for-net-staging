
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using System;

namespace azure_libraries_for_net_staging
{
    class Program
    {
        static void Main(string[] args)
        {
            var usage = "Usage: dotnet run <subscription-id> <resource-group-name> [<client-id>]";
            if (args.Length < 3)
            {
                throw new ArgumentException(usage);
            }

            var subscriptionId = args[0];
            var resourceGroupName = args[1];
            var clientId = args.Length > 2 ? args[2] : null;
            var credentials = new AzureCredentials(new MSILoginInformation(MSIResourceType.VirtualMachine)
            {
                UserAssignedIdentityClientId = clientId
            }, AzureEnvironment.AzureGlobalCloud);

            var resourceManager = ResourceManager.Authenticate(credentials).WithSubscription(subscriptionId);
            Console.WriteLine($"Got resource group: {resourceManager.ResourceGroups.GetByName(resourceGroupName).Id}");
        }
    }
}
