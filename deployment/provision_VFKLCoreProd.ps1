### PREREQUISITS
# AZ CLI
# HELM 3
# AZ Login
# azure-functions-core-tools https://www.npmjs.com/package/azure-functions-core-tools
# az extension add -n application-insights #Add support for Application Insight for AzureCLI
#

### Example usage for VFKL in TT02
#  .\provision_VFKLCore.ps1 -subscription Udir-vfkl -deployenvironment test -environmentprefix test -maskinportenclientId [INSERT MASKINPORTEN CLIENTID]  -maskinportenuri https://ver2.maskinporten.no -platformuri https://platform.tt02.altinn.no/ -appsuri https://udir.apps.tt02.altinn.no/ -sendgridapikey [key] -emailurl https://udir.apps.tt02.altinn.no/udir/vfkl/GroupAssessment?id=

### Example usage for VFKL in pilot mode
#  .\provision_VFKLCore.ps1 -subscription Udir-vfkl -deployenvironment staging -environmentprefix stage -maskinportenclientId [INSERT MASKINPORTEN CLIENTID] -maskinportenuri https://ver2.maskinporten.no -platformuri https://platform.tt02.altinn.no/ -appsuri https://udir.apps.tt02.altinn.no/ -sendgridapikey [key] -emailurl https://udir.apps.tt02.altinn.no/udir/vfkl/GroupAssessment?id=

### Example usage for VFKL in PROD
#  .\provision_VFKLCore.ps1 -subscription Udir-vfkl -deployenvironment production -environmentprefix prod -maskinportenclientId [INSERT MASKINPORTEN CLIENTID] -maskinportenuri https://ver2.maskinporten.no -platformuri https://platform.altinn.no/ -appsuri https://udir.apps.altinn.no/ -sendgridapikey [key] -emailurl https://udir.apps.altinn.no/udir/vfkl/GroupAssessment?id=


param (
  [Parameter(Mandatory=$True)][string]$subscription,   
  [Parameter(Mandatory=$True)][string]$deployenvironment,
  [Parameter(Mandatory=$True)][string]$environmentprefix, 
  [Parameter(Mandatory=$True)][string]$maskinportenclientid, 
  #[Parameter(Mandatory=$True)][string]$maskinportenclientsecretvalue,
  #[Parameter(Mandatory=$True)][string]$databasesecretname,
  #[Parameter(Mandatory=$True)][string]$databasesecretvalue,
  [Parameter(Mandatory=$True)][string]$maskinportenuri,
  [Parameter(Mandatory=$True)][string]$maskinportenaudience,
  [Parameter(Mandatory=$True)][string]$altinnmaskinportenuri,
  [Parameter(Mandatory=$True)][string]$appsuri, 
  [Parameter(Mandatory=$True)][string]$platformuri, 
  [Parameter(Mandatory=$True)][string]$emailurl, 
  [Parameter(Mandatory=$True)][string]$sendgridapikey, 
  [string]$location = "norwayeast",
  [string]$businessUnitPrefix = "vfkl",
  [string]$deploymentslot = "production"
)

### Set subscription
az account set --subscription $subscription

$vfklResourceGroupName = "rg-$businessUnitPrefix-$environmentprefix-$location-001"
$keyvaultname = "kv-$businessUnitPrefix-$environmentprefix-001"
$maskinportenclientsecretname = "kv-secret-maskinportenclientid"
$databasesecretname = "kv-secret-dbconnectionstring" 
$storageAccountName = "st"+$businessUnitPrefix+$environmentprefix+$location+"001"
$functionName = "func-$businessUnitPrefix-$environmentprefix-$location-001"

#### Check if resource group for AKS exists
$resourceGroupExists = az group exists --name $vfklResourceGroupName
if (![System.Convert]::ToBoolean($resourceGroupExists)) {
  az group create --location $location --name $vfklResourceGroupName
}

Write-Output "Create Storage Account $storageAccountName"
$storageAccount = az storage account create -n $storageAccountName -g $vfklResourceGroupName -l $location --sku Standard_GRS --kind StorageV2 --access-tier Hot
$StorageID = az storage account show --name $storageAccountName --resource-group $vfklResourceGroupName --query id --output tsv
$storageAccountAccountKey = az storage account keys list --account-name $storageAccountName --query [0].value -o tsv
az storage container create -n inbound --account-name $storageAccountName --account-key $storageAccountAccountKey --public-access off
az storage container create -n active-subscriptions --account-name $storageAccountName --account-key $storageAccountAccountKey --public-access off
az storage container create -n add-subscriptions --account-name $storageAccountName --account-key $storageAccountAccountKey --public-access off
az storage container create -n remove-subscriptions --account-name $storageAccountName --account-key $storageAccountAccountKey --public-access off
$storageConnectionString = az storage account show-connection-string -g $vfklResourceGroupName -n $storageAccountName --query connectionString --output tsv
$blobendpoint = az storage account show --name $storageAccountName --resource-group $vfklResourceGroupName --query primaryEndpoints.blob --output tsv


