using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebCABDN.Socio
{
    public partial class AgregarSocios : System.Web.UI.Page
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
            CargarComboFamilia();
            CargarComboCobro();
            CargarComboComunidad();
            CargarComboPais();
            CargarComboSituacion();
        }

        private void CargarComboFamilia()
        {
            cbFamilia.Items.Clear();

            SqlDataAdapter Adapter = new SqlDataAdapter("select null as id,'' as descripcion from familia WHERE id = 1" +
                                                        " union all " +
                                                        "Select id,descripcion from familia", Connection);
            DataTable dtFamilia = new DataTable();

            Adapter.Fill(dtFamilia);

            for (int i = 0; i < dtFamilia.Rows.Count; i++)
            {
                ListItem oItem = new ListItem(dtFamilia.Rows[i]["descripcion"].ToString(), dtFamilia.Rows[i]["id"].ToString());
                cbFamilia.Items.Add(oItem);
            }
        }

        private void CargarComboCobro()
        {
            cbCobro.Items.Clear();

            SqlDataAdapter Adapter = new SqlDataAdapter("select null as id,'' as descripcion from cobro WHERE id = 2" +
                                                        " union all " +
                                                        "Select id,Descripcion from cobro", Connection);
            DataTable dtCobro = new DataTable();

            Adapter.Fill(dtCobro);

            for (int i = 0; i < dtCobro.Rows.Count; i++)
            {
                ListItem oItem = new ListItem(dtCobro.Rows[i]["descripcion"].ToString(), dtCobro.Rows[i]["id"].ToString());
                cbCobro.Items.Add(oItem);
            }
        }

        private void CargarComboComunidad()
        {
            cbComunidad.Items.Clear();

            SqlDataAdapter Adapter = new SqlDataAdapter("select null as id,'' as Nombre from comunidadesAutonomas WHERE id = 1" +
                                                        " union all " +
                                                        "Select id,Nombre from comunidadesAutonomas", Connection);
            DataTable dtComunidadesAutonomas = new DataTable();

            Adapter.Fill(dtComunidadesAutonomas);

            for (int i = 0; i < dtComunidadesAutonomas.Rows.Count; i++)
            {
                ListItem oItem = new ListItem(dtComunidadesAutonomas.Rows[i]["Nombre"].ToString(), dtComunidadesAutonomas.Rows[i]["id"].ToString());
                cbComunidad.Items.Add(oItem);
            }
        }

        private void CargarComboPais()
        {
            cbPais.Items.Clear();

            SqlDataAdapter Adapter = new SqlDataAdapter("select null as id,'' as Nombre from pais WHERE id = 1" +
                                                        " union all " +
                                                        "Select id,Nombre from pais", Connection);
            DataTable dtPaises = new DataTable();

            Adapter.Fill(dtPaises);

            for (int i = 0; i < dtPaises.Rows.Count; i++)
            {
                ListItem oItem = new ListItem(dtPaises.Rows[i]["Nombre"].ToString(), dtPaises.Rows[i]["id"].ToString());
                cbPais.Items.Add(oItem);
            }
        }

        private void CargarComboSituacion()
        {
            cbSituacion.Items.Clear();

            SqlDataAdapter Adapter = new SqlDataAdapter("select null as id,'' as Situacion, '' as Cuota from situacion WHERE id = 1" +
                                                        " union all " +
                                                        "Select id,Situacion,Cuota from situacion", Connection);
            DataTable dtSituacion = new DataTable();

            Adapter.Fill(dtSituacion);

            for (int i = 0; i < dtSituacion.Rows.Count; i++)
            {
                decimal Cuota = (decimal)dtSituacion.Rows[i]["Cuota"];

                ListItem oItem = new ListItem(dtSituacion.Rows[i]["Situacion"].ToString() + " (" + Cuota.ToString("N2") + ")", dtSituacion.Rows[i]["id"].ToString());
                cbSituacion.Items.Add(oItem);
            }
        }

        protected void ibAñadirSocios_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                lblError.Text = "";

                NuevoSocioInsertComprovacionErrores();
                NuevoSocioInsert();
            }
            catch (Exception ex)
            { lblError.Text = ex.Message; }
        }

        private void NuevoSocioInsert()
        {
            try
            {
                int NumeroSocio = int.Parse(tbNumeroSocio.Text);

                string Nombre = tbNombre.Text;
                string Apellido1 = tbApellido1.Text;
                string Apellido2 = tbApellido2.Text;

                string DNI = tbDNI.Text;
                DateTime FechaNacimiento = DateTime.Parse(tbFechaNacimiento.Text);

                string Direccion = tbDireccion.Text;
                int CodigoPostal = int.Parse(tbCodigoPostal.Text);
                string Poblacion = tbPoblacion.Text;

                DateTime FechaAlta = DateTime.Parse(tbFechaAlta.Text);
                string Provincia = tbProvincia.Text; ;

                int Situacion = int.Parse(cbSituacion.Items[cbSituacion.SelectedIndex].Value.ToString());
                int Familia = int.Parse(cbFamilia.Items[cbFamilia.SelectedIndex].Value.ToString());
                int Cobro = int.Parse(cbCobro.Items[cbCobro.SelectedIndex].Value.ToString());

                string Comunidad = cbComunidad.SelectedItem.ToString();
                string Pais = cbPais.SelectedItem.ToString();

                string Telefono = "667344990";

                string ConnectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                Connection = new SqlConnection(ConnectionString);

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "INSERT INTO Socios (Nombre,Apellido1,Apellido2,DNI,NumeroSocio,FechaNacimiento," +
               "Direccion,CodigoPostal,Poblacion,FechaAlta,IDSituacion,IDFamilia,IDCobros,Provincia,Comunidad," +
               "Pais,Telefono)" +

                "values(@Nombre, @Apellido1, @Apellido2,@DNI,@NumeroSocio,@FechaNacimiento,@Direccion,@CodigoPostal," +
                "@Poblacion,@FechaAlta,@IDSituacion,@IDFamilia,@IDCobros,@Provincia,@Comunidad,@Pais,@Telefono);" +

                "SELECT [id],[Nombre],[Apellido1],[Apellido2],[DNI]" +
                    ",[NumeroSocio],CONVERT(VARCHAR(10),FechaNacimiento,103) AS FechaNacimiento,[Direccion],[CodigoPostal],[Poblacion]" +
                    ",CONVERT(VARCHAR(10),FechaAlta,103) AS FechaAlta,[IDSituacion],[IDFamilia],[IDCobros],[Provincia],[Comunidad]" +
                    ",[Pais],[Telefono] FROM Socios";

                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = Nombre;
                cmd.Parameters.Add("@Apellido1", SqlDbType.VarChar).Value = Apellido1;
                cmd.Parameters.Add("@Apellido2", SqlDbType.VarChar).Value = Apellido2;

                cmd.Parameters.Add("@DNI", SqlDbType.VarChar).Value = DNI;
                cmd.Parameters.Add("@NumeroSocio", SqlDbType.Int).Value = NumeroSocio;
                cmd.Parameters.Add("@FechaNacimiento", SqlDbType.Date).Value = FechaNacimiento;

                cmd.Parameters.Add("@Direccion", SqlDbType.VarChar).Value = Direccion;
                cmd.Parameters.Add("@CodigoPostal", SqlDbType.Int).Value = CodigoPostal;
                cmd.Parameters.Add("@Poblacion", SqlDbType.VarChar).Value = Poblacion;

                cmd.Parameters.Add("@FechaAlta", SqlDbType.Date).Value = FechaAlta;
                cmd.Parameters.Add("@IDSituacion", SqlDbType.Int).Value = Situacion;
                cmd.Parameters.Add("@IDFamilia", SqlDbType.Int).Value = Familia;
                cmd.Parameters.Add("@IDCobros", SqlDbType.Int).Value = Cobro;

                cmd.Parameters.Add("@Provincia", SqlDbType.VarChar).Value = Provincia;
                cmd.Parameters.Add("@Comunidad", SqlDbType.VarChar).Value = Comunidad;
                cmd.Parameters.Add("@Pais", SqlDbType.VarChar).Value = Pais;

                cmd.Parameters.Add("@Telefono", SqlDbType.VarChar).Value = Telefono;

                DataTable dtSocios = new DataTable();
                SqlDataAdapter Adapter = new SqlDataAdapter();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = Connection;
                Connection.Open();

                Adapter.SelectCommand = cmd;
                Adapter.Fill(dtSocios);

                Connection.Close();

                Response.Redirect("Socios.aspx");
            }
            catch (Exception ex)
            { throw ex; }
        }

        private void NuevoSocioInsertComprovacionErrores()
        {
            try
            {
                //Comprovaciones
                if (tbNumeroSocio.Text == "")
                    throw new Exception("El Número de socio no es correcto");
                if (tbNombre.Text == "")
                    throw new Exception("El Nombre no es correcto");
                if (tbApellido1.Text == "")
                    throw new Exception("El primer apellido no es correcto");
                if (tbApellido2.Text == "")
                    throw new Exception("El segundo apellido no es correcto");
                if (tbDNI.Text == "")
                    throw new Exception("El DNI no es correcto");
                if (tbFechaNacimiento.Text == "")
                    throw new Exception("La Fecha Nacimiento no es correcta");
                if (tbDireccion.Text == "")
                    throw new Exception("La dirección no es correcta");
                if (tbCodigoPostal.Text == "")
                    throw new Exception("El código Postal no es correcto");
                if (tbPoblacion.Text == "")
                    throw new Exception("La población no es correcta");
                if (tbFechaAlta.Text == "")
                    throw new Exception("La Fecha Alta no es correcta");
                if (tbProvincia.Text == "")
                    throw new Exception("La provincia no es correcta");

                if (cbSituacion.Text == "")
                    throw new Exception("La situación elegida no es correcta");
                if (cbFamilia.Text == "")
                    throw new Exception("La familia elegida no es correcta");
                if (cbCobro.Text == "")
                    throw new Exception("Los datos de cobro elegidos no es correcto");
                if (cbComunidad.Text == "")
                    throw new Exception("La comunidad autónoma elegida no es correcta");
                if (cbPais.Text == "")
                    throw new Exception("El pais elegido no es correcto");
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}