# Pulumi Azure Serverless Setup

Starting point from Pulumi templates. A combination of azure-cs-functions and azure-cs-static-website from https://github.com/pulumi/examples

## Commands

Deploy
`pulumi up`

Destroy
``pulumi destroy`

### Troubleshoot

Issue deploying: This request is not authorized to perform this operation using this permission. ERROR CODE: AuthorizationPermissionMismatch  
Solution: Use principal service access. Add either "Owner" or "Storage Blob Data Contributor" and "Storage Queue Data Contributor"

`az ad sp create-for-rbac --name pulumi-deployer --role "Owner" --scopes /subscriptions/<subid> --sdk-auth`

add the env variables

`$env:ARM_CLIENT_ID=""`  
`$env:ARM_CLIENT_SECRET=""`  
`$env:ARM_TENANT_ID=""`  
`$env:ARM_SUBSCRIPTION_ID=""`  