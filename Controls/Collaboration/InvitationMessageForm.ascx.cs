using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.StripePayment;
using WdS.ElioPlus.Lib.DBQueries;
using Telerik.Web.UI;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.Enums;
using System.Data;

namespace WdS.ElioPlus.Controls.Collaboration
{
    public partial class InvitationMessageForm : System.Web.UI.UserControl
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    FixPage();
                }
                else
                {
                    Response.Redirect(ControlLoader.Default(), false);
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

        private void FixPage()
        {
            divGeneralSuccess.Visible = false;
            divGeneralFailure.Visible = false;

            LoadResellers();
        }

        private void LoadResellers()
        {
            List<ElioUsers> resellers = Sql.GetUsersByCompanyType(EnumHelper.GetDescription(Types.Resellers).ToString(), session);

            CbxExistingUsers.DataValueField = "Email";
            CbxExistingUsers.DataTextField = "CompanyName";
            CbxExistingUsers.DataSource = resellers;
            CbxExistingUsers.DataBind();

            //foreach (ElioUsers reseller in resellers)
            //{
            //    ListItem item = new ListItem();
            //    item.Text = reseller.CompanyName;
            //    item.Value = reseller.Email;
            //    CbxExistingUsers.Items.Add(item);
            //}
        }

        #region Buttons

        protected void BtnAddUser_OnClick(object sender, EventArgs args)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void CbxSelectUser_OnCheckedChanged(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                CheckBox cbx = (CheckBox)sender;
                GridDataItem item = (GridDataItem)cbx.NamingContainer;

                int userId = Convert.ToInt32(item["id"].Text);
                string companyName = item["company_name"].Text;
                string companyEmail = item["email"].Text;

                List<string> emails = new List<string>();
                
                if (cbx.Checked)
                {
                    emails.Add(companyEmail);
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

        protected void CbxExistingUsers_OnTextChanged(object sender, EventArgs args)
        {
            try
            {
                List<string> emails = new List<string>();
                TbxEmail.Text = string.Empty;
                foreach (ListItem itm in CbxExistingUsers.Items)
                {
                    if (itm.Selected)
                    {
                        emails.Add(itm.Value);
                    }
                }

                foreach (string email in emails)
                {
                    TbxEmail.Text += email + ",";
                }

                TbxEmail.Text = (emails.Count > 0) ? TbxEmail.Text.Substring(0, TbxEmail.Text.Length - 1) : string.Empty;
            }

            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnProccedMessage_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    bool successUnsubscription = false;

                    if (TbxSubject.Text == string.Empty)
                    {
                        divGeneralFailure.Visible = true;
                        LblFailure.Text = "Please add Subject";
                        return;
                    }

                    if (TbxMsg.Text == string.Empty)
                    {
                        divGeneralFailure.Visible = true;
                        LblFailure.Text = "Please add Content";
                        return;
                    }

                    if (TbxEmail.Text == string.Empty)
                    {
                        divGeneralFailure.Visible = true;
                        LblFailure.Text = "Please add Email";
                        return;
                    }

                    ElioCollaborationUsersInvitations invitation = new ElioCollaborationUsersInvitations();

                    invitation.UserId = vSession.User.Id;
                    invitation.InvSubject = TbxSubject.Text;
                    invitation.InvContent = TbxMsg.Text;
                    invitation.DateCreated = DateTime.Now;
                    invitation.LastUpdated = DateTime.Now;
                    invitation.IsPublic = 1;

                    DataLoader<ElioCollaborationUsersInvitations> loader = new DataLoader<ElioCollaborationUsersInvitations>(session);
                    loader.Insert(invitation);
                    
                    string[] emailList = TbxEmail.Text.Split(',').ToArray();

                    foreach (string email in emailList)
                    {
                        //ElioCollaborationInvitationRecipients recipient = new ElioCollaborationInvitationRecipients();
                        //recipient.InvitationId = invitation.Id;
                        //recipient.UserId = vSession.User.Id;
                        //recipient.RecipientEmail = email;
                        //recipient.SendDate = DateTime.Now;

                        //DataLoader<ElioCollaborationInvitationRecipients> loader1 = new DataLoader<ElioCollaborationInvitationRecipients>(session);
                        //loader1.Insert(recipient);
                    }

                    if (successUnsubscription)
                    {

                        divGeneralSuccess.Visible = true;
                        LblGeneralSuccess.Text = "Done! ";
                        LblSuccess.Text = "Your premium plan was canceled successfully!";
                    }
                    else
                    {
                        divGeneralFailure.Visible = true;
                        LblGeneralFailure.Text = "Error! ";
                        LblFailure.Text = "Your premium plan could not be canceled. Please try again later or contact with us";
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnCancelMessage_OnClick(object sender, EventArgs args)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseInvitationPopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion

        #region Grids

        protected void RdgUsers_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                List<ElioUsers> users = Sql.GetUsersByCompanyType(EnumHelper.GetDescription(Types.Resellers).ToString(), session);
               
                if (users.Count > 0)
                {
                    RdgUsers.Visible = true;

                    DataTable table = new DataTable();

                    table.Columns.Add("id");
                    table.Columns.Add("company_name");
                    table.Columns.Add("email");

                    foreach (ElioUsers user in users)
                    {
                        table.Rows.Add(user.Id, user.CompanyName, user.Email);
                    }

                    RdgUsers.DataSource = table;
                }
                else
                {
                    RdgUsers.Visible = false;
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

        #endregion
    }
}