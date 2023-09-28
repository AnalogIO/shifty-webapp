targetScope = 'subscription'

@allowed([ 'dev', 'prd' ])
param environment string

var location = 'West Europe'

var organizationPrefix = 'aio'
var sharedResourcesAbbreviation = 'shr'
var webAppResourcesAbbreviation = 'app'

resource sharedRg 'Microsoft.Resources/resourceGroups@2022-09-01' existing = {
  name: 'rg-${organizationPrefix}-${sharedResourcesAbbreviation}-${environment}'
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
    sharedResourceGroupName: sharedRg.name
  }
}
