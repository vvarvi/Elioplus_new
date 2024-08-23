using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.ImagesHelper;
using WdS.ElioPlus.Lib.Services.CRMs.Dynamics365API;
using WdS.ElioPlus.Lib.StripePayment;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        DBSession session = new DBSession();
        ElioSession vSession = new ElioSession();

        static Random rnd = new Random();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();
                //session.BeginTransaction();

                //List<ElioSnitcherWebsiteLeads> infoLeads = new List<ElioSnitcherWebsiteLeads>();

                //List<ElioAnonymousIpInfo> leadInfos = Sql.GetAnonymousIpInfoByInsertedStatus(0, session);
                //foreach (ElioAnonymousIpInfo leadInfo in leadInfos)
                //{
                //    if (!string.IsNullOrEmpty(leadInfo.CompanyDomain))
                //    {
                //        ElioAnonymousCompaniesInfo companyInfo = Lib.Services.TheCompaniesAPI.CompaniesServiceAPI.GetCompaniesInfo(leadInfo.CompanyDomain, session);

                //        if (companyInfo != null)
                //        {
                //            ElioSnitcherWebsiteLeads infoLead = Lib.Services.AnonymousTrackingAPI.SnitcherService.GetWebsiteLeadsFromInfo(companyInfo, leadInfo, session);

                //            if (infoLead != null)
                //                infoLeads.Add(infoLead);
                //        }
                //    }

                //    leadInfo.IsInserted = 1;

                //    DataLoader<ElioAnonymousIpInfo> loader = new DataLoader<ElioAnonymousIpInfo>(session);
                //    loader.Update(leadInfo);

                //    string message = string.Format("{0} leads were inserted to Elio from Anonymous Ip Info/The Companies API at {1}", infoLeads.Count.ToString(), DateTime.Now);

                //    Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GetAPILeads", message);
                //}

                //session.CommitTransaction();

                //Lib.Services.TheCompaniesAPI.CompaniesServiceAPI.GetCompaniesInfo(Request.QueryString["domain"], session);

                //Lib.Services.IpRegistryServiceAPI.IpRegistryServiceAPI.GetRegistryInfo(Request.QueryString["ip"], "", "", session);

                //Lib.Services.IpInfoAPI.IpInfoServiceAPI.GetInfo(Request.QueryString["ip"], "", "", session);

                //TaxId txId = Lib.Services.StripeAPI.StripeAPIService.CreateCustomerTaxIdApi("cus_MgtAd0km3E3BNX", "eu_vat", "EL987654321");

                //StripeList<Session> expSess = Lib.Services.StripeAPI.StripeAPIService.GetAllCheckoutSessionsApi();
                //if (expSess != null)
                //{
                //    foreach (Session item in expSess)
                //    {
                //        if (item.Status == "Open")
                //        {
                //            Session sess = Lib.Services.StripeAPI.StripeAPIService.ExpireCheckoutSessionForPriceAndCustomerApi(item.Id);
                //        }
                //    }
                //}
                //string sessId = "cs_test_b18mseY30MaYZhtoeZnFsiDr8kis8eDveiIwKiarjfvEAeCiU70oWcn66O";
                //ElioUsers user = Sql.GetUserById(1, session);
                //if (user != null)
                //{
                //    Session ses = Lib.Services.StripeAPI.StripeAPIService.CreateCheckoutSessionForPriceAndCustomerApi("plan_E7PanZ26kWbC9K", user);
                //    if (ses != null)
                //    {
                //        string lnk = ses.Url;
                //        sessId = ses.Id;

                //        Response.Redirect(lnk, false);
                //    }
                //}

                //Session createdSession = Lib.Services.StripeAPI.StripeAPIService.GetCheckoutSessionBySessIdApi(sessId);
                //if (createdSession != null)
                //{

                //}

                //Customer cus = Lib.Services.StripeAPI.StripeAPIService.DeleteCustomerApi("cus_Nn2KhmVzzVYOdF");
                //if (cus != null && (bool)cus.Deleted)
                //{

                //}

                //FixRegisteredUsersProductForSpecificVerticals(session);
                //FixForSpecificThirdPartyUsersPartnerProgramMSP(session);
                //FixThirdPartyUsersPartnerProgramsForSpecificProducts(session);
                //FixThirdPartyUsersPartnerProgramsForSpecificVerticals(session);

                //Card card = Lib.Services.StripeAPI.StripeAPIService.DeleteCreditCardNewUnderAccountApi("acct_1MdLQpPAeFxPYI8E", "cus_NS7wkzawsMNPhb", "card_1MhDhlPAeFxPYI8EorSZUrq8");
                //if (card != null)
                //{
                //    if ((bool)card.Deleted)
                //    {

                //    }
                //}

                //Stripe.Customer delCust1 = Lib.Services.StripeAPI.StripeAPIService.DeleteCustomerToAccountApi("cus_NO93G913awP7Cx", "acct_1MdLQpPAeFxPYI8E");
                //Stripe.Customer delCust2 = Lib.Services.StripeAPI.StripeAPIService.DeleteCustomerToAccountApi("cus_NO8nF1TxvR39QP", "acct_1MdLQpPAeFxPYI8E");

                //Stripe.Account delAcc = Lib.Services.StripeAPI.StripeAPIService.DeleteAccountApi("acct_1McSq0PAcW1n0nhL");

                //if (delAcc != null)
                //{
                //    bool deleted = (bool)delAcc.Deleted;
                //    return;
                //}

                //AccountLink accountLink = Lib.Services.StripeAPI.StripeAPIService.CreateAccountLinkApi("acct_1MdLQpPAeFxPYI8E");

                //if (accountLink != null && !string.IsNullOrEmpty(accountLink.Url))
                //{
                //    //aConfigureAccountOnboarding.Target = "_blank";
                //    Response.Redirect(accountLink.Url, false);
                //}

                //Stripe.Account account = Lib.Services.StripeAPI.StripeAPIService.CreateAccountApi("express", "GR", "vvarvi@yahoo.gr", "company");

                //Lib.Services.StripeAPI.StripeService.CreateDirectCharge("vvarvi@gmail.com", 15000, 10, "eur", "card_1MdMXaPAeFxPYI8EaTjbTKWa", "acct_1MdLQpPAeFxPYI8E", "cus_NO8nF1TxvR39QP");

                //PaymentIntent payment = Lib.Services.StripeAPI.StripeAPIService.CreatePaymentIntentApi("acct_1MdLQpPAeFxPYI8E", "cus_NO93G913awP7Cx", 10000, 1000, "usd");

                //Stripe.Account delAcc = Lib.Services.StripeAPI.StripeAPIService.DeleteAccountApi("acct_1MHvaKPAkO1u1sdi");

                //ElioUsers user = Sql.GetUserById(2732, session);
                //if (user != null)
                //    ClearBit.FindCombinedPersonCompanyByEmail_v2(user, user.Email, session);

                //InsertUsersVerticalsByProducts(5);

                //UpdateUsersStatesCities(session);

                //FixSnitcherProducts(true);
                //FixUsersRegions();

                //bool exists = Lib.Services.CRMs.Dynamics365API.DNMLib.ExistContactForDublicateByDomainEmailAddress(1, "someone_c123@example.com", session);

                //ElioRegistrationDeals deal = Sql.GetDealById(143, session);
                //if (deal != null)
                //Lib.Services.CRMs.Dynamics365API.DNMLib.ReOpenOpportunity(deal, session);

                //ConnectDynamicsAndGetSystemUser(false, 1, Guid.Empty);

                //InsertContact(session);
                //GetOpportunity();
                //ConnectDynamicsAndGetSystemUser();

                //ExportPdf(Convert.ToInt32(Request.QueryString["userID"]), Convert.ToInt32(Request.QueryString["paymentID"]));

                //vSession.User = Sql.GetUserById(1, session);
                //session.BeginTransaction();
                //InsertUsersOrderPayments();
                //FixUsersEmailNotificationsData();

                //FixEmailNotificationsDataByUser();

                //if (!string.IsNullOrEmpty(Request.QueryString["scenario"]))
                //{
                //    int scenario = Convert.ToInt32(Request.QueryString["scenario"].ToString());

                //    if (scenario > 0)
                //    {
                //        //FixRegisteredUsersVerticalsForSpecificProducts(scenario, session);
                //        //WriteXML.WriteAllSiteMapFiles(scenario, 0);
                //        //WriteXML.WriteSiteMapFile(scenario, 0, session);
                //    }
                //}

                //ClearBit.FindByEmail("vangelis.varvitsiotis@peoplecert.org");

                //session.CommitTransaction();

                //SendMailjetEmails();
                //Lib.Services.MailJetAPI.MailjetSenderLib.SendEmailRunAsync("info@elioplus.com", "Elioplus", "vvarvi@gmail.com", "Varvitsiotis Evangelos", "Test email message", "This is a test message using mailjet version 3.1", "").Wait(3000);
                //FixCollaborationLibraryFilesPath();
                //FixOnboardingLibraryFilesPath();

                //GetSetCurrentCulture();

                //string encryptedPassword = EncryptMD5String("ravi.k@ozonetel.com@");
            }
            catch (Exception ex)
            {
                session.RollBackTransaction();
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        private void InsertUsersVerticalsByProducts(int caseIds)
        {
            string ids = "";
            int groupItemId = 0;
            string strQuery = "";
            DataTable usersTbs = null;
            int groupId = 0;

            switch (caseIds)
            {
                case 1:     // users count: 544
                    ids = "740,992,4150,4332";
                    groupId = 4;        //  Operations & Workflow
                    groupItemId = 141;  //  ITSM

                    strQuery = @"SELECT distinct [user_id]
                                FROM [Elioplus_DB].[dbo].[Elio_users_registration_products]
                                where reg_products_id in
                                (" + ids + ") " +
                                "and user_id not in " +
                                "( " +
                                "     select user_id " +
                                "     from Elio_users_sub_industries_group_items usigi " +
                                "     where sub_industry_group_item_id = " + groupItemId + "" +
                                ")";
                    break;

                case 2:     // users count: 354
                    ids = "5950,5989,359,16132,3513,3221";
                    groupId = 4;        //  Operations & Workflow
                    groupItemId = 129;  //  Robotic Process Automation

                    strQuery = @"SELECT distinct [user_id]
                                FROM [Elioplus_DB].[dbo].[Elio_users_registration_products]
                                where reg_products_id in
                                (" + ids + ") " +
                                "and user_id not in " +
                                "( " +
                                "     select user_id " +
                                "     from Elio_users_sub_industries_group_items usigi " +
                                "     where sub_industry_group_item_id = " + groupItemId + "" +
                                ")";
                    break;

                case 3:     // users count: 1442
                    ids = "35,516,1473,1920,2011,2060,3206,3938,4557,4770,5143";
                    groupId = 14;       //  Unified Communications
                    groupItemId = 131;  //  Contact Center

                    strQuery = @"SELECT distinct [user_id]
                                FROM [Elioplus_DB].[dbo].[Elio_users_registration_products]
                                where reg_products_id in
                                (" + ids + ") " +
                                "and user_id not in " +
                                "( " +
                                "     select user_id " +
                                "     from Elio_users_sub_industries_group_items usigi " +
                                "     where sub_industry_group_item_id = " + groupItemId + "" +
                                ")";
                    break;

                case 4:     // users count: 114
                    ids = "1761";       //  Esri
                    groupId = 15;       //  CAD & PLM
                    groupItemId = 144;  //  GIS

                    strQuery = @"SELECT distinct [user_id]
                                FROM [Elioplus_DB].[dbo].[Elio_users_registration_products]
                                where reg_products_id in
                                (" + ids + ") " +
                                "and user_id not in " +
                                "( " +
                                "     select user_id " +
                                "     from Elio_users_sub_industries_group_items usigi " +
                                "     where sub_industry_group_item_id = " + groupItemId + "" +
                                ")";
                    break;

                case 5:     // users count: 159
                    groupId = 15;       //  CAD & PLM
                    groupItemId = 144;  //  GIS

                    strQuery = @"select distinct id as user_id
                                    from elio_users
                                    where email in
                                    (
                                    'nicole@2ndnaturewater.com'
                                    ,'chris@39dn.com'
                                    ,'pparker@3-gis.com'
                                    ,'jims@911datamaster.com'
                                    ,'d.jonas@aamgroup.com'
                                    ,'alex.jevremov@abcsurveys.ae'
                                    ,'andy.stewart@activeg.com'
                                    ,'info@aegeaneg.com'
                                    ,'rao.chandu@atsmap.com'
                                    ,'holtz@aipcomputersysteme.de'
                                    ,'lucas.guenther@airclip.de'
                                    ,'adolfo@aleziteodolini.com'
                                    ,'info@aliaict.com'
                                    ,'sherif.awad@alkancit.com'
                                    ,'deb@allpointsgis.com'
                                    ,'gm@almasaha.com'
                                    ,'info@amsconsulting.net'
                                    ,'satyam.dave@analyticsolution.co.in' 
                                    ,'carlos.godinho@aquasis.pt'
                                    ,'info@arcbangladesh.com'
                                    ,'fouad@ardglobal.com'
                                    ,'brady.hustad@argis.com'
                                    ,'anton.malyshenko@arcada.com.ua'
                                    ,'ybogdanov@asidek.es'
                                    ,'stefano.sorge@aster-te.it'
                                    ,'bruce.turner@aurecongroup.com'
                                    ,'jcampbell@avineon.com'
                                    ,'axesconseil@axes.fr'
                                    ,'sales@bandwork.com.my'
                                    ,'barata@barata-technology.com'
                                    ,'tracy@batchgeo.com'
                                    ,'kocatuerk@below-software.de'
                                    ,'rprater@bentearsolutions.com'
                                    ,'info@binaco.net'
                                    ,'claudiu.toma@blacklight.ro'
                                    ,'mlippmann@blueraster.com'
                                    ,'aabeyta@bootcampgis.com'
                                    ,'aiqbal@bpmadvisors.com'
                                    ,'Bratt@c2l-equipment.com'
                                    ,'ramjayesh@capricot.com'
                                    ,'dileep@cgulfc.com'
                                    ,'george_zhao@chcnav.com'
                                    ,'asimpson@harriscomputer.com'
                                    ,'gmachado@compassdatainc.com'
                                    ,'welcome@compusense.in'
                                    ,'sonja.goodwin@contractlandstaff.com'
                                    ,'bill@credent-asia.com'
                                    ,'Anil.Mishra@csscorp.com'
                                    ,'gary.ruck@deighton.com'
                                    ,'f.delgado@demo.es'
                                    ,'tania.uszacki@divestco.com'
                                    ,'ppowell@dmtispatial.com'
                                    ,'mdolan@digforenergy.com'
                                    ,'sarah_macdonald@eagle.co.nz'
                                    ,'kevin.meredith@edsi.com'
                                    ,'casandra@elecdata.com'
                                    ,'info@e-mage.com.my'
                                    ,'herb@ermaps.com'
                                    ,'pbulavin@etele.com.ua'
                                    ,'contact@fis.com.vn'
                                    ,'bonnie.freeman@freemangis.com'
                                    ,'djones@frontierprecision.com'
                                    ,'Deepak.Rao@igenesys.com'
                                    ,'jason.knowles@geoacuity.com'
                                    ,'jkolt@geocortex.com'
                                    ,'j.zuidam@geodirect.nl'
                                    ,'stumpfk@grsgis.com'
                                    ,'info@geologicdata.com'
                                    ,'jharris@geologic.com'
                                    ,'rajkanna@geomatiques.com' 
                                    ,'egoff@geomorphis.com'
                                    ,'ajo@geopartner.dk'
                                    ,'sales@geosense.co.za'
                                    ,'ventas@geosistemas.com.ar'
                                    ,'miguel.pelaz@gtbi.net'
                                    ,'aminor@geovironment.com'
                                    ,'manjunath@geovista.co.in'
                                    ,'wayne@geowize.com'
                                    ,'karem@3g-consult.de'
                                    ,'celina.hiller@gis-ag.com'
                                    ,'gis.contact@cdg.co.th'
                                    ,'anita@gisetc.com'
                                    ,'issam.attalah@gistec.com'
                                    ,'bbursey@great-circletech.com'
                                    ,'info@gttimaging.com'
                                    ,'diellef@h2safety.ca'
                                    ,'info@haldera.com.cy'
                                    ,'dkinnes@highlandgeocomp.com'
                                    ,'mahmoud.amer@idealsolutions.com'
                                    ,'syepez@idsecuador.com'
                                    ,'dnaiker@iengaust.com.au'
                                    ,'argena.hasa@ikubinfo.al'
                                    ,'mk@in22labs.com'
                                    ,'jjimenez@isigis.net'
                                    ,'prateek.srivastava@infiniumsolutionz.com'
                                    ,'ellen.nodwell@integrashare.com'
                                    ,'gis@integrated-informatics.com'
                                    ,'info@intercad.ch'
                                    ,'mdumford@interwestgrp.com'
                                    ,'md@itcraftltd.com'
                                    ,'klawrence@itnexus.com'
                                    ,'nick@landinfo.com'
                                    ,'walterdosantos@latinproof.com.ar'
                                    ,'jody@lhgis.com'
                                    ,'rroundtree@lmkr.com'
                                    ,'pete.lund@lgan.com'
                                    ,'joe.fuller@mg-aec.com'
                                    ,'gautam.bando@meatechsolutions.com'
                                    ,'gary.outlaw@merrick.com'
                                    ,'bmiller@millerspatial.com'
                                    ,'bernard.baloukjy@mis.com.sa'
                                    ,'david@navagis.com'
                                    ,'smk@neighbor21.co.kr'
                                    ,'kland@neuralog.com'
                                    ,'tony.lopez@newcenturysoftware.com'
                                    ,'bdaugherty@newedgeservices.com'
                                    ,'mw@nois.no'
                                    ,'trip@northlinegis.com'
                                    ,'tripras@nsi.co.id'
                                    ,'ori@ofek-air.com'
                                    ,'arodgers@orcamaritime.com'
                                    ,'aisterabadi@placeworks.com'
                                    ,'iijima@pragmatica.jp'
                                    ,'info@prime-pacific.com'
                                    ,'TomPattison@PriorITConsulting.com'
                                    ,'kkirk@ppeng.com'
                                    ,'jmelendez@prxtreme.com'
                                    ,'allison.kooiman@psomas.com'
                                    ,'mark@purplelandmgmt.com'
                                    ,'mikek@quadknopf.com'
                                    ,'jodi@quarticsolutions.com'
                                    ,'adilan@risk.az'
                                    ,'jroger@rcp.com'
                                    ,'bob@redhensystems.com'
                                    ,'massimo@rendercad.it'
                                    ,'rtagle@hartenergy.com'
                                    ,'bplaird@rickengineering.com'
                                    ,'contact@sabitsoft.com'
                                    ,'magbozo@sambusgeospatial.com'
                                    ,'jcopple@sanborn.com'
                                    ,'gustavo.lopez@septentrio.com'
                                    ,'jacob.maggard@setld.com'
                                    ,'warrens@sgm-inc.com'
                                    ,'anne.herpers@siggis.fr'
                                    ,'akash@skygroup.co.in'
                                    ,'davor@sl-king.com'
                                    ,'informa@solucionesgeoinformaticas.com'
                                    ,'scarvajal@soporta.cl'
                                    ,'will@sparkgeo.com'
                                    ,'martha@sparrowds.com'
                                    ,'dennis.beck@spatialbiz.com'
                                    ,'sharon@spatialsolutionsgroup.com'
                                    ,'peter@spire.com'
                                    ,'novat@plzen.eu'
                                    ,'christopher.ash@sspinnovations.com'
                                    ,'info@sunriseteleinfra.com'
                                    ,'ktohn@suntactechnologies.com'
                                    ,'jack@svoa.co.th'
                                    ,'dlandry@sygif.qc.ca'
                                    ,'pavelp@systematics.co.il'
                                    ,'jmorse@tbplanning.com'
                                    ,'rf@tcarta.com'
                                    ,'kevin.davis@spx.com'
                                    ,'ajach@terramap.pl'
                                    ,'edward.murray@iglobe.co.za'
                                    ,'joe.bulman@thesolutionstack.com'
                                    ,'raju@thematicsinfotech.com'
                                    ,'dawn.antonucci@towill.com'
                                    ,'spyros@treecomp.gr'
                                    ,'giri.m@tricadinfo.com'
                                    ,'info@trion.co.jp'
                                    ,'jsanchez@tronix.com.py'
                                    ,'info@uacsi.com'
                                    ,'softech@unisolindia.co.in'
                                    ,'veljko.fustic@vekom.com'
                                    ,'steve.grise@vertex3.com'
                                    ,'astackhouse@vestra.com'
                                    ,'matthew.petras@vicaninnovations.com'
                                    ,'skakileti@vnuit.com'
                                    ,'brian@voyagersearch.com'
                                    ,'ashah@VSolvit.com'
                                    ,'wg@wigeogis.com'
                                    ,'jon.downey@woolpert.com'
                                    ,'kevin.edmundson@wthgis.com'
                                    )
                                    and id not in 
                                    (
                                         select user_id 
                                         from Elio_users_sub_industries_group_items usigi 
                                         where sub_industry_group_item_id = 144
                                    )";
                    break;
            }

            if (groupId > 0)
            {

            }

            usersTbs = session.GetDataTable(strQuery);

            if (usersTbs.Rows.Count > 0)
            {
                foreach (DataRow row in usersTbs.Rows)
                {
                    ElioUsersSubIndustriesGroupItems groupItem = new ElioUsersSubIndustriesGroupItems();

                    groupItem.UserId = Convert.ToInt32(row["user_id"]);
                    groupItem.SubIndustryGroupItemId = 144;
                    groupItem.SubIndustryGroupId = 15;

                    DataLoader<ElioUsersSubIndustriesGroupItems> loader = new DataLoader<ElioUsersSubIndustriesGroupItems>(session);
                    loader.Insert(groupItem);
                }
            }
        }

        private void FixThirdPartyUsersPartnerProgramsForSpecificVerticals(DBSession session)
        {
            DataTable usersVert = session.GetDataTable(@"select distinct u.id as user_id
                                                        from elio_users u
                                                        inner join Elio_users_sub_industries_group_items usigi
	                                                        on u.id = usigi.user_id
		                                                        and sub_industry_group_item_id in
		                                                        (
			                                                        19, //Help Desk
			                                                        59, //Cloud Management
			                                                        61, //Remote Access
			                                                        141 //ITSM    
		                                                        )
                                                        and u.id not in
                                                        (
                                                            select user_id
                                                            from Elio_users_partners urp
                                                            where u.id = urp.user_id
                                                            and urp.partner_id = 7  //Managed Service Provider
                                                        )
                                                        where account_status = 1
                                                        and company_type = 'Channel Partners'
                                                        and user_application_type = 2
                                                        order by u.id"
                                                    );

            if (usersVert.Rows.Count > 0)
            {
                foreach (DataRow row in usersVert.Rows)
                {
                    if (row["user_id"].ToString() != "")
                    {
                        int userId = Convert.ToInt32(row["user_id"].ToString());

                        if (userId > 0)
                        {
                            try
                            {
                                session.BeginTransaction();

                                Logger.Debug("Insert partner program for user id: " + userId + " started!");

                                DataLoader<ElioUsersPartners> loader = new DataLoader<ElioUsersPartners>(session);

                                ElioUsersPartners uPartner = new ElioUsersPartners();

                                uPartner.UserId = userId;
                                uPartner.PartnerId = 7; //Managed Service Provider

                                loader.Insert(uPartner);

                                session.CommitTransaction();
                                
                                Logger.Debug("Partner Program for user id: " + userId + " inserted successfully!");
                                Logger.Debug("----------------------------------------------------------");
                            }
                            catch (Exception ex)
                            {
                                session.RollBackTransaction();
                                Logger.Debug("ERROR --> Partner Program for user id: " + userId + " not inserted!", ex.StackTrace, ex.Message);
                            }
                        }
                    }
                }
            }
        }

        private void FixForSpecificThirdPartyUsersPartnerProgramMSP(DBSession session)
        {
            DataTable users = session.GetDataTable(@"select u.id as user_id
                                                        from elio_users	u
                                                        where account_status = 1
                                                        and company_type = 'Channel Partners'
                                                        and user_application_type = 2
                                                        --and country = 'United States'
                                                        and u.id not in
                                                        (
                                                            select user_id
                                                            from Elio_users_partners urp
                                                            where u.id = urp.user_id
                                                            and urp.partner_id = 7
                                                        )
                                                        and u.id in
                                                        (
                                                        86642,
                                                        87846,
                                                        87850,
                                                        87924,
                                                        89008,
                                                        90069,
                                                        90202,
                                                        91306,
                                                        93468,
                                                        93577,
                                                        93651,
                                                        94882,
                                                        95964,
                                                        95999,
                                                        97136,
                                                        98226,
                                                        100639,
                                                        100641,
                                                        100877,
                                                        100878,
                                                        102212,
                                                        102479,
                                                        102510,
                                                        102798,
                                                        102866,
                                                        103001,
                                                        103002,
                                                        103381,
                                                        103505,
                                                        103506,
                                                        103508,
                                                        103532,
                                                        104025,
                                                        104293,
                                                        104312,
                                                        104470,
                                                        104803,
                                                        105379,
                                                        105380,
                                                        105702,
                                                        106458,
                                                        106522,
                                                        106838,
                                                        107193,
                                                        107219,
                                                        107688,
                                                        107693,
                                                        107699,
                                                        108895,
                                                        108940,
                                                        108957,
                                                        108961,
                                                        108962,
                                                        109033,
                                                        109131,
                                                        110389,
                                                        110457,
                                                        110458,
                                                        110628,
                                                        110630,
                                                        110634,
                                                        110636,
                                                        110682,
                                                        110792,
                                                        110823,
                                                        110903,
                                                        110930,
                                                        110965,
                                                        110968,
                                                        110969,
                                                        110971,
                                                        112056,
                                                        112073,
                                                        112084,
                                                        112232,
                                                        112236,
                                                        112704,
                                                        113219,
                                                        113220,
                                                        113223,
                                                        113364,
                                                        114333,
                                                        114432,
                                                        114963,
                                                        115384,
                                                        115739,
                                                        115796,
                                                        115798,
                                                        115801,
                                                        115907,
                                                        118556,
                                                        118560,
                                                        118871,
                                                        120419,
                                                        121961,
                                                        123323,
                                                        125202,
                                                        125291,
                                                        125293,
                                                        125297,
                                                        125298,
                                                        126850,
                                                        127079,
                                                        133706,
                                                        137797,
                                                        137799,
                                                        138030,
                                                        138033,
                                                        138235,
                                                        138239,
                                                        138497,
                                                        138499,
                                                        138567,
                                                        138693,
                                                        139005,
                                                        139143,
                                                        139496,
                                                        139500,
                                                        139591,
                                                        139743,
                                                        139965,
                                                        140207,
                                                        140208,
                                                        140209,
                                                        140211,
                                                        140212,
                                                        140304,
                                                        140309,
                                                        140728,
                                                        140776,
                                                        141630,
                                                        52873,
                                                        52885,
                                                        52900,
                                                        52901,
                                                        53688,
                                                        53691,
                                                        54376,
                                                        54696,
                                                        54697,
                                                        54699,
                                                        54700,
                                                        54703,
                                                        54708,
                                                        54713,
                                                        54718,
                                                        54721,
                                                        54835,
                                                        55052,
                                                        55495,
                                                        55618,
                                                        55816,
                                                        55878,
                                                        55882,
                                                        55890,
                                                        55891,
                                                        55892,
                                                        55897,
                                                        56264,
                                                        56266,
                                                        56271,
                                                        56277,
                                                        56293,
                                                        56304,
                                                        56306,
                                                        56315,
                                                        56485,
                                                        57025,
                                                        57355,
                                                        57357,
                                                        57518,
                                                        57523,
                                                        57524,
                                                        57717,
                                                        57883,
                                                        57991,
                                                        58000,
                                                        58163,
                                                        58214,
                                                        58679,
                                                        58870,
                                                        58871,
                                                        60045,
                                                        62306,
                                                        62560,
                                                        62562,
                                                        62564,
                                                        62567,
                                                        63785,
                                                        63880,
                                                        64895,
                                                        64920,
                                                        68176,
                                                        69317,
                                                        72619,
                                                        72621,
                                                        72640,
                                                        72664,
                                                        74709,
                                                        75886,
                                                        77107,
                                                        77108,
                                                        77143,
                                                        77168,
                                                        77173,
                                                        77458,
                                                        79514,
                                                        79515,
                                                        79516,
                                                        79518,
                                                        79529,
                                                        79743,
                                                        80930,
                                                        80931,
                                                        80933,
                                                        80936,
                                                        81025,
                                                        81026,
                                                        81027,
                                                        81030,
                                                        81031,
                                                        81084,
                                                        81086,
                                                        81141,
                                                        84367,
                                                        84369,
                                                        84414,
                                                        85468,
                                                        85511,
                                                        85512,
                                                        37947,
                                                        37948,
                                                        38010,
                                                        38011,
                                                        38213,
                                                        38214,
                                                        38218,
                                                        38279,
                                                        38767,
                                                        38768,
                                                        38770,
                                                        38841,
                                                        38883,
                                                        38884,
                                                        39097,
                                                        39098,
                                                        39166,
                                                        39214,
                                                        39287,
                                                        39288,
                                                        39333,
                                                        39458,
                                                        39581,
                                                        39586,
                                                        39588,
                                                        39592,
                                                        39594,
                                                        39601,
                                                        39696,
                                                        39697,
                                                        39700,
                                                        39752,
                                                        39754,
                                                        39755,
                                                        39789,
                                                        39918,
                                                        39929,
                                                        40128,
                                                        40129,
                                                        40468,
                                                        41151,
                                                        41372,
                                                        41738,
                                                        41889,
                                                        41890,
                                                        41916,
                                                        42117,
                                                        42140,
                                                        42141,
                                                        42152,
                                                        42900,
                                                        43355,
                                                        43852,
                                                        44707,
                                                        44708,
                                                        44709,
                                                        44711,
                                                        44713,
                                                        44723,
                                                        44731,
                                                        44733,
                                                        44764,
                                                        44767,
                                                        44769,
                                                        44770,
                                                        44771,
                                                        44774,
                                                        44779,
                                                        44781,
                                                        44899,
                                                        44962,
                                                        44963,
                                                        44965,
                                                        44967,
                                                        44972,
                                                        45913,
                                                        46038,
                                                        46040,
                                                        46041,
                                                        46116,
                                                        46121,
                                                        46268,
                                                        46309,
                                                        46371,
                                                        46485,
                                                        46573,
                                                        46574,
                                                        46576,
                                                        46577,
                                                        46578,
                                                        46658,
                                                        46660,
                                                        46661,
                                                        46665,
                                                        46908,
                                                        47306,
                                                        49999,
                                                        50096,
                                                        50097,
                                                        50195,
                                                        50196,
                                                        50367,
                                                        50711,
                                                        50955,
                                                        31486,
                                                        31487,
                                                        31617,
                                                        32108,
                                                        32117,
                                                        32202,
                                                        32281,
                                                        32285,
                                                        32404,
                                                        32483,
                                                        32566,
                                                        32569,
                                                        32571,
                                                        32701,
                                                        32702,
                                                        32703,
                                                        32704,
                                                        32765,
                                                        32844,
                                                        32847,
                                                        32904,
                                                        32948,
                                                        32950,
                                                        32953,
                                                        32959,
                                                        32963,
                                                        33007,
                                                        33008,
                                                        33379,
                                                        33381,
                                                        33445,
                                                        33446,
                                                        35458,
                                                        35564,
                                                        35566,
                                                        35567,
                                                        35630,
                                                        35633,
                                                        35635,
                                                        35637,
                                                        35707,
                                                        35727,
                                                        35728,
                                                        35730,
                                                        35731,
                                                        35809,
                                                        35810,
                                                        35811,
                                                        35865,
                                                        35898,
                                                        35946,
                                                        35970,
                                                        36046,
                                                        36236,
                                                        36239,
                                                        36341,
                                                        36342,
                                                        36343,
                                                        36397,
                                                        36398,
                                                        36399,
                                                        36448,
                                                        36463,
                                                        36464,
                                                        36465,
                                                        36466,
                                                        36480,
                                                        36550,
                                                        36551,
                                                        36696,
                                                        36779,
                                                        36780,
                                                        36781,
                                                        36782,
                                                        36833,
                                                        36835,
                                                        36836,
                                                        36867,
                                                        36911,
                                                        37025,
                                                        37026,
                                                        37178,
                                                        37249,
                                                        37366,
                                                        37367,
                                                        37448,
                                                        37449,
                                                        37450,
                                                        37502,
                                                        37503,
                                                        37507,
                                                        37509,
                                                        37543,
                                                        37545,
                                                        37614,
                                                        37615,
                                                        37616,
                                                        37617,
                                                        37687,
                                                        37689,
                                                        37864,
                                                        37942,
                                                        37943,
                                                        25749,
                                                        26136,
                                                        26137,
                                                        26160,
                                                        26223,
                                                        26605,
                                                        27143,
                                                        27399,
                                                        27672,
                                                        27701,
                                                        27886,
                                                        27909,
                                                        27972,
                                                        27997,
                                                        28072,
                                                        28188,
                                                        28190,
                                                        28355,
                                                        28429,
                                                        28478,
                                                        28479,
                                                        28480,
                                                        28482,
                                                        28580,
                                                        28685,
                                                        28687,
                                                        28770,
                                                        28823,
                                                        28825,
                                                        28826,
                                                        28963,
                                                        28964,
                                                        28967,
                                                        29025,
                                                        29028,
                                                        29101,
                                                        29102,
                                                        29135,
                                                        29136,
                                                        29139,
                                                        29226,
                                                        29227,
                                                        29302,
                                                        29304,
                                                        29440,
                                                        29442,
                                                        29443,
                                                        29511,
                                                        29512,
                                                        29513,
                                                        29515,
                                                        29516,
                                                        29613,
                                                        29694,
                                                        29697,
                                                        29778,
                                                        29779,
                                                        29780,
                                                        29781,
                                                        29882,
                                                        30042,
                                                        30049,
                                                        30152,
                                                        30401,
                                                        30777,
                                                        30781,
                                                        30862,
                                                        30864,
                                                        30865,
                                                        30878,
                                                        30927,
                                                        31086,
                                                        31093,
                                                        31167,
                                                        31230,
                                                        21932,
                                                        21936,
                                                        22003,
                                                        22005,
                                                        22006,
                                                        22045,
                                                        22186,
                                                        22188,
                                                        22190,
                                                        22291,
                                                        22298,
                                                        22600,
                                                        22601,
                                                        22737,
                                                        22738,
                                                        22849,
                                                        22854,
                                                        22856,
                                                        22858,
                                                        22861,
                                                        22871,
                                                        22980,
                                                        22986,
                                                        23215,
                                                        23216,
                                                        23339,
                                                        23342,
                                                        23344,
                                                        23415,
                                                        23417,
                                                        23420,
                                                        23492,
                                                        23494,
                                                        23499,
                                                        23500,
                                                        23501,
                                                        23607,
                                                        23609,
                                                        23611,
                                                        23614,
                                                        23651,
                                                        23653,
                                                        23654,
                                                        23656,
                                                        23705,
                                                        23707,
                                                        23708,
                                                        23713,
                                                        23852,
                                                        23855,
                                                        23858,
                                                        23862,
                                                        23934,
                                                        23936,
                                                        23937,
                                                        24086,
                                                        24089,
                                                        24154,
                                                        24157,
                                                        24160,
                                                        24161,
                                                        24237,
                                                        24238,
                                                        24241,
                                                        24328,
                                                        24329,
                                                        24335,
                                                        24337,
                                                        24404,
                                                        24409,
                                                        24414,
                                                        24490,
                                                        24497,
                                                        24577,
                                                        24578,
                                                        24585,
                                                        24586,
                                                        24669,
                                                        24670,
                                                        24677,
                                                        24678,
                                                        24773,
                                                        24837,
                                                        24847,
                                                        25312,
                                                        25313,
                                                        25465,
                                                        25472,
                                                        25475,
                                                        25479,
                                                        25566,
                                                        25620,
                                                        25621,
                                                        25691,
                                                        25742,
                                                        19306,
                                                        19307,
                                                        19360,
                                                        19365,
                                                        19393,
                                                        19419,
                                                        19445,
                                                        19470,
                                                        19481,
                                                        19495,
                                                        19496,
                                                        19497,
                                                        19498,
                                                        19503,
                                                        19555,
                                                        19566,
                                                        19606,
                                                        19613,
                                                        19616,
                                                        19618,
                                                        19691,
                                                        19692,
                                                        19767,
                                                        19768,
                                                        19770,
                                                        19771,
                                                        19774,
                                                        19775,
                                                        19822,
                                                        19825,
                                                        19826,
                                                        19865,
                                                        19868,
                                                        19870,
                                                        19871,
                                                        19872,
                                                        19874,
                                                        19924,
                                                        19965,
                                                        19974,
                                                        19975,
                                                        19977,
                                                        19979,
                                                        19982,
                                                        19983,
                                                        19985,
                                                        20004,
                                                        20082,
                                                        20086,
                                                        20090,
                                                        20135,
                                                        20138,
                                                        20166,
                                                        20168,
                                                        20170,
                                                        20171,
                                                        20173,
                                                        20232,
                                                        20235,
                                                        20310,
                                                        20311,
                                                        20312,
                                                        20315,
                                                        20318,
                                                        20319,
                                                        20405,
                                                        20493,
                                                        20495,
                                                        20501,
                                                        20581,
                                                        20583,
                                                        20585,
                                                        20586,
                                                        20587,
                                                        20588,
                                                        20592,
                                                        20625,
                                                        20665,
                                                        20666,
                                                        20667,
                                                        20669,
                                                        20710,
                                                        20719,
                                                        20725,
                                                        20756,
                                                        20777,
                                                        20778,
                                                        20779,
                                                        20781,
                                                        20782,
                                                        20783,
                                                        20784,
                                                        20785,
                                                        20875,
                                                        20878,
                                                        20879,
                                                        20880,
                                                        20881,
                                                        20956,
                                                        20958,
                                                        20960,
                                                        20963,
                                                        20965,
                                                        20966,
                                                        20967,
                                                        21007,
                                                        21057,
                                                        21062,
                                                        21063,
                                                        21064,
                                                        21065,
                                                        21066,
                                                        21067,
                                                        21068,
                                                        21070,
                                                        21071,
                                                        21072,
                                                        21073,
                                                        21192,
                                                        21235,
                                                        21242,
                                                        21244,
                                                        21333,
                                                        21343,
                                                        21410,
                                                        21474,
                                                        21527,
                                                        21530,
                                                        21628,
                                                        21636,
                                                        21693,
                                                        21699,
                                                        21700,
                                                        21837,
                                                        21843,
                                                        21844,
                                                        21847,
                                                        14455,
                                                        14556,
                                                        14557,
                                                        14558,
                                                        14559,
                                                        14608,
                                                        14855,
                                                        15075,
                                                        15334,
                                                        15605,
                                                        15630,
                                                        15665,
                                                        15743,
                                                        15815,
                                                        15867,
                                                        15939,
                                                        16016,
                                                        16089,
                                                        16127,
                                                        16203,
                                                        16204,
                                                        16209,
                                                        16475,
                                                        16706,
                                                        16946,
                                                        16971,
                                                        17020,
                                                        17101,
                                                        17146,
                                                        17346,
                                                        17368,
                                                        17509,
                                                        17523,
                                                        17635,
                                                        17640,
                                                        17791,
                                                        17839,
                                                        17840,
                                                        17841,
                                                        17843,
                                                        17844,
                                                        17895,
                                                        17983,
                                                        18118,
                                                        18120,
                                                        18127,
                                                        18163,
                                                        18189,
                                                        18207,
                                                        18268,
                                                        18273,
                                                        18277,
                                                        18278,
                                                        18280,
                                                        18282,
                                                        18284,
                                                        18316,
                                                        18324,
                                                        18325,
                                                        18326,
                                                        18353,
                                                        18395,
                                                        18396,
                                                        18408,
                                                        18464,
                                                        18465,
                                                        18466,
                                                        18468,
                                                        18469,
                                                        18473,
                                                        18474,
                                                        18573,
                                                        18587,
                                                        18588,
                                                        18589,
                                                        18592,
                                                        18595,
                                                        18661,
                                                        18664,
                                                        18665,
                                                        18666,
                                                        18667,
                                                        18670,
                                                        18671,
                                                        18692,
                                                        18695,
                                                        18731,
                                                        18734,
                                                        18736,
                                                        18738,
                                                        18767,
                                                        18802,
                                                        18803,
                                                        18804,
                                                        18805,
                                                        18832,
                                                        18865,
                                                        18867,
                                                        18868,
                                                        18870,
                                                        18873,
                                                        18874,
                                                        18957,
                                                        18960,
                                                        18964,
                                                        19058,
                                                        19097,
                                                        19099,
                                                        19191,
                                                        19192,
                                                        19193,
                                                        19195,
                                                        19197,
                                                        19198,
                                                        19201,
                                                        19203,
                                                        19230,
                                                        19232,
                                                        19284,
                                                        19285,
                                                        19286,
                                                        19288,
                                                        19289,
                                                        19292,
                                                        2329,
                                                        2546,
                                                        2596,
                                                        2732,
                                                        2804,
                                                        3028,
                                                        3149,
                                                        3236,
                                                        3252,
                                                        3255,
                                                        3277,
                                                        3304,
                                                        3325,
                                                        3367,
                                                        3382,
                                                        3655,
                                                        3746,
                                                        3978,
                                                        4391,
                                                        4506,
                                                        4879,
                                                        5136,
                                                        5141,
                                                        5150,
                                                        5455,
                                                        5725,
                                                        5797,
                                                        5860,
                                                        5882,
                                                        5914,
                                                        6030,
                                                        6383,
                                                        6543,
                                                        6928,
                                                        7299,
                                                        7342,
                                                        7494,
                                                        7577,
                                                        7660,
                                                        7709,
                                                        8389,
                                                        8687,
                                                        8716,
                                                        8718,
                                                        8753,
                                                        8809,
                                                        9069,
                                                        9212,
                                                        9276,
                                                        9445,
                                                        9470,
                                                        9565,
                                                        9718,
                                                        9802,
                                                        9868,
                                                        10015,
                                                        10016,
                                                        10104,
                                                        10247,
                                                        10250,
                                                        10259,
                                                        10304,
                                                        10306,
                                                        10470,
                                                        10504,
                                                        10538,
                                                        10539,
                                                        10664,
                                                        10665,
                                                        10666,
                                                        10686,
                                                        10693,
                                                        10744,
                                                        10778,
                                                        10822,
                                                        10823,
                                                        10824,
                                                        10825,
                                                        10844,
                                                        10867,
                                                        10944,
                                                        10987,
                                                        11028,
                                                        11030,
                                                        11081,
                                                        11082,
                                                        11131,
                                                        11132,
                                                        11163,
                                                        11164,
                                                        11166,
                                                        11191,
                                                        11240,
                                                        11242,
                                                        11245,
                                                        11314,
                                                        11439,
                                                        11543,
                                                        12006,
                                                        12007,
                                                        12178,
                                                        13220,
                                                        13557,
                                                        13594,
                                                        13688,
                                                        13689,
                                                        13691,
                                                        13720,
                                                        13727,
                                                        13760,
                                                        13843,
                                                        13894,
                                                        13918,
                                                        14169,
                                                        14171,
                                                        14242,
                                                        14283,
                                                        14298,
                                                        14385,
                                                        14386,
                                                        14388,
                                                        14428,
                                                        14452
                                                        )
                                                        order by u.id");

            if (users.Rows.Count > 0)
            {
                foreach (DataRow row in users.Rows)
                {
                    if (row["user_id"].ToString() != "")
                    {
                        int userId = Convert.ToInt32(row["user_id"].ToString());

                        if (userId > 0)
                        {
                            try
                            {
                                session.BeginTransaction();

                                Logger.Debug("Insert partner program for user id: " + userId + " started!");

                                DataLoader<ElioUsersPartners> loader = new DataLoader<ElioUsersPartners>(session);

                                ElioUsersPartners uPartner = new ElioUsersPartners();

                                uPartner.UserId = userId;
                                uPartner.PartnerId = 7; //Managed Service Provider

                                loader.Insert(uPartner);

                                session.CommitTransaction();

                                Logger.Debug("Partner Program for user id: " + userId + " inserted successfully!");
                                Logger.Debug("----------------------------------------------------------");
                            }
                            catch (Exception ex)
                            {
                                session.RollBackTransaction();
                                Logger.Debug("ERROR --> Partner Program for user id: " + userId + " not inserted!", ex.StackTrace, ex.Message);
                            }
                        }
                    }
                }
            }
        }

        private void FixThirdPartyUsersPartnerProgramsForSpecificProducts(DBSession session)
        {
            DataTable usersVert = session.GetDataTable(@"select distinct u.id as user_id
                                                        from elio_users u
                                                        inner join Elio_users_registration_products urp
	                                                        on u.id = urp.user_id
		                                                        and urp.reg_products_id in
		                                                        (
                                                                    56,	    //Accelo
                                                                    9581,	//Acronis
                                                                    266,    //Altaro
                                                                    507,    //Autotask
                                                                    17169,	//Citrix
                                                                    7338,	//ConnectWise
                                                                    2016,	//Freshservice
                                                                    2574,	//Jira
                                                                    2598,	//Jumpcloud
                                                                    2632,	//Kaseya
                                                                    2919,	//ManageEngine
                                                                    19251,	//MSP360
                                                                    3113,	//N-able
                                                                    21030,	//NinjaOne
                                                                    4236	//Site24x7
		                                                        )
                                                        and u.id not in
                                                        (
                                                            select user_id
                                                            from Elio_users_partners urp
                                                            where u.id = urp.user_id
                                                            and urp.partner_id = 7
                                                        )
                                                        where account_status = 1
                                                        and company_type = 'Channel Partners'
                                                        and user_application_type = 2
                                                        order by u.id "
                                                    );

            if (usersVert.Rows.Count > 0)
            {
                foreach (DataRow row in usersVert.Rows)
                {
                    if (row["user_id"].ToString() != "")
                    {
                        int userId = Convert.ToInt32(row["user_id"].ToString());

                        if (userId > 0)
                        {
                            try
                            {
                                session.BeginTransaction();

                                Logger.Debug("Insert partner program for user id: " + userId + " started!");

                                DataLoader<ElioUsersPartners> loader = new DataLoader<ElioUsersPartners>(session);

                                ElioUsersPartners uPartner = new ElioUsersPartners();

                                uPartner.UserId = userId;
                                uPartner.PartnerId = 7; //Managed Service Provider

                                loader.Insert(uPartner);

                                session.CommitTransaction();

                                Logger.Debug("Partner Program for user id: " + userId + " inserted successfully!");
                                Logger.Debug("----------------------------------------------------------");
                            }
                            catch (Exception ex)
                            {
                                session.RollBackTransaction();
                                Logger.Debug("ERROR --> Partner Program for user id: " + userId + " not inserted!", ex.StackTrace, ex.Message);
                            }
                        }
                    }
                }
            }
        }

        private void FixRegisteredUsersProductForSpecificVerticals(DBSession session)
        {
            DataTable usersVert = session.GetDataTable(@"SELECT distinct usigi.user_id
                                                        FROM Elio_users_sub_industries_group_items usigi
                                                        where 1 = 1
                                                        and sub_industry_group_item_id in
                                                        (
                                                            75, 76, 77, 78, 79, 89, 93, 94, 121, 122, 123, 124, 125, 142, 143
                                                        )
                                                        and usigi.user_id not in
                                                        (
                                                            select user_id
                                                            from Elio_users_registration_products urp
                                                            where usigi.user_id = urp.user_id
                                                            and urp.reg_products_id = 20507 //Cyber Security
                                                        )
                                                        order by usigi.user_id"
                                                    );

            if (usersVert.Rows.Count > 0)
            {
                foreach (DataRow row in usersVert.Rows)
                {
                    if (row["user_id"].ToString() != "")
                    {
                        int userId = Convert.ToInt32(row["user_id"].ToString());

                        if (userId > 0)
                        {                            
                            //DataTable tableProd = session.GetDataTable(@"select count(id) as count 
                            //                            FROM Elio_users_registration_products
                            //                            where user_id = @user_id
                            //                            and reg_products_id = 5359"
                            //                           , DatabaseHelper.CreateIntParameter("@user_id", userId));

                            //if (tableProd != null && tableProd.Rows.Count == 0 && Convert.ToInt32(tableProd.Rows[0]["count"]) == 0)
                            //{

                            try
                            {
                                session.BeginTransaction();

                                Logger.Debug("Insert product for user id: " + userId + " started!");

                                DataLoader<ElioUsersRegistrationProducts> loader = new DataLoader<ElioUsersRegistrationProducts>(session);

                                ElioUsersRegistrationProducts uProduct = new ElioUsersRegistrationProducts();

                                uProduct.UserId = userId;
                                uProduct.RegProductsId = 20507; //Cyber Security

                                loader.Insert(uProduct);

                                session.CommitTransaction();
                                //session.ExecuteQuery("insert into Elio_users_registration_products values(" + userId + ", 5359)");
                                //}

                                Logger.Debug("Product for user id: " + userId + " inserted successfully!");
                                Logger.Debug("----------------------------------------------------------");
                            }
                            catch(Exception ex)
                            {
                                session.RollBackTransaction();
                                Logger.Debug("ERROR --> Product for user id: " + userId + " not inserted!", ex.StackTrace, ex.Message);
                            }
                        }
                    }
                }
            }
        }

        private void FixRegisteredUsersVerticalsForSpecificProducts(int scenario, DBSession session)
        {
            string strQuery = "";
            int subIndustryGroupItemId = 0;
            if (scenario == 1)
            {
                strQuery = @"SELECT distinct urp.[user_id]
		                        FROM [Elioplus_DB].[dbo].[Elio_users_registration_products] urp
		                        where reg_products_id in
		                        (
			                        599,
			                        3023,
			                        3683,
			                        4841,
			                        6799
		                        )
                                and urp.user_id not in
		                                (
			                                select user_id
			                                from Elio_users_sub_industries_group_items usigi
			                                where usigi.user_id = urp.user_id
			                                and usigi.sub_industry_group_item_id in (142)
		                                )
		                                order by user_id";

                subIndustryGroupItemId = 142;
            }
            else if (scenario == 2)
            {
                strQuery = @"SELECT distinct urp.[user_id]
		                        FROM [Elioplus_DB].[dbo].[Elio_users_registration_products] urp
		                        where reg_products_id in
		                        (
			                        678,
                                    987,
                                    1754,
                                    5891,
                                    7955,
                                    20956,
                                    8356,
                                    2953,
                                    20775,
                                    6634,
                                    4351,
                                    6315
		                        )
                                and urp.user_id not in
		                        (
			                        select user_id
			                        from Elio_users_sub_industries_group_items usigi
			                        where usigi.user_id = urp.user_id
			                        and usigi.sub_industry_group_item_id in (143)
		                        )
		                        order by user_id";

                subIndustryGroupItemId = 143;
            }
            else
                return;

            //'McAfee', 'Symantec', 'Eset', 'Kaspersky', 'Forcepoint', 'Check Point', 'Sophos', 'Malwarebytes', 'Gdata', 'Quick Heal', 'Seqrite', 'Bitdefender'
            //678,
            //987,
            //1754,
            //5891,
            //7955,
            //20956,
            //8356,
            //2953,
            //20775,
            //6634,
            //4351,
            //6315

            DataTable usersProds = session.GetDataTable(strQuery);

            if (usersProds.Rows.Count > 0)
            {
                foreach (DataRow row in usersProds.Rows)
                {
                    if (row["user_id"].ToString() != "")
                    {
                        int userId = Convert.ToInt32(row["user_id"].ToString());

                        if (userId > 0)
                        {
                            try
                            {
                                session.BeginTransaction();

                                Logger.Debug("Insert Vertical for user id: " + userId + " started!");

                                DataLoader<ElioUsersSubIndustriesGroupItems> loader = new DataLoader<ElioUsersSubIndustriesGroupItems>(session);

                                ElioUsersSubIndustriesGroupItems uVert = new ElioUsersSubIndustriesGroupItems();

                                uVert.UserId = userId;
                                uVert.SubIndustryGroupItemId = subIndustryGroupItemId; //Email Security 143  Endpoint Security
                                uVert.SubIndustryGroupId = 11;

                                loader.Insert(uVert);

                                session.CommitTransaction();

                                Logger.Debug("Vertical for user id: " + userId + " inserted successfully!");
                                Logger.Debug("----------------------------------------------------------");
                            }
                            catch (Exception ex)
                            {
                                session.RollBackTransaction();
                                Logger.Debug("ERROR --> Vertical for user id: " + userId + " not inserted!", "Stack: " + ex.StackTrace);
                            }
                        }
                    }
                }
            }
        }

        private void UpdateUsersStatesCities(DBSession session)
        {
            Lib.Utils.ExcelLib.UpdateEmptyStatesCitiesToElioUsers(session);

        }

        private void FixSnitcherProducts(bool isQuickWay)
        {
            try
            {
                if (isQuickWay)
                {
                    DataTable table = session.GetDataTable(@"SELECT product,count(id) as count
                                                          FROM [Elioplus_DB].[dbo].[Elio_snitcher_leads_pageviews]
                                                          where 1 = 1  
                                                          and CHARINDEX('_', product) > 0
                                                          group by product
                                                          order by count(id) desc");

                    if (table != null && table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            try
                            {
                                session.BeginTransaction();

                                string prod = row["product"].ToString();
                                
                                Logger.Debug(string.Format("product:{0} updating records count {1}", prod, row["count"].ToString()));

                                if (prod != "" && (prod.Contains("_") || prod.Contains("and")))
                                {
                                    session.ExecuteQuery(@"update Elio_snitcher_leads_pageviews
                                                    set product = '" + prod.Replace("_", " ").Replace("and", "&") + "' " +
                                                            "where product = '" + prod + "'");
                                }

                                Logger.Debug(string.Format("Updated product:{0}", prod.Replace("_", " ").Replace("and", "&")));

                                session.CommitTransaction();
                            }
                            catch (Exception ex)
                            {
                                session.RollBackTransaction();

                                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            }
                        }
                    }
                }
                else
                {
                    DataLoader<ElioSnitcherLeadsPageviews> loader = new DataLoader<ElioSnitcherLeadsPageviews>(session);

                    List<ElioSnitcherLeadsPageviews> products = loader.Load(@"SELECT *
                                                                          FROM [Elioplus_DB].[dbo].[Elio_snitcher_leads_pageviews]
                                                                          where 1 = 1  
                                                                          and CHARINDEX('_', product) > 0");

                    foreach (ElioSnitcherLeadsPageviews view in products)
                    {
                        if (view.Product.Contains("_") || view.Product.Contains("and"))
                        {
                            view.Product = view.Product.Replace("_", " ").Replace("and", "&").Trim();

                            loader.Update(view);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private void FixUsersRegions()
        {
            try
            {
                DataTable userCountries = session.GetDataTable(@"SELECT distinct u.country,c.region
                                                                FROM elio_users u
                                                                inner join Elio_countries c
	                                                                on u.country = c.country_name
                                                                where isnull(u.country, '') != ''
                                                                and isnull(company_region, '') = ''
                                                                and user_application_type != 5
                                                                and isnull(c.region, '') != ''
                                                                order by u.country");

                if (userCountries.Rows.Count > 0)
                {
                    foreach (DataRow row in userCountries.Rows)
                    {
                        if (row["country"].ToString() != "" && row["region"].ToString() != "")
                        {
                            try
                            {
                                session.BeginTransaction();

                                session.ExecuteQuery(@"UPDATE Elio_users 
                                                SET company_region = @company_region 
                                                WHERE country = @country
                                                and isnull(company_region, '') = ''
                                                and user_application_type != 5"
                                                    , DatabaseHelper.CreateStringParameter("@company_region", row["region"].ToString())
                                                    , DatabaseHelper.CreateStringParameter("@country", row["country"].ToString()));

                                session.CommitTransaction();
                            }
                            catch (Exception ex)
                            {
                                session.RollBackTransaction();
                                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private void InsertContact(DBSession session)
        {
            try
            {
                //session.OpenConnection();

                ElioUsers partner = Sql.GetUserById(425, session);
                if (partner != null)
                {
                    Lib.Services.CRMs.Dynamics365API.DNMLib.CreateNewContactWithDublicateCheck(partner, session);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private SystemUser ConnectDynamicsAndGetSystemUser(bool hasConnection, int vendorId, Guid currentUserId)
        {
            try
            {
                SystemUser user = null;

                if (hasConnection)
                {
                    ElioCrmUserIntegrations vendorIntegration = Sql.GetUserCrmIntegrationByCrmID(vendorId, (int)Lib.Enums.Api.Dynamics, session);
                    if (vendorIntegration != null)
                    {
                        CrmServiceClient srv = CrmSeviceHelper.Connect("Connect_365", vendorIntegration.UserApiKey, vendorIntegration.UserApiSecretKey);

                        if (srv.IsReady)
                            user = Lib.Services.CRMs.Dynamics365API.DNMLib.GetSystemUserByID(srv, currentUserId);
                    }
                }
                else
                    user = Lib.Services.CRMs.Dynamics365API.DNMLib.GetSystemUser(vendorId, session);

                return user;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        private void GetOpportunity(int vendorId)
        {
            ElioCrmUserIntegrations vendorIntegration = Sql.GetUserCrmIntegrationByCrmID(vendorId, (int)Lib.Enums.Api.Dynamics, session);
            if (vendorIntegration != null)
            {
                CrmServiceClient srv = CrmSeviceHelper.Connect("Connect_365", vendorIntegration.UserApiKey, vendorIntegration.UserApiSecretKey);

                if (srv.IsReady)
                {
                    ColumnSet cols = new ColumnSet(true);

                    Opportunity opp = (Opportunity)srv.Retrieve(Opportunity.EntityLogicalName, new Guid("0dbc0beb-c498-ec11-b400-6045bd9066d4"), cols);
                    if (opp != null)
                    {
                        OptionSetValue status = opp.StatusCode;
                        OpportunityState state = (OpportunityState)opp.StateCode;
                        string stepName = opp.StepName;

                    }
                }
            }
        }

        private string FixNumberPattern(string inputValue)
        {
            //string s = inputValue.ToString("0.00", CultureInfo.InvariantCulture);
            char delimiter = '.';
            string[] parts = inputValue.Split(delimiter);

            long i1 = 0;
            long i2 = 0;

            if (parts[0].Length > 0)
            {
                if (Validations.IsNumeric(parts[0]))
                    i1 = long.Parse(parts[0]);
            }
            else
                return "";

            if (parts.Length > 1)
            {
                i2 = long.Parse(parts[1]);
            }
            else
            {
                delimiter = ',';
                parts = inputValue.Split(delimiter);

                if (parts[0].Length > 0)
                {
                    if (Validations.IsNumeric(parts[0]))
                        i1 = long.Parse(parts[0]);
                }
                else
                    return "";

                if (parts.Length > 1)
                {
                    i2 = long.Parse(parts[1]);
                }
            }

            int numberLength = i1.ToString().Length;
            int div = numberLength / 3;
            int mod = numberLength % 3;
            string numberDelimiter = delimiter == '.' ? "," : ".";
            string finalNumber = "";

            if (mod > 0)
            {
                int startIndex = 0;
                int endIndex = numberLength - (numberLength - mod);
                finalNumber = i1.ToString().Substring(startIndex, endIndex);
                for (int i = 0; i < div; i++)
                {
                    startIndex = endIndex;
                    endIndex = startIndex + 3;

                    if (endIndex <= numberLength)
                    {
                        finalNumber += numberDelimiter + i1.ToString().Substring(startIndex, 3);
                    }
                }
            }
            else
            {
                int startIndex = 0;
                int endIndex = 3;

                for (int i = 0; i < div; i++)
                {
                    if (i < div - 1)
                    {
                        finalNumber += i1.ToString().Substring(startIndex, 3) + numberDelimiter;
                    }
                    else
                    {
                        finalNumber += i1.ToString().Substring((int)startIndex, 3);
                    }

                    startIndex = endIndex;
                    endIndex = startIndex + 3;
                }
            }

            return (i2 > 0) ? finalNumber + numberDelimiter + i2.ToString() : finalNumber;
        }

        private string EncryptMD5String(string password)
        {
            return MD5.Encrypt(password);
        }

        private void ExportPdf(int userId, int paymentId)
        {
            //int userId = Convert.ToInt32(Request.QueryString["userID"].ToString());
            //int paymentId = Convert.ToInt32(Request.QueryString["paymentID"].ToString());

            if (userId != 0 && paymentId != 0)
            {
                try
                {
                    if (session.Connection.State == ConnectionState.Closed)
                        session.OpenConnection();

                    string fileName = paymentId.ToString() + "invoice_export";

                    ElioBillingUserOrdersPayments payment = Sql.GetBillingPaymentById(paymentId, session);
                    if (payment != null)
                    {
                        //string[] parts = payment.ChargeId.Split('-').ToArray();
                        //fileName = parts[0];
                        fileName = payment.CurrentPeriodStart.Day.ToString() + "." + payment.CurrentPeriodStart.Month.ToString() + "." + payment.CurrentPeriodStart.Year.ToString() +
                            "_"
                            + payment.CurrentPeriodEnd.Day.ToString() + "." + payment.CurrentPeriodEnd.Month.ToString() + "." + payment.CurrentPeriodEnd.Year.ToString() + "_invoice_export";
                    }

                    byte[] b = InvoicesPdfGenerator.GetUserInvoiceByOrder(userId, paymentId, session);

                    try
                    {
                        Response.Clear();
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
                        Response.ContentType = "application/pdf";
                        Response.Buffer = true;
                        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                        Response.BinaryWrite(b);
                        //Response.Flush();
                        Response.End();
                        Response.Close();
                    }
                    catch (System.Threading.ThreadAbortException ex)
                    {
                        Logger.Debug(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                }
                catch (Exception ex)
                {
                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                }
                finally
                {
                    session.CloseConnection();
                }
            }
            else
            {

            }
        }

        private void SendMailjetEmails()
        {
            try
            {
                if (vSession.User != null)
                {
                    DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
                    List<ElioUsers> collaborationUsers = loader.Load(@"SELECT * FROM elio_users
                                                                        where email in
                                                                        (
                                                                            'vvarvi@gmail.com',
                                                                            'vvarvi@yahoo.gr',
                                                                            'varvitsiotisvag@yahoo.gr'
                                                                        )");
                    foreach (ElioUsers collaborationUser in collaborationUsers)
                    {
                        string confirmationLink = FileHelper.AddToPhysicalRootPath(Request) + "/free-sign-up?verificationViewID=" + collaborationUser.GuId + "&type=" + collaborationUser.UserApplicationType.ToString();

                        //EmailSenderLib.CollaborationInvitationEmail(collaborationUser.UserApplicationType, collaborationUser.Email.Trim(), collaborationUser.CompanyName, confirmationLink, vSession.Lang, session);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ImportCSV(object sender, EventArgs e)
        {
            string emails = "";

            string serverMapPathTargetFolder = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["OnboardingLibraryTargetFolder"].ToString()) + "CSV" + "\\" + vSession.User.GuId + "\\";

            //Upload and save the file  
            string csvPath = serverMapPathTargetFolder + Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(csvPath);

            //Create a DataTable.  
            DataTable table = new DataTable();
            table.Columns.AddRange(new DataColumn[1] { new DataColumn("Email", typeof(string)) });

            //Read the contents of CSV file.  
            string csvData = System.IO.File.ReadAllText(csvPath);

            List<string> rowsList = csvData.Split('\n').ToList();
            int rowsCount = rowsList.Count;

            if (rowsCount > 0)
            {
                foreach (string rw in rowsList)
                {
                    string emailInRow = rw.ToString();

                    if (emailInRow.EndsWith("\r") || emailInRow.EndsWith("\n") || emailInRow.StartsWith(@"\") || emailInRow.StartsWith(@"\""") || emailInRow.EndsWith(@"\"""))
                    {
                        emailInRow = emailInRow.Replace("\r", "").Replace("\n", "").Replace(@"\""", "").Replace("\"", "").Trim();
                    }

                    List<string> columnsList = emailInRow.Split(',').ToList();
                    int columnsCount = columnsList.Count;

                    if (columnsCount > 0)
                    {
                        foreach (string cl in columnsList)
                        {
                            if (cl != null && cl != "")
                            {
                                table.Rows.Add(cl);
                                TbxEmails.Text += cl.ToString() + ",";
                            }
                        }
                    }
                }
            }

            if (TbxEmails.Text.EndsWith(","))
                TbxEmails.Text = TbxEmails.Text.Substring(0, TbxEmails.Text.Length - 1);

            emails = TbxEmails.Text;
            //Execute a loop over the rows.  
            ////foreach (string row in csvData.Split('\n'))
            ////{
            ////    if (!string.IsNullOrEmpty(row))
            ////    {
            ////        table.Rows.Add();
            ////        //int i = 0;

            ////        List<string> columnsList = row.Split(',').ToList();
            ////        int columnsCount = columnsList.Count;

            ////        List<string> columnsList2 = row.Split(';').ToList();
            ////        int columnsCount2 = columnsList2.Count;

            ////        //Execute a loop over the columns.  
            ////        foreach (string cell in row.Split(','))
            ////        {
            ////            if (!string.IsNullOrEmpty(cell.ToString()) && cell != "," && cell != "")
            ////            {
            ////                table.Rows[table.Rows.Count - 1][0] = cell;
            ////                //i++;
            ////                table.Rows.Add();
            ////            }
            ////        }
            ////    }

            ////    foreach (string row2 in csvData.Split('\n'))
            ////    {
            ////        if (!string.IsNullOrEmpty(row2))
            ////        {
            ////            table.Rows.Add();
            ////            int i = 0;

            ////            //Execute a loop over the columns.  
            ////            foreach (string cell in row.Split(','))
            ////            {
            ////                if (!string.IsNullOrEmpty(cell.ToString()) && cell != "," && cell != "")
            ////                {
            ////                    table.Rows.Add();
            ////                    table.Rows[i][0] = cell;
            ////                    i++;
            ////                }
            ////            }
            ////        }
            ////    }
            ////}


            ////using (var reader = new StreamReader(csvPath))
            ////{
            ////    List<string> listA = new List<string>();
            ////    List<string> listB = new List<string>();
            ////    while (!reader.EndOfStream)
            ////    {
            ////        var line = reader.ReadLine();
            ////        var values = line.Split(',');

            ////        listA.Add(values[0]);
            ////        listB.Add(values[1]);
            ////    }
            ////}

            ////var lines = File.ReadAllLines(csvPath).Select(a => a.Split(';'));
            ////var csv = from line in lines
            ////          select (from piece in line
            ////                  select piece);

            ////var lines2 = File.ReadAllLines(csvPath).Select(a => a.Split(';'));
            ////var csv3 = (from line in lines
            ////            select (from col in line
            ////                    select col).Skip(1).ToArray() // skip the first column
            ////          ).Skip(2).ToArray(); // skip 2 headlines

            ////var coltitle = (from line in lines
            ////                select line.Skip(1).ToArray() // skip 1st column
            ////       ).Skip(1).Take(1).FirstOrDefault().ToArray(); // take the 2nd row
            ////var rowtitle = (from line in lines select line[0] // take 1st column
            ////               ).Skip(2).ToArray(); // skip 2 headlines

            ////var contents = File.ReadAllText(csvPath).Split('\n');
            ////var csv2 = from line in contents
            ////           select line.Split(',').ToArray();
        }

        private void InsertPackets()
        {
            ElioPackets packet = new ElioPackets();

            packet.PackDescription = "Startup";
            packet.Sysdate = DateTime.Now;
            packet.IsActive = 1;
            packet.Vat = 023;
            packet.IsDefault = 1;

            DataLoader<ElioPackets> loader = new DataLoader<ElioPackets>(session);
            loader.Insert(packet);

            packet = new ElioPackets();

            packet.PackDescription = "Growth";
            packet.Sysdate = DateTime.Now;
            packet.IsActive = 1;
            packet.Vat = 0.23m;
            packet.IsDefault = 1;

            loader = new DataLoader<ElioPackets>(session);
            loader.Insert(packet);
        }

        private void InsertPacketFeatures()
        {
            ElioPacketFeatures features = new ElioPacketFeatures();

            features.ItemDescription = "ManagePartners";
            features.Sysdate = DateTime.Now;
            features.IsActive = 1;

            DataLoader<ElioPacketFeatures> loader = new DataLoader<ElioPacketFeatures>(session);
            loader.Insert(features);

            features = new ElioPacketFeatures();

            features.ItemDescription = "LibraryStorage";
            features.Sysdate = DateTime.Now;
            features.IsActive = 1;

            loader = new DataLoader<ElioPacketFeatures>(session);
            loader.Insert(features);
        }

        private void InsertPacketFeatureItems()
        {
            ElioPacketFeaturesItems items = new ElioPacketFeaturesItems();

            items.PackId = 8;
            items.FeatureId = 1;
            items.FreeItemsNo = 15;
            items.ItemCostVat = 0.30m;
            items.ItemCostWithVat = 1.30m;
            items.Sysdate = DateTime.Now;
            items.IsPublic = 1;
            items.ItemCostWithNoVat = 1.00m;
            items.FreeItemsTrialNo = 0;

            DataLoader<ElioPacketFeaturesItems> loader = new DataLoader<ElioPacketFeaturesItems>(session);
            loader.Insert(items);

            items = new ElioPacketFeaturesItems();

            items.PackId = 8;
            items.FeatureId = 2;
            items.FreeItemsNo = 15;
            items.ItemCostVat = 0.30m;
            items.ItemCostWithVat = 1.30m;
            items.Sysdate = DateTime.Now;
            items.IsPublic = 1;
            items.ItemCostWithNoVat = 1.00m;
            items.FreeItemsTrialNo = 0;

            loader = new DataLoader<ElioPacketFeaturesItems>(session);
            loader.Insert(items);

            items = new ElioPacketFeaturesItems();

            items.PackId = 8;
            items.FeatureId = 3;
            items.FreeItemsNo = 70;
            items.ItemCostVat = 0.35m;
            items.ItemCostWithVat = 1.50m;
            items.Sysdate = DateTime.Now;
            items.IsPublic = 1;
            items.ItemCostWithNoVat = 1.15m;
            items.FreeItemsTrialNo = 0;

            loader = new DataLoader<ElioPacketFeaturesItems>(session);
            loader.Insert(items);

            items = new ElioPacketFeaturesItems();

            items.PackId = 8;
            items.FeatureId = 4;
            items.FreeItemsNo = 100;
            items.ItemCostVat = 0.42m;
            items.ItemCostWithVat = 1.40m;
            items.Sysdate = DateTime.Now;
            items.IsPublic = 1;
            items.ItemCostWithNoVat = 0.98m;
            items.FreeItemsTrialNo = 0;

            loader = new DataLoader<ElioPacketFeaturesItems>(session);
            loader.Insert(items);

            items = new ElioPacketFeaturesItems();

            items.PackId = 8;
            items.FeatureId = 5;
            items.FreeItemsNo = 10;
            items.ItemCostVat = 0.42m;
            items.ItemCostWithVat = 1.40m;
            items.Sysdate = DateTime.Now;
            items.IsPublic = 1;
            items.ItemCostWithNoVat = 0.98m;
            items.FreeItemsTrialNo = 0;

            loader = new DataLoader<ElioPacketFeaturesItems>(session);
            loader.Insert(items);

            //Packet Growth
            items = new ElioPacketFeaturesItems();

            items.PackId = 9;
            items.FeatureId = 1;
            items.FreeItemsNo = 15;
            items.ItemCostVat = 0.30m;
            items.ItemCostWithVat = 1.30m;
            items.Sysdate = DateTime.Now;
            items.IsPublic = 1;
            items.ItemCostWithNoVat = 1.00m;
            items.FreeItemsTrialNo = 0;

            loader = new DataLoader<ElioPacketFeaturesItems>(session);
            loader.Insert(items);

            items = new ElioPacketFeaturesItems();

            items.PackId = 9;
            items.FeatureId = 2;
            items.FreeItemsNo = 15;
            items.ItemCostVat = 0.30m;
            items.ItemCostWithVat = 1.30m;
            items.Sysdate = DateTime.Now;
            items.IsPublic = 1;
            items.ItemCostWithNoVat = 1.00m;
            items.FreeItemsTrialNo = 0;

            loader = new DataLoader<ElioPacketFeaturesItems>(session);
            loader.Insert(items);

            items = new ElioPacketFeaturesItems();

            items.PackId = 9;
            items.FeatureId = 3;
            items.FreeItemsNo = 150;
            items.ItemCostVat = 0.33m;
            items.ItemCostWithVat = 1.10m;
            items.Sysdate = DateTime.Now;
            items.IsPublic = 1;
            items.ItemCostWithNoVat = 1.43m;
            items.FreeItemsTrialNo = 0;

            loader = new DataLoader<ElioPacketFeaturesItems>(session);
            loader.Insert(items);

            items = new ElioPacketFeaturesItems();

            items.PackId = 9;
            items.FeatureId = 4;
            items.FreeItemsNo = 250;
            items.ItemCostVat = 0.33m;
            items.ItemCostWithVat = 1.10m;
            items.Sysdate = DateTime.Now;
            items.IsPublic = 1;
            items.ItemCostWithNoVat = 1.43m;
            items.FreeItemsTrialNo = 0;

            loader = new DataLoader<ElioPacketFeaturesItems>(session);
            loader.Insert(items);

            items = new ElioPacketFeaturesItems();

            items.PackId = 9;
            items.FeatureId = 5;
            items.FreeItemsNo = 20;
            items.ItemCostVat = 0.29m;
            items.ItemCostWithVat = 0.95m;
            items.Sysdate = DateTime.Now;
            items.IsPublic = 1;
            items.ItemCostWithNoVat = 1.24m;
            items.FreeItemsTrialNo = 0;

            loader = new DataLoader<ElioPacketFeaturesItems>(session);
            loader.Insert(items);

            //Packet Enterprise
            items = new ElioPacketFeaturesItems();

            items.PackId = 10;
            items.FeatureId = 1;
            items.FreeItemsNo = 45;
            items.ItemCostVat = 0.30m;
            items.ItemCostWithVat = 1.30m;
            items.Sysdate = DateTime.Now;
            items.IsPublic = 1;
            items.ItemCostWithNoVat = 1.00m;
            items.FreeItemsTrialNo = 0;

            loader = new DataLoader<ElioPacketFeaturesItems>(session);
            loader.Insert(items);

            items = new ElioPacketFeaturesItems();

            items.PackId = 10;
            items.FeatureId = 2;
            items.FreeItemsNo = 45;
            items.ItemCostVat = 0.30m;
            items.ItemCostWithVat = 1.30m;
            items.Sysdate = DateTime.Now;
            items.IsPublic = 1;
            items.ItemCostWithNoVat = 1.00m;
            items.FreeItemsTrialNo = 0;

            loader = new DataLoader<ElioPacketFeaturesItems>(session);
            loader.Insert(items);

            items = new ElioPacketFeaturesItems();

            items.PackId = 10;
            items.FeatureId = 3;
            items.FreeItemsNo = 400;
            items.ItemCostVat = 0.33m;
            items.ItemCostWithVat = 1.10m;
            items.Sysdate = DateTime.Now;
            items.IsPublic = 1;
            items.ItemCostWithNoVat = 1.43m;
            items.FreeItemsTrialNo = 0;

            loader = new DataLoader<ElioPacketFeaturesItems>(session);
            loader.Insert(items);

            items = new ElioPacketFeaturesItems();

            items.PackId = 10;
            items.FeatureId = 4;
            items.FreeItemsNo = 550;
            items.ItemCostVat = 0.33m;
            items.ItemCostWithVat = 1.10m;
            items.Sysdate = DateTime.Now;
            items.IsPublic = 1;
            items.ItemCostWithNoVat = 1.43m;
            items.FreeItemsTrialNo = 0;

            loader = new DataLoader<ElioPacketFeaturesItems>(session);
            loader.Insert(items);

            items = new ElioPacketFeaturesItems();

            items.PackId = 10;
            items.FeatureId = 5;
            items.FreeItemsNo = 40;
            items.ItemCostVat = 0.29m;
            items.ItemCostWithVat = 0.95m;
            items.Sysdate = DateTime.Now;
            items.IsPublic = 1;
            items.ItemCostWithNoVat = 1.24m;
            items.FreeItemsTrialNo = 0;

            loader = new DataLoader<ElioPacketFeaturesItems>(session);
            loader.Insert(items);

            //Premium
            items = new ElioPacketFeaturesItems();

            items.PackId = 1;
            items.FeatureId = 4;
            items.FreeItemsNo = 0;
            items.ItemCostVat = 0.00m;
            items.ItemCostWithVat = 0.00m;
            items.Sysdate = DateTime.Now;
            items.IsPublic = 1;
            items.ItemCostWithNoVat = 0.00m;
            items.FreeItemsTrialNo = 0;

            loader = new DataLoader<ElioPacketFeaturesItems>(session);
            loader.Insert(items);

            items = new ElioPacketFeaturesItems();

            items.PackId = 1;
            items.FeatureId = 5;
            items.FreeItemsNo = 0;
            items.ItemCostVat = 0.00m;
            items.ItemCostWithVat = 0.00m;
            items.Sysdate = DateTime.Now;
            items.IsPublic = 1;
            items.ItemCostWithNoVat = 0.00m;
            items.FreeItemsTrialNo = 0;

            loader = new DataLoader<ElioPacketFeaturesItems>(session);
            loader.Insert(items);

            //Freemium
            items = new ElioPacketFeaturesItems();

            items.PackId = 2;
            items.FeatureId = 4;
            items.FreeItemsNo = 25;
            items.ItemCostVat = 0.00m;
            items.ItemCostWithVat = 0.00m;
            items.Sysdate = DateTime.Now;
            items.IsPublic = 1;
            items.ItemCostWithNoVat = 0.00m;
            items.FreeItemsTrialNo = 0;

            loader = new DataLoader<ElioPacketFeaturesItems>(session);
            loader.Insert(items);

            items = new ElioPacketFeaturesItems();

            items.PackId = 2;
            items.FeatureId = 5;
            items.FreeItemsNo = 2;
            items.ItemCostVat = 0.00m;
            items.ItemCostWithVat = 0.00m;
            items.Sysdate = DateTime.Now;
            items.IsPublic = 1;
            items.ItemCostWithNoVat = 0.00m;
            items.FreeItemsTrialNo = 0;

            loader = new DataLoader<ElioPacketFeaturesItems>(session);
            loader.Insert(items);
        }

        private void InsertUsersFeatures()
        {
            ElioUsersFeatures uFeatures = new ElioUsersFeatures();

            uFeatures.UserBillingType = 3;
            uFeatures.PackId = 8;
            uFeatures.TotalLeads = 15;
            uFeatures.TotalMessages = 15;
            uFeatures.TotalConnections = 70;
            uFeatures.HasSearchLimit = 0;
            uFeatures.TotalSearchResults = 0;
            uFeatures.TotalManagePartners = 100;
            uFeatures.TotalLibraryStorage = 10;

            DataLoader<ElioUsersFeatures> loader = new DataLoader<ElioUsersFeatures>(session);
            loader.Insert(uFeatures);

            uFeatures = new ElioUsersFeatures();

            uFeatures.UserBillingType = 4;
            uFeatures.PackId = 9;
            uFeatures.TotalLeads = 15;
            uFeatures.TotalMessages = 15;
            uFeatures.TotalConnections = 150;
            uFeatures.HasSearchLimit = 0;
            uFeatures.TotalSearchResults = 0;
            uFeatures.TotalManagePartners = 150;
            uFeatures.TotalLibraryStorage = 20;

            loader = new DataLoader<ElioUsersFeatures>(session);
            loader.Insert(uFeatures);

            uFeatures = new ElioUsersFeatures();

            uFeatures.UserBillingType = 5;
            uFeatures.PackId = 10;
            uFeatures.TotalLeads = 45;
            uFeatures.TotalMessages = 45;
            uFeatures.TotalConnections = 400;
            uFeatures.HasSearchLimit = 0;
            uFeatures.TotalSearchResults = 0;
            uFeatures.TotalManagePartners = 550;
            uFeatures.TotalLibraryStorage = 40;

            loader = new DataLoader<ElioUsersFeatures>(session);
            loader.Insert(uFeatures);
        }

        private void FixUsersEmailNotificationsData()
        {
            List<ElioUsers> publicUsers = Sql.GetPublicUsersByTypeAndRowNumber(Types.Vendors.ToString(), 1, 100, session);

            foreach (ElioUsers pUser in publicUsers)
            {
                List<ElioEmailNotifications> notifications = Sql.GetElioEmailNotifications(session);

                foreach (ElioEmailNotifications notification in notifications)
                {
                    if (!Sql.ExistUserEmailNotificationsSettingsById(pUser.Id, notification.Id, session))
                    {
                        ElioUserEmailNotificationsSettings newNotification = new ElioUserEmailNotificationsSettings();

                        newNotification.UserId = pUser.Id;
                        newNotification.EmaiNotificationsId = notification.Id;

                        DataLoader<ElioUserEmailNotificationsSettings> loader = new DataLoader<ElioUserEmailNotificationsSettings>(session);
                        loader.Insert(newNotification);
                    }
                }
            }
        }

        private void FixEmailNotificationsDataByUser()
        {
            List<ElioUsers> publicUsers = Sql.GetPublicUsersByTypeAndRowNumber(EnumHelper.GetDescription(Types.Resellers).ToString(), 1, 1, session);

            foreach (ElioUsers pUser in publicUsers)
            {
                if (!Sql.ExistUserEmailNotificationsSettingsByUser(pUser.Id, session))
                {
                    try
                    {
                        session.BeginTransaction();

                        session.ExecuteQuery("sp_Insert_User_Email_Notifications_Settings", CommandType.StoredProcedure, new SqlParameter("@user_id", pUser.Id));

                        session.CommitTransaction();
                    }
                    catch (Exception ex)
                    {
                        session.RollBackTransaction();
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                }
            }
        }

        private void FixBillingInvoicesHistory()
        {
            List<ElioUsers> users = Sql.GetBillingUsers(session);

            foreach (ElioUsers user in users)
            {
                if (user.BillingType != (int)BillingTypePacket.FreemiumPacketType)
                {
                    if (!string.IsNullOrEmpty(user.CustomerStripeId))
                    {
                        DateTime? startDate = null;
                        DateTime? currentPeriodStart = null;
                        DateTime? currentPeriodEnd = null;
                        DateTime? trialPeriodStart = null;
                        DateTime? trialPeriodEnd = null;
                        DateTime? canceledAt = null;
                        string orderMode = string.Empty;

                        Xamarin.Payments.Stripe.StripeSubscription subscription = StripeLib.GetCustomerSubscriptionInfo(ref startDate, ref currentPeriodStart, ref currentPeriodEnd, ref trialPeriodStart, ref trialPeriodEnd, ref canceledAt, ref orderMode, user.CustomerStripeId);

                        if (subscription != null)
                        {
                            if (subscription.Status == Xamarin.Payments.Stripe.StripeSubscriptionStatus.Active)
                            {
                                ElioBillingUserOrders order = Sql.HasUserOrderByPacketStatusUse(user, Convert.ToInt32(OrderStatus.Active), Convert.ToInt32(OrderStatus.ReadyToUse), session);

                                if (order != null)
                                {
                                    List<ElioBillingUserOrdersPayments> payments = Sql.GetUserPaymentsByOrderId(order.Id, session);

                                    if (payments.Count > 0)
                                    {
                                        if (payments[0].CurrentPeriodEnd < Convert.ToDateTime(subscription.CurrentPeriodEnd) && payments[0].CurrentPeriodEnd < DateTime.Now)
                                        {
                                            int paymentCurrentStartMonth = payments[0].CurrentPeriodStart.Month;
                                            int subscriptionCurrentStartMonth = Convert.ToDateTime(subscription.CurrentPeriodStart).Month;

                                            if (subscriptionCurrentStartMonth - paymentCurrentStartMonth > 0)
                                            {
                                                int addMonths = 1;
                                                for (int i = payments[0].CurrentPeriodEnd.Month; i < Convert.ToDateTime(subscription.CurrentPeriodEnd).Month; i++)
                                                {
                                                    if (payments[0].CurrentPeriodStart.AddMonths(addMonths) < subscription.CurrentPeriodStart)
                                                    {
                                                        ElioBillingUserOrdersPayments newPayment = new ElioBillingUserOrdersPayments();

                                                        newPayment.UserId = user.Id;
                                                        newPayment.OrderId = order.Id;
                                                        newPayment.PackId = payments[0].PackId;
                                                        newPayment.DateCreated = payments[0].DateCreated.AddMonths(addMonths);
                                                        newPayment.LastUpdated = DateTime.Now;
                                                        newPayment.CurrentPeriodStart = payments[0].CurrentPeriodStart.AddMonths(addMonths);
                                                        newPayment.CurrentPeriodEnd = payments[0].CurrentPeriodEnd.AddMonths(addMonths);
                                                        newPayment.Amount = payments[0].Amount;
                                                        newPayment.Comments = "";
                                                        newPayment.ChargeId = Guid.NewGuid().ToString();
                                                        newPayment.Mode = payments[0].Mode;

                                                        DataLoader<ElioBillingUserOrdersPayments> loader = new DataLoader<ElioBillingUserOrdersPayments>(session);
                                                        loader.Insert(newPayment);

                                                        addMonths++;
                                                    }
                                                    else
                                                    {
                                                        ElioBillingUserOrdersPayments newPayment = new ElioBillingUserOrdersPayments();

                                                        newPayment.UserId = user.Id;
                                                        newPayment.OrderId = order.Id;
                                                        newPayment.PackId = order.PackId;
                                                        newPayment.DateCreated = Convert.ToDateTime(subscription.CurrentPeriodStart);
                                                        newPayment.LastUpdated = DateTime.Now;
                                                        newPayment.CurrentPeriodStart = Convert.ToDateTime(subscription.CurrentPeriodStart);
                                                        newPayment.CurrentPeriodEnd = Convert.ToDateTime(subscription.CurrentPeriodEnd);
                                                        newPayment.Amount = Convert.ToDecimal(subscription.Plan.Amount) / 100;
                                                        newPayment.Comments = "";
                                                        newPayment.ChargeId = Guid.NewGuid().ToString();
                                                        newPayment.Mode = subscription.Status.ToString();

                                                        DataLoader<ElioBillingUserOrdersPayments> loader = new DataLoader<ElioBillingUserOrdersPayments>(session);
                                                        loader.Insert(newPayment);
                                                    }
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void InsertUsersOrderPayments()
        {
            List<ElioUsers> users = Sql.GetBillingUsers(session);

            foreach (ElioUsers user in users)
            {
                if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                {
                    if (!string.IsNullOrEmpty(user.CustomerStripeId))
                    {
                        DateTime? startDate = null;
                        DateTime? currentPeriodStart = null;
                        DateTime? currentPeriodEnd = null;
                        DateTime? trialPeriodStart = null;

                        DateTime? trialPeriodEnd = null;
                        DateTime? canceledAt = null;
                        string orderMode = string.Empty;

                        Xamarin.Payments.Stripe.StripeSubscription subscription = StripeLib.GetCustomerSubscriptionInfo(ref startDate, ref currentPeriodStart, ref currentPeriodEnd, ref trialPeriodStart, ref trialPeriodEnd, ref canceledAt, ref orderMode, user.CustomerStripeId);

                        if (subscription != null)
                        {
                            if (subscription.Status == Xamarin.Payments.Stripe.StripeSubscriptionStatus.Active)
                            {
                                ElioBillingUserOrders order = Sql.HasUserOrderByPacketStatusUse(user, Convert.ToInt32(OrderStatus.Active), Convert.ToInt32(OrderStatus.ReadyToUse), session);

                                if (order != null)
                                {
                                    if (order.IsPaid == Convert.ToInt32(OrderStatus.Paid) && order.Mode == OrderMode.Active.ToString())
                                    {
                                        try
                                        {
                                            session.BeginTransaction();
                                            //insert payments
                                            ElioBillingUserOrdersPayments payment = new ElioBillingUserOrdersPayments();

                                            payment.UserId = user.Id;
                                            payment.OrderId = order.Id;
                                            payment.PackId = order.PackId;
                                            payment.DateCreated = Convert.ToDateTime(subscription.CurrentPeriodStart);
                                            payment.LastUpdated = payment.DateCreated;
                                            payment.CurrentPeriodStart = Convert.ToDateTime(subscription.CurrentPeriodStart);
                                            payment.CurrentPeriodEnd = Convert.ToDateTime(subscription.CurrentPeriodEnd);
                                            payment.Amount = Convert.ToDecimal(subscription.Plan.Amount) / 100;
                                            payment.Comments = "";
                                            payment.ChargeId = Guid.NewGuid().ToString();
                                            payment.Mode = subscription.Status.ToString();

                                            DataLoader<ElioBillingUserOrdersPayments> loader = new DataLoader<ElioBillingUserOrdersPayments>(session);
                                            loader.Insert(payment);

                                            session.CommitTransaction();
                                        }
                                        catch (Exception ex)
                                        {
                                            session.RollBackTransaction();
                                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            Logger.DetailedError(string.Format("Error for Customer with ID: {0}", user.Id.ToString()));
                        }
                    }
                }
            }
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            try
            {

                //ImportCSV();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {

            }
        }

        private void FixCollaborationLibraryFilesPath()
        {
            try
            {
                session.OpenConnection();
                session.BeginTransaction();

                DataLoader<ElioCollaborationUsersLibraryFiles> loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);

                List<ElioCollaborationUsersLibraryFiles> files = Sql.GetAllPublicCollaborationUserLibraryFiles(session);

                foreach (ElioCollaborationUsersLibraryFiles file in files)
                {
                    Logger.Debug("USER ID : " + file.UserId + " and file name: " + file.FileName);

                    string[] paths = file.FilePath.Split('/').ToArray();
                    if (paths.Length == 3)
                    {
                        if (paths[0] != "" && paths[1] != "" && paths[2] != "")
                        {
                            string newFilePath = paths[1] + "/" + paths[0] + "/" + paths[2];

                            file.FilePath = newFilePath;

                            loader.Update(file);

                            string serverMapPathOriginalFolder = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + paths[0] + "\\" + paths[1] + "\\";

                            string serverMapPathTargetFolder = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolderNew"].ToString()) + paths[1] + "\\" + paths[0] + "\\";

                            if (System.IO.File.Exists(serverMapPathOriginalFolder + file.FileName))
                            {
                                Logger.Debug("From: " + serverMapPathOriginalFolder);
                                Logger.Debug("To: " + serverMapPathTargetFolder);
                                UpLoadImage.MoveFileToFolder(serverMapPathOriginalFolder, serverMapPathTargetFolder, file.FileName);
                                Logger.Debug("File: " + file.FileName + " moved successfully");
                            }
                            else
                            {
                                Logger.Debug("File: " + file.FileName + " not exists with library ID: " + file.Id);
                            }
                        }
                    }
                }

                session.CommitTransaction();
            }
            catch (Exception ex)
            {
                session.RollBackTransaction();
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        private void FixOnboardingLibraryFilesPath()
        {
            try
            {
                session.OpenConnection();

                DataTable users = session.GetDataTable(@"SELECT distinct ulf.user_id
                                                          FROM [Elioplus_DB].[dbo].[Elio_onboarding_users_library_files] ulf
                                                          where ulf.is_public = 1 
                                                          --and category_id <= 8
                                                          and ulf.user_id not in
                                                          (
	                                                        select user_id
	                                                        from Elio_onboarding_users_library_files_categories ulfc
                                                          )         
                                                          and ulf.user_id <> 39132
                                                          order by ulf.user_id");

                if (users.Rows.Count > 0)
                {
                    foreach (DataRow row in users.Rows)
                    {
                        session.BeginTransaction();

                        int userId = Convert.ToInt32(row["user_id"].ToString());
                        Logger.Debug("BEGIN FOR USER ID : " + userId);
                        int[] defIDs = new int[9];

                        for (int i = 0; i < defIDs.Length; i++)
                        {
                            defIDs[i] = 0;
                        }

                        List<ElioOnboardingUsersLibraryFilesCategories> categories = SqlCollaboration.GetOnboardingUserLibraryPublicFilesCategoriesOrderByID(userId, session);

                        if (categories.Count == 0)
                        {
                            List<ElioOnboardingLibraryFilesDefaultCategories> defaultCategories = SqlCollaboration.GetOnboardingUserLibraryPublicDefaultFilesCategories(session);
                            if (defaultCategories.Count > 0)
                            {
                                int index = 1;
                                defIDs[0] = userId;
                                Logger.Debug("defIDs[0] : " + userId + " ");
                                foreach (ElioOnboardingLibraryFilesDefaultCategories category in defaultCategories)
                                {
                                    ElioOnboardingUsersLibraryFilesCategories item = new ElioOnboardingUsersLibraryFilesCategories();

                                    item.UserId = userId;
                                    item.CategoryDescription = category.CategoryDescription;
                                    item.DateCreated = DateTime.Now;
                                    item.LastUpdate = DateTime.Now;
                                    item.IsPublic = 1;
                                    item.IsDefault = 1;

                                    DataLoader<ElioOnboardingUsersLibraryFilesCategories> loaderUserCategories = new DataLoader<ElioOnboardingUsersLibraryFilesCategories>(session);
                                    loaderUserCategories.Insert(item);

                                    //hold default category ID and user's new category ID for each category --> TO DO
                                    defIDs[index] = item.Id;
                                    Logger.Debug("defIDs[" + index + "] : " + item.Id + " ");
                                    index++;
                                }
                            }

                            categories = SqlCollaboration.GetOnboardingUserLibraryPublicFilesCategoriesOrderByID(userId, session);
                        }
                        else
                        {
                            int index = 1;
                            defIDs[0] = userId;
                            Logger.Debug("defIDs[0] : " + userId + " ");
                            foreach (ElioOnboardingUsersLibraryFilesCategories item in categories)
                            {
                                defIDs[index] = item.Id;
                                Logger.Debug("defIDs[" + index + "] : " + item.Id + " ");
                                index++;
                            }
                        }

                        bool errorCateg = false;

                        for (int i = 0; i < defIDs.Length; i++)
                        {
                            if (defIDs[0] == 0)
                            {
                                errorCateg = true;
                                break;
                            }
                        }

                        if (errorCateg)
                            return;

                        DataLoader<ElioOnboardingUsersLibraryFiles> loader = new DataLoader<ElioOnboardingUsersLibraryFiles>(session);

                        List<ElioOnboardingUsersLibraryFiles> files = Sql.GetOnboardingUserLibraryFileByAllCategories(userId, session);

                        foreach (ElioOnboardingUsersLibraryFiles file in files)
                        {
                            Logger.Debug("USER ID : " + file.UserId + " and file name: " + file.FileName);

                            string[] paths = file.FilePath.Split('/').ToArray();
                            if (paths.Length == 4)
                            {
                                if (paths[0] != "" && paths[1] != "" && paths[2] != "" && paths[3] != "")
                                {
                                    string newFilePath = paths[2] + "/" + paths[0] + "/" + paths[1] + "/" + paths[3];

                                    file.FilePath = newFilePath;
                                    file.CategoryId = defIDs[file.CategoryId];

                                    //session.ExecuteQuery("UPDATE Elio_onboarding_users_library_files " +
                                    //                        "SET file_path = @file_path " +
                                    //                    "WHERE id = @id " +
                                    //                    "AND user_id = @user_id"
                                    //                    , DatabaseHelper.CreateStringParameter("@file_path", newFilePath)
                                    //                    , DatabaseHelper.CreateIntParameter("@id", file.Id)
                                    //                    , DatabaseHelper.CreateIntParameter("@user_id", file.UserId));

                                    loader.Update(file);

                                    string serverMapPathOriginalFolder = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["OnboardingLibraryTargetFolder"].ToString()) + paths[0] + "\\" + paths[1] + "\\" + paths[2] + "\\";

                                    string serverMapPathTargetFolder = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["OnboardingLibraryTargetFolderNew"].ToString()) + paths[2] + "\\" + paths[0] + "\\" + paths[1] + "\\";

                                    if (System.IO.File.Exists(serverMapPathOriginalFolder + file.FileName))
                                    {
                                        Logger.Debug("From: " + serverMapPathOriginalFolder);
                                        Logger.Debug("To: " + serverMapPathTargetFolder);
                                        UpLoadImage.MoveFileToFolder(serverMapPathOriginalFolder, serverMapPathTargetFolder, file.FileName);
                                        Logger.Debug("File: " + file.FileName + " moved successfully");
                                    }
                                    else
                                    {
                                        Logger.Debug("File: " + file.FileName + " not exists with library ID: " + file.Id);
                                    }
                                }
                            }
                        }

                        //break;
                        Logger.Debug("END FOR USER ID : " + userId);
                        Logger.Debug(Environment.NewLine);
                        Logger.Debug("----------------------------------");

                        session.CommitTransaction();
                    }
                }
            }
            catch (Exception ex)
            {
                session.RollBackTransaction();
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        private void GetSetCurrentCulture()
        {
            string amount = "5.600";
            CultureInfo info = CultureInfo.CurrentCulture;
            decimal convAmount = Convert.ToDecimal(amount);
            string decSeparator = info.NumberFormat.NumberDecimalSeparator;
            string groupSeparator = info.NumberFormat.NumberGroupSeparator;

            NumberFormatInfo numInfo = info.NumberFormat;
            string symbol = numInfo.CurrencySymbol;
            string finalAmount = string.Format(CultureInfo.CreateSpecificCulture("en-GB"), "{0:0" + groupSeparator + "000" + decSeparator + "00}", amount);

            CultureInfo currentThreadCultureInfo = Thread.CurrentThread.CurrentCulture;
            string currentThreadName = Thread.CurrentThread.CurrentCulture.Name;
            string currentThreadCultUIName = Thread.CurrentThread.CurrentUICulture.Name;

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-US");

            //if (Thread.CurrentThread.CurrentCulture.Name != "fr-FR")
            //{
            //    // If current culture is not fr-FR, set culture to fr-FR.
            //    CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("fr-FR");
            //    CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CreateSpecificCulture("fr-FR");
            //}
            //else
            //{
            //    // Set culture to en-US.
            //    CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            //    CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CreateSpecificCulture("en-US");
            //}

            ThreadProc();

            Thread worker = new Thread(WebForm1.ThreadProc);
            worker.Name = "WorkerThread";
            worker.Start();
        }

        private static void DisplayThreadInfo()
        {
            Logger.Debug("\nCurrent Thread Name: '{0}'",
                              Thread.CurrentThread.Name);
            Logger.Debug("Current Thread Culture/UI Culture: {0}/{1}",
                              Thread.CurrentThread.CurrentCulture.Name,
                              Thread.CurrentThread.CurrentUICulture.Name);
        }

        private static void DisplayValues()
        {
            // Create new thread and display three random numbers.
            Logger.Debug("Some currency values:");
            for (int ctr = 0; ctr <= 3; ctr++)
                Logger.Debug("   {0:C2}", rnd.NextDouble() * 10);
        }

        private static void ThreadProc()
        {
            DisplayThreadInfo();
            DisplayValues();
        }

        protected void BtnFormatNumber_Click(object sender, EventArgs e)
        {
            try
            {
                if (TbxNumberToFormat.Text != "")
                    TbxFormatedNumber.Text = FixNumberPattern(TbxNumberToFormat.Text);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnUpdateFromAccessDB_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                //Lib.Utils.ExcelLib.UpdateEmptyCitiesToElioUsers(session);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }
    }
}