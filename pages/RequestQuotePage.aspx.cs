using System;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using System.Collections.Generic;
using System.Linq;
using WdS.ElioPlus.Objects;
using System.Web;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Controls.AlertControls;
using System.Runtime.CompilerServices;

namespace WdS.ElioPlus.pages
{
    public partial class RequestQuotePage : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        public string Translation
        {
            get
            {
                if (ViewState["Translation"] == null)
                {
                    string name = "";

                    try
                    {
                        Uri path = HttpContext.Current.Request.Url;
                        var pathSegs = path.Segments;
                        string segs = pathSegs[1].TrimEnd('/').TrimEnd('-');

                        if (pathSegs.Length >= 3 && segs.Length == 2)
                        {
                            if (!Lib.Utils.Validations.IsNumber(segs))
                            {
                                name = segs.TrimEnd().ToLower();

                                ViewState["Translation"] = name;
                            }
                            else
                                ViewState["Translation"] = "";
                        }
                        else
                            ViewState["Translation"] = "";
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    }

                    return ViewState["Translation"].ToString();
                }
                else
                    return ViewState["Translation"].ToString();
            }
            set
            {
                ViewState["Translation"] = value;
            }
        }

        public int UserID
        {
            get
            {
                if (ViewState["UserID"] == null)
                {
                    int id = 0;

                    try
                    {
                        Uri path = HttpContext.Current.Request.Url;
                        var pathSegs = path.Segments;
                        string segs = pathSegs[pathSegs.Length - 2].TrimEnd('/').TrimEnd('-');

                        if (pathSegs.Length > 2)
                        {
                            if (Lib.Utils.Validations.IsNumber(segs))
                            {
                                id = Convert.ToInt32(segs.TrimEnd());

                                ViewState["UserID"] = id;
                            }
                            else
                                ViewState["UserID"] = 0;
                        }
                        else
                            ViewState["UserID"] = 0;
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    }

                    return (int)ViewState["UserID"];
                }
                else
                    return (int)ViewState["UserID"];
            }
            set
            {
                ViewState["UserID"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (!IsPostBack)
                    FixPage();

                if (UserID > 0)
                {
                    ElioUsers user = Sql.GetUserById(UserID, session);
                    if (user != null)
                    {
                        string message = user.UserApplicationType == (int)UserApplicationType.ThirdParty ? "This request will be sent to all similar businesses on our network to help you compare prices. The <b>{ThirdPartyCompanyName}</b> profile is currently not managed or endorsed by <b>{ThirdPartyCompanyName}</b>.".Replace("{ThirdPartyCompanyName}", user.CompanyName) : "This request will be sent to all similar businesses on our network to help you compare prices.";

                        GlobalMethods.ShowMessageAlertControl(UcMessageAlertTop, message, MessageTypes.Info, true, true, false, false, false);
                    }
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
            LoadCountries();
            ResetRFPsFields();
            UpdateStrings();
            SetLinks();
        }

        private void SetLinks()
        {
            //aClose.HRef = ControlLoader.SearchForChannelPartners;
        }

        private void UpdateStrings()
        {
            try
            {
                HtmlGenericControl pgTitle = (HtmlGenericControl)ControlFinder.FindControlBackWards(Master, "PgTitle");
                HtmlControl metaDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
                HtmlControl metaKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

                string msg1 = "";
                string msg2 = "";

                if (Translation == "")
                {
                    if (pgTitle != null)
                        pgTitle.InnerText = "Get a quote from MSPs and IT companies near you";

                    metaDescription.Attributes["content"] = "Share your requirements and get quotations from Managed Service Providers, IT companies and agencies near you to compare prices.";
                    metaKeywords.Attributes["content"] = "get quote, quotations, Elioplus quotations";

                    LblTitleStep1.Text = LblTitleStep1F.Text = "1. Requirements";
                    LblTitleStep2.Text = LblTitleStep2F.Text = "2. Your details";
                    LblTitleStep3.Text = LblTitleStep3F.Text = "3. Success";
                    msg1 = "Your RFQ has been submitted successfully!";
                    msg2 = "You will receive quotations directly to your inbox. This request will be sent to relevant businesses to help you compare prices.";

                    GlobalMethods.ShowMessageControlWith2Messages(UcMessageSuccess, msg1, msg2, MessageTypes.Success, true, true, false, false, true);

                    TbxProduct.Attributes["placeholder"] = "Enter Product/Technology";
                    LblProduct.Text = "Product/Technology";
                    TbxNumberUnits.Attributes["placeholder"] = "Enter No. of Users/Items";
                    LblNumberUnits.Text = "No. of Users/Items";
                    TbxMessage.Attributes["placeholder"] = "Enter your message and describe your requirements";
                    LblMessage.Text = "Message";
                    TbxFirstName.Attributes["placeholder"] = "Enter your First Name";
                    LblFirstName.Text = "First Name";
                    TbxLastName.Attributes["placeholder"] = "Enter your Last Name";
                    LblLastName.Text = "Last Name";
                    TbxCompanyEmail.Attributes["placeholder"] = "Enter your Work Email";
                    LblCompanyEmail.Text = "Work Email";
                    TbxPhoneNumber.Attributes["placeholder"] = "Enter your Phone Number";
                    LblPhoneNumber.Text = "Phone Number";
                    TbxBusinessName.Attributes["placeholder"] = "Enter your Company Name";
                    LblBusinessName.Text = "Company";
                    TbxCity.Attributes["placeholder"] = "Enter your City";
                    LblCity.Text = "City";
                    LblCountry.Text = "Country";

                    BtnBack.Text = "Back";
                    BtnProceed.Text = "Next";

                    //LblDemoSuccessMsg.Text = "";
                    //LblDemoSuccessMsgContent.Text = "";
                    UcMessageAlert.Visible = false;
                }
                else
                {
                    switch (Translation)
                    {
                        case "es":
                        case "la":

                            if (pgTitle != null)
                                pgTitle.InnerText = "Obtenga una cotización de MSP y empresas de TI cerca de usted";

                            metaDescription.Attributes["content"] = "Comparta sus requisitos y obtenga cotizaciones de proveedores de servicios administrados, empresas de TI y agencias cercanas a usted para comparar precios.";
                            metaKeywords.Attributes["content"] = "obtener cotización, cotizaciones, Elioplus cotizaciones";

                            LblTitleStep1.Text = LblTitleStep1F.Text = "1. Requisitos";
                            LblTitleStep2.Text = LblTitleStep2F.Text = "2. Tus detalles";
                            LblTitleStep3.Text = LblTitleStep3F.Text = "3. Éxito";
                            msg1 = "¡Su RFP se ha enviado con éxito!";
                            msg2 = "Recibirá cotizaciones directamente en su bandeja de entrada. Esta solicitud se enviará a las empresas pertinentes para ayudarlo a comparar precios.";

                            GlobalMethods.ShowMessageControlWith2Messages(UcMessageSuccess, msg1, msg2, MessageTypes.Success, true, true, false, false, true);

                            TbxProduct.Attributes["placeholder"] = LblProduct.Text = "Producto/Tecnología";
                            TbxNumberUnits.Attributes["placeholder"] = LblNumberUnits.Text = "Número de usuarios/elementos";
                            TbxMessage.Attributes["placeholder"] = LblMessage.Text = "Descripción";
                            TbxFirstName.Attributes["placeholder"] = LblFirstName.Text = "Primer nombre";
                            TbxLastName.Attributes["placeholder"] = LblLastName.Text = "Apellido";
                            TbxCompanyEmail.Attributes["placeholder"] = LblCompanyEmail.Text = "Correo electrónico del trabajo";
                            TbxPhoneNumber.Attributes["placeholder"] = LblPhoneNumber.Text = "Número de teléfono";
                            TbxBusinessName.Attributes["placeholder"] = LblBusinessName.Text = "Compañía";
                            TbxCity.Attributes["placeholder"] = LblCity.Text = "Ciudad";
                            LblCountry.Text = "País";

                            BtnBack.Text = "Back";
                            BtnProceed.Text = "Next";

                            break;

                        case "pt":

                            if (pgTitle != null)
                                pgTitle.InnerText = "Obtenha uma cotação de MSPs e empresas de TI perto de você";

                            metaDescription.Attributes["content"] = "Compartilhe seus requisitos e obtenha cotações de provedores de serviços gerenciados, empresas de TI e agências perto de você para comparar preços.";
                            metaKeywords.Attributes["content"] = "obter cotação, citações, Elioplus citações";

                            LblTitleStep1.Text = LblTitleStep1F.Text = "1. Requisitos";
                            LblTitleStep2.Text = LblTitleStep2F.Text = "2. Seus detalhes";
                            LblTitleStep3.Text = LblTitleStep3F.Text = "3. Sucesso";
                            msg1 = "Sua RFQ foi enviada com sucesso!";
                            msg2 = "Você receberá cotações diretamente em sua caixa de entrada. Esta solicitação será enviada às empresas relevantes para ajudá-lo a comparar preços.";

                            GlobalMethods.ShowMessageControlWith2Messages(UcMessageSuccess, msg1, msg2, MessageTypes.Success, true, true, false, false, true);

                            TbxProduct.Attributes["placeholder"] = LblProduct.Text = "Produto/Tecnologia";
                            TbxNumberUnits.Attributes["placeholder"] = LblNumberUnits.Text = "Nº de usuários/itens";
                            TbxMessage.Attributes["placeholder"] = LblMessage.Text = "Descrição";
                            TbxFirstName.Attributes["placeholder"] = LblFirstName.Text = "Primeiro nome";
                            TbxLastName.Attributes["placeholder"] = LblLastName.Text = "Último nome";
                            TbxCompanyEmail.Attributes["placeholder"] = LblCompanyEmail.Text = "Email de trabalho";
                            TbxPhoneNumber.Attributes["placeholder"] = LblPhoneNumber.Text = "Número de telefone";
                            TbxBusinessName.Attributes["placeholder"] = LblBusinessName.Text = "Companhia";
                            TbxCity.Attributes["placeholder"] = LblCity.Text = "Cidade";
                            LblCountry.Text = "País";

                            BtnBack.Text = "Back";
                            BtnProceed.Text = "Next";

                            break;

                        case "de":

                            if (pgTitle != null)
                                pgTitle.InnerText = "Fordern Sie ein Angebot von MSPs und IT-Unternehmen in Ihrer Nähe an";

                            metaDescription.Attributes["content"] = "Teilen Sie Ihre Anforderungen mit und holen Sie Angebote von Managed Service Providern, IT-Unternehmen und Agenturen in Ihrer Nähe ein, um Preise zu vergleichen.";
                            metaKeywords.Attributes["content"] = "Erhalten Sie Zitat, Zitate, Elioplus Zitate";

                            LblTitleStep1.Text = LblTitleStep1F.Text = "1. Anforderungen";
                            LblTitleStep2.Text = LblTitleStep2F.Text = "2. Deine Details";
                            LblTitleStep3.Text = LblTitleStep3F.Text = "3. Erfolg";
                            msg1 = "Ihre Anfrage wurde erfolgreich übermittelt!";
                            msg2 = "Sie erhalten Angebote direkt in Ihren Posteingang. Diese Anfrage wird an relevante Unternehmen gesendet, um Ihnen beim Preisvergleich zu helfen.";

                            GlobalMethods.ShowMessageControlWith2Messages(UcMessageSuccess, msg1, msg2, MessageTypes.Success, true, true, false, false, true);

                            TbxProduct.Attributes["placeholder"] = LblProduct.Text = "Produkt/Technologie";
                            TbxNumberUnits.Attributes["placeholder"] = LblNumberUnits.Text = "Anzahl der Benutzer/Elemente ";
                            TbxMessage.Attributes["placeholder"] = LblMessage.Text = "Beschreibung";
                            TbxFirstName.Attributes["placeholder"] = LblFirstName.Text = "Vorname";
                            TbxLastName.Attributes["placeholder"] = LblLastName.Text = "Nachname";
                            TbxCompanyEmail.Attributes["placeholder"] = LblCompanyEmail.Text = "Arbeits Email";
                            TbxPhoneNumber.Attributes["placeholder"] = LblPhoneNumber.Text = "Telefonnummer";
                            TbxBusinessName.Attributes["placeholder"] = LblBusinessName.Text = "Gesellschaft";
                            TbxCity.Attributes["placeholder"] = LblCity.Text = "Stadt";
                            LblCountry.Text = "Land";

                            BtnBack.Text = "Back";
                            BtnProceed.Text = "Next";

                            break;

                        case "pl":

                            if (pgTitle != null)
                                pgTitle.InnerText = "Poproś o wycenę MSP i firmy IT w Twojej okolicy";

                            metaDescription.Attributes["content"] = "Podziel się swoimi potrzebami i uzyskaj wyceny od dostawców usług zarządzanych, firm IT i agencji w Twojej okolicy, aby porównać ceny.";
                            metaKeywords.Attributes["content"] = "Być cytowanym, cytaty, Elioplus cytaty";

                            LblTitleStep1.Text = LblTitleStep1F.Text = "1. Wymagania";
                            LblTitleStep2.Text = LblTitleStep2F.Text = "2. Twoje szczegóły";
                            LblTitleStep3.Text = LblTitleStep3F.Text = "3. Sukces";
                            msg1 = "Twoje zapytanie ofertowe zostało pomyślnie przesłane!";
                            msg2 = "Oferty otrzymasz bezpośrednio na swoją skrzynkę e-mail. Ta prośba zostanie wysłana do odpowiednich firm, aby pomóc Ci porównać ceny.";

                            GlobalMethods.ShowMessageControlWith2Messages(UcMessageSuccess, msg1, msg2, MessageTypes.Success, true, true, false, false, true);

                            TbxProduct.Attributes["placeholder"] = LblProduct.Text = "Produkt/Technologia";
                            TbxNumberUnits.Attributes["placeholder"] = LblNumberUnits.Text = "Liczba użytkowników/elementów";
                            TbxMessage.Attributes["placeholder"] = LblMessage.Text = "Opis";
                            TbxFirstName.Attributes["placeholder"] = LblFirstName.Text = "Imię";
                            TbxLastName.Attributes["placeholder"] = LblLastName.Text = "Nazwisko";
                            TbxCompanyEmail.Attributes["placeholder"] = LblCompanyEmail.Text = "Poczta robocza";
                            TbxPhoneNumber.Attributes["placeholder"] = LblPhoneNumber.Text = "Numer telefonu";
                            TbxBusinessName.Attributes["placeholder"] = LblBusinessName.Text = "Spółka";
                            TbxCity.Attributes["placeholder"] = LblCity.Text = "Miasto";
                            LblCountry.Text = "Kraj";

                            BtnBack.Text = "Back";
                            BtnProceed.Text = "Next";

                            break;

                        case "it":

                            if (pgTitle != null)
                                pgTitle.InnerText = "Chiedi un preventivo alle MSPs e alle aziende IT della tua zona";

                            metaDescription.Attributes["content"] = "Condividi le tue esigenze e ottieni preventivi da fornitori di servizi gestiti, società IT e agenzie nella tua zona per confrontare i prezzi.";
                            metaKeywords.Attributes["content"] = "Da citare, citazioni, Elioplus citazioni";

                            LblTitleStep1.Text = LblTitleStep1F.Text = "1. Requisiti";
                            LblTitleStep2.Text = LblTitleStep2F.Text = "2. I tuoi dettagli";
                            LblTitleStep3.Text = LblTitleStep3F.Text = "3. Successo";
                            msg1 = "La tua richiesta di offerta è stata inviata correttamente!";
                            msg2 = "Riceverai preventivi direttamente nella tua casella di posta. Questa richiesta verrà inviata alle attività commerciali pertinenti per aiutarti a confrontare i prezzi.";

                            GlobalMethods.ShowMessageControlWith2Messages(UcMessageSuccess, msg1, msg2, MessageTypes.Success, true, true, false, false, true);

                            TbxProduct.Attributes["placeholder"] = LblProduct.Text = "Prodotto/Tecnologia";
                            TbxNumberUnits.Attributes["placeholder"] = LblNumberUnits.Text = "N. di utenti/articoli";
                            TbxMessage.Attributes["placeholder"] = LblMessage.Text = "Descrizione";
                            TbxFirstName.Attributes["placeholder"] = LblFirstName.Text = "Nome";
                            TbxLastName.Attributes["placeholder"] = LblLastName.Text = "Cognome";
                            TbxCompanyEmail.Attributes["placeholder"] = LblCompanyEmail.Text = "Email di lavoro";
                            TbxPhoneNumber.Attributes["placeholder"] = LblPhoneNumber.Text = "Numero di telefono";
                            TbxBusinessName.Attributes["placeholder"] = LblBusinessName.Text = "Azienda";
                            TbxCity.Attributes["placeholder"] = LblCity.Text = "Città";
                            LblCountry.Text = "Paese";

                            BtnBack.Text = "Back";
                            BtnProceed.Text = "Next";

                            break;

                        case "nl":

                            if (pgTitle != null)
                                pgTitle.InnerText = "Vraag een offerte aan bij MSPs en IT-bedrijven bij u in de buurt";

                            metaDescription.Attributes["content"] = "Deel uw behoeften en ontvang offertes van managed service providers, IT-bedrijven en bureaus bij u in de buurt om prijzen te vergelijken.";
                            metaKeywords.Attributes["content"] = "Vermeld worden, citaten, Elioplus citaten";

                            LblTitleStep1.Text = LblTitleStep1F.Text = "1. Voorwaarden";
                            LblTitleStep2.Text = LblTitleStep2F.Text = "2. Jouw details";
                            LblTitleStep3.Text = LblTitleStep3F.Text = "3. Succes";
                            msg1 = "Uw RFQ is succesvol ingediend!";
                            msg2 = "Offertes ontvang je direct in je inbox. Dit verzoek wordt verzonden naar relevante bedrijven om u te helpen prijzen te vergelijken.";

                            GlobalMethods.ShowMessageControlWith2Messages(UcMessageSuccess, msg1, msg2, MessageTypes.Success, true, true, false, false, true);

                            TbxProduct.Attributes["placeholder"] = LblProduct.Text = "Product/Technologie";
                            TbxNumberUnits.Attributes["placeholder"] = LblNumberUnits.Text = "Aantal gebruikers/items";
                            TbxMessage.Attributes["placeholder"] = LblMessage.Text = "Beschrijving";
                            TbxFirstName.Attributes["placeholder"] = LblFirstName.Text = "Voornaam";
                            TbxLastName.Attributes["placeholder"] = LblLastName.Text = "Achternaam";
                            TbxCompanyEmail.Attributes["placeholder"] = LblCompanyEmail.Text = "Werk email";
                            TbxPhoneNumber.Attributes["placeholder"] = LblPhoneNumber.Text = "Telefoonnummer";
                            TbxBusinessName.Attributes["placeholder"] = LblBusinessName.Text = "Bedrijf";
                            TbxCity.Attributes["placeholder"] = LblCity.Text = "Stad";
                            LblCountry.Text = "Land";

                            BtnBack.Text = "Back";
                            BtnProceed.Text = "Next";

                            break;

                        case "fr":

                            if (pgTitle != null)
                                pgTitle.InnerText = "Demandez un devis aux MSP et aux entreprises informatiques près de chez vous";

                            metaDescription.Attributes["content"] = "Partagez vos besoins et obtenez des devis auprès de fournisseurs de services gérés, de sociétés informatiques et d'agences près de chez vous pour comparer les prix.";
                            metaKeywords.Attributes["content"] = "Demander Un Devis, devis, Elioplus devis";

                            LblTitleStep1.Text = LblTitleStep1F.Text = "1. Conditions";
                            LblTitleStep2.Text = LblTitleStep2F.Text = "2. Vos détails";
                            LblTitleStep3.Text = LblTitleStep3F.Text = "3. Succès";
                            msg1 = "Votre RFQ a été soumise avec succès!";
                            msg2 = "Vous recevrez des devis directement dans votre boîte de réception. Cette demande sera envoyée aux entreprises concernées pour vous aider à comparer les prix.";

                            GlobalMethods.ShowMessageControlWith2Messages(UcMessageSuccess, msg1, msg2, MessageTypes.Success, true, true, false, false, true);

                            TbxProduct.Attributes["placeholder"] = LblProduct.Text = "Produit/Technologie";
                            TbxNumberUnits.Attributes["placeholder"] = LblNumberUnits.Text = "Nombre d'utilisateurs/éléments";
                            TbxMessage.Attributes["placeholder"] = LblMessage.Text = "Description";
                            TbxFirstName.Attributes["placeholder"] = LblFirstName.Text = "Prénom";
                            TbxLastName.Attributes["placeholder"] = LblLastName.Text = "Nom de famille";
                            TbxCompanyEmail.Attributes["placeholder"] = LblCompanyEmail.Text = "Email de travail";
                            TbxPhoneNumber.Attributes["placeholder"] = LblPhoneNumber.Text = "Numéro de téléphone";
                            TbxBusinessName.Attributes["placeholder"] = LblBusinessName.Text = "Compagnie";
                            TbxCity.Attributes["placeholder"] = LblCity.Text = "Ville";
                            LblCountry.Text = "Pays";

                            BtnBack.Text = "Back";
                            BtnProceed.Text = "Next";

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private void ResetRFPsFields()
        {
            //divDemoWarningMsg.Visible = false;
            //divDemoSuccessMsg.Visible = false;
            UcMessageAlert.Visible = false;

            divStepOne.Visible = true;
            divStepOne.Attributes["class"] = "w-full flex flex-col gap-30px step-content active";

            divStepTwo.Visible = divStepThree.Visible = false;
            divStepTwo.Attributes["class"] = divStepThree.Attributes["class"] = "w-full flex flex-col gap-30px step-content";

            LoadStep(1);

            BtnBack.Visible = false;
            //aClose.Visible = false;
            BtnProceed.Text = "Next";

            TbxFirstName.Text = "";
            TbxCompanyEmail.Text = "";
            TbxLastName.Text = "";
            TbxBusinessName.Text = "";
            TbxPhoneNumber.Text = "";
            DdlCountries.SelectedIndex = -1;
            TbxProduct.Text = "";
            TbxNumberUnits.Text = "";
            TbxMessage.Text = "";
            TbxCity.Text = "";

            HdnLeadId.Value = "0";
        }

        private void LoadCountries()
        {
            DdlCountries.Items.Clear();

            ListItem item = new ListItem();
            item.Value = "0";
            item.Text = "Select Country";

            DdlCountries.Items.Add(item);

            List<ElioCountries> countries = Sql.GetPublicCountries(session);
            foreach (ElioCountries country in countries)
            {
                item = new ListItem();
                item.Value = country.Id.ToString();
                item.Text = country.CountryName;

                DdlCountries.Items.Add(item);
            }
        }

        private void LoadStep(int step)
        {
            switch(step)
            {
                case 1:

                    aStep1.Attributes["class"] = "stepTab flex flex-col gap-10px items-center justify-center text-center relative z-2 active";
                    divStepOne.Visible = true;
                    divStepOne.Attributes["class"] = "w-full flex flex-col gap-30px step-content active";
                    
                    aStep2.Attributes["class"] = aStep3.Attributes["class"] = "stepTab flex flex-col gap-10px items-center justify-center text-center relative z-2";
                    divStepTwo.Visible = divStepThree.Visible = false;
                    divStepTwo.Attributes["class"] = divStepThree.Attributes["class"] = "w-full flex flex-col gap-30px step-content";
                    
                    break;

                case 2:

                    aStep2.Attributes["class"] = "stepTab flex flex-col gap-10px items-center justify-center text-center relative z-2 active";
                    divStepTwo.Visible = true;
                    divStepTwo.Attributes["class"] = "w-full flex flex-col gap-30px step-content active";

                    aStep1.Attributes["class"] = aStep3.Attributes["class"] = "stepTab flex flex-col gap-10px items-center justify-center text-center relative z-2";
                    divStepOne.Visible = divStepThree.Visible = false;
                    divStepOne.Attributes["class"] = divStepThree.Attributes["class"] = "w-full flex flex-col gap-30px step-content";

                    break;

                case 3:

                    aStep3.Attributes["class"] = "stepTab flex flex-col gap-10px items-center justify-center text-center relative z-2 active";
                    divStepThree.Visible = true;
                    divStepThree.Attributes["class"] = "w-full flex flex-col gap-30px step-content active";

                    aStep1.Attributes["class"] = aStep2.Attributes["class"] = "stepTab flex flex-col gap-10px items-center justify-center text-center relative z-2";
                    divStepOne.Visible = divStepTwo.Visible = false;
                    divStepOne.Attributes["class"] = divStepTwo.Attributes["class"] = "w-full flex flex-col gap-30px step-content";

                    break;
            }
        }

        #endregion

        #region Buttons

        protected void BtnProceed_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                UcMessageAlert.Visible = false;

                //divDemoSuccessMsg.Visible = divDemoWarningMsg.Visible = false;

                if (divStepOne.Visible)
                {
                    if (TbxProduct.Text == "")
                    {
                        //divDemoWarningMsg.Visible = true;
                        //LblDemoWarningMsg.Text = "Error! ";
                        //LblDemoWarningMsgContent.Text = "Please fill your product/technology";
                        GlobalMethods.ShowMessageAlertControl(UcMessageAlert, "Please fill your product/technology", MessageTypes.Error, true, true, false, false, false);
                        return;
                    }

                    if (TbxNumberUnits.Text == "")
                    {
                        //divDemoWarningMsg.Visible = true;
                        //LblDemoWarningMsg.Text = "Error! ";
                        //LblDemoWarningMsgContent.Text = "Please fill number of users/units";
                        GlobalMethods.ShowMessageAlertControl(UcMessageAlert, "Please fill number of users/units", MessageTypes.Error, true, true, false, false, false);
                        return;
                    }
                    else
                    {
                        if (!Validations.IsNumber(TbxNumberUnits.Text))
                        {
                            //divDemoWarningMsg.Visible = true;
                            //LblDemoWarningMsg.Text = "Error! ";
                            //LblDemoWarningMsgContent.Text = "Please fill only numbers for users/units";
                            GlobalMethods.ShowMessageAlertControl(UcMessageAlert, "Please fill only numbers for users/units", MessageTypes.Error, true, true, false, false, false);
                            return;
                        }
                    }

                    if (TbxMessage.Text == "")
                    {
                        //divDemoWarningMsg.Visible = true;
                        //LblDemoWarningMsg.Text = "Error! ";
                        //LblDemoWarningMsgContent.Text = "Please fill a message";
                        GlobalMethods.ShowMessageAlertControl(UcMessageAlert, "Please fill a message", MessageTypes.Error, true, true, false, false, false);
                        return;
                    }

                    LoadStep(2);

                    BtnProceed.Text = "Submit";
                    BtnBack.Visible = true;
                }
                else if (divStepTwo.Visible)
                {
                    if (TbxFirstName.Text == "")
                    {
                        //divDemoWarningMsg.Visible = true;
                        //LblDemoWarningMsg.Text = "Error! ";
                        //LblDemoWarningMsgContent.Text = "Please fill your first name";
                        GlobalMethods.ShowMessageAlertControl(UcMessageAlert, "Please fill your first name", MessageTypes.Error, true, true, false, false, false);
                        return;
                    }

                    if (TbxLastName.Text == "")
                    {
                        //divDemoWarningMsg.Visible = true;
                        //LblDemoWarningMsg.Text = "Error! ";
                        //LblDemoWarningMsgContent.Text = "Please fill your last name";
                        GlobalMethods.ShowMessageAlertControl(UcMessageAlert, "Please fill your last name", MessageTypes.Error, true, true, false, false, false);
                        return;
                    }

                    if (TbxCompanyEmail.Text == "")
                    {
                        //divDemoWarningMsg.Visible = true;
                        //LblDemoWarningMsg.Text = "Error! ";
                        //LblDemoWarningMsgContent.Text = "Please fill your company email";
                        GlobalMethods.ShowMessageAlertControl(UcMessageAlert, "Please fill your company email", MessageTypes.Error, true, true, false, false, false);
                        return;
                    }
                    else
                    {
                        if (!Validations.IsEmail(TbxCompanyEmail.Text))
                        {
                            //divDemoWarningMsg.Visible = true;
                            //LblDemoWarningMsg.Text = "Error! ";
                            //LblDemoWarningMsgContent.Text = "Please fill a valid company email";
                            GlobalMethods.ShowMessageAlertControl(UcMessageAlert, "Please fill a valid company email", MessageTypes.Error, true, true, false, false, false);
                            return;
                        }
                    }

                    if (TbxBusinessName.Text == "")
                    {
                        //divDemoWarningMsg.Visible = true;
                        //LblDemoWarningMsg.Text = "Error! ";
                        //LblDemoWarningMsgContent.Text = "Please fill your company name";
                        GlobalMethods.ShowMessageAlertControl(UcMessageAlert, "Please fill your company name", MessageTypes.Error, true, true, false, false, false);
                        return;
                    }

                    if (DdlCountries.SelectedValue == "0" || DdlCountries.SelectedValue == "")
                    {
                        //divDemoWarningMsg.Visible = true;
                        //LblDemoWarningMsg.Text = "Error! ";
                        //LblDemoWarningMsgContent.Text = "Please select your country";
                        GlobalMethods.ShowMessageAlertControl(UcMessageAlert, "Please fill your country", MessageTypes.Error, true, true, false, false, false);
                        return;
                    }

                    if (TbxCity.Text == "")
                    {
                        //divDemoWarningMsg.Visible = true;
                        //LblDemoWarningMsg.Text = "Error! ";
                        //LblDemoWarningMsgContent.Text = "Please fill your city";
                        GlobalMethods.ShowMessageAlertControl(UcMessageAlert, "Please fill your city", MessageTypes.Error, true, true, false, false, false);
                        return;
                    }

                    if (TbxPhoneNumber.Text == "")
                    {
                        //divDemoWarningMsg.Visible = true;
                        //LblDemoWarningMsg.Text = "Error! ";
                        //LblDemoWarningMsgContent.Text = "Please fill your phone number";
                        GlobalMethods.ShowMessageAlertControl(UcMessageAlert, "Please fill your phone number", MessageTypes.Error, true, true, false, false, false);
                        return;
                    }

                    if (!CbxAgree.Checked)
                    {
                        GlobalMethods.ShowMessageAlertControl(UcMessageAlert, "You have to accept the terms & conditions of Elioplus", MessageTypes.Error, true, true, false, false, false);
                        return;
                    }

                    ElioSnitcherWebsiteLeads lead = null;

                    DataLoader<ElioSnitcherWebsiteLeads> loader = new DataLoader<ElioSnitcherWebsiteLeads>(session);

                    if (HdnLeadId.Value == "0")
                    {
                        lead = new ElioSnitcherWebsiteLeads();

                        lead.WebsiteId = "19976";
                        lead.ElioSnitcherWebsiteId = 2;
                        lead.SessionReferrer = "https://www.elioplus.com";
                        lead.SessionOperatingSystem = "";
                        lead.SessionBrowser = "";
                        lead.SessionDeviceType = "";
                        lead.SessionCampaign = "";
                        lead.SessionStart = DateTime.Now;
                        lead.SessionDuration = 0;
                        lead.SessionTotalPageviews = 1;

                        string number = Guid.NewGuid().GetHashCode().ToString().Substring(0, 8);
                        int count = 1;
                        while (Sql.GetSnitcherLeadIDByLeadId(number, session) != "" && count < 10)
                        {
                            number = Guid.NewGuid().GetHashCode().ToString().Substring(0, 8);
                            count++;
                        }

                        if (count >= 10)
                        {
                            throw new Exception("ERROR -> string number = Guid.NewGuid().GetHashCode().ToString().Substring(0, 8) could not created");
                        }

                        lead.LeadId = number;
                        lead.LeadLastSeen = DateTime.Now;
                        lead.LeadFirstName = TbxFirstName.Text;
                        lead.LeadLastName = TbxLastName.Text;
                        lead.LeadCompanyName = TbxBusinessName.Text;
                        lead.LeadCompanyLogo = "";
                        lead.LeadCompanyWebsite = "";
                        lead.LeadCountry = DdlCountries.SelectedItem.Text;
                        lead.LeadCity = TbxCity.Text;
                        lead.LeadCompanyAddress = "";
                        lead.LeadCompanyFounded = "0";
                        lead.LeadCompanySize = TbxNumberUnits.Text;
                        lead.LeadCompanyIndustry = "";
                        lead.LeadCompanyPhone = TbxPhoneNumber.Text;
                        lead.LeadCompanyEmail = TbxCompanyEmail.Text;
                        lead.LeadCompanyContacts = "";
                        lead.LeadLinkedinHandle = "";
                        lead.LeadFacebookHandle = "";
                        lead.LeadYoutubeHandle = "";
                        lead.LeadInstagramHandle = "";
                        lead.LeadTwitterHandle = "";
                        lead.LeadPinterestHandle = "";
                        lead.LeadAngellistHandle = "";
                        lead.Message = TbxMessage.Text;
                        lead.IsApiLead = (int)ApiLeadCategory.isRFQRequest;
                        lead.IsApproved = 0;
                        lead.IsPublic = 1;
                        lead.IsConfirmed = 0;
                        lead.Sysdate = DateTime.Now;
                        lead.LastUpdate = DateTime.Now;

                        loader.Insert(lead);

                        HdnLeadId.Value = lead.Id.ToString();

                        List<string> products = TbxProduct.Text.Trim().Split(',').ToList();
                        if (products.Count > 0)
                        {
                            foreach (string product in products)
                            {
                                if (product != "")
                                {
                                    ElioSnitcherLeadsPageviews pageView = new ElioSnitcherLeadsPageviews();

                                    pageView.LeadId = lead.LeadId;
                                    pageView.ElioWebsiteLeadsId = lead.Id;
                                    pageView.Url = product.Trim();
                                    pageView.Product = product.Trim();
                                    pageView.TimeSpent = 1;
                                    pageView.ActionTime = DateTime.Now;
                                    pageView.Sysdate = DateTime.Now;
                                    pageView.LastUpdate = DateTime.Now;

                                    DataLoader<ElioSnitcherLeadsPageviews> loaderView = new DataLoader<ElioSnitcherLeadsPageviews>(session);
                                    loaderView.Insert(pageView);
                                }
                            }
                        }
                    }
                    else
                    {
                        lead = Sql.GetSnitcherWebsiteLeadById(Convert.ToInt32(HdnLeadId.Value), session);
                        if (lead != null)
                        {
                            lead.LeadFirstName = TbxFirstName.Text;
                            lead.LeadLastName = TbxLastName.Text;
                            lead.LeadCompanyName = TbxBusinessName.Text;
                            lead.LeadCountry = DdlCountries.SelectedItem.Text;
                            lead.LeadCity = TbxCity.Text;
                            lead.LeadCompanyPhone = TbxPhoneNumber.Text;
                            lead.LeadCompanySize = TbxNumberUnits.Text;
                            lead.LeadCompanyEmail = TbxCompanyEmail.Text;
                            lead.Message = TbxMessage.Text;
                            lead.LastUpdate = DateTime.Now;
                            lead.IsConfirmed = 0;

                            loader.Update(lead);

                            List<string> products = TbxProduct.Text.Trim().Split(',').ToList();
                            if (products.Count > 0)
                            {
                                foreach (string product in products)
                                {
                                    if (product != "")
                                    {
                                        DataLoader<ElioSnitcherLeadsPageviews> loaderView = new DataLoader<ElioSnitcherLeadsPageviews>(session);

                                        ElioSnitcherLeadsPageviews pageView = Sql.GetSnitcherLeadPageViewByLeadIdAndUrlOrProduct(lead.LeadId, product, product, session);
                                        if (pageView == null)
                                        {
                                            pageView = new ElioSnitcherLeadsPageviews();

                                            pageView.LeadId = lead.LeadId;
                                            pageView.ElioWebsiteLeadsId = lead.Id;
                                            pageView.Url = product.Trim();
                                            pageView.Product = product.Trim();
                                            pageView.TimeSpent = 1;
                                            pageView.ActionTime = DateTime.Now;
                                            pageView.Sysdate = DateTime.Now;
                                            pageView.LastUpdate = DateTime.Now;

                                            loaderView.Insert(pageView);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    LoadStep(3);
                    UpdateStrings();

                    BtnBack.Visible = false;
                    BtnProceed.Text = "Start new";                    
                }
                else
                {
                    ResetRFPsFields();
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                //divDemoWarningMsg.Visible = true;
                //LblDemoWarningMsg.Text = "Error! ";
                //LblDemoWarningMsgContent.Text = "Something went wrong! Please try again later.";
                GlobalMethods.ShowMessageAlertControl(UcMessageAlert, "Something went wrong! Please try again later.", MessageTypes.Error, true, true, false, false, false);
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            try
            {
                divStepOne.Visible = true;
                divStepOne.Attributes["class"] = "w-full flex flex-col gap-30px step-content active";

                divStepTwo.Visible = false;
                divStepTwo.Attributes["class"] = "w-full flex flex-col gap-30px step-content";

                UcMessageAlert.Visible = false;
                //divDemoSuccessMsg.Visible = divDemoWarningMsg.Visible = false;

                BtnBack.Visible = false;
                BtnProceed.Text = "Next";
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

        protected void aStep1_ServerClick(object sender, EventArgs e)
        {
            try
            {
                LoadStep(1);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aStep2_ServerClick(object sender, EventArgs e)
        {
            try
            {
                LoadStep(2);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aStep3_ServerClick(object sender, EventArgs e)
        {
            try
            {
                LoadStep(3);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion        
    }
}