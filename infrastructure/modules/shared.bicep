@allowed([ 'dev', 'prd' ])
param environment string
param location string = resourceGroup().location

param organizationPrefix string
param sharedResourcesAbbreviation string

var isPrd = environment == 'prd'
var settings = isPrd ? loadJsonContent('../prd.settings.json') : loadJsonContent('../dev.settings.json')

resource appservicePlan 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: 'asp-${organizationPrefix}-${sharedResourcesAbbreviation}-${environment}'
  location: location
  sku: settings.appservicePlan.sku
  kind: 'linux'
  properties: {
    perSiteScaling: true
    reserved: true
  }
}

module insightsModule 'insights.bicep' = {
  name: '${deployment().name}-insights'
  params: {
    location: location
    organizationPrefix: organizationPrefix
    applicationPrefix: sharedResourcesAbbreviation
    environment: environment
  }
}

output appServicePlanName string = appservicePlan.name
output logAnalyticsWorkspaceName string = insightsModule.outputs.logAnalyticsWorkspaceName
output applicationInsightsName string = insightsModule.outputs.applicationInsightsName
