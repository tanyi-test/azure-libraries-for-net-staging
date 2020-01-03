
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using System;

namespace azure_libraries_for_net_staging
{
    class Program
    {
        static void Main(string[] args)
        {
            var resourceGroupName = "RandomGroup";
            var region = Region.USWest;
            var credentials = new AzureCredentialsFactory().FromFile(Environment.GetEnvironmentVariable("AZURE_AUTH_LOCATION"));
            var resourceManager = ResourceManager.Authenticate(credentials).WithSubscription(credentials.DefaultSubscriptionId);

            var isExist = resourceManager.ResourceGroups.Contain(resourceGroupName);
            if (!isExist)
                resourceManager.ResourceGroups.Define(resourceGroupName)
                    .WithRegion(region)
                    .Create();

            Console.WriteLine(resourceManager.ResourceGroups.GetByName(resourceGroupName).Id);

            if (!isExist)
                resourceManager.ResourceGroups.BeginDeleteByName(resourceGroupName);
        }
    }
}
