using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.Common;

namespace WebCABDN.Socio
{
    public partial class Socio : System.Web.UI.Page
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

                    cargaGridSocios();
                    CargarComboFiltro();

                    Connection.Close();
                }
                catch (Exception ex)
                { lblError.Text = ex.Message; }
            }
        }

        private void CargarComboFiltro()
        {
            cbFiltro.Items.Clear();

            SqlDataAdapter Adapter = new SqlDataAdapter("SELECT COLUMN_NAME " +
                                                        "FROM INFORMATION_SCHEMA.COLUMNS " +
                                                        "WHERE TABLE_NAME = 'Socios' " +
                                                        "and COLUMN_NAME not in ('id')", Connection);
            DataTable dtFiltro = new DataTable();

            Adapter.Fill(dtFiltro);

            for (int i = 0; i < dtFiltro.Rows.Count; i++)
            {
                ListItem oItem = new ListItem(dtFiltro.Rows[i]["COLUMN_NAME"].ToString());
                cbFiltro.Items.Add(oItem);
            }
        }

        protected void ibNewSocio_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("AgregarSocios.aspx");
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            CrearPDF();
        }

        protected void gvSocios_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSocios.EditIndex = -1;

            try
            {
                string ConnectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                Connection = new SqlConnection(ConnectionString);
                Connection.Open();

                cargaGridSocios();

                Connection.Close();
            }
            catch (Exception ex)
            { lblError.Text = ex.Message; }
        }

        protected void gvSocios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Indice = Convert.ToInt32(e.CommandArgument);
        }

        protected void gvSocios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                lblError.Text = "";

                int id = int.Parse(gvSocios.Rows[Indice].Cells[1].Text);

                string ConnectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                Connection = new SqlConnection(ConnectionString);

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from Socios where " +
                "id=@id;" +

                "SELECT [id],[Nombre],[Apellido1],[Apellido2],[DNI]" +
                    ",[NumeroSocio],CONVERT(VARCHAR(10),FechaNacimiento,103) AS FechaNacimiento,[Direccion],[CodigoPostal],[Poblacion]" +
                    ",CONVERT(VARCHAR(10),FechaAlta,103) AS FechaAlta,[IDSituacion],[IDFamilia],[IDCobros],[Provincia],[Comunidad]" +
                    ",[Pais],[Telefono] FROM Socios";

                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                DataTable dtSocios = new DataTable();
                SqlDataAdapter Adapter = new SqlDataAdapter();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = Connection;
                Connection.Open();

                Adapter.SelectCommand = cmd;
                Adapter.Fill(dtSocios);

                gvSocios.DataSource = dtSocios;
                gvSocios.DataBind();

                Connection.Close();
            }
            catch (Exception ex)
            { lblError.Text = ex.Message; }
        }

        protected void gvSocios_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSocios.EditIndex = e.NewEditIndex;

            try
            {
                string ConnectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                Connection = new SqlConnection(ConnectionString);
                Connection.Open();

                cargaGridSocios();

                Connection.Close();
            }
            catch (Exception ex)
            { lblError.Text = ex.Message; }
        }

        protected void gvSocios_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                lblError.Text = "";

                SocioUpdateComprovacionErres(e);
                SocioUpdate(e);
            }
            catch (Exception ex)
            { lblError.Text = ex.Message; }
            finally
            {
                gvSocios.EditIndex = -1;

                try
                {
                    string ConnectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                    Connection = new SqlConnection(ConnectionString);
                    Connection.Open();

                    cargaGridSocios();

                    Connection.Close();
                }
                catch (Exception ex)
                { lblError.Text = ex.Message; }
            }
        }

        private void cargaGridSocios()
        {
            SqlDataAdapter Adapter = new SqlDataAdapter("SELECT [id],[Nombre],[Apellido1],[Apellido2],[DNI]" +
            ",[NumeroSocio],CONVERT(VARCHAR(10),FechaNacimiento,103) AS FechaNacimiento,[Direccion],[CodigoPostal],[Poblacion]" +
            ",CONVERT(VARCHAR(10),FechaAlta,103) AS FechaAlta,[IDSituacion],[IDFamilia],[IDCobros],[Provincia],[Comunidad]" +
            ",[Pais],[Telefono] FROM Socios", Connection);

            DataTable dtSocios = new DataTable();

            Adapter.Fill(dtSocios);

            gvSocios.DataSource = dtSocios;
            gvSocios.DataBind();
        }

        private void cargaGridSociosConFiltro(string filtro)
        {
            SqlDataAdapter Adapter = new SqlDataAdapter("SELECT [id],[Nombre],[Apellido1],[Apellido2],[DNI]" +
            ",[NumeroSocio],CONVERT(VARCHAR(10),FechaNacimiento,103) AS FechaNacimiento,[Direccion],[CodigoPostal],[Poblacion]" +
            ",CONVERT(VARCHAR(10),FechaAlta,103) AS FechaAlta,[IDSituacion],[IDFamilia],[IDCobros],[Provincia],[Comunidad]" +
            ",[Pais],[Telefono] FROM Socios where " + filtro, Connection);

            DataTable dtSocios = new DataTable();

            Adapter.Fill(dtSocios);

            gvSocios.DataSource = dtSocios;
            gvSocios.DataBind();
        }

        private void SocioUpdate(GridViewUpdateEventArgs e)
        {
            int id = int.Parse(e.NewValues[0].ToString());
            int NumeroSocio = int.Parse(e.NewValues[5].ToString());

            string Nombre = e.NewValues[1].ToString();
            string Apellido1 = e.NewValues[2].ToString();
            string Apellido2 = e.NewValues[3].ToString();

            string DNI = e.NewValues[4].ToString();
            DateTime FechaNacimiento = DateTime.Parse(e.NewValues[6].ToString());

            string Direccion = e.NewValues[7].ToString();
            int CodigoPostal = int.Parse(e.NewValues[8].ToString());
            string Poblacion = e.NewValues[9].ToString();

            DateTime FechaAlta = DateTime.Parse(e.NewValues[10].ToString());
            string Provincia = e.NewValues[14].ToString();

            int Situacion = int.Parse(e.NewValues[11].ToString());
            int Familia = int.Parse(e.NewValues[12].ToString());
            int Cobro = int.Parse(e.NewValues[13].ToString());

            string Comunidad = e.NewValues[15].ToString();
            string Pais = e.NewValues[16].ToString();
            string Telefono = e.NewValues[17].ToString();

            string ConnectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            Connection = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;


            cmd.CommandText = "UPDATE Socios " +
                "SET Nombre = @Nombre,Apellido1 = @Apellido1,Apellido2 = @Apellido2,DNI = @DNI,NumeroSocio = @NumeroSocio," +
                "FechaNacimiento = @FechaNacimiento,Direccion = @Direccion,CodigoPostal = @CodigoPostal,Poblacion = @Poblacion," +
            "FechaAlta = @FechaAlta,IDSituacion = @IDSituacion,IDFamilia = @IDFamilia,IDCobros = @IDCobros,Provincia =@Provincia," +
            "Comunidad = @Comunidad,Pais = @Pais,Telefono = @Telefono " +
            "where id = @id";

            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;

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

            gvSocios.DataSource = dtSocios;
            gvSocios.DataBind();

            Connection.Close();
        }

        private void SocioUpdateComprovacionErres(GridViewUpdateEventArgs e)
        {
            if (e.NewValues[5].ToString() == "")
                throw new Exception("El Número de socio no es correcto");

            if (e.NewValues[1].ToString() == "")
                throw new Exception("El Nombre no es correcto");
            if (e.NewValues[2].ToString() == "")
                throw new Exception("El primer apellido no es correcto");
            if (e.NewValues[3].ToString() == "")
                throw new Exception("El segundo apellido no es correcto");

            if (e.NewValues[4].ToString() == "")
                throw new Exception("El DNI no es correcto");
            if (e.NewValues[6].ToString() == "")
                throw new Exception("La Fecha Nacimiento no es correcta");
            if (e.NewValues[7].ToString() == "")
                throw new Exception("La dirección no es correcta");
            if (e.NewValues[8].ToString() == "")
                throw new Exception("El código Postal no es correcto");
            if (e.NewValues[9].ToString() == "")
                throw new Exception("La población no es correcta");
            if (e.NewValues[10].ToString() == "")
                throw new Exception("La Fecha Alta no es correcta");

            if (e.NewValues[11].ToString() == "")
                throw new Exception("La situación elegida no es correcta");
            if (e.NewValues[12].ToString() == "")
                throw new Exception("La familia elegida no es correcta");
            if (e.NewValues[13].ToString() == "")
                throw new Exception("Los datos de cobro elegidos no es correcto");
            if (e.NewValues[14].ToString() == "")
                throw new Exception("La provincia elegida no es correcta");
            if (e.NewValues[15].ToString() == "")
                throw new Exception("La comunidad autónoma elegida no es correcta");
            if (e.NewValues[16].ToString() == "")
                throw new Exception("El pais elegido no es correcto");
        }

        protected void ibBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string filtro = "";
                filtro = cbFiltro.SelectedItem.ToString() + " like '%" + tbFiltro.Text + "%'";

                string ConnectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                Connection = new SqlConnection(ConnectionString);
                Connection.Open();

                cargaGridSociosConFiltro(filtro);

                Connection.Close();
            }
            catch (Exception ex)
            { lblError.Text = ex.Message; }
        }

        private void CrearPDF()
        {
            DataSet ds = new DataSet();
            ds.DataSetName = "ds";

            DataTable dsData = new DataTable();
            dsData = getReportData();

            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            LocalReport report = new LocalReport();
            report.ReportPath = Server.MapPath("Report2.rdlc");
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "ds";//This refers to the dataset name in the RDLC file  
            rds.Value = dsData;
            report.DataSources.Add(rds);

            Byte[] mybytes = report.Render("PDF", null,
                            out extension, out encoding,
                            out mimeType, out streamIds, out warnings); //for exporting to PDF

            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=" + "a.pdf");
            Response.BinaryWrite(mybytes); // create the file
            Response.Flush(); // send it to the client to download
        }

        private DataTable getReportData()
        {
            DataSet dsData = new DataSet();
            SqlConnection sqlCon = null;
            SqlDataAdapter sqlCmd = null;

            try
            {
                using (sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString))
                {
                    sqlCmd = new SqlDataAdapter("SELECT Nombre, Apellido1, Apellido2, Direccion, Poblacion, Provincia, id, CodigoPostal FROM Socios", sqlCon);
                    sqlCmd.SelectCommand.CommandType = CommandType.Text;

                    sqlCon.Open();
                    sqlCmd.Fill(dsData);

                    sqlCon.Close();
                }
            }
            catch
            {
                throw;
            }
            return dsData.Tables[0];
        } 
    }
}