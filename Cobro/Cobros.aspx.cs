using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebCABDN.Cobro
{
    public partial class Cobros : System.Web.UI.Page
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

                    CargarGridCobros();

                    Connection.Close();
                }
                catch (Exception ex)
                { lblError.Text = ex.Message; }
            }
        }

        private void CargarGridCobros()
        {
            SqlDataAdapter Adapter = new SqlDataAdapter("SELECT [id],[Nombre],[Apellido1],[Apellido2],[DNI],[NumeroCuenta],"+
            "[Entidad],[EstadoCobro],[MotivoNoCobrado],[Descripcion]" +
            " FROM Cobro", Connection);
            DataTable dtSocios = new DataTable();

            Adapter.Fill(dtSocios);

            gvCobros.DataSource = dtSocios;
            gvCobros.DataBind();
        }

        protected void ibNewCobro_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("NuevoCobro.aspx");
        }

        protected void gvCobros_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCobros.EditIndex = e.NewEditIndex;

            try
            {
                string ConnectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                Connection = new SqlConnection(ConnectionString);
                Connection.Open();

                CargarGridCobros();

                Connection.Close();
            }
            catch (Exception ex)
            { lblError.Text = ex.Message; }
        }

        protected void gvCobros_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                lblError.Text = "";

                CobroUpdateComprovacionErres(e);
                CobroUpdate(e);
            }
            catch (Exception ex)
            { lblError.Text = ex.Message; }
            finally
            {
                gvCobros.EditIndex = -1;

                try
                {
                    string ConnectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                    Connection = new SqlConnection(ConnectionString);
                    Connection.Open();

                    CargarGridCobros();

                    Connection.Close();
                }
                catch (Exception ex)
                { lblError.Text = ex.Message; }
            }
        }

        protected void gvCobros_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCobros.EditIndex = -1;

            try
            {
                string ConnectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                Connection = new SqlConnection(ConnectionString);
                Connection.Open();

                CargarGridCobros();

                Connection.Close();
            }
            catch (Exception ex)
            { lblError.Text = ex.Message; }
        }

        protected void gvCobros_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Indice = Convert.ToInt32(e.CommandArgument);
        }

        protected void gvCobros_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                lblError.Text = "";

                int id = int.Parse(gvCobros.Rows[Indice].Cells[1].Text);

                string ConnectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                Connection = new SqlConnection(ConnectionString);

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from Cobro where " +
                "id=@id;" +

                "SELECT [id],[Nombre],[Apellido1],[Apellido2],[DNI],[NumeroCuenta]," +
                "[Entidad],[EstadoCobro],[MotivoNoCobrado],[Descripcion]" +
                " FROM Cobro";

                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                DataTable dtCobros = new DataTable();
                SqlDataAdapter Adapter = new SqlDataAdapter();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = Connection;
                Connection.Open();

                Adapter.SelectCommand = cmd;
                Adapter.Fill(dtCobros);

                gvCobros.DataSource = dtCobros;
                gvCobros.DataBind();

                Connection.Close();
            }
            catch (Exception ex)
            { lblError.Text = ex.Message; }
        }

        private void CobroUpdate(GridViewUpdateEventArgs e)
        {
            int id = int.Parse(e.NewValues[0].ToString());

            string Nombre = e.NewValues[1].ToString();
            string Apellido1 = e.NewValues[2].ToString();
            string Apellido2 = e.NewValues[3].ToString();

            string DNI = e.NewValues[4].ToString();

            string NumeroCuenta = e.NewValues[4].ToString();
            string Entidad = e.NewValues[4].ToString();

            int EstadoCobro = int.Parse(e.NewValues[8].ToString());
            int MotivoNoCobrado = int.Parse(e.NewValues[8].ToString());

            string Descripcion = e.NewValues[9].ToString();

            string ConnectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            Connection = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;


            cmd.CommandText = "UPDATE Cobro " +
                "SET Nombre = @Nombre,Apellido1 = @Apellido1,Apellido2 = @Apellido2,DNI = @DNI,NumeroCuenta = @NumeroCuenta," +
                "Entidad = @Entidad,EstadoCobro = @EstadoCobro,MotivoNoCobrado = @MotivoNoCobrado,Descripcion = @Descripcion " +
            "where id = @id";

            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;

            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = Nombre;
            cmd.Parameters.Add("@Apellido1", SqlDbType.VarChar).Value = Apellido1;
            cmd.Parameters.Add("@Apellido2", SqlDbType.VarChar).Value = Apellido2;

            cmd.Parameters.Add("@DNI", SqlDbType.VarChar).Value = DNI;

            cmd.Parameters.Add("@NumeroCuenta", SqlDbType.VarChar).Value = NumeroCuenta;
            cmd.Parameters.Add("@Entidad", SqlDbType.VarChar).Value = Entidad;

            cmd.Parameters.Add("@EstadoCobro", SqlDbType.Int).Value = EstadoCobro;
            cmd.Parameters.Add("@MotivoNoCobrado", SqlDbType.Int).Value = MotivoNoCobrado;

            cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = Descripcion;

            DataTable dtCobros = new DataTable();
            SqlDataAdapter Adapter = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Connection;
            Connection.Open();

            Adapter.SelectCommand = cmd;
            Adapter.Fill(dtCobros);

            gvCobros.DataSource = dtCobros;
            gvCobros.DataBind();

            Connection.Close();
        }

        private void CobroUpdateComprovacionErres(GridViewUpdateEventArgs e)
        {
            if (e.NewValues[1].ToString() == "")
                throw new Exception("El Nombre no es correcto");
            if (e.NewValues[2].ToString() == "")
                throw new Exception("El primer apellido no es correcto");
            if (e.NewValues[3].ToString() == "")
                throw new Exception("El segundo apellido no es correcto");

            if (e.NewValues[4].ToString() == "")
                throw new Exception("El DNI no es correcto");

            if (e.NewValues[5].ToString() == "")
                throw new Exception("El Número de cuenta no es correcto");
            if (e.NewValues[6].ToString() == "")
                throw new Exception("La Entidad no es correcta");

            if (e.NewValues[7].ToString() == "")
                throw new Exception("El estado de cobro no es correcto");
            if (e.NewValues[8].ToString() == "")
                throw new Exception("El motivo de no cobro no es correcto");
            if (e.NewValues[9].ToString() == "")
                throw new Exception("La descripción no es correcta");
        }
    }
}