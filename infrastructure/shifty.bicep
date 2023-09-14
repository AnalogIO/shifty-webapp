param location string = resourceGroup().location

param environment string

param organizationPrefix string
param applicationPrefix string

resource staticwebapp 'Microsoft.Web/staticSites@2022-03-01' = {
  name: 'stapp-${organizationPrefix}-${applicationPrefix}-${environment}'
  location: location
  sku: {
    name: 'Free'
    tier: 'Free'
  }
  properties: {
    allowConfigFileUpdates: false
    repositoryUrl: 'https://github.com/AnalogIO/shifty-webapp'
    branch: 'develop'
    provider: 'GitHub'
    stagingEnvironmentPolicy: 'Disabled'
    enterpriseGradeCdnStatus: 'Disabled'
  }
}
