param location string = resourceGroup().location

param environment string

param organizationPrefix string
param applicationPrefix string

param sharedResourceGroupName string

resource staticwebapp 'Microsoft.Web/staticSites@2022-03-01' = {
  name: 'stapp-${organizationPrefix}-${applicationPrefix}-${environment}'
  location: location
  sku: {
    name: 'Free'
    tier: 'Free'
  }
  properties: {
    allowConfigFileUpdates: true
    repositoryUrl: 'https://github.com/AnalogIO/shifty-webapp'
    branch: 'main'
    provider: 'GitHub'
    stagingEnvironmentPolicy: 'Disabled'
    enterpriseGradeCdnStatus: 'Disabled'
  }
}

module dns 'modules/dns.bicep' = {
  name: '${deployment().name}-dns'
  scope: resourceGroup(sharedResourceGroupName)
  params: {
    environment: environment
    webappAzureGeneratedFqdn: staticwebapp.properties.defaultHostname
  }
}

resource staticwebappCustomDomain 'Microsoft.Web/staticSites/customDomains@2022-03-01' = {
  name: 'shifty.${environment}.analogio.dk'
  parent: staticwebapp
}
