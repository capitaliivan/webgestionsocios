using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebCABDN.Familia
{
    public partial class NuevaFamilia : System.Web.UI.Page
    {

        SqlConnection Connection;

        protected void Page_Load(object sender, EventArgs e)
        {
            /*if (!IsPostBack)
            {
                try
                {
                    string ConnectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                    Connection = new SqlConnection(ConnectionString);
                    Connection.Open();

                    Connection.Close();
                }
                catch (Exception ex)
                { lblError.Text = ex.Message; }
            }
             * */
        }

        protected void ibAñadirCobros_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                lblError.Text = "";

                NuevaFamiliaInsertComprovacionErrores();
                NuevaFamiliaInsert();
            }
            catch (Exception ex)
            { lblError.Text = ex.Message; }
        }

        private void NuevaFamiliaInsert()
        {
            try
            {
                string Nombre = tbNombre.Text;
                string Apellido1 = tbApellido1.Text;
                string Apellido2 = tbApellido2.Text;

                string Descripcion = tbDescripcion.Text;
                string Direccion = tbDireccion.Text;

                string ConnectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                Connection = new SqlConnection(ConnectionString);

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "INSERT INTO familia (NombrePPAL,Apellido1PPAL,Apellido2PPAL,Descripcion,Direccion)" +
                "values(@Nombre, @Apellido1, @Apellido2,@Descripcion,@Direccion);" +

                "SELECT [id],[NombrePPAL],[Apellido1PPAL],[Apellido2PPAL],[Descripcion],[Direccion]" +
                    " FROM familia";

                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = Nombre;
                cmd.Parameters.Add("@Apellido1", SqlDbType.VarChar).Value = Apellido1;
                cmd.Parameters.Add("@Apellido2", SqlDbType.VarChar).Value = Apellido2;

                cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = Descripcion;
                cmd.Parameters.Add("@Direccion", SqlDbType.VarChar).Value = Direccion;

                DataTable dtFamilias = new DataTable();
                SqlDataAdapter Adapter = new SqlDataAdapter();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = Connection;
                Connection.Open();

                Adapter.SelectCommand = cmd;
                Adapter.Fill(dtFamilias);

                Connection.Close();

                Response.Redirect("Familias.aspx");
            }
            catch (Exception ex)
            { throw ex; }
        }

        private void NuevaFamiliaInsertComprovacionErrores()
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
                if (tbDescripcion.Text == "")
                    throw new Exception("El DNI no es correcto");
                if (tbDireccion.Text == "")
                    throw new Exception("La Fecha Nacimiento no es correcta");
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}