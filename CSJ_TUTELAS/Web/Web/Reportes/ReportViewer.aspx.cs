using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Web.Configuration;
using Web.Clases;


namespace Web.Reportes
{
    public partial class ListaUniversidades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ShowReport(Request.QueryString["reporte"]);//, Request.QueryString["indicador"]);
            }
        }


        private void ShowReport(string reporte)//, string indicador)
        {
            try
            {
                string urlReportServer = WebConfigurationManager.AppSettings["urlReportServer"];
                rptViewer.ServerReport.ReportServerUrl = new Uri(urlReportServer);
                rptViewer.ProcessingMode = ProcessingMode.Remote;

                // setting report path  
                //Passing the Report Path with report name no need to add report extension   
                rptViewer.ServerReport.ReportPath = WebConfigurationManager.AppSettings["ReportFolder"] + "/" + reporte;

                // pass credential as if any... no need to pass anything if we use windows authentication  
                //rptViewer.ServerReport.ReportServerCredentials = new CustomReportCredentials("BISA", "112233", "LAPTOPBISA");
                /*rptViewer.ServerReport.ReportServerCredentials = new CustomReportCredentials(WebConfigurationManager.AppSettings["RSUser"]
                                                                                            , WebConfigurationManager.AppSettings["RSPassword"]
                                                                                            , WebConfigurationManager.AppSettings["RSDomain"]);*/

                rptViewer.ServerReport.ReportServerCredentials = new ReportViewerCredentials(WebConfigurationManager.AppSettings["RSUser"],
                                                                                             WebConfigurationManager.AppSettings["RSPassword"],
                                                                                             WebConfigurationManager.AppSettings["RSDomain"]);


                //Set report Parameter  
                //if ((reporte == "ind_Base_Distrito") || (reporte == "ind_Base_Localidad"))
                //{

                //    List<ReportParameter> parameters = new List<ReportParameter>();
                //    parameters.Add(new ReportParameter("fecIni", "2015-01-01"));
                //    parameters.Add(new ReportParameter("fecfin", "2016-12-31"));
                //    parameters.Add(new ReportParameter("Loc", "0"));
                //    parameters.Add(new ReportParameter("Indicador", indicador));
                //    rptViewer.ServerReport.SetParameters(parameters);

                //}

                rptViewer.ServerReport.Refresh();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}