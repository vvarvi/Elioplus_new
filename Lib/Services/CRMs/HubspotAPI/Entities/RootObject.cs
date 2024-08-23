using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Lib.Services.CRMs.HubspotAPI.Entities
{
    public class Contact
    {
        //public string Name { get; set; }
        //public string Email { get; set; }
        //public string PhoneNumber { get; set; }
        //public string ContactOwner { get; set; }
        //public string AssociatedCompany { get; set; }
        //public DateTime LastActivityDate { get; set; }
        //public string LeadStatus { get; set; }

        public object addedAt { get; set; }
        public int vid { get; set; }
        public Properties properties { get; set; }
    }

    public class Properties
    {
        public Firstname firstname { get; set; }
        public Lastmodifieddate lastmodifieddate { get; set; }
        public Company company { get; set; }
        public Lastname lastname { get; set; }
        public Email email { get; set; }
        public Phone phone { get; set; }
        public Contactowner contactowner { get; set; }
        public Associatedcompany associatedcompany { get; set; }
        public Leadstatus lead_status { get; set; }
        public Leadcoutry lead_country { get; set; }
        public Createddate created_date { get; set; }
        public Website website { get; set; }
        public Comments comments { get; set; }
        public Lastupdate last_update { get; set; }
        public Ispublic is_public { get; set; }
    }

    public class Ispublic
    {
        public string value { get; set; }
    }

    public class Lastupdate
    {
        public DateTime value { get; set; }
    }

    public class Comments
    {
        public string value { get; set; }
    }

    public class Website
    {
        public string value { get; set; }
    }

    public class Leadcoutry
    {
        public string value { get; set; }
    }

    public class Firstname
    {
        public string value { get; set; }
    }

    public class Lastmodifieddate
    {
        public string value { get; set; }
    }

    public class Company
    {
        public string value { get; set; }
    }

    public class Lastname
    {
        public string value { get; set; }
    }

    public class Email
    {
        public string value { get; set; }
    }

    public class Phone
    {
        public string value { get; set; }
    }

    public class Contactowner
    {
        public string value { get; set; }
    }

    public class Associatedcompany
    {
        public string value { get; set; }
    }

    public class Leadstatus
    {
        public string value { get; set; }
    }

    public class Createddate
    {
        public DateTime value { get; set; }
    }

    public class RootObject
    {
        public List<Contact> contacts { get; set; }
        public Contact contact { get; set; }
    }
}