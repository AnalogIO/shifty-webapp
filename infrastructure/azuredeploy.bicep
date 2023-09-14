targetScope = 'subscription'

@allowed([ 'dev', 'prd' ])
param environment string

var location = 'West Europe'

var organizationPrefix = 'aio'
var sharedResourcesAbbreviation = 'shr'
var webAppResourcesAbbreviation = 'app'

resource sharedRg 'Microsoft.Resources/resourceGroups@2022-09-01' = {
  name: 'rg-${organizationPrefix}-${sharedResourcesAbbreviation}-${environment}'
  location: location
}

module sharedResources 'modules/shared.bicep' = {
  name: '${deployment().name}-shared'
  scope: sharedRg
  params: {
    location: location
    environment: environment
    organizationPrefix: organizationPrefix
    sharedResourcesAbbreviation: sharedResourcesAbbreviation
  }
}

resource shiftyRg 'Microsoft.Resources/resourceGroups@2022-09-01' = {
  name: 'rg-${organizationPrefix}-${webAppResourcesAbbreviation}-shifty-${environment}'
  location: location
}

module shiftywebapp 'shifty.bicep' = {
  name: '${deployment().name}-app-shifty'
  scope: shiftyRg
  params: {
    location: location
    organizationPrefix: organizationPrefix
    applicationPrefix: 'shifty'
    environment: environment
  }
}

module dns 'modules/dns.bicep' = {
  name: '${deployment().name}-dns'
  scope: sharedRg
  params: {
    environment: environment
  }
}
