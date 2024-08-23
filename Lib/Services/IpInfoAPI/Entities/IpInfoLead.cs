using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WdS.ElioPlus.Lib.Services.IpInfoAPI.Entities
{
    public class IpInfoLead
    {
        public bool Anycast { get; set; }

        public bool Bogon { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string CountryName { get; set; }

        public bool IsEU { get; set; }

        public CountryFlag CountryFlag { get; set; }

        public string CountryFlagURL { get; set; }

        public CountryCurrency CountryCurrency { get; set; }

        public Continent Continent { get; set; }

        public string Hostname { get; set; }

        public string Loc { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string Org { get; set; }

        public string Postal { get; set; }

        public string Region { get; set; }

        public string Timezone { get; set; }
        
        public string IP { get; set; }

        public ASN Asn { get; set; }

        public Company Company { get; set; }

        public Carrier Carrier { get; set; }

        public Privacy Privacy { get; set; }

        public Abuse Abuse { get; set; }

        public DomainsList Domains { get; set; }
    }

    public class Company
    {
        public string Domain { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        // immutable type
        [JsonConstructor]
        public Company(string domain, string name, string type) =>
              (Domain, Name, Type) = (domain, name, type);
    }

    public class Carrier
    {
        public string Mcc { get; set; }
        public string Mnc { get; set; }
        public string Name { get; set; }

        // immutable type
        [JsonConstructor]
        public Carrier(string mcc, string mnc, string name) =>
              (Mcc, Mnc, Name) = (mcc, mnc, name);
    }

    public class CountryFlag
    {
        public string Emoji { get; set; }

        public string Unicode { get; set; }
    }

    public class CountryCurrency
    {
        public string Code { get; set; }

        public string Symbol { get; set; }
    }

    public class Continent
    {
        public string Code { get; set; }

        public string Name { get; set; }
    }

    public class ASN
    {
        public string Asn { get; set; }

        public string Domain { get; set; }

        public string Name { get; set; }

        public string Route { get; set; }

        public string Type { get; set; }

        public ASN(string asn, string domain, string name, string route, string type) =>
              (Asn, Domain, Name, Route, Type) = (asn, domain, name, route, type);
    }

    public class Privacy
    {
        public bool Hosting { get; set; }

        public bool Proxy { get; set; }

        public bool Relay { get; set; }

        public string Service { get; set; }

        public bool Tor { get; set; }

        public bool Vpn { get; set; }

        public Privacy(bool hosting, bool proxy, bool relay, string service, bool tor, bool vpn) =>
              (Hosting, Proxy, Relay, Service, Tor, Vpn) = (hosting, proxy, relay, service, tor, vpn);
    }

    public class Abuse
    {
        public string Address { get; set; }

        public string Country { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Network { get; set; }

        public string Phone { get; set; }

        public Abuse(string address, string country, string email, string name, string network, string phone) =>
              (Address, Country, Email, Name, Network, Phone) = (address, country, email, name, network, phone);
    }

    public class DomainsList
    {
        public List<string> Domains { get; set; }

        public string IP { get; set; }

        public int Total { get; set; }

        public DomainsList(List<string> domains, string ip, int total) =>
              (Domains, IP, Total) = (domains, ip, total);
    }
}