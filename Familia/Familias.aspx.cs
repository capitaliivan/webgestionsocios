using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebCABDN.Familia
{
    public partial class Familias : System.Web.UI.Page
    {

        SqlConnection Connection;
        int Indice = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string ConnectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                    Connection = new SqlConnection(ConnectionString);
                    Connection.Open();

                    CargarGridFamilias();

                    Connection.Close();
                }
                catch (Exception ex)
                { lblError.Text = ex.Message; }
            }
        }

        private void CargarGridFamilias()
        {
            SqlDataAdapter Adapter = new SqlDataAdapter("SELECT [id],[NombrePPal],[Apellido1PPal],[Apellido2PPal]," + 
            "[Descripcion],[Direccion]" +
            " FROM familia", Connection);
            DataTable dtSocios = new DataTable();

            Adapter.Fill(dtSocios);

            gvFamilias.DataSource = dtSocios;
            gvFamilias.DataBind();
        }

        protected void ibNewFamilia_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("NuevaFamilia.aspx");
        }

        protected void gvFamilias_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvFamilias.EditIndex = -1;

            try
            {
                string ConnectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                Connection = new SqlConnection(ConnectionString);
                Connection.Open();

                CargarGridFamilias();

                Connection.Close();
            }
            catch (Exception ex)
            { lblError.Text = ex.Message; }
        }

        protected void gvFamilias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Indice = Convert.ToInt32(e.CommandArgument);
        }

        protected void gvFamilias_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                lblError.Text = "";

                int id = int.Parse(gvFamilias.Rows[Indice].Cells[1].Text);

                string ConnectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                Connection = new SqlConnection(ConnectionString);

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from familia where " +
                "id=@id;" +

                "SELECT [id],[NombrePPal],[Apellido1PPal],[Apellido2PPal]," +
                "[Descripcion],[Direccion]" +
                " FROM familia";

                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                DataTable dtFamilias = new DataTable();
                SqlDataAdapter Adapter = new SqlDataAdapter();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = Connection;
                Connection.Open();

                Adapter.SelectCommand = cmd;
                Adapter.Fill(dtFamilias);

                gvFamilias.DataSource = dtFamilias;
                gvFamilias.DataBind();

                Connection.Close();
            }
            catch (Exception ex)
            { lblError.Text = ex.Message; }
        }

        protected void gvFamilias_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvFamilias.EditIndex = e.NewEditIndex;

            try
            {
                string ConnectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                Connection = new SqlConnection(ConnectionString);
                Connection.Open();

                CargarGridFamilias();

                Connection.Close();
            }
            catch (Exception ex)
            { lblError.Text = ex.Message; }
        }

        protected void gvFamilias_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                lblError.Text = "";

                FamiliaUpdateComprovacionErres(e);
                FamiliaUpdate(e);
            }
            catch (Exception ex)
            { lblError.Text = ex.Message; }
            finally
            {
                gvFamilias.EditIndex = -1;

                try
                {
                    string ConnectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                    Connection = new SqlConnection(ConnectionString);
                    Connection.Open();

                    CargarGridFamilias();

                    Connection.Close();
                }
                catch (Exception ex)
                { lblError.Text = ex.Message; }
            }
        }

        private void FamiliaUpdate(GridViewUpdateEventArgs e)
        {
            int id = int.Parse(e.NewValues[0].ToString());

            string NombrePPal = e.NewValues[1].ToString();
            string Apellido1PPal = e.NewValues[2].ToString();
            string Apellido2PPal = e.NewValues[3].ToString();

            string Descripcion = e.NewValues[4].ToString();
            string Direccion = e.NewValues[4].ToString();

            string ConnectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            Connection = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;


            cmd.CommandText = "UPDATE familia " +
                "SET NombrePPal = @NombrePPal,Apellido1PPal = @Apellido1PPal,Apellido2PPal = @Apellido2PPal,"+
                "Descripcion = @Descripcion,Direccion = @Direccion " +
                "where id = @id";

            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;

            cmd.Parameters.Add("@NombrePPal", SqlDbType.VarChar).Value = NombrePPal;
            cmd.Parameters.Add("@Apellido1PPal", SqlDbType.VarChar).Value = Apellido1PPal;
            cmd.Parameters.Add("@Apellido2PPal", SqlDbType.VarChar).Value = Apellido2PPal;

            cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = Descripcion;
            cmd.Parameters.Add("@Direccion", SqlDbType.VarChar).Value = Direccion;

            DataTable dtFamilias = new DataTable();
            SqlDataAdapter Adapter = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Connection;
            Connection.Open();

            Adapter.SelectCommand = cmd;
            Adapter.Fill(dtFamilias);

            gvFamilias.DataSource = dtFamilias;
            gvFamilias.DataBind();

            Connection.Close();
        }

        private void FamiliaUpdateComprovacionErres(GridViewUpdateEventArgs e)
        {
            if (e.NewValues[1].ToString() == "")
                throw new Exception("El Nombre no es correcto");
            if (e.NewValues[2].ToString() == "")
                throw new Exception("El primer apellido no es correcto");
            if (e.NewValues[3].ToString() == "")
                throw new Exception("El segundo apellido no es correcto");

            if (e.NewValues[4].ToString() == "")
                throw new Exception("La descripcion no es correcta");
            if (e.NewValues[5].ToString() == "")
                throw new Exception("La Direccion no es correcta");
        }
    }
}