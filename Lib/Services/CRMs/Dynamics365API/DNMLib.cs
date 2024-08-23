using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.Services.CurrencyConverterAPI.CurrencyConverter;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Lib.Services.CRMs.Dynamics365API
{
    public class DNMLib
    {
        public static bool ExistContactForDublicateByDomainEmailAddress(int vendorId, string email, DBSession session)
        {
            EntityCollection collection = null;
            string domain = email.Split('@')[1];

            if (domain != "")
            {
                if (domain != "email.com" && domain != "gmail.com" && domain != "hotmail.com" && domain != "yahoo.com" && domain != "yahoo.gr" && domain != "outlook.com" && domain != "outlook.gr")
                {
                    ElioCrmUserIntegrations vendorIntegration = Sql.GetUserCrmIntegrationByCrmID(vendorId, (int)Lib.Enums.Api.Dynamics, session);
                    if (vendorIntegration != null)
                    {
                        CrmServiceClient service = CrmSeviceHelper.Connect("Connect_365", vendorIntegration.UserApiKey, vendorIntegration.UserApiSecretKey);

                        if (service.IsReady)
                        {
                            QueryExpression query = new QueryExpression(Contact.EntityLogicalName);
                            query.ColumnSet = new ColumnSet("emailaddress1", "firstname", "lastname");
                            query.Criteria.AddCondition(new ConditionExpression("emailaddress1", ConditionOperator.Like, "%" + domain));

                            collection = service.RetrieveMultiple(query);
                        }
                    }
                }
            }

            return collection.Entities.Any();
        }

        public static bool ExistContactForDublicateByEmailAddress(int vendorId, string email, DBSession session)
        {
            EntityCollection collection = null;
            string domain = email.Split('@')[1];

            ElioCrmUserIntegrations vendorIntegration = Sql.GetUserCrmIntegrationByCrmID(vendorId, (int)Lib.Enums.Api.Dynamics, session);
            if (vendorIntegration != null)
            {
                CrmServiceClient service = CrmSeviceHelper.Connect("Connect_365", vendorIntegration.UserApiKey, vendorIntegration.UserApiSecretKey);

                if (service.IsReady)
                {
                    QueryExpression query = new QueryExpression(Contact.EntityLogicalName);
                    query.ColumnSet = new ColumnSet("emailaddress1", "firstname", "lastname");
                    query.Criteria.AddCondition(new ConditionExpression("emailaddress1", ConditionOperator.Equal, email));

                    collection = service.RetrieveMultiple(query);
                }
            }

            if (!collection.Entities.Any())
                return ExistContactForDublicateByDomainEmailAddress(vendorId, email, session);

            return collection.Entities.Any();
        }

        public static EntityCollection GetAllContactsByAccountId(int vendorId, Guid accountId, DBSession session)
        {
            EntityCollection collection = null;

            ElioCrmUserIntegrations vendorIntegration = Sql.GetUserCrmIntegrationByCrmID(vendorId, (int)Lib.Enums.Api.Dynamics, session);
            if (vendorIntegration != null)
            {
                CrmServiceClient service = CrmSeviceHelper.Connect("Connect_365", vendorIntegration.UserApiKey, vendorIntegration.UserApiSecretKey);

                if (service.IsReady)
                {
                    QueryExpression query = new QueryExpression(Contact.EntityLogicalName);
                    query.ColumnSet = new ColumnSet(true);
                    query.Criteria.AddCondition(new ConditionExpression("parentcustomerid", ConditionOperator.Equal, accountId));

                    collection = service.RetrieveMultiple(query);
                }
            }

            return collection;
        }

        public static EntityCollection GetAllAccountsByOwningUserId(int vendorId, Guid ownerId, DBSession session)
        {
            EntityCollection collection = null;

            ElioCrmUserIntegrations vendorIntegration = Sql.GetUserCrmIntegrationByCrmID(vendorId, (int)Lib.Enums.Api.Dynamics, session);
            if (vendorIntegration != null)
            {
                CrmServiceClient service = CrmSeviceHelper.Connect("Connect_365", vendorIntegration.UserApiKey, vendorIntegration.UserApiSecretKey);

                if (service.IsReady)
                {
                    QueryExpression query = new QueryExpression(Account.EntityLogicalName);
                    query.ColumnSet = new ColumnSet(true);
                    query.Criteria.AddCondition(new ConditionExpression("parentaccountid", ConditionOperator.Equal, ownerId));

                    collection = service.RetrieveMultiple(query);
                }
            }

            return collection;
        }

        public static SystemUser GetSystemUser(int vendorId, DBSession session)
        {
            SystemUser currentUser = null;

            ElioCrmUserIntegrations vendorIntegration = Sql.GetUserCrmIntegrationByCrmID(vendorId, (int)Lib.Enums.Api.Dynamics, session);
            if (vendorIntegration != null)
            {
                CrmServiceClient service = CrmSeviceHelper.Connect("Connect_365", vendorIntegration.UserApiKey, vendorIntegration.UserApiSecretKey);

                if (service.IsReady)
                {
                    // Obtain the current user's information.
                    WhoAmIRequest who = new WhoAmIRequest();
                    WhoAmIResponse whoResp = (WhoAmIResponse)service.Execute(who);
                    Guid currentUserId = whoResp.UserId;
                    //ColumnSet cols = new ColumnSet("domainname", "ownerid", "systemuserid", "fullname", "firstname", "lastname");
                    ColumnSet allCols = new ColumnSet(true);
                    
                    currentUser = service.Retrieve(SystemUser.EntityLogicalName, currentUserId, allCols).ToEntity<SystemUser>();
                }
            }

            return currentUser;
        }

        public static bool SendDealORLeadToCRM(ElioUsers partner, ElioRegistrationDeals deal, ElioLeadDistributions lead, DBSession session)
        {
            bool success = true;

            if (partner != null)
            {
                //CreateNewContact(partner);
            }

            if (deal != null)
            {
                //CreateNewDealOpportunity(partner, deal);
            }
            else if (lead != null)
            {
                CreateNewLeadOpportunity(lead, session);
            }
            else
                success = false;

            return success;
        }

        public static string CreateNewContactOpportunity(int vendorId, ElioUsers partner, ElioRegistrationDeals deal, ElioLeadDistributions lead, DBSession session)
        {
            ElioCrmUserIntegrations vendorIntegration = Sql.GetUserCrmIntegrationByCrmID(vendorId, (int)Lib.Enums.Api.Dynamics, session);
            if (vendorIntegration != null)
            {
                CrmServiceClient service = CrmSeviceHelper.Connect("Connect_365", vendorIntegration.UserApiKey, vendorIntegration.UserApiSecretKey);

                if (service.IsReady)
                {
                    SystemUser currentUser = GetSystemUserByID(service, Guid.Empty);
                    if (currentUser != null && currentUser.Id != Guid.Empty)
                    {
                        Guid accountId = CreateNewAccount(service, currentUser.Id, deal, session);
                        if (accountId != Guid.Empty)
                        {
                            Guid contactId = CreateNewContact(service, currentUser.Id, deal, session);
                            if (contactId != Guid.Empty)
                            {
                                Relationship accountContactRelationship = new Relationship("account_primary_contact");
                                EntityReferenceCollection relatedContact = new EntityReferenceCollection();
                                relatedContact.Add(new EntityReference(Contact.EntityLogicalName, contactId));
                                service.Associate(Account.EntityLogicalName, accountId, accountContactRelationship, relatedContact);

                                Relationship contactAccountCRelationship = new Relationship("contact_customer_accounts");
                                EntityReferenceCollection relatedAccount = new EntityReferenceCollection();
                                relatedAccount.Add(new EntityReference(Account.EntityLogicalName, accountId));
                                service.Associate(Contact.EntityLogicalName, contactId, contactAccountCRelationship, relatedAccount);

                                if (deal != null)
                                {
                                    Opportunity opportunity = null;
                                    bool exist = false;
                                    Guid opportunityId = Guid.Empty;

                                    string crmDealId = Sql365.GetUserCrmDealIdByElioDealId(deal.Id, session);
                                    if (crmDealId != "")
                                    {
                                        ColumnSet cols = new ColumnSet(true);

                                        opportunity = (Opportunity)service.Retrieve(Opportunity.EntityLogicalName, new Guid(crmDealId), cols);
                                        exist = true;
                                    }
                                    else
                                    {
                                        opportunity = new Opportunity();
                                    }

                                    if (opportunity != null)
                                    {
                                        opportunity.Name = deal.CompanyName;
                                        opportunity.LogicalName = Opportunity.EntityLogicalName;

                                        //SystemUser user = GetSystemUserByID(service);
                                        //if (user.Id != Guid.Empty)
                                        opportunity.OwnerId = new EntityReference(SystemUser.EntityLogicalName, currentUser.Id);

                                        Money m = new Money(deal.Amount);

                                        if (partner.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                                        {
                                            ElioCurrenciesCountries vendorCurrency = Sql.GetCurrencyCountriesIJUserCurrencyByUserID(vendorId, session);

                                            if (vendorCurrency != null)
                                            {
                                                string vendorCurrencyID = vendorCurrency.CurrencyId;

                                                ElioCurrenciesCountries currency = Sql.GetCurrencyCountryByCurId(deal.CurId, session);
                                                if (currency != null)
                                                {
                                                    string resellerCurrencyID = currency.CurrencyId;

                                                    if (vendorCurrencyID != "" && resellerCurrencyID != "")
                                                    {
                                                        if (vendorCurrencyID != resellerCurrencyID)
                                                        {
                                                            double newAmount = ConverterLib.Convert(Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", deal.Amount)), resellerCurrencyID, vendorCurrencyID);
                                                            if (newAmount > 0)
                                                            {
                                                                m = new Money(Convert.ToDecimal(newAmount));
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        opportunity.BudgetAmount = m;
                                        opportunity.EstimatedValue = m;
                                        //opportunity.EstimatedValue_Base = opportunity.EstimatedValue;
                                        //opportunity.IsRevenueSystemCalculated = false;
                                        opportunity.EstimatedCloseDate = new DateTime(deal.ExpectedClosedDate.Year, deal.ExpectedClosedDate.Month, deal.ExpectedClosedDate.Day);
                                        opportunity.CustomerNeed = deal.Description;
                                        opportunity.PurchaseTimeframe = new OptionSetValue((int)purchasetimeframe.NextQuarter);
                                        opportunity.PurchaseProcess = new OptionSetValue((int)purchaseprocess.Individual);
                                        opportunity.Description = deal.Description;
                                        opportunity.CurrentSituation = deal.Description;
                                        opportunity.ProposedSolution = "solution";
                                        opportunity.StateCode = OpportunityState.Open;
                                        opportunity.StatusCode = new OptionSetValue((int)opportunityclose_statuscode.Open);
                                        opportunity.SalesStage = new OptionSetValue((int)opportunity_salesstage.Qualify);

                                        if (exist)
                                        {
                                            service.Update(opportunity);
                                            opportunityId = opportunity.Id;
                                        }
                                        else
                                        {
                                            opportunityId = service.Create(opportunity);
                                        }

                                        if (opportunityId != Guid.Empty)
                                        {
                                            if (!exist)
                                            {
                                                Relationship opportunityContactRelationship = new Relationship("opportunity_parent_contact");
                                                EntityReferenceCollection relatedContactOpportunity = new EntityReferenceCollection();
                                                relatedContactOpportunity.Add(new EntityReference(Opportunity.EntityLogicalName, opportunityId));
                                                service.Associate(Contact.EntityLogicalName, contactId, opportunityContactRelationship, relatedContactOpportunity);

                                                Relationship opportunityAccountRelationship = new Relationship("opportunity_parent_account");
                                                EntityReferenceCollection relatedAccountOpportunity = new EntityReferenceCollection();
                                                relatedAccountOpportunity.Add(new EntityReference(Opportunity.EntityLogicalName, opportunityId));
                                                service.Associate(Account.EntityLogicalName, accountId, opportunityAccountRelationship, relatedAccountOpportunity);

                                                //success
                                                //ElioCrmUserIntegrations vendorIntegration = Sql.GetUserCrmIntegrationByCrmID(vendorId, (int)Lib.Enums.Api.Dynamics, session);
                                                //if (vendorIntegration != null)
                                                //{
                                                ElioCrmDealContacts365 dealContact = new ElioCrmDealContacts365();
                                                dealContact.ElioDealId = deal.Id;
                                                dealContact.DealEmail = deal.Email;
                                                dealContact.CrmAccountId = accountId.ToString();
                                                dealContact.CrmContactId = contactId.ToString();
                                                dealContact.DateInsert = DateTime.Now;
                                                dealContact.LastUpdate = DateTime.Now;
                                                dealContact.IsActive = 1;

                                                DataLoader<ElioCrmDealContacts365> loaderContact = new DataLoader<ElioCrmDealContacts365>(session);
                                                loaderContact.Insert(dealContact);

                                                ElioCrmUserDeals crmUserDeal = new ElioCrmUserDeals();

                                                crmUserDeal.CrmIntegrationId = (int)Lib.Enums.Api.Dynamics;
                                                crmUserDeal.DealId = deal.Id;
                                                crmUserDeal.CrmDeadId = opportunityId.ToString();
                                                crmUserDeal.DateInsert = DateTime.Now;
                                                crmUserDeal.LastUpdate = DateTime.Now;

                                                DataLoader<ElioCrmUserDeals> loaderCrm = new DataLoader<ElioCrmUserDeals>(session);
                                                loaderCrm.Insert(crmUserDeal);
                                                //}
                                                //else
                                                //    return "";
                                            }

                                            return opportunityId.ToString();
                                        }
                                        else
                                        {
                                            //error
                                            return "";
                                        }
                                    }
                                    else
                                        return "";
                                }
                                else if (lead != null)
                                {
                                    #region Lead case

                                    Opportunity opportunity = new Opportunity();

                                    opportunity.Name = lead.CompanyName;
                                    opportunity.LogicalName = Opportunity.EntityLogicalName;

                                    SystemUser user = GetSystemUserByID(service, Guid.Empty);
                                    if (user.Id != Guid.Empty)
                                        opportunity.OwnerId = new EntityReference(SystemUser.EntityLogicalName, user.Id);

                                    Money m = new Money((decimal)lead.Amount);
                                    opportunity.BudgetAmount = m;
                                    opportunity.EstimatedValue = m;
                                    opportunity.EstimatedCloseDate = new DateTime(lead.CreatedDate.AddMonths(3).Year, lead.CreatedDate.AddMonths(3).Month, lead.CreatedDate.AddMonths(3).Day);
                                    opportunity.CustomerNeed = lead.Comments;
                                    opportunity.PurchaseTimeframe = new OptionSetValue((int)purchasetimeframe.NextQuarter);
                                    opportunity.PurchaseProcess = new OptionSetValue((int)purchaseprocess.Individual);
                                    opportunity.Description = lead.Comments;
                                    opportunity.CurrentSituation = lead.Comments;
                                    opportunity.ProposedSolution = "solution";
                                    opportunity.StateCode = OpportunityState.Open;
                                    opportunity.StatusCode = new OptionSetValue((int)opportunityclose_statuscode.Open);
                                    opportunity.SalesStage = new OptionSetValue((int)opportunity_salesstage.Qualify);

                                    Guid opportunityId = service.Create(opportunity);
                                    if (opportunityId != Guid.Empty)
                                    {
                                        Relationship opportunityContactRelationship = new Relationship("opportunity_parent_contact");
                                        EntityReferenceCollection relatedOpportunity = new EntityReferenceCollection();
                                        relatedOpportunity.Add(new EntityReference(Opportunity.EntityLogicalName, opportunityId));
                                        service.Associate(Contact.EntityLogicalName, contactId, opportunityContactRelationship, relatedOpportunity);

                                        //success
                                        //ElioCrmUserIntegrations vendorIntegration = Sql.GetUserCrmIntegrationByCrmID(vendorId, (int)Lib.Enums.Api.Dynamics, session);
                                        //if (vendorIntegration != null)
                                        //{
                                        ElioCrmUserDeals crmUserDeal = new ElioCrmUserDeals();

                                        crmUserDeal.CrmIntegrationId = (int)Lib.Enums.Api.Dynamics;
                                        crmUserDeal.DealId = deal.Id;
                                        crmUserDeal.CrmDeadId = opportunityId.ToString();
                                        crmUserDeal.DateInsert = DateTime.Now;
                                        crmUserDeal.LastUpdate = DateTime.Now;

                                        DataLoader<ElioCrmUserDeals> loaderCrm = new DataLoader<ElioCrmUserDeals>(session);
                                        loaderCrm.Insert(crmUserDeal);

                                        return opportunityId.ToString();
                                        //}
                                        //else
                                        //    return "";
                                    }
                                    else
                                    {
                                        //error
                                        return "";
                                    }

                                    #endregion
                                }
                                else
                                    return "";
                            }
                            else
                                return "";
                        }
                        else
                            return "";
                    }
                    else
                        return "";
                }
                else
                    return "";
            }
            else
                return "";
        }

        public static Opportunity GetOpportunityByDealId(ElioRegistrationDeals deal, DBSession session)
        {
            if (deal != null)
            {
                string elioCrmDealId = Sql365.GetUserCrmDealIdByElioDealId(deal.Id, session);
                if (elioCrmDealId != "")
                {
                    ElioCrmUserIntegrations vendorIntegration = Sql.GetUserCrmIntegrationByCrmID(deal.VendorId, (int)Lib.Enums.Api.Dynamics, session);
                    if (vendorIntegration != null)
                    {
                        CrmServiceClient service = CrmSeviceHelper.Connect("Connect_365", vendorIntegration.UserApiKey, vendorIntegration.UserApiSecretKey);

                        if (service.IsReady)
                        {
                            ColumnSet cols = new ColumnSet(true);
                            Opportunity opp = (Opportunity)service.Retrieve(Opportunity.EntityLogicalName, new Guid(elioCrmDealId), cols);
                            if (opp != null)
                                return opp;
                            else
                                return null;
                        }
                        else
                            return null;
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public static void GetOpportunityClose(ElioRegistrationDeals deal, DBSession session)
        {
            if (deal != null)
            {
                ElioCrmUserIntegrations vendorIntegration = Sql.GetUserCrmIntegrationByCrmID(1, (int)Lib.Enums.Api.Dynamics, session);
                if (vendorIntegration != null)
                {
                    CrmServiceClient service = CrmSeviceHelper.Connect("Connect_365", vendorIntegration.UserApiKey, vendorIntegration.UserApiSecretKey);

                    if (service.IsReady)
                    {
                        string elioCrmDealId = Sql365.GetUserCrmDealIdByElioDealId(deal.Id, session);
                        if (elioCrmDealId != "")
                        {
                            ColumnSet cols = new ColumnSet(true);
                            Opportunity opp = (Opportunity)service.Retrieve(Opportunity.EntityLogicalName, new Guid(elioCrmDealId), cols);
                            if (opp != null)
                            {
                                OpportunityClose oppClose = new OpportunityClose();
                                if (oppClose != null)
                                {
                                    oppClose.StateCode = OpportunityCloseState.Completed;
                                    oppClose.StatusCode = new OptionSetValue((int)opportunityclose_statuscode.Completed);
                                    oppClose.ActualEnd = DateTime.Now;
                                    oppClose.ActualRevenue = opp.BudgetAmount;
                                    oppClose.Subject = "Opportunity close as Won";
                                    oppClose.OwnerId = new EntityReference(SystemUser.EntityLogicalName, opp.OwnerId.Id);
                                    oppClose.OpportunityId = new EntityReference(Opportunity.EntityLogicalName, opp.Id);

                                    Guid oppCloseId = service.Create(oppClose);
                                    if (oppCloseId != Guid.Empty)
                                    {
                                        Relationship opportunityRelationship = new Relationship("Opportunity_OpportunityClose");
                                        EntityReferenceCollection relatedOpportunity = new EntityReferenceCollection();
                                        relatedOpportunity.Add(new EntityReference(Opportunity.EntityLogicalName, opp.Id));
                                        service.Associate(OpportunityClose.EntityLogicalName, oppCloseId, opportunityRelationship, relatedOpportunity);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static bool CloseOpportunityAsWon(ElioRegistrationDeals deal, DBSession session)
        {
            if (deal != null)
            {
                ElioCrmUserIntegrations vendorIntegration = Sql.GetUserCrmIntegrationByCrmID(1, (int)Lib.Enums.Api.Dynamics, session);
                if (vendorIntegration != null)
                {
                    CrmServiceClient service = CrmSeviceHelper.Connect("Connect_365", vendorIntegration.UserApiKey, vendorIntegration.UserApiSecretKey);

                    if (service.IsReady)
                    {
                        string elioCrmDealId = Sql365.GetUserCrmDealIdByElioDealId(deal.Id, session);
                        if (elioCrmDealId != "")
                        {
                            ColumnSet cols = new ColumnSet(true);
                            Opportunity opp = (Opportunity)service.Retrieve(Opportunity.EntityLogicalName, new Guid(elioCrmDealId), cols);
                            if (opp != null)
                            {
                                // Close the opportunity as won
                                var winOppRequest = new WinOpportunityRequest
                                {
                                    OpportunityClose = new OpportunityClose
                                    {
                                        OpportunityId = new EntityReference
                                        (Opportunity.EntityLogicalName, opp.Id),
                                        ActualEnd = DateTime.Now,
                                        ActualRevenue = opp.BudgetAmount,
                                        Description = "Opportunity closed as won by your partner"
                                    },
                                    Status = new OptionSetValue((int)opportunity_statuscode.Won)
                                };

                                service.Execute(winOppRequest);

                                return true;
                            }
                            else
                                return false;
                        }
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public static bool CloseOpportunityAsLost(ElioRegistrationDeals deal, DBSession session)
        {
            if (deal != null)
            {
                ElioCrmUserIntegrations vendorIntegration = Sql.GetUserCrmIntegrationByCrmID(1, (int)Lib.Enums.Api.Dynamics, session);
                if (vendorIntegration != null)
                {
                    CrmServiceClient service = CrmSeviceHelper.Connect("Connect_365", vendorIntegration.UserApiKey, vendorIntegration.UserApiSecretKey);

                    if (service.IsReady)
                    {
                        string elioCrmDealId = Sql365.GetUserCrmDealIdByElioDealId(deal.Id, session);
                        if (elioCrmDealId != "")
                        {
                            ColumnSet cols = new ColumnSet(true);
                            Opportunity opp = (Opportunity)service.Retrieve(Opportunity.EntityLogicalName, new Guid(elioCrmDealId), cols);
                            if (opp != null)
                            {
                                // Close the opportunity as won
                                var winOppRequest = new LoseOpportunityRequest
                                {
                                    OpportunityClose = new OpportunityClose
                                    {
                                        OpportunityId = new EntityReference
                                        (Opportunity.EntityLogicalName, opp.Id),
                                        ActualEnd = DateTime.Now,
                                        ActualRevenue = opp.BudgetAmount,
                                        Description = "Opportunity closed as lost by your partner"                                        
                                    },
                                    Status = new OptionSetValue((int)opportunity_statuscode.Canceled)
                                };
                                service.Execute(winOppRequest);

                                return true;
                            }
                            else
                                return false;
                        }
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public static bool ReOpenOpportunity(ElioRegistrationDeals deal, DBSession session)
        {
            if (deal != null)
            {
                ElioCrmUserIntegrations vendorIntegration = Sql.GetUserCrmIntegrationByCrmID(1, (int)Lib.Enums.Api.Dynamics, session);
                if (vendorIntegration != null)
                {
                    CrmServiceClient service = CrmSeviceHelper.Connect("Connect_365", vendorIntegration.UserApiKey, vendorIntegration.UserApiSecretKey);

                    if (service.IsReady)
                    {
                        string elioCrmDealId = Sql365.GetUserCrmDealIdByElioDealId(deal.Id, session);
                        if (elioCrmDealId != "")
                        {
                            ColumnSet cols = new ColumnSet(true);
                            Opportunity opp = (Opportunity)service.Retrieve(Opportunity.EntityLogicalName, new Guid(elioCrmDealId), cols);
                            if (opp != null)
                            {
                                opp.StateCode = OpportunityState.Open;
                                opp.StatusCode = new OptionSetValue((int)opportunity_statuscode.InProgress);

                                service.Update(opp);

                                return true;
                            }
                            else
                                return false;
                        }
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public static bool UpdateOpportunityStageByDealId(ElioRegistrationDeals deal, string result, DBSession session)
        {
            if (deal != null)
            {
                string elioCrmDealId = Sql365.GetUserCrmDealIdByElioDealId(deal.Id, session);
                if (elioCrmDealId != "")
                {
                    ElioCrmUserIntegrations vendorIntegration = Sql.GetUserCrmIntegrationByCrmID(deal.VendorId, (int)Lib.Enums.Api.Dynamics, session);
                    if (vendorIntegration != null)
                    {
                        CrmServiceClient service = CrmSeviceHelper.Connect("Connect_365", vendorIntegration.UserApiKey, vendorIntegration.UserApiSecretKey);

                        if (service.IsReady)
                        {
                            ColumnSet cols = new ColumnSet(true);
                            Opportunity opp = (Opportunity)service.Retrieve(Opportunity.EntityLogicalName, new Guid(elioCrmDealId), cols);
                            if (opp != null)
                            {
                                if (result == "Won")
                                {
                                    opp.StateCode = OpportunityState.Won;
                                    opp.StatusCode = new OptionSetValue((int)opportunity_statuscode.Won);
                                }
                                else
                                {
                                    opp.StateCode = OpportunityState.Lost;
                                    opp.StatusCode = new OptionSetValue((int)opportunity_statuscode.OutSold);
                                }
                                
                                //opp.SalesStage = new OptionSetValue((int)opportunity_salesstage.Close);
                                //opp.ActualCloseDate = DateTime.Now;
                                //opp.ActualValue = opp.BudgetAmount;
                                ////opp.EntityState = EntityState.Changed;
                                ////opp.StatusCode = new OptionSetValue((int)opportunityclose_statuscode.Completed);

                                opp.SalesStage = new OptionSetValue((int)opportunity_salesstage.Develop);
                                opp.PurchaseTimeframe = new OptionSetValue((int)purchasetimeframe.ThisQuarter);
                                opp.PurchaseProcess = new OptionSetValue((int)purchaseprocess.Committee);
                                opp.DecisionMaker = true;

                                //OpportunityClose opportunityClose = new OpportunityClose();
                                //opportunityClose.StatusCode = new OptionSetValue((int)opportunityclose_statuscode.Completed);
                                //opportunityClose.StateCode = OpportunityCloseState.Completed;

                                service.Update(opp);
                                
                                return true;
                            }
                            else
                                return false;
                        }
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public static string CreateNewLeadOpportunity(ElioLeadDistributions lead, DBSession session)
        {
            ElioCrmUserIntegrations vendorIntegration = Sql.GetUserCrmIntegrationByCrmID(lead.VendorId, (int)Lib.Enums.Api.Dynamics, session);
            if (vendorIntegration != null)
            {
                CrmServiceClient service = CrmSeviceHelper.Connect("Connect_365", vendorIntegration.UserApiKey, vendorIntegration.UserApiSecretKey);

                if (service.IsReady)
                {
                    Opportunity opportunity = new Opportunity();

                    opportunity.Name = lead.CompanyName;
                    opportunity.LogicalName = Opportunity.EntityLogicalName;

                    SystemUser user = GetSystemUserByID(service, Guid.Empty);
                    if (user.Id != Guid.Empty)
                        opportunity.OwnerId = new EntityReference(SystemUser.EntityLogicalName, user.Id);

                    Money m = new Money((decimal)lead.Amount);
                    opportunity.BudgetAmount = m;
                    opportunity.EstimatedValue = m;
                    //opportunity.EstimatedCloseDate = new DateTime(lead.ExpectedClosedDate.Year, deal.ExpectedClosedDate.Month, deal.ExpectedClosedDate.Day);
                    opportunity.CustomerNeed = lead.Comments;
                    opportunity.PurchaseTimeframe = new OptionSetValue((int)purchasetimeframe.NextQuarter);
                    opportunity.PurchaseProcess = new OptionSetValue((int)purchaseprocess.Individual);
                    opportunity.Description = lead.Comments;
                    opportunity.CurrentSituation = lead.Comments;
                    opportunity.ProposedSolution = "solution";
                    opportunity.StateCode = OpportunityState.Open;
                    opportunity.StatusCode = new OptionSetValue((int)opportunityclose_statuscode.Open);
                    opportunity.SalesStage = new OptionSetValue((int)opportunity_salesstage.Qualify);

                    Guid id = service.Create(opportunity);
                    if (id != Guid.Empty)
                    {
                        //success
                        return id.ToString();
                    }
                    else
                    {
                        //error
                        return "";
                    }
                }
                else
                    return "";
            }
            else
                return "";
        }

        public static string CreateNewLead(ElioLeadDistributions userLead, DBSession session)
        {
            ElioCrmUserIntegrations vendorIntegration = Sql.GetUserCrmIntegrationByCrmID(userLead.VendorId, (int)Lib.Enums.Api.Dynamics, session);
            if (vendorIntegration != null)
            {
                CrmServiceClient service = CrmSeviceHelper.Connect("Connect_365", vendorIntegration.UserApiKey, vendorIntegration.UserApiSecretKey);

                if (service.IsReady)
                {
                    ElioUsers partner = Sql.GetUserById(userLead.ResellerId, session);
                    if (partner != null)
                    {
                        Lead lead = new Lead();
                        lead.LogicalName = "lead";
                        lead.FirstName = userLead.FirstName;
                        lead.LastName = userLead.LastName;
                        lead.Address1_AddressTypeCode = new OptionSetValue((int)AccountAddress1_AddressTypeCode.Primary);
                        lead.Address1_City = partner.City;
                        lead.Address1_Country = partner.Country;
                        lead.Address1_Name = partner.Address;
                        lead.Address1_PostalCode = "";
                        lead.Address1_Telephone1 = userLead.Phone;
                        Money m = new Money();
                        m.Value = Convert.ToDecimal(userLead.Amount);
                        lead.BudgetAmount = m;
                        lead.BudgetStatus = new OptionSetValue((int)budgetstatus.NoCommittedBudget);
                        lead.Description = userLead.Comments;
                        lead.EMailAddress1 = userLead.Email;
                        lead.EstimatedAmount = m;
                        lead.EstimatedCloseDate = userLead.CreatedDate.AddMonths(3);
                        //lead.FullName = userLead.CompanyName;
                        lead.IndustryCode = new OptionSetValue((int)AccountIndustryCode.Accounting);
                        lead.JobTitle = "Job Title";
                        lead.LeadQualityCode = new OptionSetValue((int)LeadLeadQualityCode.Hot);
                        lead.LeadSourceCode = new OptionSetValue((int)LeadLeadSourceCode.Partner);
                        lead.StateCode = LeadState.Open;
                        lead.StatusCode = new OptionSetValue((int)lead_statuscode.New);
                        lead.MobilePhone = userLead.Phone;
                        lead.Need = new OptionSetValue((int)need.Shouldhave);
                        lead.Revenue = m;
                        lead.SalesStage = new OptionSetValue((int)lead_salesstage.Qualify);
                        lead.SalesStageCode = new OptionSetValue((int)LeadSalesStageCode.DefaultValue);
                        lead.Telephone1 = userLead.Phone;
                        lead.WebSiteUrl = userLead.Website;

                        Guid id = service.Create(lead);

                        if (id != Guid.Empty)
                            return id.ToString();
                        else
                            return "";
                    }
                    else
                        return "";
                }
                else
                    return "";
            }
            else
                return "";
        }

        public static string CreateNewAccount1(ElioLeadDistributions lead, DBSession session)
        {
            ElioCrmUserIntegrations vendorIntegration = Sql.GetUserCrmIntegrationByCrmID(lead.VendorId, (int)Lib.Enums.Api.Dynamics, session);
            if (vendorIntegration != null)
            {
                CrmServiceClient service = CrmSeviceHelper.Connect("Connect_365", vendorIntegration.UserApiKey, vendorIntegration.UserApiSecretKey);

                if (service.IsReady)
                {
                    ElioUsers partner = Sql.GetUserById(lead.ResellerId, session);
                    if (partner != null)
                    {
                        Account account = new Account();
                        account.LogicalName = "account";
                        account.AccountCategoryCode = new OptionSetValue((int)AccountAccountCategoryCode.Standard);
                        account.AccountClassificationCode = new OptionSetValue((int)AccountAccountClassificationCode.DefaultValue);
                        account.AccountRatingCode = new OptionSetValue((int)AccountAccountRatingCode.DefaultValue);
                        account.Name = "";
                        account.Address1_AddressTypeCode = new OptionSetValue((int)AccountAddress1_AddressTypeCode.Primary);
                        account.Address1_City = partner.City;
                        account.Address1_Country = partner.Country;
                        account.Address1_Name = partner.Address;
                        account.Address1_PostalCode = "123654";
                        account.Address1_Telephone1 = lead.Phone;
                        account.BusinessTypeCode = new OptionSetValue((int)AccountBusinessTypeCode.DefaultValue);
                        account.CustomerSizeCode = new OptionSetValue((int)AccountCustomerSizeCode.DefaultValue);
                        account.CustomerTypeCode = new OptionSetValue((int)AccountCustomerTypeCode.Reseller);
                        account.Description = lead.Comments;
                        account.EMailAddress1 = lead.Email;
                        account.StateCode = AccountState.Active;
                        account.StatusCode = new OptionSetValue((int)account_statuscode.Active);
                        account.Telephone1 = lead.Phone;
                        account.WebSiteURL = lead.Website;

                        Guid id = service.Create(account);

                        if (id != Guid.Empty)
                            return id.ToString();
                        else
                            return "";
                    }
                    else
                        return "";
                }
                else
                    return "";
            }
            else
                return "";
        }

        public static void RetrieveContact(int vendorId, DBSession session)
        {
            ElioCrmUserIntegrations vendorIntegration = Sql.GetUserCrmIntegrationByCrmID(vendorId, (int)Lib.Enums.Api.Dynamics, session);
            if (vendorIntegration != null)
            {
                CrmServiceClient service = CrmSeviceHelper.Connect("Connect_365", vendorIntegration.UserApiKey, vendorIntegration.UserApiSecretKey);

                if (service.IsReady)
                {
                    ColumnSet cols = new ColumnSet(true);
                    service.Retrieve(Contact.EntityLogicalName, Guid.Empty, cols);

                }
            }
        }

        public static Guid CreateNewAccount(CrmServiceClient service, Guid currentUserId, ElioRegistrationDeals deal, DBSession session)
        {
            //CrmServiceClient service = CrmSeviceHelper.Connect("Connect_365");

            if (service.IsReady)
            {
                if (deal != null)
                {
                    string accountId = Sql365.GetCrmDealAccountId(deal.Id, session);
                    if (accountId != "")
                    {
                        return new Guid(accountId);
                    }
                    else
                    {
                        Account account = new Account();

                        account.LogicalName = Account.EntityLogicalName;
                        account.Name = deal.CompanyName;
                        account.WebSiteURL = deal.Website;
                        account.Address1_Name = deal.Address;
                        account.Address1_City = deal.City;
                        account.Address1_Country = deal.Country;
                        account.Address1_PostalCode = deal.DivisionalPostalCode;

                        //SystemUser user = GetSystemUserByID(service);
                        //if (user.Id != Guid.Empty)
                        account.OwnerId = new EntityReference(SystemUser.EntityLogicalName, currentUserId);

                        Guid accountGuid = service.Create(account);

                        if (accountGuid != Guid.Empty)
                        {
                            ElioCrmDealAccounts365 dealAccount = new ElioCrmDealAccounts365();
                            dealAccount.ElioDealId = deal.Id;
                            dealAccount.CrmAccountId = accountGuid.ToString();
                            dealAccount.DateInsert = DateTime.Now;
                            dealAccount.LastUpdate = DateTime.Now;
                            dealAccount.IsActive = 1;

                            DataLoader<ElioCrmDealAccounts365> loaderContact = new DataLoader<ElioCrmDealAccounts365>(session);
                            loaderContact.Insert(dealAccount);

                            return accountGuid;
                        }
                        else
                        {
                            return Guid.Empty;
                        }
                    }
                }
                else
                    return Guid.Empty;
            }
            else
                return Guid.Empty;
        }
        public static Guid CreateNewContact(CrmServiceClient service, Guid currentUserId, ElioRegistrationDeals deal, DBSession session)
        {
            //CrmServiceClient service = CrmSeviceHelper.Connect("Connect_365");

            if (service.IsReady)
            {
                if (deal != null)
                {
                    string contactId = Sql365.GetCrmDealContactId(deal.Id, deal.Email, session);
                    if (contactId != "")
                    {
                        return new Guid(contactId);
                    }
                    else
                    {
                        Contact contact = new Contact();
                        //contact.OwnerId = new EntityReference(SystemUser.EntityLogicalName, new Guid("01cca96d-2a7d-ec11-8d21-00224880bade"));

                        contact.LogicalName = Contact.EntityLogicalName;
                        contact.FirstName = deal.FirstName;
                        contact.LastName = deal.LastName;
                        contact.JobTitle = deal.JobTitle;
                        contact.Telephone1 = deal.Phone;
                        contact.EMailAddress1 = deal.Email;
                        contact.StateCode = ContactState.Active;
                        contact.CustomerTypeCode = new OptionSetValue((int)ContactCustomerTypeCode.DefaultValue);
                        
                        //SystemUser user = GetSystemUserByID(service);
                        //if (user != null && user.Id != Guid.Empty)
                        contact.OwnerId = new EntityReference(SystemUser.EntityLogicalName, currentUserId);

                        Guid contactGuid = service.Create(contact);

                        if (contactGuid != Guid.Empty)
                        {
                            return contactGuid;
                        }
                        else
                        {
                            return Guid.Empty;
                        }
                    }
                }
                else
                    return Guid.Empty;
            }
            else
                return Guid.Empty;
        }

        public static Guid CreateNewContactWithDublicateCheck(ElioUsers partner, DBSession session)
        {
            ElioCrmUserIntegrations vendorIntegration = Sql.GetUserCrmIntegrationByCrmID(partner.Id, (int)Lib.Enums.Api.Dynamics, session);
            if (vendorIntegration != null)
            {
                CrmServiceClient service = CrmSeviceHelper.Connect("Connect_365", vendorIntegration.UserApiKey, vendorIntegration.UserApiSecretKey);

                if (service.IsReady)
                {
                    if (partner != null)
                    {
                        ////string contactId = Sql365.GetUserCrmContactId(partner.Id, session);
                        //if (contactId != "")
                        //{
                        //    return new Guid(contactId);
                        //}
                        //else
                        //{
                        Dictionary<string, CrmDataTypeWrapper> data = new Dictionary<string, CrmDataTypeWrapper>();

                        //data.Add("fullname", new CrmDataTypeWrapper(partner.FirstName, CrmFieldType.String));
                        data.Add("firstname", new CrmDataTypeWrapper(partner.FirstName, CrmFieldType.String));
                        data.Add("lastname", new CrmDataTypeWrapper(partner.LastName, CrmFieldType.String));
                        data.Add("websiteurl", new CrmDataTypeWrapper(partner.WebSite, CrmFieldType.String));
                        data.Add("address1_name", new CrmDataTypeWrapper(partner.Address, CrmFieldType.String));
                        data.Add("address1_city", new CrmDataTypeWrapper(partner.City, CrmFieldType.String));
                        data.Add("address1_country", new CrmDataTypeWrapper(partner.Country, CrmFieldType.String));
                        data.Add("jobtitle", new CrmDataTypeWrapper(partner.Position, CrmFieldType.String));
                        data.Add("telephone1", new CrmDataTypeWrapper(partner.Phone, CrmFieldType.String));
                        data.Add("emailaddress1", new CrmDataTypeWrapper(partner.Email, CrmFieldType.String));
                        data.Add("company", new CrmDataTypeWrapper(partner.CompanyName, CrmFieldType.String));
                        data.Add("statecode", new CrmDataTypeWrapper(ContactState.Active, CrmFieldType.Picklist, Contact.EntityLogicalName));

                        SystemUser systemUser = GetSystemUserByID(service, Guid.Empty);
                        data.Add("ownerid", new CrmDataTypeWrapper(systemUser.Id, CrmFieldType.Key));

                        Guid contId = service.CreateNewRecord(Contact.EntityLogicalName, data, "", true, Guid.Empty);

                        if (contId != Guid.Empty)
                        {
                            //ElioCrmUserContacts365 userContact = new ElioCrmUserContacts365();
                            //userContact.UserId = partner.Id;
                            //userContact.CrmContactId = contId.ToString();
                            //userContact.DateInsert = DateTime.Now;
                            //userContact.LastUpdate = DateTime.Now;
                            //userContact.IsActive = 1;

                            //DataLoader<ElioCrmUserContacts365> loaderContact = new DataLoader<ElioCrmUserContacts365>(session);
                            //loaderContact.Insert(userContact);

                            return contId;
                        }
                        else
                        {
                            service.Delete(Contact.EntityLogicalName, new Guid("b46aed72-5899-ec11-b400-6045bd9066d4"));
                            return Guid.Empty;
                        }
                        //}
                    }
                    else
                        return Guid.Empty;
                }
                else
                    return Guid.Empty;
            }
            else
                return Guid.Empty;
        }

        public static SystemUser GetSystemUserByID(CrmServiceClient service, Guid currentUserId)
        {
            if (service.IsReady)
            {
                ColumnSet columnSet = new ColumnSet(true);

                if (currentUserId == Guid.Empty)
                    currentUserId = new Guid("e14d4003-4ea0-ec11-b3fe-0022489d2b1d");   //01cca96d-2a7d-ec11-8d21-00224880bade

                SystemUser user = (SystemUser)service.Retrieve(SystemUser.EntityLogicalName, currentUserId, columnSet);

                if (user.Id != Guid.Empty)
                {
                    return user;
                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}