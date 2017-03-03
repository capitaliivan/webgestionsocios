using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace WebCABDN
{
    public partial class Default : System.Web.UI.Page
    {
        private String ConnectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            ((LinkButton)(Master.FindControl("lbSocios"))).Visible = false;
            ((LinkButton)(Master.FindControl("lbCobro"))).Visible = false;
            ((LinkButton)(Master.FindControl("lbFamilia"))).Visible = false;
        }

        protected void bLogin_Click(object sender, EventArgs e)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            Connection.Open();

            SqlDataAdapter Adapter = new SqlDataAdapter("Select * from Usuario where Nombre = '" + tbUser.Text + "' and Password = '" + tbPass.Text + "'", Connection);
            DataTable dtLogin = new DataTable();

            Adapter.Fill(dtLogin);

            if (dtLogin.Rows.Count > 0)
            {
                ((LinkButton)(Master.FindControl("lbSocios"))).Visible = true;
                ((LinkButton)(Master.FindControl("lbCobro"))).Visible = true;
                ((LinkButton)(Master.FindControl("lbFamilia"))).Visible = true;

                Response.Redirect("/Socio/Socio.aspx");
            }
        }
    }
}