Write-Output "Create KeyVault"
#$keyvault = az keyvault create --location $location --name $keyvaultname --resource-group $vfklResourceGroupName
#$secretId = az keyvault secret set --vault-name $keyvaultname --name $maskinportenclientsecretname --value $maskinportenclientid
#$databasesecretId = az keyvault secret set --vault-name $keyvaultname --name $databasesecretname --value $databasesecretvalue
$KeyvaultID = az keyvault show --name $keyvaultname --resource-group $vfklResourceGroupName --query id --output tsv
$vaultUri = az keyvault show --name $keyvaultname --resource-group $vfklResourceGroupName --query properties.vaultUri --output tsv


#Write-Output "Import Maskinporten cert"
#az keyvault certificate import --vault-name $keyvaultname -n maskinportenclientcert -f $maskinportenclientcert --password $maskinportenclientcertpwd

Write-Output "Create Function App"
az functionapp create --resource-group $vfklResourceGroupName --consumption-plan-location $location --runtime dotnet-isolated --functions-version 4 --name $functionName --storage-account $storageAccountName
#az functionapp deployment slot create --name $functionName --resource-group $vfklResourceGroupName --slot $deploymentslot
az functionapp identity assign -g $vfklResourceGroupName -n $functionName
$funcprincipalId = az functionapp identity show --name $functionName --resource-group $vfklResourceGroupName --query principalId  --output tsv

Write-Output "Set policy"
az keyvault set-policy --name $keyvaultname --object-id $funcprincipalId --secret-permissions get 
#--certificate-permissions list

Write-Output "Set config"
az functionapp config appsettings set --name $functionname --resource-group $vfklResourceGroupName --settings "VFKLCoreSettings:AccountKey=$storageAccountAccountKey" 
az functionapp config appsettings set --name $functionname --resource-group $vfklResourceGroupName --settings "VFKLCoreSettings:AccountName=$storageAccountName" 
az functionapp config appsettings set --name $functionname --resource-group $vfklResourceGroupName --settings "VFKLCoreSettings:AppsBaseUrl=$appsuri"
az functionapp config appsettings set --name $functionname --resource-group $vfklResourceGroupName --settings "VFKLCoreSettings:BlobEndpoint=$blobendpoint"
az functionapp config appsettings set --name $functionname --resource-group $vfklResourceGroupName --settings "VFKLCoreSettings:DatabaseSecretId=$databasesecretname"
az functionapp config appsettings set --name $functionname --resource-group $vfklResourceGroupName --settings "QueueStorageSettings:ConnectionString=$storageConnectionString"
az functionapp config appsettings set --name $functionname --resource-group $vfklResourceGroupName --settings "VFKLCoreSettings:PlatformBaseUrl=$platformuri"
az functionapp config appsettings set --name $functionname --resource-group $vfklResourceGroupName --settings "VFKLCoreSettings:MaskinportenBaseAddress=$maskinportenuri"
az functionapp config appsettings set --name $functionname --resource-group $vfklResourceGroupName --settings "VFKLCoreSettings:AltinnMaskinportenApiEndpoint=$altinnmaskinportenuri"
az functionapp config appsettings set --name $functionname --resource-group $vfklResourceGroupName --settings "VFKLCoreSettings:MaskinportenAudience=$maskinportenaudience"
az functionapp config appsettings set --name $functionname --resource-group $vfklResourceGroupName --settings "VFKLCoreSettings:MaskinPortenClientId=$maskinportenclientid"
az functionapp config appsettings set --name $functionname --resource-group $vfklResourceGroupName --settings "VFKLCoreSettings:TestMode=true"
az functionapp config appsettings set --name $functionname --resource-group $vfklResourceGroupName --settings "KeyVault:KeyVaultURI=$vaultUri"
az functionapp config appsettings set --name $functionname --resource-group $vfklResourceGroupName --settings "KeyVault:MaskinportenClientSecretId=$maskinportenclientsecretname"
# az functionapp config appsettings set --name $functionname --resource-group $vfklResourceGroupName --settings "KeyVault:MaskinPortenCertSecretId=$maskinportenclientcertname"
az functionapp config appsettings set --name $functionname --resource-group $vfklResourceGroupName --settings "KEYVAULT_ENDPOINT=$vaultUri"
az functionapp config appsettings set --name $functionname --resource-group $vfklResourceGroupName --settings "ASPNETCORE_ENVIRONMENT=$deployenvironment"
az functionapp config appsettings set --name $functionname --resource-group $vfklResourceGroupName --settings "VFKLCoreSettings:EmailAccount=vfkl@udir.no"
az functionapp config appsettings set --name $functionname --resource-group $vfklResourceGroupName --settings "VFKLCoreSettings:EmailAccountName=VFKL Invitasjon"
az functionapp config appsettings set --name $functionname --resource-group $vfklResourceGroupName --settings "VFKLCoreSettings:SendGridApiKey=$sendgridapikey"
az functionapp config appsettings set --name $functionname --resource-group $vfklResourceGroupName --settings "VFKLCoreSettings:EmailUrl=$emailurl"


Write-Output "Set config"
Set-Location ..\src\VFKLCore\Functions
func azure functionapp publish $functionName 
#--slot $deploymentslot
Set-Location ..\..\..\deployment