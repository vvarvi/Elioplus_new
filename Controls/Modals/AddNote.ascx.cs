using System;
using System.Linq;
using System.Web.UI;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Utils;
using Telerik.Web.UI;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Controls.Modals
{
    public partial class AddNote : System.Web.UI.UserControl
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        public int OpportunityId
        {
            get
            {
                return ViewState["OpportunityId"] == null ? 0 : (int)ViewState["OpportunityId"];
            }
            set
            {
                ViewState["OpportunityId"] = value;
            }
        }

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

        #region Methods

        private void FixPage()
        {
            ResetFields();

            BtnBack.Visible = (Request.QueryString["btn"] != null) ? true : false;
        }

        private void ResetFields()
        {
            divGeneralSuccess.Visible = false;
            divGeneralFailure.Visible = false;

            LblFailure.Text = string.Empty;
            LblSuccess.Text = string.Empty;
        }

        #endregion

        #region Buttons

        protected void BtnProccedMessage_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ResetFields();

                if (vSession.User != null)
                {
                    if (OpportunityId > 0 || Request.QueryString["opportunityViewID"] != null)
                    {
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

                        ElioOpportunitiesNotes note = new ElioOpportunitiesNotes();

                        note.UserId = vSession.User.Id;
                        note.OpportunityUserId = Convert.ToInt32(Session[Request.QueryString["opportunityViewID"]]);
                        note.NoteSubject = TbxSubject.Text;
                        note.NoteContent = TbxMsg.Text;
                        note.Sysdate = DateTime.Now;
                        note.LastUpdated = DateTime.Now;
                        note.IsPublic = 1;

                        DataLoader<ElioOpportunitiesNotes> loader = new DataLoader<ElioOpportunitiesNotes>(session);
                        loader.Insert(note);

                        divGeneralSuccess.Visible = true;
                        LblGeneralSuccess.Text = "Done! ";
                        LblSuccess.Text = "Your note was added successfully!";

                        RadGrid rdgNotes = (RadGrid)ControlFinder.FindControlBackWards(this, "RdgNotes");
                        if (rdgNotes != null)
                            rdgNotes.Rebind();

                        TbxSubject.Text = string.Empty;
                        TbxMsg.Text = string.Empty;
                    }
                    else
                    {
                        divGeneralFailure.Visible = true;
                        LblFailure.Text = "You must select a contact in order to attach a note on it";
                        return;
                    }
                }
                else
                {
                    Response.Redirect(vSession.Page, false);
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

        protected void BtnCancelMessage_OnClick(object sender, EventArgs args)
        {
            try
            {
                ResetFields();

                TbxSubject.Text = string.Empty;
                TbxMsg.Text = string.Empty;

                ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "ClosePopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnBack_OnClick(object sender, EventArgs args)
        {
            try
            {
                Response.Redirect(ControlLoader.Dashboard(vSession.User, "opportunities"), false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion
    }
}