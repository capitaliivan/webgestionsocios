<%@ Page Title="" Language="C#" MasterPageFile="~/Familia/MPFamilia.master" AutoEventWireup="true" CodeBehind="NuevaFamilia.aspx.cs" Inherits="WebCABDN.Familia.NuevaFamilia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="server">
        <h2>Agregar Familia</h2>
        <table style="width:100%;">
            <tr>
                <td class="auto-style9">
                    <table style="width: 100%;">
                        <tr>
                            <td class="auto-style15">Nombre:</td>
                            <td class="auto-style7">
                                <asp:TextBox ID="tbNombre" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style15">Apellido:</td>
                            <td class="auto-style7">
                                <asp:TextBox ID="tbApellido1" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style15">Apellido:</td>
                            <td class="auto-style7">
                                <asp:TextBox ID="tbApellido2" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table style="width: 100%; margin-left: 0px;">
                        <tr>
                            <td class="auto-style12">Descripción:</td>
                            <td class="auto-style17">
                                <asp:TextBox ID="tbDescripcion" runat="server" Width="261px"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style12">Dirección:</td>
                            <td>
                                <asp:TextBox ID="tbDireccion" runat="server" Width="261px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style12">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            </table>
    <table style="width:100%;">
        <tr>
            <td class="auto-style8">
                <asp:Label ID="lblError" runat="server"></asp:Label>
            </td>
            <td class="auto-style18"></td>
            <td class="auto-style8">&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td class="auto-style19">&nbsp;</td>
            <td>
                <asp:ImageButton ID="ibAñadirFamilias" runat="server" ImageAlign="Right" ImageUrl="~/images/Save64.png" OnClick="ibAñadirCobros_Click" />
            </td>
        </tr>
    </table>


</asp:Content>
