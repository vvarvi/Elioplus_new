<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CriteriaSelection.ascx.cs" Inherits="WdS.ElioPlus.Controls.CriteriaSelection" %>

<!DOCTYPE html>
<!--[if IE 8]> <html lang="en" class="ie8 no-js"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9 no-js"> <![endif]-->
<!--[if !IE]><!-->
<html lang="en">
<!--<![endif]-->
<!-- BEGIN HEAD -->
<head>
    <meta charset="utf-8" />
    <title>Elioplus</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="/assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="/assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="/assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/assets/global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css" />
    <link href="/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <link href="/assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="/assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <link href="/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.css" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN THEME GLOBAL STYLES -->
    <link href="/assets/global/css/components.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="/assets/global/css/plugins.min.css" rel="stylesheet" type="text/css" />
    <!-- END THEME GLOBAL STYLES -->
    <!-- BEGIN THEME LAYOUT STYLES -->
    <link href="/assets/layouts/layout/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="/assets/layouts/layout/css/themes/darkblue.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="/assets/layouts/layout/css/custom.min.css" rel="stylesheet" type="text/css" />
    <!-- END THEME LAYOUT STYLES -->
</head>
<!-- END HEAD -->
<body>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet light bordered" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class="icon-layers font-green"></i>
                                <span class="caption-subject font-green bold uppercase">
                                    <asp:Label ID="LblWizardTitle" runat="server" />
                                    -                                 
                                        <span class="step-title">
                                            <asp:Label ID="LblWizardTitleSteps" runat="server" /></span>
                                </span>
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <form></form>
                            <div class="form-horizontal form-bordered">
                                <div id="divFilesError" runat="server" visible="false" class="alert alert-danger">
                                    <strong>
                                        <asp:Label ID="LblFilesError" runat="server" /></strong><asp:Label ID="LblFilesErrorContent" runat="server" />
                                </div>
                                <div id="divFilesInfo" runat="server" class="alert alert-info" style="margin-bottom: 0px;">
                                    <strong>
                                        <asp:Label ID="LblFilesInfo" runat="server" /></strong><asp:Label ID="LblFilesInfoContent" runat="server" />
                                </div>
                                <div class="form-body col-md-6" style="float: left;">
                                    <div id="divPdfFile" runat="server" visible="false" class="form-group">
                                        <label class="control-label col-md-2">
                                            <asp:Label ID="LblPdfTitle" runat="server" /></label>
                                        <div class="col-md-3">
                                            <div class="fileinput fileinput-new" data-provides="fileinput">
                                                <div class="input-group input-large">
                                                    <div class="form-control uneditable-input input-fixed input-medium" data-trigger="fileinput">
                                                        <i class="fa fa-file fileinput-exists"></i>&nbsp;
                                                            <span class="fileinput-filename">
                                                                <asp:Label ID="LblExistingPdf" runat="server" /></span>
                                                    </div>
                                                    <span class="input-group-addon btn default btn-file">
                                                        <span class="fileinput-new">
                                                            <asp:Label ID="LblPdfSelect" runat="server" /></span>
                                                        <span class="fileinput-exists">
                                                            <asp:Label ID="LblPdfChange" runat="server" /></span>
                                                        <input type="file" name="pdfFile" enableviewstate="true" id="pdfFile" accept=".pdf,application/pdf" runat="server" />
                                                    </span>
                                                    <a class="input-group-addon btn red fileinput-exists" data-dismiss="fileinput">
                                                        <asp:Label ID="LblPdfRemove" runat="server" />
                                                    </a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="divCsvFile" class="form-body col-md-6" style="float: left;">
                                    <div class="form-group">
                                        <label class="control-label col-md-2">
                                            <asp:Label ID="LblCsvTitle" runat="server" /></label>
                                        <div class="col-md-3">
                                            <div class="fileinput fileinput-new" data-provides="fileinput">
                                                <div class="input-group input-large">
                                                    <div class="form-control uneditable-input input-fixed input-medium" data-trigger="fileinput">
                                                        <i class="fa fa-file fileinput-exists"></i>&nbsp;
                                                            <span class="fileinput-filename">
                                                                <asp:Label ID="LblExistingCsv" runat="server" /></span>
                                                    </div>
                                                    <span class="input-group-addon btn default btn-file">
                                                        <span class="fileinput-new">
                                                            <asp:Label ID="LblCsvSelect" runat="server" /></span>
                                                        <span class="fileinput-exists">
                                                            <asp:Label ID="LblCsvChange" runat="server" /></span>
                                                        <input type="file" name="csvFile" enableviewstate="true" id="csvFile" accept=".csv" runat="server" />
                                                    </span>
                                                    <a class="input-group-addon btn red fileinput-exists" data-dismiss="fileinput">
                                                        <asp:Label ID="LblCsvRemove" runat="server" />
                                                    </a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <form action="#" method="post" class="form-horizontal" id="submit_form" enctype="multipart/form-data">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <ul class="nav nav-pills nav-justified steps">
                                            <li>
                                                <a href="#tab1" data-toggle="tab" class="step">
                                                    <span class="number">1 </span>
                                                    <span class="desc">
                                                        <i class="fa fa-check"></i>
                                                        <asp:Label ID="LblBasicCriteria" runat="server" />
                                                    </span>
                                                </a>
                                            </li>
                                            <li>
                                                <a href="#tab2" data-toggle="tab" class="step">
                                                    <span class="number">2 </span>
                                                    <span class="desc">
                                                        <i class="fa fa-check"></i>
                                                        <asp:Label ID="LblOptionalCriteria" runat="server" />
                                                    </span>
                                                </a>
                                            </li>
                                            <li>
                                                <a href="#tab3" data-toggle="tab" class="step active">
                                                    <span class="number">3 </span>
                                                    <span class="desc">
                                                        <i class="fa fa-check"></i>
                                                        <asp:Label ID="LblOptionalCriteria2" runat="server" />
                                                    </span>
                                                </a>
                                            </li>
                                            <li>
                                                <a href="#tab4" data-toggle="tab" class="step">
                                                    <span class="number">4 </span>
                                                    <span class="desc">
                                                        <i class="fa fa-check"></i>
                                                        <asp:Label ID="LblSummary" Text="Summary" runat="server" />
                                                    </span>
                                                </a>
                                            </li>
                                        </ul>
                                        <div id="bar" class="progress progress-striped" role="progressbar">
                                            <div class="progress-bar progress-bar-success"></div>
                                        </div>
                                        <div class="tab-content">
                                            <div class="alert alert-danger display-none">
                                                <button class="close" data-dismiss="alert"></button>
                                                <asp:Label ID="LblAlertDanger" runat="server" />
                                            </div>
                                            <div class="alert alert-success display-none">
                                                <button class="close" data-dismiss="alert"></button>
                                                <asp:Label ID="LblAlertSuccess" runat="server" />
                                            </div>
                                            <div class="tab-pane active" id="tab1">
                                                <h3 class="block">
                                                    <asp:Label ID="LblRequiredCriteria" runat="server" /></h3>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">
                                                        <asp:Label ID="LblIndVerticals" runat="server" />
                                                        <span class="required">* </span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <div class="checkbox-list">
                                                            <label id="lblV1" runat="server" visible="false">
                                                                <input id="CbxVert1" type="checkbox" name="payment[]" value="payment[]" data-title="Email Marketing" /><asp:Label ID="LblVertical1" runat="server" />
                                                            </label>
                                                            <label id="lblV2" runat="server" visible="false">
                                                                <input id="CbxVert2" type="checkbox" name="payment[]" value="payment[]" data-title="Campaign Management" /><asp:Label ID="LblVertical2" runat="server" />
                                                            </label>
                                                            <label id="lblV3" runat="server" visible="false">
                                                                <input id="CbxVert3" type="checkbox" name="payment[]" value="payment[]" data-title="Marketing Automation" /><asp:Label ID="LblVertical3" runat="server" />
                                                            </label>
                                                            <label id="lblV4" runat="server" visible="false">
                                                                <input id="CbxVert4" type="checkbox" name="payment[]" value="payment[]" data-title="Content Marketing" /><asp:Label ID="LblVertical4" runat="server" />
                                                            </label>
                                                            <label id="lblV5" runat="server" visible="false">
                                                                <input id="CbxVert5" type="checkbox" name="payment[]" value="payment[]" data-title="SEO & SEM" /><asp:Label ID="LblVertical5" runat="server" />
                                                            </label>
                                                            <label id="lblV6" runat="server" visible="false">
                                                                <input id="CbxVert6" type="checkbox" name="payment[]" value="payment[]" data-title="Social Media Marketing" /><asp:Label ID="LblVertical6" runat="server" />
                                                            </label>
                                                            <label id="lblV7" runat="server" visible="false">
                                                                <input id="CbxVert7" type="checkbox" name="payment[]" value="payment[]" data-title="Affiliate Marketing" /><asp:Label ID="LblVertical7" runat="server" />
                                                            </label>
                                                            <label id="lblV8" runat="server" visible="false">
                                                                <input id="CbxVert8" type="checkbox" name="payment[]" value="payment[]" data-title="Surveys & Forms" /><asp:Label ID="LblVertical8" runat="server" />
                                                            </label>
                                                            <label id="lblV9" runat="server" visible="false">
                                                                <input id="CbxVert9" type="checkbox" name="payment[]" value="payment[]" data-title="Ad Serving" /><asp:Label ID="LblVertical9" runat="server" />
                                                            </label>
                                                            <label id="lblV10" runat="server" visible="false">
                                                                <input id="CbxVert10" type="checkbox" name="payment[]" value="payment[]" data-title="Event Management" /><asp:Label ID="LblVertical10" runat="server" />
                                                            </label>
                                                            <label id="lblV11" runat="server" visible="false">
                                                                <input id="CbxVert11" type="checkbox" name="payment[]" value="payment[]" data-title="Sales Process Management" /><asp:Label ID="LblVertical11" runat="server" />
                                                            </label>
                                                            <label id="lblV12" runat="server" visible="false">
                                                                <input id="CbxVert12" type="checkbox" name="payment[]" value="payment[]" data-title="Quotes & Orders" /><asp:Label ID="LblVertical12" runat="server" />
                                                            </label>
                                                            <label id="lblV13" runat="server" visible="false">
                                                                <input id="CbxVert13" type="checkbox" name="payment[]" value="payment[]" data-title="Document Generation" /><asp:Label ID="LblVertical13" runat="server" />
                                                            </label>
                                                            <label id="lblV14" runat="server" visible="false">
                                                                <input id="CbxVert14" type="checkbox" name="payment[]" value="payment[]" data-title="Sales Intelligence" /><asp:Label ID="LblVertical14" runat="server" />
                                                            </label>
                                                            <label id="lblV15" runat="server" visible="false">
                                                                <input id="CbxVert15" type="checkbox" name="payment[]" value="payment[]" data-title="Engagement Tools" /><asp:Label ID="LblVertical15" runat="server" />
                                                            </label>
                                                            <label id="lblV16" runat="server" visible="false">
                                                                <input id="CbxVert16" type="checkbox" name="payment[]" value="payment[]" data-title="POS" /><asp:Label ID="LblVertical16" runat="server" />
                                                            </label>
                                                            <label id="lblV17" runat="server" visible="false">
                                                                <input id="CbxVert17" type="checkbox" name="payment[]" value="payment[]" data-title="E-Signature" /><asp:Label ID="LblVertical17" runat="server" />
                                                            </label>
                                                            <label id="lblV18" runat="server" visible="false">
                                                                <input id="CbxVert18" type="checkbox" name="payment[]" value="payment[]" data-title="CRM" /><asp:Label ID="LblVertical18" runat="server" />
                                                            </label>
                                                            <label id="lblV19" runat="server" visible="false">
                                                                <input id="CbxVert19" type="checkbox" name="payment[]" value="payment[]" data-title="Help Desk" /><asp:Label ID="LblVertical19" runat="server" />
                                                            </label>
                                                            <label id="lblV20" runat="server" visible="false">
                                                                <input id="CbxVert20" type="checkbox" name="payment[]" value="payment[]" data-title="Live Chat" /><asp:Label ID="LblVertical20" runat="server" />
                                                            </label>
                                                            <label id="lblV21" runat="server" visible="false">
                                                                <input id="CbxVert21" type="checkbox" name="payment[]" value="payment[]" data-title="Feedback Management" /><asp:Label ID="LblVertical21" runat="server" />
                                                            </label>
                                                            <label id="lblV22" runat="server" visible="false">
                                                                <input id="CbxVert22" type="checkbox" name="payment[]" value="payment[]" data-title="Gamification & Loyalty" /><asp:Label ID="LblVertical22" runat="server" />
                                                            </label>
                                                            <label id="lblV23" runat="server" visible="false">
                                                                <input id="CbxVert23" type="checkbox" name="payment[]" value="payment[]" data-title="Project Management Tools" /><asp:Label ID="LblVertical23" runat="server" />
                                                            </label>
                                                            <label id="lblV24" runat="server" visible="false">
                                                                <input id="CbxVert24" type="checkbox" name="payment[]" value="payment[]" data-title="Chat & Web Conference" /><asp:Label ID="LblVertical24" runat="server" />
                                                            </label>
                                                            <label id="lblV25" runat="server" visible="false">
                                                                <input id="CbxVert25" type="checkbox" name="payment[]" value="payment[]" data-title="Knowledge Management" /><asp:Label ID="LblVertical25" runat="server" />
                                                            </label>
                                                            <label id="lblV26" runat="server" visible="false">
                                                                <input id="CbxVert26" type="checkbox" name="payment[]" value="payment[]" data-title="File Sharing Software" /><asp:Label ID="LblVertical26" runat="server" />
                                                            </label>
                                                            <label id="lblV27" runat="server" visible="false">
                                                                <input id="CbxVert27" type="checkbox" name="payment[]" value="payment[]" data-title="Business Process Management" /><asp:Label ID="LblVertical27" runat="server" />
                                                            </label>
                                                            <label id="lblV28" runat="server" visible="false">
                                                                <input id="CbxVert28" type="checkbox" name="payment[]" value="payment[]" data-title="Digital Asset Management" /><asp:Label ID="LblVertical28" runat="server" />
                                                            </label>
                                                            <label id="lblV29" runat="server" visible="false">
                                                                <input id="CbxVert29" type="checkbox" name="payment[]" value="payment[]" data-title="ERP" /><asp:Label ID="LblVertical29" runat="server" />
                                                            </label>
                                                            <label id="lblV30" runat="server" visible="false">
                                                                <input id="CbxVert30" type="checkbox" name="payment[]" value="payment[]" data-title="Inventory Management" /><asp:Label ID="LblVertical30" runat="server" />
                                                            </label>
                                                            <label id="lblV31" runat="server" visible="false">
                                                                <input id="CbxVert31" type="checkbox" name="payment[]" value="payment[]" data-title="Shipping & Tracking" /><asp:Label ID="LblVertical31" runat="server" />
                                                            </label>
                                                            <label id="lblV32" runat="server" visible="false">
                                                                <input id="CbxVert32" type="checkbox" name="payment[]" value="payment[]" data-title="Supply Chain Management" /><asp:Label ID="LblVertical32" runat="server" />
                                                            </label>
                                                            <label id="lblV33" runat="server" visible="false">
                                                                <input id="CbxVert33" type="checkbox" name="payment[]" value="payment[]" data-title="Analytics Software" /><asp:Label ID="LblVertical33" runat="server" />
                                                            </label>
                                                            <label id="lblV34" runat="server" visible="false">
                                                                <input id="CbxVert34" type="checkbox" name="payment[]" value="payment[]" data-title="Business Intelligence" /><asp:Label ID="LblVertical34" runat="server" />
                                                            </label>
                                                            <label id="lblV35" runat="server" visible="false">
                                                                <input id="CbxVert35" type="checkbox" name="payment[]" value="payment[]" data-title="Data Visualization" /><asp:Label ID="LblVertical35" runat="server" />
                                                            </label>
                                                            <label id="lblV36" runat="server" visible="false">
                                                                <input id="CbxVert36" type="checkbox" name="payment[]" value="payment[]" data-title="Competitive Intelligence" /><asp:Label ID="LblVertical36" runat="server" />
                                                            </label>
                                                            <label id="lblV37" runat="server" visible="false">
                                                                <input id="CbxVert37" type="checkbox" name="payment[]" value="payment[]" data-title="Accounting" /><asp:Label ID="LblVertical37" runat="server" />
                                                            </label>
                                                            <label id="lblV38" runat="server" visible="false">
                                                                <input id="CbxVert38" type="checkbox" name="payment[]" value="payment[]" data-title="Payment Processing" /><asp:Label ID="LblVertical38" runat="server" />
                                                            </label>
                                                            <label id="lblV39" runat="server" visible="false">
                                                                <input id="CbxVert39" type="checkbox" name="payment[]" value="payment[]" data-title="Time & Expenses" /><asp:Label ID="LblVertical39" runat="server" />
                                                            </label>
                                                            <label id="lblV40" runat="server" visible="false">
                                                                <input id="CbxVert40" type="checkbox" name="payment[]" value="payment[]" data-title="Billing & Invoicing" /><asp:Label ID="LblVertical40" runat="server" />
                                                            </label>
                                                            <label id="lblV41" runat="server" visible="false">
                                                                <input id="CbxVert41" type="checkbox" name="payment[]" value="payment[]" data-title="Budgeting" /><asp:Label ID="LblVertical41" runat="server" />
                                                            </label>
                                                            <label id="lblV42" runat="server" visible="false">
                                                                <input id="CbxVert42" type="checkbox" name="payment[]" value="payment[]" data-title="Applicant Tracking" /><asp:Label ID="LblVertical42" runat="server" />
                                                            </label>
                                                            <label id="lblV43" runat="server" visible="false">
                                                                <input id="CbxVert43" type="checkbox" name="payment[]" value="payment[]" data-title="HR Administration" /><asp:Label ID="LblVertical43" runat="server" />
                                                            </label>
                                                            <label id="lblV44" runat="server" visible="false">
                                                                <input id="CbxVert44" type="checkbox" name="payment[]" value="payment[]" data-title="Payroll" /><asp:Label ID="LblVertical44" runat="server" />
                                                            </label>
                                                            <label id="lblV45" runat="server" visible="false">
                                                                <input id="CbxVert45" type="checkbox" name="payment[]" value="payment[]" data-title="Performance Management" /><asp:Label ID="LblVertical45" runat="server" />
                                                            </label>
                                                            <label id="lblV46" runat="server" visible="false">
                                                                <input id="CbxVert46" type="checkbox" name="payment[]" value="payment[]" data-title="Recruiting" /><asp:Label ID="LblVertical46" runat="server" />
                                                            </label>
                                                            <label id="lblV47" runat="server" visible="false">
                                                                <input id="CbxVert47" type="checkbox" name="payment[]" value="payment[]" data-title="Learning Management System" /><asp:Label ID="LblVertical47" runat="server" />
                                                            </label>
                                                            <label id="lblV48" runat="server" visible="false">
                                                                <input id="CbxVert48" type="checkbox" name="payment[]" value="payment[]" data-title="Time & Expense" /><asp:Label ID="LblVertical48" runat="server" />
                                                            </label>
                                                            <label id="lblV49" runat="server" visible="false">
                                                                <input id="CbxVert49" type="checkbox" name="payment[]" value="payment[]" data-title="API Tools" /><asp:Label ID="LblVertical49" runat="server" />
                                                            </label>
                                                            <label id="lblV50" runat="server" visible="false">
                                                                <input id="CbxVert50" type="checkbox" name="payment[]" value="payment[]" data-title="Bug Trackers" /><asp:Label ID="LblVertical50" runat="server" />
                                                            </label>
                                                            <label id="lblV51" runat="server" visible="false">
                                                                <input id="CbxVert51" type="checkbox" name="payment[]" value="payment[]" data-title="Development Tools" /><asp:Label ID="LblVertical51" runat="server" />
                                                            </label>
                                                            <label id="lblV52" runat="server" visible="false">
                                                                <input id="CbxVert52" type="checkbox" name="payment[]" value="payment[]" data-title="eCommerce" /><asp:Label ID="LblVertical52" runat="server" />
                                                            </label>
                                                            <label id="lblV53" runat="server" visible="false">
                                                                <input id="CbxVert53" type="checkbox" name="payment[]" value="payment[]" data-title="Frameworks & Libraries" /><asp:Label ID="LblVertical53" runat="server" />
                                                            </label>
                                                            <label id="lblV54" runat="server" visible="false">
                                                                <input id="CbxVert54" type="checkbox" name="payment[]" value="payment[]" data-title="Mobile Development" /><asp:Label ID="LblVertical54" runat="server" />
                                                            </label>
                                                            <label id="lblV55" runat="server" visible="false">
                                                                <input id="CbxVert55" type="checkbox" name="payment[]" value="payment[]" data-title="Optimization" /><asp:Label ID="LblVertical55" runat="server" />
                                                            </label>
                                                            <label id="lblV56" runat="server" visible="false">
                                                                <input id="CbxVert56" type="checkbox" name="payment[]" value="payment[]" data-title="Usability Testing" /><asp:Label ID="LblVertical56" runat="server" />
                                                            </label>
                                                            <label id="lblV57" runat="server" visible="false">
                                                                <input id="CbxVert57" type="checkbox" name="payment[]" value="payment[]" data-title="Websites" /><asp:Label ID="LblVertical57" runat="server" />
                                                            </label>
                                                            <label id="lblV58" runat="server" visible="false">
                                                                <input id="CbxVert58" type="checkbox" name="payment[]" value="payment[]" data-title="Cloud Integration (iPaaS)" /><asp:Label ID="LblVertical58" runat="server" />
                                                            </label>
                                                            <label id="lblV59" runat="server" visible="false">
                                                                <input id="CbxVert59" type="checkbox" name="payment[]" value="payment[]" data-title="Cloud Management" /><asp:Label ID="LblVertical59" runat="server" />
                                                            </label>
                                                            <label id="lblV60" runat="server" visible="false">
                                                                <input id="CbxVert60" type="checkbox" name="payment[]" value="payment[]" data-title="Cloud Storage" /><asp:Label ID="LblVertical60" runat="server" />
                                                            </label>
                                                            <label id="lblV61" runat="server" visible="false">
                                                                <input id="CbxVert61" type="checkbox" name="payment[]" value="payment[]" data-title="Remote Access" /><asp:Label ID="LblVertical61" runat="server" />
                                                            </label>
                                                            <label id="lblV62" runat="server" visible="false">
                                                                <input id="CbxVert62" type="checkbox" name="payment[]" value="payment[]" data-title="Virtualization" /><asp:Label ID="LblVertical62" runat="server" />
                                                            </label>
                                                            <label id="lblV63" runat="server" visible="false">
                                                                <input id="CbxVert63" type="checkbox" name="payment[]" value="payment[]" data-title="Web Hosting" /><asp:Label ID="LblVertical63" runat="server" />
                                                            </label>
                                                            <label id="lblV64" runat="server" visible="false">
                                                                <input id="CbxVert64" type="checkbox" name="payment[]" value="payment[]" data-title="Web Monitoring" /><asp:Label ID="LblVertical64" runat="server" />
                                                            </label>
                                                            <label id="lblV65" runat="server" visible="false">
                                                                <input id="CbxVert65" type="checkbox" name="payment[]" value="payment[]" data-title="VOIP" /><asp:Label ID="LblVertical65" runat="server" />
                                                            </label>
                                                            <label id="lblV66" runat="server" visible="false">
                                                                <input id="CbxVert66" type="checkbox" name="payment[]" value="payment[]" data-title="Calendar & Scheduling" /><asp:Label ID="LblVertical66" runat="server" />
                                                            </label>
                                                            <label id="lblV67" runat="server" visible="false">
                                                                <input id="CbxVert67" type="checkbox" name="payment[]" value="payment[]" data-title="Email" /><asp:Label ID="LblVertical67" runat="server" />
                                                            </label>
                                                            <label id="lblV68" runat="server" visible="false">
                                                                <input id="CbxVert68" type="checkbox" name="payment[]" value="payment[]" data-title="Note Taking" /><asp:Label ID="LblVertical68" runat="server" />
                                                            </label>
                                                            <label id="lblV69" runat="server" visible="false">
                                                                <input id="CbxVert69" type="checkbox" name="payment[]" value="payment[]" data-title="Password Management" /><asp:Label ID="LblVertical69" runat="server" />
                                                            </label>
                                                            <label id="lblV70" runat="server" visible="false">
                                                                <input id="CbxVert70" type="checkbox" name="payment[]" value="payment[]" data-title="Presentations" /><asp:Label ID="LblVertical70" runat="server" />
                                                            </label>
                                                            <label id="lblV71" runat="server" visible="false">
                                                                <input id="CbxVert71" type="checkbox" name="payment[]" value="payment[]" data-title="Productivity Suites" /><asp:Label ID="LblVertical71" runat="server" />
                                                            </label>
                                                            <label id="lblV72" runat="server" visible="false">
                                                                <input id="CbxVert72" type="checkbox" name="payment[]" value="payment[]" data-title="Spreadsheets" /><asp:Label ID="LblVertical72" runat="server" />
                                                            </label>
                                                            <label id="lblV73" runat="server" visible="false">
                                                                <input id="CbxVert73" type="checkbox" name="payment[]" value="payment[]" data-title="Task Management" /><asp:Label ID="LblVertical73" runat="server" />
                                                            </label>
                                                            <label id="lblV74" runat="server" visible="false">
                                                                <input id="CbxVert74" type="checkbox" name="payment[]" value="payment[]" data-title="Time Management" /><asp:Label ID="LblVertical74" runat="server" />
                                                            </label>
                                                            <label id="lblV75" runat="server" visible="false">
                                                                <input id="CbxVert75" type="checkbox" name="payment[]" value="payment[]" data-title="Cybersecurity" /><asp:Label ID="LblVertical75" runat="server" />
                                                            </label>
                                                            <label id="lblV76" runat="server" visible="false">
                                                                <input id="CbxVert76" type="checkbox" name="payment[]" value="payment[]" data-title="Vulnerability Management" /><asp:Label ID="LblVertical76" runat="server" />
                                                            </label>
                                                            <label id="lblV77" runat="server" visible="false">
                                                                <input id="CbxVert77" type="checkbox" name="payment[]" value="payment[]" data-title="Firewall" /><asp:Label ID="LblVertical77" runat="server" />
                                                            </label>
                                                            <label id="lblV78" runat="server" visible="false">
                                                                <input id="CbxVert78" type="checkbox" name="payment[]" value="payment[]" data-title="Mobile Data Security" /><asp:Label ID="LblVertical78" runat="server" />
                                                            </label>
                                                            <label id="lblV79" runat="server" visible="false">
                                                                <input id="CbxVert79" type="checkbox" name="payment[]" value="payment[]" data-title="Backup & Restore" /><asp:Label ID="LblVertical79" runat="server" />
                                                            </label>
                                                            <label id="lblV80" runat="server" visible="false">
                                                                <input id="CbxVert80" type="checkbox" name="payment[]" value="payment[]" data-title="Graphic Design" /><asp:Label ID="LblVertical80" runat="server" />
                                                            </label>
                                                            <label id="lblV81" runat="server" visible="false">
                                                                <input id="CbxVert81" type="checkbox" name="payment[]" value="payment[]" data-title="Infographics" /><asp:Label ID="LblVertical81" runat="server" />
                                                            </label>
                                                            <label id="lblV82" runat="server" visible="false">
                                                                <input id="CbxVert82" type="checkbox" name="payment[]" value="payment[]" data-title="Video Editing" /><asp:Label ID="LblVertical82" runat="server" />
                                                            </label>
                                                            <label id="lblV83" runat="server" visible="false">
                                                                <input id="CbxVert83" type="checkbox" name="payment[]" value="payment[]" data-title="Warehouse Management" /><asp:Label ID="LblVertical83" runat="server" />
                                                            </label>
                                                            <label id="lblV84" runat="server" visible="false">
                                                                <input id="CbxVert84" type="checkbox" name="payment[]" value="payment[]" data-title="Supply Chain Execution" /><asp:Label ID="LblVertical84" runat="server" />
                                                            </label>
                                                            <label id="lblV85" runat="server" visible="false">
                                                                <input id="CbxVert85" type="checkbox" name="payment[]" value="payment[]" data-title="eLearning" /><asp:Label ID="LblVertical85" runat="server" />
                                                            </label>
                                                            <label id="lblV86" runat="server" visible="false">
                                                                <input id="CbxVert86" type="checkbox" name="payment[]" value="payment[]" data-title="Healthcare" /><asp:Label ID="LblVertical86" runat="server" />
                                                            </label>
                                                            <label id="lblV87" runat="server" visible="false">
                                                                <input id="CbxVert87" type="checkbox" name="payment[]" value="payment[]" data-title="Big Data" /><asp:Label ID="LblVertical87" runat="server" />
                                                            </label>
                                                            <label id="lblV88" runat="server" visible="false">
                                                                <input id="CbxVert88" type="checkbox" name="payment[]" value="payment[]" data-title="Data Warehousing" /><asp:Label ID="LblVertical88" runat="server" />
                                                            </label>
                                                            <label id="lblV89" runat="server" visible="false">
                                                                <input id="CbxVert89" type="checkbox" name="payment[]" value="payment[]" data-title="Data Masking" /><asp:Label ID="LblVertical89" runat="server" />
                                                            </label>
                                                            <label id="lblV90" runat="server" visible="false">
                                                                <input id="CbxVert90" type="checkbox" name="payment[]" value="payment[]" data-title="Databases" /><asp:Label ID="LblVertical90" runat="server" />
                                                            </label>
                                                            <label id="lblV91" runat="server" visible="false">
                                                                <input id="CbxVert91" type="checkbox" name="payment[]" value="payment[]" data-title="Data Integration" /><asp:Label ID="LblVertical91" runat="server" />
                                                            </label>
                                                            <label id="lblV92" runat="server" visible="false">
                                                                <input id="CbxVert92" type="checkbox" name="payment[]" value="payment[]" data-title="Data Management" /><asp:Label ID="LblVertical92" runat="server" />
                                                            </label>
                                                            <label id="lblV93" runat="server" visible="false">
                                                                <input id="CbxVert93" type="checkbox" name="payment[]" value="payment[]" data-title="Identity Management" /><asp:Label ID="LblVertical93" runat="server" />
                                                            </label>
                                                            <label id="lblV94" runat="server" visible="false">
                                                                <input id="CbxVert94" type="checkbox" name="payment[]" value="payment[]" data-title="Risk Management" /><asp:Label ID="LblVertical94" runat="server" />
                                                            </label>
                                                            <label id="lblV95" runat="server" visible="false">
                                                                <input id="CbxVert95" type="checkbox" name="payment[]" value="payment[]" data-title="ECM" /><asp:Label ID="LblVertical95" runat="server" />
                                                            </label>
                                                            <label id="lblV96" runat="server" visible="false">
                                                                <input id="CbxVert96" type="checkbox" name="payment[]" value="payment[]" data-title="Mobility" /><asp:Label ID="LblVertical96" runat="server" />
                                                            </label>
                                                            <label id="lblV97" runat="server" visible="false">
                                                                <input id="CbxVert97" type="checkbox" name="payment[]" value="payment[]" data-title="Collaboration" /><asp:Label ID="LblVertical97" runat="server" />
                                                            </label>
                                                            <label id="lblV98" runat="server" visible="false">
                                                                <input id="CbxVert98" type="checkbox" name="payment[]" value="payment[]" data-title="Conferencing" /><asp:Label ID="LblVertical98" runat="server" />
                                                            </label>
                                                            <label id="lblV99" runat="server" visible="false">
                                                                <input id="CbxVert99" type="checkbox" name="payment[]" value="payment[]" data-title="Unified Messaging" /><asp:Label ID="LblVertical99" runat="server" />
                                                            </label>
                                                            <label id="lblV100" runat="server" visible="false">
                                                                <input id="CbxVert100" type="checkbox" name="payment[]" value="payment[]" data-title="Unified Communications" /><asp:Label ID="LblVertical100" runat="server" />
                                                            </label>
                                                            <label id="lblV101" runat="server" visible="false">
                                                                <input id="CbxVert101" type="checkbox" name="payment[]" value="payment[]" data-title="Team Collaboration" /><asp:Label ID="LblVertical101" runat="server" />
                                                            </label>
                                                            <label id="lblV102" runat="server" visible="false">
                                                                <input id="CbxVert102" type="checkbox" name="payment[]" value="payment[]" data-title="Video Conferencing" /><asp:Label ID="LblVertical102" runat="server" />
                                                            </label>

                                                            <label id="lblV103" runat="server" visible="false">
                                                                <input id="CbxVert103" type="checkbox" name="payment[]" value="payment[]" data-title="General-Purpose CAD" /><asp:Label ID="LblVertical103" runat="server" />
                                                            </label>
                                                            <label id="lblV104" runat="server" visible="false">
                                                                <input id="CbxVert104" type="checkbox" name="payment[]" value="payment[]" data-title="CAM" /><asp:Label ID="LblVertical104" runat="server" />
                                                            </label>
                                                            <label id="lblV105" runat="server" visible="false">
                                                                <input id="CbxVert105" type="checkbox" name="payment[]" value="payment[]" data-title="PLM" /><asp:Label ID="LblVertical105" runat="server" />
                                                            </label>
                                                            <label id="lblV106" runat="server" visible="false">
                                                                <input id="CbxVert106" type="checkbox" name="payment[]" value="payment[]" data-title="PDM (Product Data Management)" /><asp:Label ID="LblVertical106" runat="server" />
                                                            </label>
                                                            <label id="lblV107" runat="server" visible="false">
                                                                <input id="CbxVert107" type="checkbox" name="payment[]" value="payment[]" data-title="BIM" /><asp:Label ID="LblVertical107" runat="server" />
                                                            </label>
                                                            <label id="lblV108" runat="server" visible="false">
                                                                <input id="CbxVert108" type="checkbox" name="payment[]" value="payment[]" data-title="3D Architecture" /><asp:Label ID="LblVertical108" runat="server" />
                                                            </label>
                                                            <label id="lblV109" runat="server" visible="false">
                                                                <input id="CbxVert109" type="checkbox" name="payment[]" value="payment[]" data-title="3D CAD" /><asp:Label ID="LblVertical109" runat="server" />
                                                            </label>
                                                            <label id="lblV110" runat="server" visible="false">
                                                                <input id="CbxVert110" type="checkbox" name="payment[]" value="payment[]" data-title="Location Intelligence" /><asp:Label ID="LblVertical110" runat="server" />
                                                            </label>
                                                            <label id="lblV111" runat="server" visible="false">
                                                                <input id="CbxVert111" type="checkbox" name="payment[]" value="payment[]" data-title="Track Management" /><asp:Label ID="LblVertical111" runat="server" />
                                                            </label>
                                                            <label id="lblV112" runat="server" visible="false">
                                                                <input id="CbxVert112" type="checkbox" name="payment[]" value="payment[]" data-title="Workflow Management" /><asp:Label ID="LblVertical112" runat="server" />
                                                            </label>
                                                            <label id="lblV113" runat="server" visible="false">
                                                                <input id="CbxVert113" type="checkbox" name="payment[]" value="payment[]" data-title="Enterprise Asset Management" /><asp:Label ID="LblVertical113" runat="server" />
                                                            </label>
                                                            <label id="lblV114" runat="server" visible="false">
                                                                <input id="CbxVert114" type="checkbox" name="payment[]" value="payment[]" data-title="Facility Management" /><asp:Label ID="LblVertical114" runat="server" />
                                                            </label>
                                                            <label id="lblV115" runat="server" visible="false">
                                                                <input id="CbxVert115" type="checkbox" name="payment[]" value="payment[]" data-title="Asset Lifecycle Management" /><asp:Label ID="LblVertical115" runat="server" />
                                                            </label>
                                                            <label id="lblV116" runat="server" visible="false">
                                                                <input id="CbxVert116" type="checkbox" name="payment[]" value="payment[]" data-title="CMMS" /><asp:Label ID="LblVertical116" runat="server" />
                                                            </label>
                                                            <label id="lblV117" runat="server" visible="false">
                                                                <input id="CbxVert117" type="checkbox" name="payment[]" value="payment[]" data-title="Fleet Management" /><asp:Label ID="LblVertical117" runat="server" />
                                                            </label>
                                                            <label id="lblV118" runat="server" visible="false">
                                                                <input id="CbxVert118" type="checkbox" name="payment[]" value="payment[]" data-title="Change Management" /><asp:Label ID="LblVertical118" runat="server" />
                                                            </label>
                                                            <label id="lblV119" runat="server" visible="false">
                                                                <input id="CbxVert119" type="checkbox" name="payment[]" value="payment[]" data-title="Procurement" /><asp:Label ID="LblVertical119" runat="server" />
                                                            </label>
                                                            <label id="lblV120" runat="server" visible="false">
                                                                <input id="CbxVert120" type="checkbox" name="payment[]" value="payment[]" data-title="Chatbot" /><asp:Label ID="LblVertical120" runat="server" />
                                                            </label>
                                                            <label id="lblV121" runat="server" visible="false">
                                                                <input id="CbxVert121" type="checkbox" name="payment[]" value="payment[]" data-title="Penetration Testing" /><asp:Label ID="LblVertical121" runat="server" />
                                                            </label>
                                                            <label id="lblV122" runat="server" visible="false">
                                                                <input id="CbxVert122" type="checkbox" name="payment[]" value="payment[]" data-title="Application Security" /><asp:Label ID="LblVertical122" runat="server" />
                                                            </label>
                                                            <label id="lblV123" runat="server" visible="false">
                                                                <input id="CbxVert123" type="checkbox" name="payment[]" value="payment[]" data-title="Governance, Risk & Compliance (GRC)" /><asp:Label ID="LblVertical123" runat="server" />
                                                            </label>
                                                            <label id="lblV124" runat="server" visible="false">
                                                                <input id="CbxVert124" type="checkbox" name="payment[]" value="payment[]" data-title="Compliance" /><asp:Label ID="LblVertical124" runat="server" />
                                                            </label>
                                                            <label id="lblV125" runat="server" visible="false">
                                                                <input id="CbxVert125" type="checkbox" name="payment[]" value="payment[]" data-title="Fraud Prevention" /><asp:Label ID="LblVertical125" runat="server" />
                                                            </label>
                                                        </div>
                                                        <div id="form_payment_error"></div>
                                                        <span class="help-block">
                                                            <asp:Label ID="LblVerticalsHelp" runat="server" /></span>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">
                                                        <asp:Label ID="LblSetUpFee" runat="server" />
                                                        <span class="required">* </span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <select name="setUpFee" id="setUpFee" class="form-control" onchange="SetSelectedTextFee(this)">
                                                            <option id="optFee1" runat="server" value="">
                                                                <asp:Label ID="LblOptFee1" runat="server" /></option>
                                                            <option id="optFee2" runat="server" value="1">
                                                                <asp:Label ID="LblOptFee2" runat="server" /></option>
                                                            <option id="optFee3" runat="server" value="2">
                                                                <asp:Label ID="LblOptFee3" runat="server" /></option>
                                                            <option id="optFee4" runat="server" value="3">
                                                                <asp:Label ID="LblOptFee4" runat="server" /></option>
                                                        </select>
                                                        <span class="help-block">
                                                            <asp:Label ID="LblSetUpFeeHelp" runat="server" /></span>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">
                                                        <asp:Label ID="LblRevenue" runat="server" />
                                                        <span class="required">* </span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <select name="revenue" id="revenue" class="form-control" onchange="SetSelectedTextRevenue(this)">
                                                            <option id="optRevenue1" runat="server" value="">
                                                                <asp:Label ID="LblOptRevue1" runat="server" /></option>
                                                            <option id="optRevenue2" runat="server" value="1">
                                                                <asp:Label ID="LblOptRevue2" runat="server" /></option>
                                                            <option id="optRevenue3" runat="server" value="2">
                                                                <asp:Label ID="LblOptRevue3" runat="server" /></option>
                                                            <option id="optRevenue4" runat="server" value="3">
                                                                <asp:Label ID="LblOptRevue4" runat="server" /></option>
                                                        </select>
                                                        <span class="help-block">
                                                            <asp:Label ID="LblRevenueHelp" runat="server" /></span>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">
                                                        <asp:Label ID="LblSupport" runat="server" />
                                                        <span class="required">* </span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <div class="checkbox-list">
                                                            <label id="lblS1" runat="server">
                                                                <input id="CbxSuppIndifferent" type="checkbox" name="support[]" value="support[]" data-title="Indifferent" /><asp:Label ID="LblSupport1" runat="server" />
                                                            </label>
                                                            <label id="lblS2" runat="server">
                                                                <input id="CbxSuppDedicated" type="checkbox" class="CbxSuppIndifferent" name="support[]" value="support[]" data-title="Dedicated" /><asp:Label ID="LblSupport2" runat="server" />
                                                            </label>
                                                            <label id="lblS3" runat="server">
                                                                <input id="CbxSuppPhone" type="checkbox" class="CbxSuppIndifferent" name="support[]" value="support[]" data-title="Phone" /><asp:Label ID="LblSupport3" runat="server" />
                                                            </label>
                                                            <label id="lblS4" runat="server">
                                                                <input id="CbxSuppMail" type="checkbox" class="CbxSuppIndifferent" name="support[]" value="support[]" data-title="Mail" /><asp:Label ID="LblSupport4" runat="server" />
                                                            </label>
                                                        </div>
                                                        <div id="form_payment_error1"></div>
                                                        <span class="help-block">
                                                            <asp:Label ID="LblSupportHelp" runat="server" /></span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="tab-pane" id="tab2">
                                                <h3 class="block">
                                                    <asp:Label ID="LblOptCriteria" runat="server" /></h3>
                                                <div class="form-group col-md-6" style="min-height: 120px; margin-top: 50px;">
                                                    <label class="control-label col-md-6">
                                                        <asp:Label ID="LblTraining" runat="server" />
                                                    </label>
                                                    <div class="col-md-6">
                                                        <select name="training" id="training" class="form-control" onchange="SetSelectedTextTraining(this)">
                                                            <option id="optTrain1" runat="server" value="">
                                                                <asp:Label ID="LblTrain1" runat="server" /></option>
                                                            <option id="optTrain2" runat="server" value="1">
                                                                <asp:Label ID="LblTrain2" runat="server" /></option>
                                                            <option id="optTrain3" runat="server" value="2">
                                                                <asp:Label ID="LblTrain3" runat="server" /></option>
                                                            <option id="optTrain4" runat="server" value="3">
                                                                <asp:Label ID="LblTrain4" runat="server" /></option>
                                                        </select>
                                                        <span class="help-block">
                                                            <asp:Label ID="LblTrainingHelp" runat="server" /></span>
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-6" style="min-height: 120px; margin-top: 50px;">
                                                    <label class="control-label col-md-6">
                                                        <asp:Label ID="LblFreeTraining" runat="server" />
                                                    </label>
                                                    <div class="col-md-6">
                                                        <select name="freeTraining" id="freeTraining" class="form-control" onchange="SetSelectedTextFTraining(this)">
                                                            <option id="optFTrain1" runat="server" value="">
                                                                <asp:Label ID="LblFTrain1" runat="server" /></option>
                                                            <option id="optFTrain2" runat="server" value="1">
                                                                <asp:Label ID="LblFTrain2" runat="server" /></option>
                                                            <option id="optFTrain3" runat="server" value="2">
                                                                <asp:Label ID="LblFTrain3" runat="server" /></option>
                                                            <option id="optFTrain4" runat="server" value="3">
                                                                <asp:Label ID="LblFTrain4" runat="server" /></option>
                                                        </select>
                                                        <span class="help-block">
                                                            <asp:Label ID="LblFreeTrainingHelp" runat="server" /></span>
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-6" style="min-height: 120px;">
                                                    <label class="control-label col-md-6">
                                                        <asp:Label ID="LblProgMaturity" runat="server" />
                                                    </label>
                                                    <div class="col-md-6">
                                                        <select name="progMaturity" id="progMaturity" class="form-control" onchange="SetSelectedTextProgMatur(this)">
                                                            <option id="optProgMat1" runat="server" value="">
                                                                <asp:Label ID="LblProgMat1" runat="server" /></option>
                                                            <option id="optProgMat2" runat="server" value="1">
                                                                <asp:Label ID="LblProgMat2" runat="server" /></option>
                                                            <option id="optProgMat3" runat="server" value="2">
                                                                <asp:Label ID="LblProgMat3" runat="server" /></option>
                                                            <option id="optProgMat4" runat="server" value="3">
                                                                <asp:Label ID="LblProgMat4" runat="server" /></option>
                                                        </select>
                                                        <span class="help-block">
                                                            <asp:Label ID="LblProgMatHelp" runat="server" /></span>
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-6" style="min-height: 120px;">
                                                    <label class="control-label col-md-6">
                                                        <asp:Label ID="LblCompMaturity" runat="server" />
                                                    </label>
                                                    <div class="col-md-6">
                                                        <select name="compMaturity" id="compMaturity" class="form-control" onchange="SetSelectedTextCompMatur(this)">
                                                            <option id="optCompMat1" runat="server" value="">
                                                                <asp:Label ID="LblCompMat1" runat="server" /></option>
                                                            <option id="optCompMat2" runat="server" value="1">
                                                                <asp:Label ID="LblCompMat2" runat="server" /></option>
                                                            <option id="optCompMat3" runat="server" value="2">
                                                                <asp:Label ID="LblCompMat3" runat="server" /></option>
                                                            <option id="optCompMat4" runat="server" value="3">
                                                                <asp:Label ID="LblCompMat4" runat="server" /></option>
                                                        </select>
                                                        <span class="help-block">
                                                            <asp:Label ID="LblCompMatHelp" runat="server" /></span>
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-6" style="min-height: 120px;">
                                                    <label class="control-label col-md-6">
                                                        <asp:Label ID="LblMarkMaterial" runat="server" />
                                                    </label>
                                                    <div class="col-md-6">
                                                        <select name="markMater" id="markMater" class="form-control" onchange="SetSelectedTextMarkMat(this)">
                                                            <option id="optMarkMat1" runat="server" value="">
                                                                <asp:Label ID="LblMarkMat1" runat="server" /></option>
                                                            <option id="optMarkMat2" runat="server" value="1">
                                                                <asp:Label ID="LblMarkMat2" runat="server" /></option>
                                                            <option id="optMarkMat3" runat="server" value="2">
                                                                <asp:Label ID="LblMarkMat3" runat="server" /></option>
                                                            <option id="optMarkMat4" runat="server" value="3">
                                                                <asp:Label ID="LblMarkMat4" runat="server" /></option>
                                                        </select>
                                                        <span class="help-block">
                                                            <asp:Label ID="LblMarkMaterialHelp" runat="server" /></span>
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-6" style="min-height: 120px;">
                                                    <label class="control-label col-md-6">
                                                        <asp:Label ID="LblLocalization" runat="server" />
                                                    </label>
                                                    <div class="col-md-6">
                                                        <select name="localiz" id="localiz" class="form-control" onchange="SetSelectedTextLocaliz(this)">
                                                            <option id="optLocal1" runat="server" value="">
                                                                <asp:Label ID="LblLocal1" runat="server" /></option>
                                                            <option id="optLocal2" runat="server" value="1">
                                                                <asp:Label ID="LblLocal2" runat="server" /></option>
                                                            <option id="optLocal3" runat="server" value="2">
                                                                <asp:Label ID="LblLocal3" runat="server" /></option>
                                                            <option id="optLocal4" runat="server" value="3">
                                                                <asp:Label ID="LblLocal4" runat="server" /></option>
                                                        </select>
                                                        <span class="help-block">
                                                            <asp:Label ID="LblLocalizationHelp" runat="server" /></span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="tab-pane" id="tab3">
                                                <h3 class="block">
                                                    <asp:Label ID="LblUpFiles" runat="server" /></h3>
                                                <div class="form-group col-md-6" style="min-height: 120px; margin-top: 50px;">
                                                    <label class="control-label col-md-6">
                                                        <asp:Label ID="LblMDF" runat="server" />
                                                    </label>
                                                    <div class="col-md-6">
                                                        <select name="Mdf" id="Mdf" class="form-control" onchange="SetSelectedTextMdf(this)">
                                                            <option id="optMDF1" runat="server" value="">
                                                                <asp:Label ID="LblMDF1" runat="server" /></option>
                                                            <option id="optMDF2" runat="server" value="1">
                                                                <asp:Label ID="LblMDF2" runat="server" /></option>
                                                            <option id="optMDF3" runat="server" value="2">
                                                                <asp:Label ID="LblMDF3" runat="server" /></option>
                                                            <option id="optMDF4" runat="server" value="3">
                                                                <asp:Label ID="LblMDF4" runat="server" /></option>
                                                        </select>
                                                        <span class="help-block">
                                                            <asp:Label ID="LblMDFHelp" runat="server" /></span>
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-6" style="min-height: 120px; margin-top: 50px;">
                                                    <label class="control-label col-md-6">
                                                        <asp:Label ID="LblCertifRequired" runat="server" />
                                                    </label>
                                                    <div class="col-md-6">
                                                        <select name="certReq" id="certReq" class="form-control" onchange="SetSelectedTextCert(this)">
                                                            <option id="optCertReq1" runat="server" value="">
                                                                <asp:Label ID="LblCertReq1" runat="server" /></option>
                                                            <option id="optCertReq2" runat="server" value="1">
                                                                <asp:Label ID="LblCertReq2" runat="server" /></option>
                                                            <option id="optCertReq3" runat="server" value="2">
                                                                <asp:Label ID="LblCertReq3" runat="server" /></option>
                                                            <option id="optCertReq4" runat="server" value="3">
                                                                <asp:Label ID="LblCertReq4" runat="server" /></option>
                                                        </select>
                                                        <span class="help-block">
                                                            <asp:Label ID="LblCertifRequiredHelp" runat="server" /></span>
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-6" style="min-height: 120px;">
                                                    <label class="control-label col-md-6">
                                                        <asp:Label ID="LblPortal" runat="server" />
                                                    </label>
                                                    <div class="col-md-6">
                                                        <select name="portal" id="portal" class="form-control" onchange="SetSelectedTextPortal(this)">
                                                            <option id="optPortal1" runat="server" value="">
                                                                <asp:Label ID="LblPortal1" runat="server" /></option>
                                                            <option id="optPortal2" runat="server" value="1">
                                                                <asp:Label ID="LblPortal2" runat="server" /></option>
                                                            <option id="optPortal3" runat="server" value="2">
                                                                <asp:Label ID="LblPortal3" runat="server" /></option>
                                                            <option id="optPortal4" runat="server" value="3">
                                                                <asp:Label ID="LblPortal4" runat="server" /></option>
                                                        </select>
                                                        <span class="help-block">
                                                            <asp:Label ID="LblPortalHelp" runat="server" /></span>
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-6" style="min-height: 120px;">
                                                    <label class="control-label col-md-6">
                                                        <asp:Label ID="LblNumPartners" runat="server" />
                                                    </label>
                                                    <div class="col-md-6">
                                                        <select name="nubPartners" id="nubPartners" class="form-control" onchange="SetSelectedTextPartners(this)">
                                                            <option id="optNumPartn1" runat="server" value="">
                                                                <asp:Label ID="LblNumPartn1" runat="server" /></option>
                                                            <option id="optNumPartn2" runat="server" value="1">
                                                                <asp:Label ID="LblNumPartn2" runat="server" /></option>
                                                            <option id="optNumPartn3" runat="server" value="2">
                                                                <asp:Label ID="LblNumPartn3" runat="server" /></option>
                                                            <option id="optNumPartn4" runat="server" value="3">
                                                                <asp:Label ID="LblNumPartn4" runat="server" /></option>
                                                            <option id="optNumPartn5" runat="server" value="4">
                                                                <asp:Label ID="LblNumPartn5" runat="server" /></option>
                                                        </select>
                                                        <span class="help-block">
                                                            <asp:Label ID="LblNumPartnHelp" runat="server" /></span>
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-6" style="min-height: 120px;">
                                                    <label class="control-label col-md-6">
                                                        <asp:Label ID="LblProgTiers" runat="server" />
                                                    </label>
                                                    <div class="col-md-6">
                                                        <select name="progTiers" id="progTiers" class="form-control" onchange="SetSelectedTextTiers(this)">
                                                            <option id="optProgTier1" runat="server" value="">
                                                                <asp:Label ID="LblProgTier1" runat="server" /></option>
                                                            <option id="optProgTier2" runat="server" value="1">
                                                                <asp:Label ID="LblProgTier2" runat="server" /></option>
                                                            <option id="optProgTier3" runat="server" value="2">
                                                                <asp:Label ID="LblProgTier3" runat="server" /></option>
                                                            <option id="optProgTier4" runat="server" value="3">
                                                                <asp:Label ID="LblProgTier4" runat="server" /></option>
                                                            <option id="optProgTier5" runat="server" value="4">
                                                                <asp:Label ID="LblProgTier5" runat="server" /></option>
                                                        </select>
                                                        <span class="help-block">
                                                            <asp:Label ID="LblProgTiersHelp" runat="server" /></span>
                                                    </div>
                                                </div>
                                                <div id="divCountries" runat="server" visible="false" class="form-group col-md-6">
                                                    <label class="control-label col-md-3">
                                                        <asp:Label ID="LblCountries" runat="server" />
                                                    </label>
                                                    <div class="col-md-9">
                                                        <div class="checkbox-list">
                                                            <label id="lblC1" runat="server" class="col-md-6" style="min-width: 160px;">
                                                                <input id="CbxCountriesSelAll" type="checkbox" name="countries[]" value="countries[]" data-title="Select All" /><asp:Label ID="LblCntr1" runat="server" />
                                                            </label>
                                                            <label id="lblC2" runat="server" class="col-md-6" style="min-width: 160px;">
                                                                <input id="CbxCountriesAsiaPacif" class="CbxCountriesSelAll" type="checkbox" name="countries[]" value="countries[]" data-title="Asia-Pacific" /><asp:Label ID="LblCntr2" runat="server" />
                                                            </label>
                                                            <label id="lblC3" runat="server" class="col-md-6" style="min-width: 160px;">
                                                                <input id="CbxCountriesAfrica" class="CbxCountriesSelAll" type="checkbox" name="countries[]" value="countries[]" data-title="Africa" /><asp:Label ID="LblCntr3" runat="server" />
                                                            </label>
                                                            <label id="lblC4" runat="server" class="col-md-6" style="min-width: 160px;">
                                                                <input id="CbxCountriesEurope" class="CbxCountriesSelAll" type="checkbox" name="countries[]" value="countries[]" data-title="Europe" /><asp:Label ID="LblCntr4" runat="server" />
                                                            </label>
                                                            <label id="lblC5" runat="server" class="col-md-6" style="min-width: 160px;">
                                                                <input id="CbxCountriesMidEast" class="CbxCountriesSelAll" type="checkbox" name="countries[]" value="countries[]" data-title="Middle East" /><asp:Label ID="LblCntr5" runat="server" />
                                                            </label>
                                                            <label id="lblC6" runat="server" class="col-md-6" style="min-width: 160px;">
                                                                <input id="CbxCountriesNortAmer" class="CbxCountriesSelAll" type="checkbox" name="countries[]" value="countries[]" data-title="North America" /><asp:Label ID="LblCntr6" runat="server" />
                                                            </label>
                                                            <label id="lblC7" runat="server" class="col-md-6" style="min-width: 160px;">
                                                                <input id="CbxCountriesSoutAmer" class="CbxCountriesSelAll" type="checkbox" name="countries[]" value="countries[]" data-title="South America" /><asp:Label ID="LblCntr7" runat="server" />
                                                            </label>
                                                            <label id="lblC8" runat="server" class="col-md-6" style="min-width: 160px;">
                                                                <input id="CbxCountriesArgen" class="CbxCountriesSelAll CbxCountriesSoutAmer" type="checkbox" name="countries[]" value="countries[]" data-title="Argentina" /><asp:Label ID="LblCntr8" runat="server" />
                                                            </label>
                                                            <label id="lblC9" runat="server" class="col-md-6" style="min-width: 160px;">
                                                                <input id="CbxCountriesAustr" class="CbxCountriesSelAll CbxCountriesAsiaPacif" type="checkbox" name="countries[]" value="countries[]" data-title="Australia" /><asp:Label ID="LblCntr9" runat="server" />
                                                            </label>
                                                            <label id="lblC10" runat="server" class="col-md-6" style="min-width: 160px;">
                                                                <input id="CbxCountriesBraz" class="CbxCountriesSelAll CbxCountriesSoutAmer" type="checkbox" name="countries[]" value="countries[]" data-title="Brazil" /><asp:Label ID="LblCntr10" runat="server" />
                                                            </label>
                                                            <label id="lblC11" runat="server" class="col-md-6" style="min-width: 160px;">
                                                                <input id="CbxCountriesCanada" class="CbxCountriesSelAll CbxCountriesNortAmer" type="checkbox" name="countries[]" value="countries[]" data-title="Canada" /><asp:Label ID="LblCntr11" runat="server" />
                                                            </label>
                                                            <label id="lblC12" runat="server" class="col-md-6" style="min-width: 160px;">
                                                                <input id="CbxCountriesFrance" class="CbxCountriesSelAll CbxCountriesEurope" type="checkbox" name="countries[]" value="countries[]" data-title="France" /><asp:Label ID="LblCntr12" runat="server" />
                                                            </label>
                                                            <label id="lblC13" runat="server" class="col-md-6" style="min-width: 160px;">
                                                                <input id="CbxCountriesGermany" class="CbxCountriesSelAll CbxCountriesEurope" type="checkbox" name="countries[]" value="countries[]" data-title="Germany" /><asp:Label ID="LblCntr13" runat="server" />
                                                            </label>
                                                            <label id="lblC14" runat="server" class="col-md-6" style="min-width: 160px;">
                                                                <input id="CbxCountriesIndia" class="CbxCountriesSelAll CbxCountriesAsiaPacif" type="checkbox" name="countries[]" value="countries[]" data-title="India" /><asp:Label ID="LblCntr14" runat="server" />
                                                            </label>
                                                            <label id="lblC15" runat="server" class="col-md-6" style="min-width: 160px;">
                                                                <input id="CbxCountriesMexico" class="CbxCountriesSelAll CbxCountriesSoutAmer" type="checkbox" name="countries[]" value="countries[]" data-title="Mexico" /><asp:Label ID="LblCntr15" runat="server" />
                                                            </label>
                                                            <label id="lblC16" runat="server" class="col-md-6" style="min-width: 160px;">
                                                                <input id="CbxCountriesPakistan" class="CbxCountriesSelAll CbxCountriesAsiaPacif" type="checkbox" name="countries[]" value="countries[]" data-title="Pakistan" /><asp:Label ID="LblCntr16" runat="server" />
                                                            </label>
                                                            <label id="lblC17" runat="server" class="col-md-6" style="min-width: 160px;">
                                                                <input id="CbxCountriesSpain" class="CbxCountriesSelAll CbxCountriesEurope" type="checkbox" name="countries[]" value="countries[]" data-title="Spain" /><asp:Label ID="LblCntr17" runat="server" />
                                                            </label>
                                                            <label id="lblC18" runat="server" class="col-md-6" style="min-width: 160px;">
                                                                <input id="CbxCountriesUnKing" class="CbxCountriesSelAll CbxCountriesEurope" type="checkbox" name="countries[]" value="countries[]" data-title="United Kingdom" /><asp:Label ID="LblCntr18" runat="server" />
                                                            </label>
                                                            <label id="lblC19" runat="server" class="col-md-6" style="min-width: 160px;">
                                                                <input id="CbxCountriesUnStat" class="CbxCountriesSelAll CbxCountriesNortAmer" type="checkbox" name="countries[]" value="countries[]" data-title="United States" /><asp:Label ID="LblCntr19" runat="server" />
                                                            </label>
                                                        </div>
                                                    </div>
                                                    <span class="help-block" style="float: right;">
                                                        <asp:Label ID="LblCountriesHelp" runat="server" /></span>
                                                </div>
                                            </div>
                                            <div class="tab-pane" id="tab4">
                                                <h3 class="block">
                                                    <asp:Label ID="LblOverview" runat="server" /></h3>
                                                <h4 class="form-section">
                                                    <asp:Label ID="LblOverviewRequired" runat="server" /></h4>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">
                                                        <asp:Label ID="LblOverviewVerticals" runat="server" /></label>
                                                    <div class="col-md-4">
                                                        <p class="form-control-static" runat="server" data-display="payment[]"></p>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">
                                                        <asp:Label ID="LblOverviewFee" runat="server" /></label>
                                                    <div class="col-md-4">
                                                        <p id="pFee" runat="server" class="form-control-static" data-display="setUpFee"></p>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">
                                                        <asp:Label ID="LblOverviewRevenue" runat="server" /></label>
                                                    <div class="col-md-4">
                                                        <p class="form-control-static" id="ptest" runat="server" data-display="revenue"></p>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">
                                                        <asp:Label ID="LblOverviewSupport" runat="server" /></label>
                                                    <div class="col-md-4">
                                                        <p class="form-control-static" id="p1" runat="server" data-display="support[]"></p>
                                                    </div>
                                                </div>
                                                <h4 class="form-section">
                                                    <asp:Label ID="LblOverviewOptional" runat="server" /></h4>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">
                                                        <asp:Label ID="LblOverviewTraining" runat="server" /></label>
                                                    <div class="col-md-4">
                                                        <p class="form-control-static" data-display="training"></p>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">
                                                        <asp:Label ID="LblOverviewFreeTraining" runat="server" /></label>
                                                    <div class="col-md-4">
                                                        <p class="form-control-static" data-display="freeTraining"></p>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">
                                                        <asp:Label ID="LblOverviewCompMaturity" runat="server" /></label>
                                                    <div class="col-md-4">
                                                        <p class="form-control-static" data-display="compMaturity"></p>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">
                                                        <asp:Label ID="LblProgramMaturity" runat="server" /></label>
                                                    <div class="col-md-4">
                                                        <p class="form-control-static" data-display="progMaturity"></p>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">
                                                        <asp:Label ID="LblOverviewMarkMaterial" runat="server" /></label>
                                                    <div class="col-md-4">
                                                        <p class="form-control-static" data-display="markMater"></p>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">
                                                        <asp:Label ID="LblOverviewLocalization" runat="server" /></label>
                                                    <div class="col-md-4">
                                                        <p class="form-control-static" data-display="localiz"></p>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">
                                                        <asp:Label ID="LblOverviewMDF" runat="server" /></label>
                                                    <div class="col-md-4">
                                                        <p class="form-control-static" data-display="Mdf"></p>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">
                                                        <asp:Label ID="LblOverviewCertification" runat="server" /></label>
                                                    <div class="col-md-4">
                                                        <p class="form-control-static" data-display="certReq"></p>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">
                                                        <asp:Label ID="LblOverviewPortal" runat="server" /></label>
                                                    <div class="col-md-4">
                                                        <p class="form-control-static" data-display="portal"></p>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">
                                                        <asp:Label ID="LblOverviewNumPartners" runat="server" /></label>
                                                    <div class="col-md-4">
                                                        <p class="form-control-static" data-display="nubPartners"></p>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">
                                                        <asp:Label ID="LblOverviewTiers" runat="server" /></label>
                                                    <div class="col-md-4">
                                                        <p class="form-control-static" data-display="progTiers"></p>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">
                                                        <asp:Label ID="LblOverviewCountries" runat="server" /></label>
                                                    <div class="col-md-4">
                                                        <p class="form-control-static" id="p2" runat="server" data-display="countries[]"></p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-actions">
                                        <div class="row">
                                            <div class="col-md-offset-3 col-md-9">
                                                <a class="btn default button-previous">
                                                    <i class="fa fa-angle-left"></i>Back
                                                </a>
                                                <a class="btn btn-outline green button-next">Continue
                                                        <i class="fa fa-angle-right"></i>
                                                </a>
                                                <a class="btn green button-submit">Submit
                                                        <i class="fa fa-check"></i>
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </form>
                            <input type="hidden" id="HdnSetUpFee" name="HdnSetUpFee" />
                            <input type="hidden" id="HdnRevenue" name="HdnRevenue" />
                            <input type="hidden" id="HdnTraining" name="HdnTraining" />
                            <input type="hidden" id="HdnFTraining" name="HdnFTraining" />
                            <input type="hidden" id="HdnProgMatur" name="HdnProgMatur" />
                            <input type="hidden" id="HdnCompMatur" name="HdnCompMatur" />
                            <input type="hidden" id="HdnMarkMat" name="HdnMarkMat" />
                            <input type="hidden" id="HdnLocaliz" name="HdnLocaliz" />
                            <input type="hidden" id="HdnMdf" name="HdnMdf" />
                            <input type="hidden" id="HdnCertif" name="HdnCertif" />
                            <input type="hidden" id="HdnPortal" name="HdnPortal" />
                            <input type="hidden" id="HdnPartners" name="HdnPartners" />
                            <input type="hidden" id="HdnTiers" name="HdnTiers" />
                            <div style="display: none;">
                                <asp:HiddenField ID="HdnVert1Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert2Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert3Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert4Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert5Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert6Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert7Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert8Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert9Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert10Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert11Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert12Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert13Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert14Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert15Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert16Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert17Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert18Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert19Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert20Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert21Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert22Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert23Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert24Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert25Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert26Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert27Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert28Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert29Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert30Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert31Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert32Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert33Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert34Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert35Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert36Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert37Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert38Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert39Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert40Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert41Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert42Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert43Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert44Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert45Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert46Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert47Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert48Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert49Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert50Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert51Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert52Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert53Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert54Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert55Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert56Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert57Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert58Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert59Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert60Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert61Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert62Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert63Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert64Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert65Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert66Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert67Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert68Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert69Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert70Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert71Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert72Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert73Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert74Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert75Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert76Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert77Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert78Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert79Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert80Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert81Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert82Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert83Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert84Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert85Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert86Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert87Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert88Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert89Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert90Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert91Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert92Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert93Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert94Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert95Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert96Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert97Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert98Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert99Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert100Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert101Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert102Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert103Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert104Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert105Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert106Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert107Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert108Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert109Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert110Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert111Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert112Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert113Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert114Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert115Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert116Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert117Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert118Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert119Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert120Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert121Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert122Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert123Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert124Ckd" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnVert125Ckd" Value="0" runat="server" />
                            </div>
                            <div style="display: none;">
                                <asp:HiddenField ID="HdnVert1" Value="1" runat="server" />
                                <asp:HiddenField ID="HdnVert2" Value="2" runat="server" />
                                <asp:HiddenField ID="HdnVert3" Value="3" runat="server" />
                                <asp:HiddenField ID="HdnVert4" Value="4" runat="server" />
                                <asp:HiddenField ID="HdnVert5" Value="5" runat="server" />
                                <asp:HiddenField ID="HdnVert6" Value="6" runat="server" />
                                <asp:HiddenField ID="HdnVert7" Value="7" runat="server" />
                                <asp:HiddenField ID="HdnVert8" Value="8" runat="server" />
                                <asp:HiddenField ID="HdnVert9" Value="9" runat="server" />
                                <asp:HiddenField ID="HdnVert10" Value="10" runat="server" />
                                <asp:HiddenField ID="HdnVert11" Value="11" runat="server" />
                                <asp:HiddenField ID="HdnVert12" Value="12" runat="server" />
                                <asp:HiddenField ID="HdnVert13" Value="13" runat="server" />
                                <asp:HiddenField ID="HdnVert14" Value="14" runat="server" />
                                <asp:HiddenField ID="HdnVert15" Value="15" runat="server" />
                                <asp:HiddenField ID="HdnVert16" Value="16" runat="server" />
                                <asp:HiddenField ID="HdnVert17" Value="17" runat="server" />
                                <asp:HiddenField ID="HdnVert18" Value="18" runat="server" />
                                <asp:HiddenField ID="HdnVert19" Value="19" runat="server" />
                                <asp:HiddenField ID="HdnVert20" Value="20" runat="server" />
                                <asp:HiddenField ID="HdnVert21" Value="21" runat="server" />
                                <asp:HiddenField ID="HdnVert22" Value="22" runat="server" />
                                <asp:HiddenField ID="HdnVert23" Value="23" runat="server" />
                                <asp:HiddenField ID="HdnVert24" Value="24" runat="server" />
                                <asp:HiddenField ID="HdnVert25" Value="25" runat="server" />
                                <asp:HiddenField ID="HdnVert26" Value="26" runat="server" />
                                <asp:HiddenField ID="HdnVert27" Value="27" runat="server" />
                                <asp:HiddenField ID="HdnVert28" Value="28" runat="server" />
                                <asp:HiddenField ID="HdnVert29" Value="29" runat="server" />
                                <asp:HiddenField ID="HdnVert30" Value="30" runat="server" />
                                <asp:HiddenField ID="HdnVert31" Value="31" runat="server" />
                                <asp:HiddenField ID="HdnVert32" Value="32" runat="server" />
                                <asp:HiddenField ID="HdnVert33" Value="33" runat="server" />
                                <asp:HiddenField ID="HdnVert34" Value="34" runat="server" />
                                <asp:HiddenField ID="HdnVert35" Value="35" runat="server" />
                                <asp:HiddenField ID="HdnVert36" Value="36" runat="server" />
                                <asp:HiddenField ID="HdnVert37" Value="37" runat="server" />
                                <asp:HiddenField ID="HdnVert38" Value="38" runat="server" />
                                <asp:HiddenField ID="HdnVert39" Value="39" runat="server" />
                                <asp:HiddenField ID="HdnVert40" Value="40" runat="server" />
                                <asp:HiddenField ID="HdnVert41" Value="41" runat="server" />
                                <asp:HiddenField ID="HdnVert42" Value="42" runat="server" />
                                <asp:HiddenField ID="HdnVert43" Value="43" runat="server" />
                                <asp:HiddenField ID="HdnVert44" Value="44" runat="server" />
                                <asp:HiddenField ID="HdnVert45" Value="45" runat="server" />
                                <asp:HiddenField ID="HdnVert46" Value="46" runat="server" />
                                <asp:HiddenField ID="HdnVert47" Value="47" runat="server" />
                                <asp:HiddenField ID="HdnVert48" Value="48" runat="server" />
                                <asp:HiddenField ID="HdnVert49" Value="49" runat="server" />
                                <asp:HiddenField ID="HdnVert50" Value="50" runat="server" />
                                <asp:HiddenField ID="HdnVert51" Value="51" runat="server" />
                                <asp:HiddenField ID="HdnVert52" Value="52" runat="server" />
                                <asp:HiddenField ID="HdnVert53" Value="53" runat="server" />
                                <asp:HiddenField ID="HdnVert54" Value="54" runat="server" />
                                <asp:HiddenField ID="HdnVert55" Value="55" runat="server" />
                                <asp:HiddenField ID="HdnVert56" Value="56" runat="server" />
                                <asp:HiddenField ID="HdnVert57" Value="57" runat="server" />
                                <asp:HiddenField ID="HdnVert58" Value="58" runat="server" />
                                <asp:HiddenField ID="HdnVert59" Value="59" runat="server" />
                                <asp:HiddenField ID="HdnVert60" Value="60" runat="server" />
                                <asp:HiddenField ID="HdnVert61" Value="61" runat="server" />
                                <asp:HiddenField ID="HdnVert62" Value="62" runat="server" />
                                <asp:HiddenField ID="HdnVert63" Value="63" runat="server" />
                                <asp:HiddenField ID="HdnVert64" Value="64" runat="server" />
                                <asp:HiddenField ID="HdnVert65" Value="65" runat="server" />
                                <asp:HiddenField ID="HdnVert66" Value="66" runat="server" />
                                <asp:HiddenField ID="HdnVert67" Value="67" runat="server" />
                                <asp:HiddenField ID="HdnVert68" Value="68" runat="server" />
                                <asp:HiddenField ID="HdnVert69" Value="69" runat="server" />
                                <asp:HiddenField ID="HdnVert70" Value="70" runat="server" />
                                <asp:HiddenField ID="HdnVert71" Value="71" runat="server" />
                                <asp:HiddenField ID="HdnVert72" Value="72" runat="server" />
                                <asp:HiddenField ID="HdnVert73" Value="73" runat="server" />
                                <asp:HiddenField ID="HdnVert74" Value="74" runat="server" />
                                <asp:HiddenField ID="HdnVert75" Value="75" runat="server" />
                                <asp:HiddenField ID="HdnVert76" Value="76" runat="server" />
                                <asp:HiddenField ID="HdnVert77" Value="77" runat="server" />
                                <asp:HiddenField ID="HdnVert78" Value="78" runat="server" />
                                <asp:HiddenField ID="HdnVert79" Value="79" runat="server" />
                                <asp:HiddenField ID="HdnVert80" Value="80" runat="server" />
                                <asp:HiddenField ID="HdnVert81" Value="81" runat="server" />
                                <asp:HiddenField ID="HdnVert82" Value="82" runat="server" />
                                <asp:HiddenField ID="HdnVert83" Value="83" runat="server" />
                                <asp:HiddenField ID="HdnVert84" Value="84" runat="server" />
                                <asp:HiddenField ID="HdnVert85" Value="85" runat="server" />
                                <asp:HiddenField ID="HdnVert86" Value="86" runat="server" />
                                <asp:HiddenField ID="HdnVert87" Value="87" runat="server" />
                                <asp:HiddenField ID="HdnVert88" Value="88" runat="server" />
                                <asp:HiddenField ID="HdnVert89" Value="89" runat="server" />
                                <asp:HiddenField ID="HdnVert90" Value="90" runat="server" />
                                <asp:HiddenField ID="HdnVert91" Value="91" runat="server" />
                                <asp:HiddenField ID="HdnVert92" Value="92" runat="server" />
                                <asp:HiddenField ID="HdnVert93" Value="93" runat="server" />
                                <asp:HiddenField ID="HdnVert94" Value="94" runat="server" />
                                <asp:HiddenField ID="HdnVert95" Value="95" runat="server" />
                                <asp:HiddenField ID="HdnVert96" Value="96" runat="server" />
                                <asp:HiddenField ID="HdnVert97" Value="97" runat="server" />
                                <asp:HiddenField ID="HdnVert98" Value="98" runat="server" />
                                <asp:HiddenField ID="HdnVert99" Value="99" runat="server" />
                                <asp:HiddenField ID="HdnVert100" Value="100" runat="server" />
                                <asp:HiddenField ID="HdnVert101" Value="101" runat="server" />
                                <asp:HiddenField ID="HdnVert102" Value="102" runat="server" />
                                <asp:HiddenField ID="HdnVert103" Value="103" runat="server" />
                                <asp:HiddenField ID="HdnVert104" Value="104" runat="server" />
                                <asp:HiddenField ID="HdnVert105" Value="105" runat="server" />
                                <asp:HiddenField ID="HdnVert106" Value="106" runat="server" />
                                <asp:HiddenField ID="HdnVert107" Value="107" runat="server" />
                                <asp:HiddenField ID="HdnVert108" Value="108" runat="server" />
                                <asp:HiddenField ID="HdnVert109" Value="109" runat="server" />
                                <asp:HiddenField ID="HdnVert110" Value="110" runat="server" />
                                <asp:HiddenField ID="HdnVert111" Value="111" runat="server" />
                                <asp:HiddenField ID="HdnVert112" Value="112" runat="server" />
                                <asp:HiddenField ID="HdnVert113" Value="113" runat="server" />
                                <asp:HiddenField ID="HdnVert114" Value="114" runat="server" />
                                <asp:HiddenField ID="HdnVert115" Value="115" runat="server" />
                                <asp:HiddenField ID="HdnVert116" Value="116" runat="server" />
                                <asp:HiddenField ID="HdnVert117" Value="117" runat="server" />
                                <asp:HiddenField ID="HdnVert118" Value="118" runat="server" />
                                <asp:HiddenField ID="HdnVert119" Value="119" runat="server" />
                                <asp:HiddenField ID="HdnVert120" Value="120" runat="server" />
                                <asp:HiddenField ID="HdnVert121" Value="121" runat="server" />
                                <asp:HiddenField ID="HdnVert122" Value="122" runat="server" />
                                <asp:HiddenField ID="HdnVert123" Value="123" runat="server" />
                                <asp:HiddenField ID="HdnVert124" Value="124" runat="server" />
                                <asp:HiddenField ID="HdnVert125" Value="125" runat="server" />
                            </div>
                        </div>
                        <div style="display: none;">
                            <asp:HiddenField ID="HdnSuppIndifCkd" Value="0" runat="server" />
                            <asp:HiddenField ID="HdnSuppDedicCkd" Value="0" runat="server" />
                            <asp:HiddenField ID="HdnSuppPhoneCkd" Value="0" runat="server" />
                            <asp:HiddenField ID="HdnSuppMailCkd" Value="0" runat="server" />
                        </div>
                        <div style="display: none;">
                            <asp:HiddenField ID="HdnSuppIndif" Value="Indifferent" runat="server" />
                            <asp:HiddenField ID="HdnSuppDedic" Value="Dedicated" runat="server" />
                            <asp:HiddenField ID="HdnSuppPhone" Value="Phone" runat="server" />
                            <asp:HiddenField ID="HdnSuppMail" Value="Mail" runat="server" />
                        </div>
                    </div>
                    <div style="display: none;">
                        <asp:HiddenField ID="HdnCountriesSelAllCkd" Value="0" runat="server" />
                        <asp:HiddenField ID="HdnCountriesAsiaPacifCkd" Value="0" runat="server" />
                        <asp:HiddenField ID="HdnCountriesAfricaCkd" Value="0" runat="server" />
                        <asp:HiddenField ID="HdnCountriesEuropeCkd" Value="0" runat="server" />
                        <asp:HiddenField ID="HdnCountriesMidEastCkd" Value="0" runat="server" />
                        <asp:HiddenField ID="HdnCountriesNortAmerCkd" Value="0" runat="server" />
                        <asp:HiddenField ID="HdnCountriesSoutAmerCkd" Value="0" runat="server" />
                        <asp:HiddenField ID="HdnCountriesArgenCkd" Value="0" runat="server" />
                        <asp:HiddenField ID="HdnCountriesAustrCkd" Value="0" runat="server" />
                        <asp:HiddenField ID="HdnCountriesBrazCkd" Value="0" runat="server" />
                        <asp:HiddenField ID="HdnCountriesCanadaCkd" Value="0" runat="server" />
                        <asp:HiddenField ID="HdnCountriesFranceCkd" Value="0" runat="server" />
                        <asp:HiddenField ID="HdnCountriesGermanyCkd" Value="0" runat="server" />
                        <asp:HiddenField ID="HdnCountriesIndiaCkd" Value="0" runat="server" />
                        <asp:HiddenField ID="HdnCountriesMexicoCkd" Value="0" runat="server" />
                        <asp:HiddenField ID="HdnCountriesPakistanCkd" Value="0" runat="server" />
                        <asp:HiddenField ID="HdnCountriesSpainCkd" Value="0" runat="server" />
                        <asp:HiddenField ID="HdnCountriesUnKingCkd" Value="0" runat="server" />
                        <asp:HiddenField ID="HdnCountriesUnStatCkd" Value="0" runat="server" />
                    </div>
                    <div style="display: none;">
                        <asp:HiddenField ID="HdnCountriesSelAll" Value="Select All" runat="server" />
                        <asp:HiddenField ID="HdnCountriesAsiaPacif" Value="Asia-Pacific" runat="server" />
                        <asp:HiddenField ID="HdnCountriesAfrica" Value="Africa" runat="server" />
                        <asp:HiddenField ID="HdnCountriesEurope" Value="Europe" runat="server" />
                        <asp:HiddenField ID="HdnCountriesMidEast" Value="Middle East" runat="server" />
                        <asp:HiddenField ID="HdnCountriesNortAmer" Value="North America" runat="server" />
                        <asp:HiddenField ID="HdnCountriesSoutAmer" Value="South America" runat="server" />
                        <asp:HiddenField ID="HdnCountriesArgen" Value="Argentina" runat="server" />
                        <asp:HiddenField ID="HdnCountriesAustr" Value="Australia" runat="server" />
                        <asp:HiddenField ID="HdnCountriesBraz" Value="Brazil" runat="server" />
                        <asp:HiddenField ID="HdnCountriesCanada" Value="Canada" runat="server" />
                        <asp:HiddenField ID="HdnCountriesFrance" Value="France" runat="server" />
                        <asp:HiddenField ID="HdnCountriesGermany" Value="Germany" runat="server" />
                        <asp:HiddenField ID="HdnCountriesIndia" Value="India" runat="server" />
                        <asp:HiddenField ID="HdnCountriesMexico" Value="Mexico" runat="server" />
                        <asp:HiddenField ID="HdnCountriesPakistan" Value="Pakistan" runat="server" />
                        <asp:HiddenField ID="HdnCountriesSpain" Value="Spain" runat="server" />
                        <asp:HiddenField ID="HdnCountriesUnKing" Value="United Kingdom" runat="server" />
                        <asp:HiddenField ID="HdnCountriesUnStat" Value="United States" runat="server" />
                    </div>
                </div>
                <div style="display: none;">
                    <a id="aSubmit" runat="server" onserverclick="BtnSubmit_OnClick"></a>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="aSubmit" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function SetSelectedTextFee(setUpFee) {
            var selectedText = setUpFee.options[setUpFee.selectedIndex].innerHTML;
            document.getElementById("HdnSetUpFee").value = selectedText;
        }
    </script>
    <script type="text/javascript">
        function SetSelectedTextRevenue(revenue) {
            var selectedText = revenue.options[revenue.selectedIndex].innerHTML;
            document.getElementById("HdnRevenue").value = selectedText;
        }
    </script>
    <script type="text/javascript">
        function SetSelectedTextTraining(training) {
            var selectedText = training.options[training.selectedIndex].innerHTML;
            document.getElementById("HdnTraining").value = selectedText;
        }
    </script>
    <script type="text/javascript">
        function SetSelectedTextFTraining(freeTraining) {
            var selectedText = freeTraining.options[freeTraining.selectedIndex].innerHTML;
            document.getElementById("HdnFTraining").value = selectedText;
        }
    </script>
    <script type="text/javascript">
        function SetSelectedTextProgMatur(progMaturity) {
            var selectedText = progMaturity.options[progMaturity.selectedIndex].innerHTML;
            document.getElementById("HdnProgMatur").value = selectedText;
        }
    </script>
    <script type="text/javascript">
        function SetSelectedTextCompMatur(compMaturity) {
            var selectedText = compMaturity.options[compMaturity.selectedIndex].innerHTML;
            document.getElementById("HdnCompMatur").value = selectedText;
        }
    </script>
    <script type="text/javascript">
        function SetSelectedTextCompMatur(compMaturity) {
            var selectedText = compMaturity.options[compMaturity.selectedIndex].innerHTML;
            document.getElementById("HdnCompMatur").value = selectedText;
        }
    </script>
    <script type="text/javascript">
        function SetSelectedTextMarkMat(markMater) {
            var selectedText = markMater.options[markMater.selectedIndex].innerHTML;
            document.getElementById("HdnMarkMat").value = selectedText;
        }
    </script>
    <script type="text/javascript">
        function SetSelectedTextLocaliz(localiz) {
            var selectedText = localiz.options[localiz.selectedIndex].innerHTML;
            document.getElementById("HdnLocaliz").value = selectedText;
        }
    </script>
    <script type="text/javascript">
        function SetSelectedTextMdf(Mdf) {
            var selectedText = Mdf.options[Mdf.selectedIndex].innerHTML;
            document.getElementById("HdnMdf").value = selectedText;
        }
    </script>
    <script type="text/javascript">
        function SetSelectedTextCert(certReq) {
            var selectedText = certReq.options[certReq.selectedIndex].innerHTML;
            document.getElementById("HdnCertif").value = selectedText;
        }
    </script>
    <script type="text/javascript">
        function SetSelectedTextPortal(portal) {
            var selectedText = portal.options[portal.selectedIndex].innerHTML;
            document.getElementById("HdnPortal").value = selectedText;
        }
    </script>
    <script type="text/javascript">
        function SetSelectedTextPartners(nubPartners) {
            var selectedText = nubPartners.options[nubPartners.selectedIndex].innerHTML;
            document.getElementById("HdnPartners").value = selectedText;
        }
    </script>
    <script type="text/javascript">
        function SetSelectedTextTiers(progTiers) {
            var selectedText = progTiers.options[progTiers.selectedIndex].innerHTML;
            document.getElementById("HdnTiers").value = selectedText;
        }
    </script>
    <script id="SetCheckedCountries" type="text/javascript">
        function SetCheckedCountries() {
            var x1 = document.getElementById('<%= HdnCountriesSelAllCkd.ClientID%>');
                if (x1.value == "1") {
                    var y1 = document.getElementById("CbxCountriesSelAll").checked = true;
                }
                var x2 = document.getElementById('<%= HdnCountriesAsiaPacifCkd.ClientID%>');
                if (x2.value == "1") {
                    var y2 = document.getElementById("CbxCountriesAsiaPacif").checked = true;
                }
                var x3 = document.getElementById('<%= HdnCountriesAfricaCkd.ClientID%>');
                if (x3.value == "1") {
                    var y3 = document.getElementById("CbxCountriesAfrica").checked = true;
                }
                var x4 = document.getElementById('<%= HdnCountriesEuropeCkd.ClientID%>');
                if (x4.value == "1") {
                    var y4 = document.getElementById("CbxCountriesEurope").checked = true;
                }
                var x5 = document.getElementById('<%= HdnCountriesMidEastCkd.ClientID%>');
                if (x5.value == "1") {
                    var y5 = document.getElementById("CbxCountriesMidEast").checked = true;
                }
                var x6 = document.getElementById('<%= HdnCountriesNortAmerCkd.ClientID%>');
                if (x6.value == "1") {
                    var y6 = document.getElementById("CbxCountriesNortAmer").checked = true;
                }
                var x7 = document.getElementById('<%= HdnCountriesSoutAmerCkd.ClientID%>');
                if (x7.value == "1") {
                    var y7 = document.getElementById("CbxCountriesSoutAmer").checked = true;
                }
                var x8 = document.getElementById('<%= HdnCountriesArgenCkd.ClientID%>');
                if (x8.value == "1") {
                    var y8 = document.getElementById("CbxCountriesArgen").checked = true;
                }
                var x9 = document.getElementById('<%= HdnCountriesAustrCkd.ClientID%>');
                if (x9.value == "1") {
                    var y9 = document.getElementById("CbxCountriesAustr").checked = true;
                }
                var x10 = document.getElementById('<%= HdnCountriesBrazCkd.ClientID%>');
                if (x10.value == "1") {
                    var y10 = document.getElementById("CbxCountriesBraz").checked = true;
                }
                var x11 = document.getElementById('<%= HdnCountriesCanadaCkd.ClientID%>');
                if (x11.value == "1") {
                    var y11 = document.getElementById("CbxCountriesCanada").checked = true;
                }
                var x12 = document.getElementById('<%= HdnCountriesFranceCkd.ClientID%>');
                if (x12.value == "1") {
                    var y12 = document.getElementById("CbxCountriesFrance").checked = true;
                }
                var x13 = document.getElementById('<%= HdnCountriesGermanyCkd.ClientID%>');
                if (x13.value == "1") {
                    var y13 = document.getElementById("CbxCountriesGermany").checked = true;
                }
                var x14 = document.getElementById('<%= HdnCountriesIndiaCkd.ClientID%>');
                if (x14.value == "1") {
                    var y14 = document.getElementById("CbxCountriesIndia").checked = true;
                }
                var x15 = document.getElementById('<%= HdnCountriesMexicoCkd.ClientID%>');
                if (x15.value == "1") {
                    var y15 = document.getElementById("CbxCountriesMexico").checked = true;
                }
                var x16 = document.getElementById('<%= HdnCountriesPakistanCkd.ClientID%>');
                if (x16.value == "1") {
                    var y16 = document.getElementById("CbxCountriesPakistan").checked = true;
                }
                var x17 = document.getElementById('<%= HdnCountriesSpainCkd.ClientID%>');
                if (x17.value == "1") {
                    var y17 = document.getElementById("CbxCountriesSpain").checked = true;
                }
                var x18 = document.getElementById('<%= HdnCountriesUnKingCkd.ClientID%>');
                if (x18.value == "1") {
                    var y18 = document.getElementById("CbxCountriesUnKing").checked = true;
                }
                var x19 = document.getElementById('<%= HdnCountriesUnStatCkd.ClientID%>');
            if (x19.value == "1") {
                var y19 = document.getElementById("CbxCountriesUnStat").checked = true;
            }
        }
    </script>
    <script id="SetCheckedSupport" type="text/javascript">
        function SetCheckedSupport() {
            var x1 = document.getElementById('<%= HdnSuppIndifCkd.ClientID%>');
                if (x1.value == "1") {
                    var y1 = document.getElementById("CbxSuppIndifferent").checked = true;
                }
                var x2 = document.getElementById('<%= HdnSuppDedicCkd.ClientID%>');
                if (x2.value == "1") {
                    var y2 = document.getElementById("CbxSuppDedicated").checked = true;
                }
                var x3 = document.getElementById('<%= HdnSuppPhoneCkd.ClientID%>');
                if (x3.value == "1") {
                    var y3 = document.getElementById("CbxSuppPhone").checked = true;
                }
                var x4 = document.getElementById('<%= HdnSuppMailCkd.ClientID%>');
            if (x4.value == "1") {
                var y4 = document.getElementById("CbxSuppMail").checked = true;
            }
        }
    </script>
    <script id="SetCheckedVerticals" type="text/javascript">
        function SetCheckedVertivals() {
            var x1 = document.getElementById('<%= HdnVert1Ckd.ClientID%>');
                if (x1.value == "1") {
                    var y1 = document.getElementById("CbxVert1").checked = true;
                }
                var x2 = document.getElementById('<%= HdnVert2Ckd.ClientID%>');
                if (x2.value == "1") {
                    var y2 = document.getElementById("CbxVert2").checked = true;
                }
                var x3 = document.getElementById('<%= HdnVert3Ckd.ClientID%>');
                if (x3.value == "1") {
                    var y3 = document.getElementById("CbxVert3").checked = true;
                }
                var x4 = document.getElementById('<%= HdnVert4Ckd.ClientID%>');
                if (x4.value == "1") {
                    var y4 = document.getElementById("CbxVert4").checked = true;
                }
                var x5 = document.getElementById('<%= HdnVert5Ckd.ClientID%>');
                if (x5.value == "1") {
                    var y5 = document.getElementById("CbxVert5").checked = true;
                }
                var x6 = document.getElementById('<%= HdnVert6Ckd.ClientID%>');
                if (x6.value == "1") {
                    var y6 = document.getElementById("CbxVert6").checked = true;
                }
                var x7 = document.getElementById('<%= HdnVert7Ckd.ClientID%>');
                if (x7.value == "1") {
                    var y7 = document.getElementById("CbxVert7").checked = true;
                }
                var x8 = document.getElementById('<%= HdnVert8Ckd.ClientID%>');
                if (x8.value == "1") {
                    var y8 = document.getElementById("CbxVert8").checked = true;
                }
                var x9 = document.getElementById('<%= HdnVert9Ckd.ClientID%>');
                if (x9.value == "1") {
                    var y9 = document.getElementById("CbxVert9").checked = true;
                }
                var x10 = document.getElementById('<%= HdnVert10Ckd.ClientID%>');
                if (x10.value == "1") {
                    var y10 = document.getElementById("CbxVert10").checked = true;
                }
                var x11 = document.getElementById('<%= HdnVert11Ckd.ClientID%>');
                if (x11.value == "1") {
                    var y11 = document.getElementById("CbxVert11").checked = true;
                }
                var x12 = document.getElementById('<%= HdnVert12Ckd.ClientID%>');
                if (x12.value == "1") {
                    var y12 = document.getElementById("CbxVert12").checked = true;
                }
                var x13 = document.getElementById('<%= HdnVert13Ckd.ClientID%>');
                if (x13.value == "1") {
                    var y13 = document.getElementById("CbxVert13").checked = true;
                }
                var x14 = document.getElementById('<%= HdnVert14Ckd.ClientID%>');
                if (x14.value == "1") {
                    var y14 = document.getElementById("CbxVert14").checked = true;
                }
                var x15 = document.getElementById('<%= HdnVert15Ckd.ClientID%>');
                if (x15.value == "1") {
                    var y15 = document.getElementById("CbxVert15").checked = true;
                }
                var x16 = document.getElementById('<%= HdnVert16Ckd.ClientID%>');
                if (x16.value == "1") {
                    var y16 = document.getElementById("CbxVert16").checked = true;
                }
                var x17 = document.getElementById('<%= HdnVert17Ckd.ClientID%>');
                if (x17.value == "1") {
                    var y17 = document.getElementById("CbxVert17").checked = true;
                }
                var x18 = document.getElementById('<%= HdnVert18Ckd.ClientID%>');
                if (x18.value == "1") {
                    var y18 = document.getElementById("CbxVert18").checked = true;
                }
                var x19 = document.getElementById('<%= HdnVert19Ckd.ClientID%>');
                if (x19.value == "1") {
                    var y19 = document.getElementById("CbxVert19").checked = true;
                }
                var x20 = document.getElementById('<%= HdnVert20Ckd.ClientID%>');
                if (x20.value == "1") {
                    var y20 = document.getElementById("CbxVert20").checked = true;
                }
                var x21 = document.getElementById('<%= HdnVert21Ckd.ClientID%>');
                if (x21.value == "1") {
                    var y21 = document.getElementById("CbxVert21").checked = true;
                }
                var x22 = document.getElementById('<%= HdnVert22Ckd.ClientID%>');
                if (x22.value == "1") {
                    var y22 = document.getElementById("CbxVert22").checked = true;
                }
                var x23 = document.getElementById('<%= HdnVert23Ckd.ClientID%>');
                if (x23.value == "1") {
                    var y23 = document.getElementById("CbxVert23").checked = true;
                }
                var x24 = document.getElementById('<%= HdnVert24Ckd.ClientID%>');
                if (x24.value == "1") {
                    var y24 = document.getElementById("CbxVert24").checked = true;
                }
                var x25 = document.getElementById('<%= HdnVert25Ckd.ClientID%>');
                if (x25.value == "1") {
                    var y25 = document.getElementById("CbxVert25").checked = true;
                }
                var x26 = document.getElementById('<%= HdnVert26Ckd.ClientID%>');
                if (x26.value == "1") {
                    var y26 = document.getElementById("CbxVert26").checked = true;
                }
                var x27 = document.getElementById('<%= HdnVert27Ckd.ClientID%>');
                if (x27.value == "1") {
                    var y27 = document.getElementById("CbxVert27").checked = true;
                }
                var x28 = document.getElementById('<%= HdnVert28Ckd.ClientID%>');
                if (x28.value == "1") {
                    var y28 = document.getElementById("CbxVert28").checked = true;
                }
                var x29 = document.getElementById('<%= HdnVert29Ckd.ClientID%>');
                if (x29.value == "1") {
                    var y29 = document.getElementById("CbxVert29").checked = true;
                }
                var x30 = document.getElementById('<%= HdnVert30Ckd.ClientID%>');
                if (x30.value == "1") {
                    var y30 = document.getElementById("CbxVert30").checked = true;
                }
                var x31 = document.getElementById('<%= HdnVert31Ckd.ClientID%>');
                if (x31.value == "1") {
                    var y31 = document.getElementById("CbxVert31").checked = true;
                }
                var x32 = document.getElementById('<%= HdnVert32Ckd.ClientID%>');
                if (x32.value == "1") {
                    var y32 = document.getElementById("CbxVert32").checked = true;
                }
                var x33 = document.getElementById('<%= HdnVert33Ckd.ClientID%>');
                if (x33.value == "1") {
                    var y33 = document.getElementById("CbxVert33").checked = true;
                }
                var x34 = document.getElementById('<%= HdnVert34Ckd.ClientID%>');
                if (x34.value == "1") {
                    var y34 = document.getElementById("CbxVert34").checked = true;
                }
                var x35 = document.getElementById('<%= HdnVert35Ckd.ClientID%>');
                if (x35.value == "1") {
                    var y35 = document.getElementById("CbxVert35").checked = true;
                }
                var x36 = document.getElementById('<%= HdnVert36Ckd.ClientID%>');
                if (x36.value == "1") {
                    var y36 = document.getElementById("CbxVert36").checked = true;
                }
                var x37 = document.getElementById('<%= HdnVert37Ckd.ClientID%>');
                if (x37.value == "1") {
                    var y37 = document.getElementById("CbxVert37").checked = true;
                }
                var x38 = document.getElementById('<%= HdnVert38Ckd.ClientID%>');
                if (x38.value == "1") {
                    var y38 = document.getElementById("CbxVert38").checked = true;
                }
                var x39 = document.getElementById('<%= HdnVert39Ckd.ClientID%>');
                if (x39.value == "1") {
                    var y39 = document.getElementById("CbxVert39").checked = true;
                }
                var x40 = document.getElementById('<%= HdnVert40Ckd.ClientID%>');
                if (x40.value == "1") {
                    var y40 = document.getElementById("CbxVert40").checked = true;
                }
                var x41 = document.getElementById('<%= HdnVert41Ckd.ClientID%>');
                if (x41.value == "1") {
                    var y41 = document.getElementById("CbxVert41").checked = true;
                }
                var x42 = document.getElementById('<%= HdnVert42Ckd.ClientID%>');
                if (x42.value == "1") {
                    var y42 = document.getElementById("CbxVert42").checked = true;
                }
                var x43 = document.getElementById('<%= HdnVert43Ckd.ClientID%>');
                if (x43.value == "1") {
                    var y43 = document.getElementById("CbxVert43").checked = true;
                }
                var x44 = document.getElementById('<%= HdnVert44Ckd.ClientID%>');
                if (x44.value == "1") {
                    var y44 = document.getElementById("CbxVert44").checked = true;
                }
                var x45 = document.getElementById('<%= HdnVert45Ckd.ClientID%>');
                if (x45.value == "1") {
                    var y45 = document.getElementById("CbxVert45").checked = true;
                }
                var x46 = document.getElementById('<%= HdnVert46Ckd.ClientID%>');
                if (x46.value == "1") {
                    var y46 = document.getElementById("CbxVert46").checked = true;
                }
                var x47 = document.getElementById('<%= HdnVert47Ckd.ClientID%>');
                if (x47.value == "1") {
                    var y47 = document.getElementById("CbxVert47").checked = true;
                }
                var x48 = document.getElementById('<%= HdnVert48Ckd.ClientID%>');
                if (x48.value == "1") {
                    var y48 = document.getElementById("CbxVert48").checked = true;
                }
                var x49 = document.getElementById('<%= HdnVert49Ckd.ClientID%>');
                if (x49.value == "1") {
                    var y49 = document.getElementById("CbxVert49").checked = true;
                }
                var x50 = document.getElementById('<%= HdnVert50Ckd.ClientID%>');
                if (x50.value == "1") {
                    var y50 = document.getElementById("CbxVert50").checked = true;
                }
                var x51 = document.getElementById('<%= HdnVert51Ckd.ClientID%>');
                if (x51.value == "1") {
                    var y51 = document.getElementById("CbxVert51").checked = true;
                }
                var x52 = document.getElementById('<%= HdnVert52Ckd.ClientID%>');
                if (x52.value == "1") {
                    var y52 = document.getElementById("CbxVert52").checked = true;
                }
                var x53 = document.getElementById('<%= HdnVert53Ckd.ClientID%>');
                if (x53.value == "1") {
                    var y53 = document.getElementById("CbxVert53").checked = true;
                }
                var x54 = document.getElementById('<%= HdnVert54Ckd.ClientID%>');
                if (x54.value == "1") {
                    var y54 = document.getElementById("CbxVert54").checked = true;
                }
                var x55 = document.getElementById('<%= HdnVert55Ckd.ClientID%>');
                if (x55.value == "1") {
                    var y55 = document.getElementById("CbxVert55").checked = true;
                }
                var x56 = document.getElementById('<%= HdnVert56Ckd.ClientID%>');
                if (x56.value == "1") {
                    var y56 = document.getElementById("CbxVert56").checked = true;
                }
                var x57 = document.getElementById('<%= HdnVert57Ckd.ClientID%>');
                if (x57.value == "1") {
                    var y57 = document.getElementById("CbxVert57").checked = true;
                }
                var x58 = document.getElementById('<%= HdnVert58Ckd.ClientID%>');
                if (x58.value == "1") {
                    var y58 = document.getElementById("CbxVert58").checked = true;
                }
                var x59 = document.getElementById('<%= HdnVert59Ckd.ClientID%>');
                if (x59.value == "1") {
                    var y59 = document.getElementById("CbxVert59").checked = true;
                }
                var x60 = document.getElementById('<%= HdnVert60Ckd.ClientID%>');
                if (x60.value == "1") {
                    var y60 = document.getElementById("CbxVert60").checked = true;
                }
                var x61 = document.getElementById('<%= HdnVert61Ckd.ClientID%>');
                if (x61.value == "1") {
                    var y61 = document.getElementById("CbxVert61").checked = true;
                }
                var x62 = document.getElementById('<%= HdnVert62Ckd.ClientID%>');
                if (x62.value == "1") {
                    var y62 = document.getElementById("CbxVert62").checked = true;
                }
                var x63 = document.getElementById('<%= HdnVert63Ckd.ClientID%>');
                if (x63.value == "1") {
                    var y63 = document.getElementById("CbxVert63").checked = true;
                }
                var x64 = document.getElementById('<%= HdnVert64Ckd.ClientID%>');
                if (x64.value == "1") {
                    var y64 = document.getElementById("CbxVert64").checked = true;
                }
                var x65 = document.getElementById('<%= HdnVert65Ckd.ClientID%>');
                if (x65.value == "1") {
                    var y65 = document.getElementById("CbxVert65").checked = true;
                }
                var x66 = document.getElementById('<%= HdnVert66Ckd.ClientID%>');
                if (x66.value == "1") {
                    var y66 = document.getElementById("CbxVert66").checked = true;
                }
                var x67 = document.getElementById('<%= HdnVert67Ckd.ClientID%>');
                if (x67.value == "1") {
                    var y67 = document.getElementById("CbxVert67").checked = true;
                }
                var x68 = document.getElementById('<%= HdnVert68Ckd.ClientID%>');
                if (x68.value == "1") {
                    var y68 = document.getElementById("CbxVert68").checked = true;
                }
                var x69 = document.getElementById('<%= HdnVert69Ckd.ClientID%>');
                if (x69.value == "1") {
                    var y69 = document.getElementById("CbxVert69").checked = true;
                }
                var x70 = document.getElementById('<%= HdnVert70Ckd.ClientID%>');
                if (x70.value == "1") {
                    var y70 = document.getElementById("CbxVert70").checked = true;
                }
                var x71 = document.getElementById('<%= HdnVert71Ckd.ClientID%>');
                if (x71.value == "1") {
                    var y71 = document.getElementById("CbxVert71").checked = true;
                }
                var x72 = document.getElementById('<%= HdnVert72Ckd.ClientID%>');
                if (x72.value == "1") {
                    var y72 = document.getElementById("CbxVert72").checked = true;
                }
                var x73 = document.getElementById('<%= HdnVert73Ckd.ClientID%>');
                if (x73.value == "1") {
                    var y73 = document.getElementById("CbxVert73").checked = true;
                }
                var x74 = document.getElementById('<%= HdnVert74Ckd.ClientID%>');
                if (x74.value == "1") {
                    var y74 = document.getElementById("CbxVert74").checked = true;
                }
                var x75 = document.getElementById('<%= HdnVert75Ckd.ClientID%>');
                if (x75.value == "1") {
                    var y75 = document.getElementById("CbxVert75").checked = true;
                }
                var x76 = document.getElementById('<%= HdnVert76Ckd.ClientID%>');
                if (x76.value == "1") {
                    var y76 = document.getElementById("CbxVert76").checked = true;
                }
                var x77 = document.getElementById('<%= HdnVert77Ckd.ClientID%>');
                if (x77.value == "1") {
                    var y77 = document.getElementById("CbxVert77").checked = true;
                }
                var x78 = document.getElementById('<%= HdnVert78Ckd.ClientID%>');
                if (x78.value == "1") {
                    var y78 = document.getElementById("CbxVert78").checked = true;
                }
                var x79 = document.getElementById('<%= HdnVert79Ckd.ClientID%>');
                if (x79.value == "1") {
                    var y79 = document.getElementById("CbxVert79").checked = true;
                }
                var x80 = document.getElementById('<%= HdnVert80Ckd.ClientID%>');
                if (x80.value == "1") {
                    var y80 = document.getElementById("CbxVert80").checked = true;
                }
                var x81 = document.getElementById('<%= HdnVert81Ckd.ClientID%>');
                if (x81.value == "1") {
                    var y81 = document.getElementById("CbxVert81").checked = true;
                }
                var x82 = document.getElementById('<%= HdnVert82Ckd.ClientID%>');
                if (x82.value == "1") {
                    var y82 = document.getElementById("CbxVert82").checked = true;
                }
                var x83 = document.getElementById('<%= HdnVert83Ckd.ClientID%>');
                if (x83.value == "1") {
                    var y83 = document.getElementById("CbxVert83").checked = true;
                }
                var x84 = document.getElementById('<%= HdnVert84Ckd.ClientID%>');
                if (x84.value == "1") {
                    var y84 = document.getElementById("CbxVert84").checked = true;
                }
                var x85 = document.getElementById('<%= HdnVert85Ckd.ClientID%>');
                if (x85.value == "1") {
                    var y85 = document.getElementById("CbxVert85").checked = true;
                }
                var x86 = document.getElementById('<%= HdnVert86Ckd.ClientID%>');
                if (x86.value == "1") {
                    var y86 = document.getElementById("CbxVert86").checked = true;
                }
                var x87 = document.getElementById('<%= HdnVert87Ckd.ClientID%>');
                if (x87.value == "1") {
                    var y87 = document.getElementById("CbxVert87").checked = true;
                }
                var x88 = document.getElementById('<%= HdnVert88Ckd.ClientID%>');
                if (x88.value == "1") {
                    var y88 = document.getElementById("CbxVert88").checked = true;
                }
                var x89 = document.getElementById('<%= HdnVert89Ckd.ClientID%>');
                if (x89.value == "1") {
                    var y89 = document.getElementById("CbxVert89").checked = true;
                }
                var x90 = document.getElementById('<%= HdnVert90Ckd.ClientID%>');
                if (x90.value == "1") {
                    var y90 = document.getElementById("CbxVert90").checked = true;
                }
                var x91 = document.getElementById('<%= HdnVert91Ckd.ClientID%>');
                if (x91.value == "1") {
                    var y91 = document.getElementById("CbxVert91").checked = true;
                }
                var x92 = document.getElementById('<%= HdnVert92Ckd.ClientID%>');
                if (x92.value == "1") {
                    var y92 = document.getElementById("CbxVert92").checked = true;
                }
                var x93 = document.getElementById('<%= HdnVert93Ckd.ClientID%>');
                if (x93.value == "1") {
                    var y93 = document.getElementById("CbxVert93").checked = true;
                }
                var x94 = document.getElementById('<%= HdnVert94Ckd.ClientID%>');
                if (x94.value == "1") {
                    var y94 = document.getElementById("CbxVert94").checked = true;
                }
                var x95 = document.getElementById('<%= HdnVert95Ckd.ClientID%>');
                if (x95.value == "1") {
                    var y95 = document.getElementById("CbxVert95").checked = true;
                }
                var x96 = document.getElementById('<%= HdnVert96Ckd.ClientID%>');
                if (x96.value == "1") {
                    var y96 = document.getElementById("CbxVert96").checked = true;
                }
                var x97 = document.getElementById('<%= HdnVert97Ckd.ClientID%>');
                if (x97.value == "1") {
                    var y97 = document.getElementById("CbxVert97").checked = true;
                }
                var x98 = document.getElementById('<%= HdnVert98Ckd.ClientID%>');
                if (x98.value == "1") {
                    var y98 = document.getElementById("CbxVert98").checked = true;
                }
                var x99 = document.getElementById('<%= HdnVert99Ckd.ClientID%>');
                if (x99.value == "1") {
                    var y99 = document.getElementById("CbxVert99").checked = true;
                }
                var x100 = document.getElementById('<%= HdnVert100Ckd.ClientID%>');
                if (x100.value == "1") {
                    var y100 = document.getElementById("CbxVert100").checked = true;
                }
                var x101 = document.getElementById('<%= HdnVert101Ckd.ClientID%>');
                if (x101.value == "1") {
                    var y101 = document.getElementById("CbxVert101").checked = true;
                }
                var x102 = document.getElementById('<%= HdnVert102Ckd.ClientID%>');
                if (x102.value == "1") {
                    var y102 = document.getElementById("CbxVert102").checked = true;
                }
                var x103 = document.getElementById('<%= HdnVert103Ckd.ClientID%>');
                if (x103.value == "1") {
                    var y103 = document.getElementById("CbxVert103").checked = true;
                }
                var x104 = document.getElementById('<%= HdnVert104Ckd.ClientID%>');
                if (x104.value == "1") {
                    var y104 = document.getElementById("CbxVert104").checked = true;
                }
                var x105 = document.getElementById('<%= HdnVert105Ckd.ClientID%>');
                if (x105.value == "1") {
                    var y105 = document.getElementById("CbxVert105").checked = true;
                }
                var x106 = document.getElementById('<%= HdnVert106Ckd.ClientID%>');
                if (x106.value == "1") {
                    var y106 = document.getElementById("CbxVert106").checked = true;
                }
                var x107 = document.getElementById('<%= HdnVert107Ckd.ClientID%>');
                if (x107.value == "1") {
                    var y107 = document.getElementById("CbxVert107").checked = true;
                }
                var x108 = document.getElementById('<%= HdnVert108Ckd.ClientID%>');
                if (x108.value == "1") {
                    var y108 = document.getElementById("CbxVert108").checked = true;
                }
                var x109 = document.getElementById('<%= HdnVert109Ckd.ClientID%>');
                if (x109.value == "1") {
                    var y109 = document.getElementById("CbxVert109").checked = true;
                }
                var x110 = document.getElementById('<%= HdnVert110Ckd.ClientID%>');
                if (x110.value == "1") {
                    var y110 = document.getElementById("CbxVert110").checked = true;
                }
                var x111 = document.getElementById('<%= HdnVert111Ckd.ClientID%>');
                if (x111.value == "1") {
                    var y111 = document.getElementById("CbxVert111").checked = true;
                }
                var x112 = document.getElementById('<%= HdnVert112Ckd.ClientID%>');
                if (x112.value == "1") {
                    var y112 = document.getElementById("CbxVert112").checked = true;
                }
                var x113 = document.getElementById('<%= HdnVert113Ckd.ClientID%>');
                if (x113.value == "1") {
                    var y113 = document.getElementById("CbxVert113").checked = true;
                }
                var x114 = document.getElementById('<%= HdnVert114Ckd.ClientID%>');
                if (x114.value == "1") {
                    var y114 = document.getElementById("CbxVert114").checked = true;
                }
                var x115 = document.getElementById('<%= HdnVert115Ckd.ClientID%>');
                if (x115.value == "1") {
                    var y115 = document.getElementById("CbxVert115").checked = true;
                }
                var x116 = document.getElementById('<%= HdnVert116Ckd.ClientID%>');
                if (x116.value == "1") {
                    var y116 = document.getElementById("CbxVert116").checked = true;
                }
                var x117 = document.getElementById('<%= HdnVert117Ckd.ClientID%>');
                if (x117.value == "1") {
                    var y117 = document.getElementById("CbxVert117").checked = true;
                }
                var x118 = document.getElementById('<%= HdnVert118Ckd.ClientID%>');
                if (x118.value == "1") {
                    var y118 = document.getElementById("CbxVert118").checked = true;
                }
                var x119 = document.getElementById('<%= HdnVert119Ckd.ClientID%>');
                if (x119.value == "1") {
                    var y119 = document.getElementById("CbxVert119").checked = true;
                }
                var x120 = document.getElementById('<%= HdnVert120Ckd.ClientID%>');
                if (x120.value == "1") {
                    var y120 = document.getElementById("CbxVert120").checked = true;
                }
                var x121 = document.getElementById('<%= HdnVert121Ckd.ClientID%>');
                if (x121.value == "1") {
                    var y121 = document.getElementById("CbxVert121").checked = true;
                }
                var x122 = document.getElementById('<%= HdnVert122Ckd.ClientID%>');
                if (x122.value == "1") {
                    var y122 = document.getElementById("CbxVert122").checked = true;
                }
                var x123 = document.getElementById('<%= HdnVert93Ckd.ClientID%>');
                if (x123.value == "1") {
                    var y123 = document.getElementById("CbxVert93").checked = true;
                }
                var x124 = document.getElementById('<%= HdnVert124Ckd.ClientID%>');
                if (x124.value == "1") {
                    var y124 = document.getElementById("CbxVert124").checked = true;
                }
                var x125 = document.getElementById('<%= HdnVert125Ckd.ClientID%>');
            if (x125.value == "1") {
                var y125 = document.getElementById("CbxVert125").checked = true;
            }

        }
    </script>
    <script type="text/javascript">
        function VerticalsFunction() {
            var x1 = document.getElementById("CbxVert1");
            if (x1 == null || !x1.checked) {
                var y1 = document.getElementById('<%= HdnVert1.ClientID%>');
                    y1.value = "0";
                }

                var x2 = document.getElementById("CbxVert2");
                if (x2 == null || !x2.checked) {
                    var y2 = document.getElementById('<%= HdnVert2.ClientID%>');
                    y2.value = "0";
                }

                var x3 = document.getElementById("CbxVert3");
                if (x3 == null || !x3.checked) {
                    var y3 = document.getElementById('<%= HdnVert3.ClientID%>');
                    y3.value = "0";
                }

                var x4 = document.getElementById("CbxVert4");
                if (x4 == null || !x4.checked) {
                    var y4 = document.getElementById('<%= HdnVert4.ClientID%>');
                    y4.value = "0";
                }

                var x5 = document.getElementById("CbxVert5");
                if (x5 == null || !x5.checked) {
                    var y5 = document.getElementById('<%= HdnVert5.ClientID%>');
                    y5.value = "0";
                }

                var x6 = document.getElementById("CbxVert6");
                if (x6 == null || !x6.checked) {
                    var y6 = document.getElementById('<%= HdnVert6.ClientID%>');
                    y6.value = "0";
                }

                var x7 = document.getElementById("CbxVert7");
                if (x7 == null || !x7.checked) {
                    var y7 = document.getElementById('<%= HdnVert7.ClientID%>');
                    y7.value = "0";
                }

                var x8 = document.getElementById("CbxVert8");
                if (x8 == null || !x8.checked) {
                    var y8 = document.getElementById('<%= HdnVert8.ClientID%>');
                    y8.value = "0";
                }

                var x9 = document.getElementById("CbxVert9");
                if (x9 == null || !x9.checked) {
                    var y9 = document.getElementById('<%= HdnVert9.ClientID%>');
                    y9.value = "0";
                }

                var x10 = document.getElementById("CbxVert10");
                if (x10 == null || !x10.checked) {
                    var y10 = document.getElementById('<%= HdnVert10.ClientID%>');
                    y10.value = "0";
                }

                var x11 = document.getElementById("CbxVert11");
                if (x11 == null || !x11.checked) {
                    var y11 = document.getElementById('<%= HdnVert11.ClientID%>');
                    y11.value = "0";
                }

                var x12 = document.getElementById("CbxVert12");
                if (x12 == null || !x12.checked) {
                    var y12 = document.getElementById('<%= HdnVert12.ClientID%>');
                    y12.value = "0";
                }

                var x13 = document.getElementById("CbxVert13");
                if (x13 == null || !x13.checked) {
                    var y13 = document.getElementById('<%= HdnVert13.ClientID%>');
                    y13.value = "0";
                }

                var x14 = document.getElementById("CbxVert14");
                if (x14 == null || !x14.checked) {
                    var y14 = document.getElementById('<%= HdnVert14.ClientID%>');
                    y14.value = "0";
                }

                var x15 = document.getElementById("CbxVert15");
                if (x15 == null || !x15.checked) {
                    var y15 = document.getElementById('<%= HdnVert15.ClientID%>');
                    y15.value = "0";
                }

                var x16 = document.getElementById("CbxVert16");
                if (x16 == null || !x16.checked) {
                    var y16 = document.getElementById('<%= HdnVert16.ClientID%>');
                    y16.value = "0";
                }

                var x17 = document.getElementById("CbxVert17");
                if (x17 == null || !x17.checked) {
                    var y17 = document.getElementById('<%= HdnVert17.ClientID%>');
                    y17.value = "0";
                }

                var x18 = document.getElementById("CbxVert18");
                if (x18 == null || !x18.checked) {
                    var y18 = document.getElementById('<%= HdnVert18.ClientID%>');
                    y18.value = "0";
                }

                var x19 = document.getElementById("CbxVert19");
                if (x19 == null || !x19.checked) {
                    var y19 = document.getElementById('<%= HdnVert19.ClientID%>');
                    y19.value = "0";
                }

                var x20 = document.getElementById("CbxVert20");
                if (x20 == null || !x20.checked) {
                    var y20 = document.getElementById('<%= HdnVert20.ClientID%>');
                    y20.value = "0";
                }

                var x21 = document.getElementById("CbxVert21");
                if (x21 == null || !x21.checked) {
                    var y21 = document.getElementById('<%= HdnVert21.ClientID%>');
                    y21.value = "0";
                }

                var x22 = document.getElementById("CbxVert22");
                if (x22 == null || !x22.checked) {
                    var y22 = document.getElementById('<%= HdnVert22.ClientID%>');
                    y22.value = "0";
                }

                var x23 = document.getElementById("CbxVert23");
                if (x23 == null || !x23.checked) {
                    var y23 = document.getElementById('<%= HdnVert23.ClientID%>');
                    y23.value = "0";
                }

                var x24 = document.getElementById("CbxVert24");
                if (x24 == null || !x24.checked) {
                    var y24 = document.getElementById('<%= HdnVert24.ClientID%>');
                    y24.value = "0";
                }

                var x25 = document.getElementById("CbxVert25");
                if (x25 == null || !x25.checked) {
                    var y25 = document.getElementById('<%= HdnVert25.ClientID%>');
                    y25.value = "0";
                }

                var x26 = document.getElementById("CbxVert26");
                if (x26 == null || !x26.checked) {
                    var y26 = document.getElementById('<%= HdnVert26.ClientID%>');
                    y26.value = "0";
                }

                var x27 = document.getElementById("CbxVert27");
                if (x27 == null || !x27.checked) {
                    var y27 = document.getElementById('<%= HdnVert27.ClientID%>');
                    y27.value = "0";
                }

                var x28 = document.getElementById("CbxVert28");
                if (x28 == null || !x28.checked) {
                    var y28 = document.getElementById('<%= HdnVert28.ClientID%>');
                    y28.value = "0";
                }

                var x29 = document.getElementById("CbxVert29");
                if (x29 == null || !x29.checked) {
                    var y29 = document.getElementById('<%= HdnVert29.ClientID%>');
                    y29.value = "0";
                }

                var x30 = document.getElementById("CbxVert30");
                if (x30 == null || !x30.checked) {
                    var y30 = document.getElementById('<%= HdnVert30.ClientID%>');
                    y30.value = "0";
                }

                var x31 = document.getElementById("CbxVert31");
                if (x31 == null || !x31.checked) {
                    var y31 = document.getElementById('<%= HdnVert31.ClientID%>');
                    y31.value = "0";
                }

                var x32 = document.getElementById("CbxVert32");
                if (x32 == null || !x32.checked) {
                    var y32 = document.getElementById('<%= HdnVert32.ClientID%>');
                    y32.value = "0";
                }

                var x33 = document.getElementById("CbxVert33");
                if (x33 == null || !x33.checked) {
                    var y33 = document.getElementById('<%= HdnVert33.ClientID%>');
                    y33.value = "0";
                }

                var x34 = document.getElementById("CbxVert34");
                if (x34 == null || !x34.checked) {
                    var y34 = document.getElementById('<%= HdnVert34.ClientID%>');
                    y34.value = "0";
                }

                var x35 = document.getElementById("CbxVert35");
                if (x35 == null || !x35.checked) {
                    var y35 = document.getElementById('<%= HdnVert35.ClientID%>');
                    y35.value = "0";
                }

                var x36 = document.getElementById("CbxVert36");
                if (x36 == null || !x36.checked) {
                    var y36 = document.getElementById('<%= HdnVert36.ClientID%>');
                    y36.value = "0";
                }

                var x37 = document.getElementById("CbxVert37");
                if (x37 == null || !x37.checked) {
                    var y37 = document.getElementById('<%= HdnVert37.ClientID%>');
                    y37.value = "0";
                }

                var x38 = document.getElementById("CbxVert38");
                if (x38 == null || !x38.checked) {
                    var y38 = document.getElementById('<%= HdnVert38.ClientID%>');
                    y38.value = "0";
                }

                var x39 = document.getElementById("CbxVert39");
                if (x39 == null || !x39.checked) {
                    var y39 = document.getElementById('<%= HdnVert39.ClientID%>');
                    y39.value = "0";
                }

                var x40 = document.getElementById("CbxVert40");
                if (x40 == null || !x40.checked) {
                    var y40 = document.getElementById('<%= HdnVert40.ClientID%>');
                    y40.value = "0";
                }

                var x41 = document.getElementById("CbxVert41");
                if (x41 == null || !x41.checked) {
                    var y41 = document.getElementById('<%= HdnVert41.ClientID%>');
                    y41.value = "0";
                }

                var x42 = document.getElementById("CbxVert42");
                if (x42 == null || !x42.checked) {
                    var y42 = document.getElementById('<%= HdnVert42.ClientID%>');
                    y42.value = "0";
                }

                var x43 = document.getElementById("CbxVert43");
                if (x43 == null || !x43.checked) {
                    var y43 = document.getElementById('<%= HdnVert43.ClientID%>');
                    y43.value = "0";
                }

                var x44 = document.getElementById("CbxVert44");
                if (x44 == null || !x44.checked) {
                    var y44 = document.getElementById('<%= HdnVert44.ClientID%>');
                    y44.value = "0";
                }

                var x45 = document.getElementById("CbxVert45");
                if (x45 == null || !x45.checked) {
                    var y45 = document.getElementById('<%= HdnVert45.ClientID%>');
                    y45.value = "0";
                }

                var x46 = document.getElementById("CbxVert46");
                if (x46 == null || !x46.checked) {
                    var y46 = document.getElementById('<%= HdnVert46.ClientID%>');
                    y46.value = "0";
                }

                var x47 = document.getElementById("CbxVert47");
                if (x47 == null || !x47.checked) {
                    var y47 = document.getElementById('<%= HdnVert47.ClientID%>');
                    y47.value = "0";
                }

                var x48 = document.getElementById("CbxVert48");
                if (x48 == null || !x48.checked) {
                    var y48 = document.getElementById('<%= HdnVert48.ClientID%>');
                    y48.value = "0";
                }

                var x49 = document.getElementById("CbxVert49");
                if (x49 == null || !x49.checked) {
                    var y49 = document.getElementById('<%= HdnVert49.ClientID%>');
                    y49.value = "0";
                }

                var x50 = document.getElementById("CbxVert50");
                if (x50 == null || !x50.checked) {
                    var y50 = document.getElementById('<%= HdnVert50.ClientID%>');
                    y50.value = "0";
                }

                var x51 = document.getElementById("CbxVert51");
                if (x51 == null || !x51.checked) {
                    var y51 = document.getElementById('<%= HdnVert51.ClientID%>');
                    y51.value = "0";
                }

                var x52 = document.getElementById("CbxVert52");
                if (x52 == null || !x52.checked) {
                    var y52 = document.getElementById('<%= HdnVert52.ClientID%>');
                    y52.value = "0";
                }

                var x53 = document.getElementById("CbxVert53");
                if (x53 == null || !x53.checked) {
                    var y53 = document.getElementById('<%= HdnVert53.ClientID%>');
                    y53.value = "0";
                }

                var x54 = document.getElementById("CbxVert54");
                if (x54 == null || !x54.checked) {
                    var y54 = document.getElementById('<%= HdnVert54.ClientID%>');
                    y54.value = "0";
                }

                var x55 = document.getElementById("CbxVert55");
                if (x55 == null || !x55.checked) {
                    var y55 = document.getElementById('<%= HdnVert55.ClientID%>');
                    y55.value = "0";
                }

                var x56 = document.getElementById("CbxVert56");
                if (x56 == null || !x56.checked) {
                    var y56 = document.getElementById('<%= HdnVert56.ClientID%>');
                    y56.value = "0";
                }

                var x57 = document.getElementById("CbxVert57");
                if (x57 == null || !x57.checked) {
                    var y57 = document.getElementById('<%= HdnVert57.ClientID%>');
                    y57.value = "0";
                }

                var x58 = document.getElementById("CbxVert58");
                if (x58 == null || !x58.checked) {
                    var y58 = document.getElementById('<%= HdnVert58.ClientID%>');
                    y58.value = "0";
                }

                var x59 = document.getElementById("CbxVert59");
                if (x59 == null || !x59.checked) {
                    var y59 = document.getElementById('<%= HdnVert59.ClientID%>');
                    y59.value = "0";
                }

                var x60 = document.getElementById("CbxVert60");
                if (x60 == null || !x60.checked) {
                    var y60 = document.getElementById('<%= HdnVert60.ClientID%>');
                    y60.value = "0";
                }

                var x61 = document.getElementById("CbxVert61");
                if (x61 == null || !x61.checked) {
                    var y61 = document.getElementById('<%= HdnVert61.ClientID%>');
                    y61.value = "0";
                }

                var x62 = document.getElementById("CbxVert62");
                if (x62 == null || !x62.checked) {
                    var y62 = document.getElementById('<%= HdnVert62.ClientID%>');
                    y62.value = "0";
                }

                var x63 = document.getElementById("CbxVert63");
                if (x63 == null || !x63.checked) {
                    var y63 = document.getElementById('<%= HdnVert63.ClientID%>');
                    y63.value = "0";
                }

                var x64 = document.getElementById("CbxVert64");
                if (x64 == null || !x64.checked) {
                    var y64 = document.getElementById('<%= HdnVert64.ClientID%>');
                    y64.value = "0";
                }

                var x65 = document.getElementById("CbxVert65");
                if (x65 == null || !x65.checked) {
                    var y65 = document.getElementById('<%= HdnVert65.ClientID%>');
                    y65.value = "0";
                }

                var x66 = document.getElementById("CbxVert66");
                if (x66 == null || !x66.checked) {
                    var y66 = document.getElementById('<%= HdnVert66.ClientID%>');
                    y66.value = "0";
                }

                var x67 = document.getElementById("CbxVert67");
                if (x67 == null || !x67.checked) {
                    var y67 = document.getElementById('<%= HdnVert67.ClientID%>');
                    y67.value = "0";
                }

                var x68 = document.getElementById("CbxVert68");
                if (x68 == null || !x68.checked) {
                    var y68 = document.getElementById('<%= HdnVert68.ClientID%>');
                    y68.value = "0";
                }

                var x69 = document.getElementById("CbxVert69");
                if (x69 == null || !x69.checked) {
                    var y69 = document.getElementById('<%= HdnVert69.ClientID%>');
                    y69.value = "0";
                }

                var x70 = document.getElementById("CbxVert70");
                if (x70 == null || !x70.checked) {
                    var y70 = document.getElementById('<%= HdnVert70.ClientID%>');
                    y70.value = "0";
                }

                var x71 = document.getElementById("CbxVert71");
                if (x71 == null || !x71.checked) {
                    var y71 = document.getElementById('<%= HdnVert71.ClientID%>');
                    y71.value = "0";
                }

                var x72 = document.getElementById("CbxVert72");
                if (x72 == null || !x72.checked) {
                    var y72 = document.getElementById('<%= HdnVert72.ClientID%>');
                    y72.value = "0";
                }

                var x73 = document.getElementById("CbxVert73");
                if (x73 == null || !x73.checked) {
                    var y73 = document.getElementById('<%= HdnVert73.ClientID%>');
                    y73.value = "0";
                }

                var x74 = document.getElementById("CbxVert74");
                if (x74 == null || !x74.checked) {
                    var y74 = document.getElementById('<%= HdnVert74.ClientID%>');
                    y74.value = "0";
                }

                var x75 = document.getElementById("CbxVert75");
                if (x75 == null || !x75.checked) {
                    var y75 = document.getElementById('<%= HdnVert75.ClientID%>');
                    y75.value = "0";
                }

                var x76 = document.getElementById("CbxVert76");
                if (x76 == null || !x76.checked) {
                    var y76 = document.getElementById('<%= HdnVert76.ClientID%>');
                    y76.value = "0";
                }

                var x77 = document.getElementById("CbxVert77");
                if (x77 == null || !x77.checked) {
                    var y77 = document.getElementById('<%= HdnVert77.ClientID%>');
                    y77.value = "0";
                }

                var x78 = document.getElementById("CbxVert78");
                if (x78 == null || !x78.checked) {
                    var y78 = document.getElementById('<%= HdnVert78.ClientID%>');
                    y78.value = "0";
                }

                var x79 = document.getElementById("CbxVert79");
                if (x79 == null || !x79.checked) {
                    var y79 = document.getElementById('<%= HdnVert79.ClientID%>');
                    y79.value = "0";
                }

                var x80 = document.getElementById("CbxVert80");
                if (x80 == null || !x80.checked) {
                    var y80 = document.getElementById('<%= HdnVert80.ClientID%>');
                    y80.value = "0";
                }

                var x81 = document.getElementById("CbxVert81");
                if (x81 == null || !x81.checked) {
                    var y81 = document.getElementById('<%= HdnVert81.ClientID%>');
                    y81.value = "0";
                }

                var x82 = document.getElementById("CbxVert82");
                if (x82 == null || !x82.checked) {
                    var y82 = document.getElementById('<%= HdnVert82.ClientID%>');
                    y82.value = "0";
                }

                var x83 = document.getElementById("CbxVert83");
                if (x83 == null || !x83.checked) {
                    var y83 = document.getElementById('<%= HdnVert83.ClientID%>');
                    y83.value = "0";
                }

                var x84 = document.getElementById("CbxVert84");
                if (x84 == null || !x84.checked) {
                    var y84 = document.getElementById('<%= HdnVert84.ClientID%>');
                    y84.value = "0";
                }

                var x85 = document.getElementById("CbxVert85");
                if (x85 == null || !x85.checked) {
                    var y85 = document.getElementById('<%= HdnVert85.ClientID%>');
                    y85.value = "0";
                }

                var x86 = document.getElementById("CbxVert86");
                if (x86 == null || !x86.checked) {
                    var y86 = document.getElementById('<%= HdnVert86.ClientID%>');
                    y86.value = "0";
                }

                var x87 = document.getElementById("CbxVert87");
                if (x87 == null || !x87.checked) {
                    var y87 = document.getElementById('<%= HdnVert87.ClientID%>');
                    y87.value = "0";
                }

                var x88 = document.getElementById("CbxVert88");
                if (x88 == null || !x88.checked) {
                    var y88 = document.getElementById('<%= HdnVert88.ClientID%>');
                    y88.value = "0";
                }

                var x89 = document.getElementById("CbxVert89");
                if (x89 == null || !x89.checked) {
                    var y89 = document.getElementById('<%= HdnVert89.ClientID%>');
                    y89.value = "0";
                }

                var x90 = document.getElementById("CbxVert90");
                if (x90 == null || !x90.checked) {
                    var y90 = document.getElementById('<%= HdnVert90.ClientID%>');
                    y90.value = "0";
                }

                var x91 = document.getElementById("CbxVert91");
                if (x91 == null || !x91.checked) {
                    var y91 = document.getElementById('<%= HdnVert91.ClientID%>');
                    y91.value = "0";
                }

                var x92 = document.getElementById("CbxVert92");
                if (x92 == null || !x92.checked) {
                    var y92 = document.getElementById('<%= HdnVert92.ClientID%>');
                    y92.value = "0";
                }

                var x93 = document.getElementById("CbxVert93");
                if (x93 == null || !x93.checked) {
                    var y93 = document.getElementById('<%= HdnVert93.ClientID%>');
                    y93.value = "0";
                }

                var x94 = document.getElementById("CbxVert94");
                if (x94 == null || !x94.checked) {
                    var y94 = document.getElementById('<%= HdnVert94.ClientID%>');
                    y94.value = "0";
                }

                var x95 = document.getElementById("CbxVert95");
                if (x95 == null || !x95.checked) {
                    var y95 = document.getElementById('<%= HdnVert95.ClientID%>');
                    y95.value = "0";
                }

                var x96 = document.getElementById("CbxVert96");
                if (x96 == null || !x96.checked) {
                    var y96 = document.getElementById('<%= HdnVert96.ClientID%>');
                    y96.value = "0";
                }

                var x97 = document.getElementById("CbxVert97");
                if (x97 == null || !x97.checked) {
                    var y97 = document.getElementById('<%= HdnVert97.ClientID%>');
                    y97.value = "0";
                }

                var x98 = document.getElementById("CbxVert98");
                if (x98 == null || !x98.checked) {
                    var y98 = document.getElementById('<%= HdnVert98.ClientID%>');
                    y98.value = "0";
                }

                var x99 = document.getElementById("CbxVert99");
                if (x99 == null || !x99.checked) {
                    var y99 = document.getElementById('<%= HdnVert99.ClientID%>');
                    y99.value = "0";
                }

                var x100 = document.getElementById("CbxVert100");
                if (x100 == null || !x100.checked) {
                    var y100 = document.getElementById('<%= HdnVert100.ClientID%>');
                    y100.value = "0";
                }

                var x101 = document.getElementById("CbxVert101");
                if (x101 == null || !x101.checked) {
                    var y101 = document.getElementById('<%= HdnVert101.ClientID%>');
                    y101.value = "0";
                }

                var x102 = document.getElementById("CbxVert102");
                if (x102 == null || !x102.checked) {
                    var y102 = document.getElementById('<%= HdnVert102.ClientID%>');
                    y102.value = "0";
                }

                var x103 = document.getElementById("CbxVert103");
                if (x103 == null || !x103.checked) {
                    var y103 = document.getElementById('<%= HdnVert103.ClientID%>');
                    y103.value = "0";
                }

                var x104 = document.getElementById("CbxVert104");
                if (x104 == null || !x104.checked) {
                    var y104 = document.getElementById('<%= HdnVert104.ClientID%>');
                    y104.value = "0";
                }

                var x105 = document.getElementById("CbxVert105");
                if (x105 == null || !x105.checked) {
                    var y105 = document.getElementById('<%= HdnVert105.ClientID%>');
                    y105.value = "0";
                }

                var x106 = document.getElementById("CbxVert106");
                if (x106 == null || !x106.checked) {
                    var y106 = document.getElementById('<%= HdnVert106.ClientID%>');
                    y106.value = "0";
                }

                var x107 = document.getElementById("CbxVert107");
                if (x107 == null || !x107.checked) {
                    var y107 = document.getElementById('<%= HdnVert107.ClientID%>');
                    y107.value = "0";
                }

                var x108 = document.getElementById("CbxVert108");
                if (x108 == null || !x108.checked) {
                    var y108 = document.getElementById('<%= HdnVert108.ClientID%>');
                    y108.value = "0";
                }

                var x109 = document.getElementById("CbxVert109");
                if (x109 == null || !x109.checked) {
                    var y109 = document.getElementById('<%= HdnVert109.ClientID%>');
                    y109.value = "0";
                }

                var x110 = document.getElementById("CbxVert110");
                if (x110 == null || !x110.checked) {
                    var y110 = document.getElementById('<%= HdnVert110.ClientID%>');
                    y110.value = "0";
                }

                var x111 = document.getElementById("CbxVert111");
                if (x111 == null || !x111.checked) {
                    var y111 = document.getElementById('<%= HdnVert111.ClientID%>');
                    y111.value = "0";
                }

                var x112 = document.getElementById("CbxVert112");
                if (x112 == null || !x112.checked) {
                    var y112 = document.getElementById('<%= HdnVert112.ClientID%>');
                    y112.value = "0";
                }

                var x113 = document.getElementById("CbxVert113");
                if (x113 == null || !x113.checked) {
                    var y113 = document.getElementById('<%= HdnVert113.ClientID%>');
                    y113.value = "0";
                }

                var x114 = document.getElementById("CbxVert114");
                if (x114 == null || !x114.checked) {
                    var y114 = document.getElementById('<%= HdnVert114.ClientID%>');
                    y114.value = "0";
                }

                var x115 = document.getElementById("CbxVert115");
                if (x115 == null || !x115.checked) {
                    var y115 = document.getElementById('<%= HdnVert115.ClientID%>');
                    y115.value = "0";
                }

                var x116 = document.getElementById("CbxVert116");
                if (x116 == null || !x116.checked) {
                    var y116 = document.getElementById('<%= HdnVert116.ClientID%>');
                    y116.value = "0";
                }

                var x117 = document.getElementById("CbxVert117");
                if (x117 == null || !x117.checked) {
                    var y117 = document.getElementById('<%= HdnVert117.ClientID%>');
                    y117.value = "0";
                }

                var x118 = document.getElementById("CbxVert118");
                if (x118 == null || !x118.checked) {
                    var y118 = document.getElementById('<%= HdnVert118.ClientID%>');
                    y118.value = "0";
                }

                var x119 = document.getElementById("CbxVert119");
                if (x119 == null || !x119.checked) {
                    var y119 = document.getElementById('<%= HdnVert119.ClientID%>');
                    y119.value = "0";
                }

                var x120 = document.getElementById("CbxVert120");
                if (x120 == null || !x120.checked) {
                    var y120 = document.getElementById('<%= HdnVert120.ClientID%>');
                    y120.value = "0";
                }

                var x121 = document.getElementById("CbxVert121");
                if (x121 == null || !x121.checked) {
                    var y121 = document.getElementById('<%= HdnVert121.ClientID%>');
                    y121.value = "0";
                }

                var x122 = document.getElementById("CbxVert122");
                if (x122 == null || !x122.checked) {
                    var y122 = document.getElementById('<%= HdnVert122.ClientID%>');
                    y122.value = "0";
                }

                var x123 = document.getElementById("CbxVert123");
                if (x123 == null || !x123.checked) {
                    var y123 = document.getElementById('<%= HdnVert123.ClientID%>');
                    y123.value = "0";
                }

                var x124 = document.getElementById("CbxVert124");
                if (x124 == null || !x124.checked) {
                    var y124 = document.getElementById('<%= HdnVert124.ClientID%>');
                    y124.value = "0";
                }

                var x125 = document.getElementById("CbxVert125");
                if (x125 == null || !x125.checked) {
                    var y125 = document.getElementById('<%= HdnVert125.ClientID%>');
                y125.value = "0";
            }
        }
    </script>
    <script type="text/javascript">
        function SupportFunction() {
            var x1 = document.getElementById("CbxSuppIndifferent");
            if (x1 == null || !x1.checked) {
                var y1 = document.getElementById('<%= HdnSuppIndif.ClientID%>');
                    y1.value = "0";
                }

                var x2 = document.getElementById("CbxSuppDedicated");
                if (x2 == null || !x2.checked) {
                    var y2 = document.getElementById('<%= HdnSuppDedic.ClientID%>');
                    y2.value = "0";
                }

                var x3 = document.getElementById("CbxSuppPhone");
                if (x3 == null || !x3.checked) {
                    var y3 = document.getElementById('<%= HdnSuppPhone.ClientID%>');
                    y3.value = "0";
                }

                var x4 = document.getElementById("CbxSuppMail");
                if (x4 == null || !x4.checked) {
                    var y4 = document.getElementById('<%= HdnSuppMail.ClientID%>');
                y4.value = "0";
            }
        }
    </script>
    <script type="text/javascript">
        function CountriesFunction() {
            var x1 = document.getElementById("CbxCountriesSelAll");
            if (x1 == null || !x1.checked) {
                var y1 = document.getElementById('<%= HdnCountriesSelAll.ClientID%>');
                    y1.value = "0";
                }

                var x2 = document.getElementById("CbxCountriesAsiaPacif");
                if (x2 == null || !x2.checked) {
                    var y2 = document.getElementById('<%= HdnCountriesAsiaPacif.ClientID%>');
                    y2.value = "0";
                }

                var x3 = document.getElementById("CbxCountriesAfrica");
                if (x3 == null || !x3.checked) {
                    var y3 = document.getElementById('<%= HdnCountriesAfrica.ClientID%>');
                    y3.value = "0";
                }

                var x4 = document.getElementById("CbxCountriesEurope");
                if (x4 == null || !x4.checked) {
                    var y4 = document.getElementById('<%= HdnCountriesEurope.ClientID%>');
                    y4.value = "0";
                }

                var x5 = document.getElementById("CbxCountriesMidEast");
                if (x5 == null || !x5.checked) {
                    var y5 = document.getElementById('<%= HdnCountriesMidEast.ClientID%>');
                    y5.value = "0";
                }

                var x6 = document.getElementById("CbxCountriesNortAmer");
                if (x6 == null || !x6.checked) {
                    var y6 = document.getElementById('<%= HdnCountriesNortAmer.ClientID%>');
                    y6.value = "0";
                }

                var x7 = document.getElementById("CbxCountriesSoutAmer");
                if (x7 == null || !x7.checked) {
                    var y7 = document.getElementById('<%= HdnCountriesSoutAmer.ClientID%>');
                    y7.value = "0";
                }

                var x8 = document.getElementById("CbxCountriesArgen");
                if (x8 == null || !x8.checked) {
                    var y8 = document.getElementById('<%= HdnCountriesArgen.ClientID%>');
                    y8.value = "0";
                }

                var x9 = document.getElementById("CbxCountriesAustr");
                if (x9 == null || !x9.checked) {
                    var y9 = document.getElementById('<%= HdnCountriesAustr.ClientID%>');
                    y9.value = "0";
                }

                var x10 = document.getElementById("CbxCountriesBraz");
                if (x10 == null || !x10.checked) {
                    var y10 = document.getElementById('<%= HdnCountriesBraz.ClientID%>');
                    y10.value = "0";
                }

                var x11 = document.getElementById("CbxCountriesCanada");
                if (x11 == null || !x11.checked) {
                    var y11 = document.getElementById('<%= HdnCountriesCanada.ClientID%>');
                    y11.value = "0";
                }

                var x12 = document.getElementById("CbxCountriesFrance");
                if (x12 == null || !x12.checked) {
                    var y12 = document.getElementById('<%= HdnCountriesFrance.ClientID%>');
                    y12.value = "0";
                }

                var x13 = document.getElementById("CbxCountriesGermany");
                if (x13 == null || !x13.checked) {
                    var y13 = document.getElementById('<%= HdnCountriesGermany.ClientID%>');
                    y13.value = "0";
                }

                var x14 = document.getElementById("CbxCountriesIndia");
                if (x14 == null || !x14.checked) {
                    var y14 = document.getElementById('<%= HdnCountriesIndia.ClientID%>');
                    y14.value = "0";
                }

                var x15 = document.getElementById("CbxCountriesMexico");
                if (x15 == null || !x15.checked) {
                    var y15 = document.getElementById('<%= HdnCountriesMexico.ClientID%>');
                    y15.value = "0";
                }

                var x16 = document.getElementById("CbxCountriesPakistan");
                if (x16 == null || !x16.checked) {
                    var y16 = document.getElementById('<%= HdnCountriesPakistan.ClientID%>');
                    y16.value = "0";
                }

                var x17 = document.getElementById("CbxCountriesSpain");
                if (x17 == null || !x17.checked) {
                    var y17 = document.getElementById('<%= HdnCountriesSpain.ClientID%>');
                    y17.value = "0";
                }

                var x18 = document.getElementById("CbxCountriesUnKing");
                if (x18 == null || !x18.checked) {
                    var y18 = document.getElementById('<%= HdnCountriesUnKing.ClientID%>');
                    y18.value = "0";
                }

                var x19 = document.getElementById("CbxCountriesUnStat");
                if (x19 == null || !x19.checked) {
                    var y19 = document.getElementById('<%= HdnCountriesUnStat.ClientID%>');
                y19.value = "0";
            }
        }
    </script>
    <script type="text/javascript">
        function SubmitFunction() {
            VerticalsFunction();
            SupportFunction();
            CountriesFunction();

            var x = document.getElementById('<%= aSubmit.ClientID%>');

            x.click();
        }
    </script>
    <!--[if lt IE 9]>
        <script src="/assets/global/plugins/respond.min.js"></script>
        <script src="/assets/global/plugins/excanvas.min.js"></script> 
        <![endif]-->
    <!-- BEGIN CORE PLUGINS -->
    <script src="/assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="/assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="/assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
    <script src="/assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js" type="text/javascript"></script>
    <script src="/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="/assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="/assets/global/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>
    <script src="/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
    <!-- END CORE PLUGINS -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <script src="/assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
    <script src="/assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
    <script src="/assets/global/plugins/jquery-validation/js/additional-methods.min.js" type="text/javascript"></script>
    <script src="/assets/global/plugins/bootstrap-wizard/jquery.bootstrap.wizard.min.js" type="text/javascript"></script>
    <script src="/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
    <%--<script src="/assets/pages/scripts/components-bootstrap-switch.min.js" type="text/javascript"></script>--%>
    <script src="/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.js" type="text/javascript"></script>
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN THEME GLOBAL SCRIPTS -->
    <script src="/assets/global/scripts/app.min.js" type="text/javascript"></script>
    <!-- END THEME GLOBAL SCRIPTS -->
    <!-- BEGIN PAGE LEVEL SCRIPTS -->
    <script src="/assets/pages/scripts/form-wizard-criteria.js" type="text/javascript"></script>
    <!-- END PAGE LEVEL SCRIPTS -->
    <!-- BEGIN THEME LAYOUT SCRIPTS -->
    <script src="/assets/layouts/layout/scripts/layout.min.js" type="text/javascript"></script>
    <script src="/assets/layouts/layout/scripts/demo.min.js" type="text/javascript"></script>
    <script src="/assets/layouts/global/scripts/quick-sidebar.min.js" type="text/javascript"></script>
    <%--<script src="/assets/pages/scripts/form-icheck.min.js" type="text/javascript"></script>
        <script src="/assets/global/plugins/icheck/icheck.min.js" type="text/javascript"></script>--%>
    <!-- END THEME LAYOUT SCRIPTS -->
</body>
</html>
