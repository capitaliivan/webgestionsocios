using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebCABDN.Cobro
{
    public partial class NuevoCobro : System.Web.UI.Page
    {
        SqlConnection Connection;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string ConnectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                    Connection = new SqlConnection(ConnectionString);
                    Connection.Open();

                    CargaCombos();

                    Connection.Close();
                }
                catch (Exception ex)
                { lblError.Text = ex.Message; }
            }
        }

        private void CargaCombos()
        {
            CargarComboEstadoCobro();
            CargarComboMotivoNoCobro();
        }

        private void CargarComboEstadoCobro()
        {
            cbEstadoCobro.Items.Clear();

            SqlDataAdapter Adapter = new SqlDataAdapter("select null as id,'' as descripcion from cobro WHERE id = 2" +
                                                        " union all " +
                                                        "Select id,Estado from EstadoCobro", Connection);
            DataTable dtEstadoCobro = new DataTable();

            Adapter.Fill(dtEstadoCobro);

            for (int i = 0; i < dtEstadoCobro.Rows.Count; i++)
            {
                ListItem oItem = new ListItem(dtEstadoCobro.Rows[i]["descripcion"].ToString(), dtEstadoCobro.Rows[i]["id"].ToString());
                cbEstadoCobro.Items.Add(oItem);
            }
        }

        private void CargarComboMotivoNoCobro()
        {
            cbMotivoNoCobro.Items.Clear();

            SqlDataAdapter Adapter = new SqlDataAdapter("select null as id,'' as descripcion from cobro WHERE id = 2" +
                                                        " union all " +
                                                        "Select id,MotivoNoCobro from MotivoNoCobro", Connection);
            DataTable dtMotivoNoCobro = new DataTable();

            Adapter.Fill(dtMotivoNoCobro);

            for (int i = 0; i < dtMotivoNoCobro.Rows.Count; i++)
            {
                ListItem oItem = new ListItem(dtMotivoNoCobro.Rows[i]["descripcion"].ToString(), dtMotivoNoCobro.Rows[i]["id"].ToString());
                cbMotivoNoCobro.Items.Add(oItem);
            }
        }

        protected void ibAñadirCobros_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                lblError.Text = "";

                NuevoCobroInsertComprovacionErrores();
                NuevoCobroInsert();
            }
            catch (Exception ex)
            { lblError.Text = ex.Message; }
        }

        private void NuevoCobroInsert()
        {
            try
            {
                string Nombre = tbNombre.Text;
                string Apellido1 = tbApellido1.Text;
                string Apellido2 = tbApellido2.Text;

                string DNI = tbDNI.Text;

                string NumeroCuenta = tbNumeroCuenta.Text;
                string Entidad = tbEntidad.Text;

                int EstadoCobro = int.Parse(cbEstadoCobro.Items[cbEstadoCobro.SelectedIndex].Value.ToString());
                int MotivoNoCobro = int.Parse(cbMotivoNoCobro.Items[cbMotivoNoCobro.SelectedIndex].Value.ToString());

                string Descripcion = tbDescripcion.Text;

                string ConnectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                Connection = new SqlConnection(ConnectionString);

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "INSERT INTO cobro (Nombre,Apellido1,Apellido2,DNI,NumeroCuenta,Entidad," +
               "EstadoCobro,MotivoNoCobrado,Descripcion)" +

                "values(@Nombre, @Apellido1, @Apellido2,@DNI,@NumeroCuenta,@Entidad,@EstadoCobro,@MotivoNoCobro," +
                "@Descripcion);" +

                "SELECT [id],[Nombre],[Apellido1],[Apellido2],[DNI]" +
                    ",[NumeroCuenta],[Entidad],[EstadoCobro],[MotivoNoCobrado],[Descripcion]" +
                    " FROM Cobro";

                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = Nombre;
                cmd.Parameters.Add("@Apellido1", SqlDbType.VarChar).Value = Apellido1;
                cmd.Parameters.Add("@Apellido2", SqlDbType.VarChar).Value = Apellido2;

                cmd.Parameters.Add("@DNI", SqlDbType.VarChar).Value = DNI;

                cmd.Parameters.Add("@NumeroCuenta", SqlDbType.VarChar).Value = NumeroCuenta;
                cmd.Parameters.Add("@Entidad", SqlDbType.VarChar).Value = Entidad;

                cmd.Parameters.Add("@EstadoCobro", SqlDbType.Int).Value = EstadoCobro;
                cmd.Parameters.Add("@MotivoNoCobro", SqlDbType.Int).Value = MotivoNoCobro;

                cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = Descripcion;

                DataTable dtCobros = new DataTable();
                SqlDataAdapter Adapter = new SqlDataAdapter();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = Connection;
                Connection.Open();

                Adapter.SelectCommand = cmd;
                Adapter.Fill(dtCobros);

                Connection.Close();

                Response.Redirect("Cobros.aspx");
            }
            catch (Exception ex)
            { throw ex; }
        }

        private void NuevoCobroInsertComprovacionErrores()
        {
            try
            {
                //Comprovaciones
                if (tbNombre.Text == "")
                    throw new Exception("El Nombre no es correcto");
                if (tbApellido1.Text == "")
                    throw new Exception("El primer apellido no es correcto");
                if (tbApellido2.Text == "")
                    throw new Exception("El segundo apellido no es correcto");
                if (tbDNI.Text == "")
                    throw new Exception("El DNI no es correcto");
                if (tbNumeroCuenta.Text == "")
                    throw new Exception("La Fecha Nacimiento no es correcta");
                if (tbEntidad.Text == "")
                    throw new Exception("La dirección no es correcta");

                if (cbEstadoCobro.Text == "")
                    throw new Exception("El código Postal no es correcto");
                if (cbMotivoNoCobro.Text == "")
                    throw new Exception("La población no es correcta");

                if (tbDescripcion.Text == "")
                    throw new Exception("La situación elegida no es correcta");
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}