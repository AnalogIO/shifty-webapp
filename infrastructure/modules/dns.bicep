param environment string

param webappAzureGeneratedFqdn string

resource zone 'Microsoft.Network/dnsZones@2018-05-01' = {
  name: '${environment}.analogio.dk'
  location: 'global'
}

resource cname 'Microsoft.Network/dnsZones/CNAME@2018-05-01' = {
  name: 'shifty'
  parent: zone
  properties: {
    TTL: 3600
    CNAMERecord: {
      cname: webappAzureGeneratedFqdn
    }
  }
}

output customDomainFqdn string = cname.properties.fqdn
