using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_tier_management_reporting_users_values_custom")]
    public class ElioTierManagementReportingUsersValuesCustom : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("user_id")]
        public int UserId{ get; set; }

        [FieldInfo("customer_name1")]
        public string CustomerName1 { get; set; }

        [FieldInfo("industry_id1")]
        public int IndustryId1 { get; set; }

        [FieldInfo("customer_name2")]
        public string CustomerName2 { get; set; }

        [FieldInfo("industry_id2")]
        public int IndustryId2 { get; set; }

        [FieldInfo("customer_name3")]
        public string CustomerName3 { get; set; }

        [FieldInfo("industry_id3")]
        public int IndustryId3 { get; set; }

        [FieldInfo("customer_name4")]
        public string CustomerName4 { get; set; }

        [FieldInfo("industry_id4")]
        public int IndustryId4 { get; set; }

        [FieldInfo("customer_name5")]
        public string CustomerName5 { get; set; }

        [FieldInfo("industry_id5")]
        public int IndustryId5 { get; set; }

        [FieldInfo("total_customers_count")]
        public int TotalCustomersCount { get; set; }

        [FieldInfo("customers_revenue")]
        public decimal CustomersRevenue { get; set; }

        [FieldInfo("active_customers_count")]
        public int ActiveCustomersCount { get; set; }

        [FieldInfo("total_spend_on")]
        public decimal TotalSpendOn { get; set; }

        [FieldInfo("customers_market_share_averge")]
        public decimal CustomersMarketShareAverge { get; set; }

        [FieldInfo("spend_market_share_average")]
        public decimal SpendMarketShareAverage { get; set; }

        [FieldInfo("admin_capacity_Q1")]
        public int AdminCapacityQ1 { get; set; }

        [FieldInfo("admin_capacity_Q2")]
        public int AdminCapacityQ2 { get; set; }

        [FieldInfo("admin_capacity_Q3")]
        public int AdminCapacityQ3 { get; set; }

        [FieldInfo("admin_capacity_Q4")]
        public int AdminCapacityQ4 { get; set; }

        [FieldInfo("admin_capacity_avg")]
        public decimal AdminCapacityAvg { get; set; }

        [FieldInfo("bus_dev_capacity_Q1")]
        public int BusDevCapacityQ1 { get; set; }

        [FieldInfo("bus_dev_capacity_Q2")]
        public int BusDevCapacityQ2 { get; set; }

        [FieldInfo("bus_dev_capacity_Q3")]
        public int BusDevCapacityQ3 { get; set; }

        [FieldInfo("bus_dev_capacity_Q4")]
        public int BusDevCapacityQ4 { get; set; }

        [FieldInfo("bus_dev_capacity_avg")]
        public decimal BusDevCapacityAvg { get; set; }

        [FieldInfo("developers_capacity_Q1")]
        public int DevelopersCapacityQ1 { get; set; }

        [FieldInfo("developers_capacity_Q2")]
        public int DevelopersCapacityQ2 { get; set; }

        [FieldInfo("developers_capacity_Q3")]
        public int DevelopersCapacityQ3 { get; set; }

        [FieldInfo("developers_capacity_Q4")]
        public int DevelopersCapacityQ4 { get; set; }

        [FieldInfo("developers_capacity_avg")]
        public decimal DevelopersCapacityAvg { get; set; }

        [FieldInfo("cert_developers_capacity_Q1")]
        public int CertDevelopersCapacityQ1 { get; set; }

        [FieldInfo("cert_developers_capacity_Q2")]
        public int CertDevelopersCapacityQ2 { get; set; }

        [FieldInfo("cert_developers_capacity_Q3")]
        public int CertDevelopersCapacityQ3 { get; set; }

        [FieldInfo("cert_developers_capacity_Q4")]
        public int CertDevelopersCapacityQ4 { get; set; }

        [FieldInfo("cert_developers_capacity_avg")]
        public decimal CertDevelopersCapacityAvg { get; set; }

        [FieldInfo("planned_sales_Q1")]
        public decimal PlannedSalesQ1 { get; set; }

        [FieldInfo("planned_sales_Q2")]
        public decimal PlannedSalesQ2 { get; set; }

        [FieldInfo("planned_sales_Q3")]
        public decimal PlannedSalesQ3 { get; set; }

        [FieldInfo("planned_sales_Q4")]
        public decimal PlannedSalesQ4 { get; set; }

        [FieldInfo("planned_sales_total")]
        public decimal PlannedSalesTotal { get; set; }

        [FieldInfo("actual_sales_Q1")]
        public decimal ActualSalesQ1 { get; set; }

        [FieldInfo("actual_sales_Q2")]
        public decimal ActualSalesQ2 { get; set; }

        [FieldInfo("actual_sales_Q3")]
        public decimal ActualSalesQ3 { get; set; }

        [FieldInfo("actual_sales_Q4")]
        public decimal ActualSalesQ4 { get; set; }

        [FieldInfo("actual_sales_total")]
        public decimal ActualSalesTotal { get; set; }

        [FieldInfo("objectives1")]
        public string Objectives1 { get; set; }

        [FieldInfo("costs1")]
        public string Costs1 { get; set; }

        [FieldInfo("date_created1")]
        public DateTime? DateCreated1 { get; set; }

        [FieldInfo("activity1")]
        public string Activity1 { get; set; }

        [FieldInfo("objectives2")]
        public string Objectives2 { get; set; }

        [FieldInfo("costs2")]
        public string Costs2 { get; set; }

        [FieldInfo("date_created2")]
        public DateTime? DateCreated2 { get; set; }

        [FieldInfo("activity2")]
        public string Activity2 { get; set; }

        [FieldInfo("objectives3")]
        public string Objectives3 { get; set; }

        [FieldInfo("costs3")]
        public string Costs3 { get; set; }

        [FieldInfo("date_created3")]
        public DateTime? DateCreated3 { get; set; }

        [FieldInfo("activity3")]
        public string Activity3 { get; set; }

        [FieldInfo("objectives4")]
        public string Objectives4 { get; set; }

        [FieldInfo("costs4")]
        public string Costs4 { get; set; }

        [FieldInfo("date_created4")]
        public DateTime? DateCreated4 { get; set; }

        [FieldInfo("activity4")]
        public string Activity4 { get; set; }

        [FieldInfo("objectives5")]
        public string Objectives5 { get; set; }

        [FieldInfo("costs5")]
        public string Costs5 { get; set; }

        [FieldInfo("date_created5")]
        public DateTime? DateCreated5 { get; set; }

        [FieldInfo("activity5")]
        public string Activity5 { get; set; }

        [FieldInfo("technical_support_issues")]
        public string TechnicalSupportIssues { get; set; }

        [FieldInfo("general_issues")]
        public string GeneralIssues { get; set; }

        [FieldInfo("insert_user_id")]
        public int InsertUserId { get; set; }

        [FieldInfo("insert_date")]
        public DateTime InsertDate { get; set; }

        [FieldInfo("update_user_id")]
        public int UpdateUserId { get; set; }

        [FieldInfo("update_date")]
        public DateTime UpdateDate { get; set; }

        [FieldInfo("is_public")]
        public int IsPublic { get; set; }

        [FieldInfo("is_active")]
        public int IsActive { get; set; }
    }
}