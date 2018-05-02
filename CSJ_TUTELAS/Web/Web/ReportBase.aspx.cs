using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Data.SqlClient;
using Datos;

namespace Web
{
    public partial class ReportBase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                DAL dal = new DAL();
                DataTable objRes = new DataTable();
                string query =
                    "SELECT U.IDUNIVERSIDAD, U.UNABREVIATURA, " +
                    "       U.UNNOMBRE, C.CIUDAD, " +
                    "       P.PAIS, U.UNDIRECCION, " +
                    "       U.UNACTIVO, U.UNOBSERVACION, " +
                    "       U.UNCODICFES, PG.PARAMETRO AS UNORIGEN, " +
                    "       PG1.PARAMETRO AS UNTIPO " +
                    "FROM   dwh.DIMUNIVERSIDAD AS U LEFT OUTER JOIN " +
                    "       dwh.DIMCIUDAD AS C ON U.IDCIUDAD = C.IDCIUDAD LEFT OUTER JOIN " +
                    "       dwh.DIMPAIS AS P ON C.IDPAIS = P.IDPAIS LEFT OUTER JOIN " +
                    "       dwh.DIMPARAMETROSGENERALES AS PG ON U.IDUNORIGEN = PG.IDPARAMETRO LEFT OUTER JOIN " +
                    "       dwh.DIMPARAMETROSGENERALES AS PG1 ON U.IDUNTIPO = PG1.IDPARAMETRO ";

                objRes = dal.GetRListaUniversidades(query);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Lista_Universidades", objRes));
                ReportViewer1.LocalReport.ReportEmbeddedResource = "ListaUniversidades.rdl";
                ReportViewer1.LocalReport.ReportPath = "Reportes/ListaUniversidades.rdl";
                ReportViewer1.LocalReport.Refresh();


                //        ReportViewer1.ProcessingMode = ProcessingMode.Local;
                //        LocalReport localReport = ReportViewer1.LocalReport;
                //        localReport.ReportPath = AppDomain.CurrentDomain.BaseDirectory + "Datos\\ReportesCOPNIA\\ListaUniversidades.rdl";
                //        DataSet dataset = new DataSet("DsReport");

                //        GetData(ref dataset);

                //        ReportDataSource dsReport = new ReportDataSource();
                //        dsReport.Name = "DsReport";
                //        dsReport.Value = dataset.Tables["Table"];

                //        localReport.DataSources.Add(dsReport);

                //        //GetSalesOrderDetailData(salesOrderNumber, ref dataset);

                //        //ReportDataSource dsSalesOrderDetail = new ReportDataSource();
                //        //dsSalesOrderDetail.Name = "SalesOrderDetail";
                //        //dsSalesOrderDetail.Value = dataset.Tables["SalesOrderDetail"];

                //        //localReport.DataSources.Add(dsSalesOrderDetail);

                //        // Create the sales order number report parameter  
                //        //ReportParameter rpSalesOrderNumber = new ReportParameter();
                //        //rpSalesOrderNumber.Name = "SalesOrderNumber";
                //        //rpSalesOrderNumber.Values.Add("SO43661");

                //        // Set the report parameters for the report  
                //        //localReport.SetParameters(
                //        //    new ReportParameter[] { rpSalesOrderNumber });

                //    }
            }

            //private void GetData(ref DataSet dsSalesOrder)
            //{
            //    string sqlSalesOrder =
            //        "SELECT U.IDUNIVERSIDAD, U.UNABREVIATURA, " +
            //        "       U.UNNOMBRE, C.CIUDAD, " +
            //        "       P.PAIS, U.UNDIRECCION, " +
            //        "       U.UNACTIVO, U.UNOBSERVACION, " +
            //        "       U.UNCODICFES, PG.PARAMETRO AS UNORIGEN, " +
            //        "       PG1.PARAMETRO AS UNTIPO " +
            //        "FROM   dwh.DIMUNIVERSIDAD AS U LEFT OUTER JOIN " +
            //        "       dwh.DIMCIUDAD AS C ON U.IDCIUDAD = C.IDCIUDAD LEFT OUTER JOIN " +
            //        "       dwh.DIMPAIS AS P ON C.IDPAIS = P.IDPAIS LEFT OUTER JOIN " +
            //        "       dwh.DIMPARAMETROSGENERALES AS PG ON U.IDUNORIGEN = PG.IDPARAMETRO LEFT OUTER JOIN " +
            //        "       dwh.DIMPARAMETROSGENERALES AS PG1 ON U.IDUNTIPO = PG1.IDPARAMETRO ";

            //    SqlConnection connection = new
            //        SqlConnection("Data Source=(192.168.100.130\\SQLCOPNIA); " +
            //                      "Initial Catalog=DWHCOPNIA; " +
            //                      "Persist Security Info = True; " +
            //                      "User ID = bisa; " +
            //                      "Password = bisa$$b2015");


            //    SqlCommand command =
            //        new SqlCommand(sqlSalesOrder, connection);

            //    SqlDataAdapter salesOrderAdapter = new
            //        SqlDataAdapter(command);

            //    salesOrderAdapter.Fill(dsSalesOrder, "Table");
            //}
        }
    }
}