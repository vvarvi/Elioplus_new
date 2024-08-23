using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Lib.Services.TheCompaniesAPI.Entities
{
    public class City
    {
        public string address { get; set; }
        public string code { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string name { get; set; }
        public object postcode { get; set; }
    }

    public class CompaniesSimilar
    {
        public string descriptionShort { get; set; }
        public string domain { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string similitude { get; set; }
    }

    public class Continent
    {
        public string code { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string name { get; set; }
        public string nameEs { get; set; }
        public string nameFr { get; set; }
    }

    public class Country
    {
        public string code { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string name { get; set; }
        public string nameEs { get; set; }
        public string nameFr { get; set; }
    }

    public class EmailPattern
    {
        public string pattern { get; set; }
        public double usagePercentage { get; set; }
    }

    public class RootCompamies
    {
        public int? alexaRank { get; set; }
        public object businessType { get; set; }
        public City city { get; set; }
        public string codeNaics { get; set; }
        public string codeSic { get; set; }
        public List<object> companiesAcquisitions { get; set; }
        public List<CompaniesSimilar> companiesSimilar { get; set; }
        public List<object> companiesSubsidiaries { get; set; }
        public object companyParent { get; set; }
        public Continent continent { get; set; }
        public Country country { get; set; }
        public object county { get; set; }
        public string description { get; set; }
        public string descriptionShort { get; set; }
        public string domain { get; set; }
        public List<string> domainAlts { get; set; }
        public string domainName { get; set; }
        public string domainTld { get; set; }
        public List<EmailPattern> emailPatterns { get; set; }
        public int id { get; set; }
        public List<string> industries { get; set; }
        public string industryMain { get; set; }
        public string logo { get; set; }
        public string monthlyVisitors { get; set; }
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public string revenue { get; set; }
        public SocialNetworks socialNetworks { get; set; }
        public State state { get; set; }
        public object stockExchange { get; set; }
        public object stockSymbol { get; set; }
        public List<string> technologies { get; set; }
        public List<string> technologyCategories { get; set; }
        public string totalEmployees { get; set; }
        public string totalEmployeesExact { get; set; }
        public string yearFounded { get; set; }
    }

    public class SocialNetworks
    {
        public string angellist { get; set; }
        public string angellistId { get; set; }
        public string facebook { get; set; }
        public string facebookId { get; set; }
        public string instagram { get; set; }
        public string instagramId { get; set; }
        public string linkedin { get; set; }
        public string linkedinIdAlpha { get; set; }
        public int? linkedinIdNumeric { get; set; }
        public string linkedinSalesNavigator { get; set; }
        public string twitter { get; set; }
        public string twitterId { get; set; }
        public string youtube { get; set; }
        public string youtubeId { get; set; }
    }

    public class State
    {
        public string code { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string name { get; set; }
    }
}