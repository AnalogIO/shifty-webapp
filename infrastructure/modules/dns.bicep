param environment string

resource zone 'Microsoft.Network/dnsZones@2018-05-01' = {
  name: '${environment}.analogio.dk'
  location: 'global'
}
