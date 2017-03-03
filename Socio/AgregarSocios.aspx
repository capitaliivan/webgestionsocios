<%@ Page Title="" Language="C#" MasterPageFile="~/Socio/MPSocio.master" AutoEventWireup="true" CodeBehind="AgregarSocios.aspx.cs" Inherits="WebCABDN.Socio.AgregarSocios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=tbFechaNacimiento.ClientID%>").dynDateTime({
                showsTime: true,
                ifFormat: "%d/%m/%Y",
                daFormat: "%l;%M %p, %e %m,  %Y",
                align: "BR",
                electric: false,
                singleClick: false,
                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });
        });
    </script>
        <script type="text/javascript">
            $(document).ready(function () {
                $("#<%=tbFechaAlta.ClientID%>").dynDateTime({
                    showsTime: true,
                    ifFormat: "%d/%m/%Y",
                    daFormat: "%l;%M %p, %e %m,  %Y",
                    align: "BR",
                    electric: false,
                    singleClick: false,
                    displayArea: ".siblings('.dtcDisplayArea')",
                    button: ".next()"
                });
            });
    </script>

    <h2></h2>

        <style type="text/css">
        .auto-style7 {
            width: 321px;
        }
        .auto-style8 {
            height: 23px;
        }
        .auto-style9 {
            width: 404px;
        }
        .auto-style11 {
            height: 23px;
            width: 117px;
        }
        .auto-style12 {
            width: 117px;
        }
        .auto-style15 {
            width: 139px;
        }
        .auto-style16 {
            width: 117px;
            height: 26px;
        }
        .auto-style17 {
            height: 26px;
        }
            .auto-style18 {
                height: 23px;
                width: 113px;
            }
            .auto-style19 {
                width: 113px;
            }
        </style>

        <table style="width:100%;">
            <tr>
                <td class="auto-style9">
                    <table style="width: 100%;">
                        <tr>
                            <td class="auto-style15">Nº Socio:</td>
                            <td class="auto-style7">
                                <asp:TextBox ID="tbNumeroSocio" runat="server"></asp:TextBox>
                            </td>
                        </tr>
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
                    </table>
                </td>
                <td>
                    <table style="width: 100%; margin-left: 0px;">
                        <tr>
                            <td class="auto-style12">Fecha alta:</td>
                            <td class="auto-style17">
                                <asp:TextBox ID="tbFechaAlta" runat="server"></asp:TextBox>
                                <img src="../images/calender.png" /> </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style12">Situación:</td>
                            <td>
                                <asp:DropDownList ID="cbSituacion" runat="server" Height="23px" Width="128px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style12">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="auto-style9">
                    <table style="width:100%;">
                        <tr>
                            <td class="auto-style16">Apellido:</td>
                            <td class="auto-style17">
                                <asp:TextBox ID="tbApellido2" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style11">D.N.I.</td>
                            <td class="auto-style8">
                                <asp:TextBox ID="tbDNI" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style12">Fecha Nacimiento:</td>
                            <td class="auto-style17">
                                <asp:TextBox ID="tbFechaNacimiento" runat="server"></asp:TextBox>
                                <img src="../images/calender.png" /> </td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table style="width:100%;">
                        <tr>
                            <td class="auto-style12">Familia:</td>
                            <td>
                                <asp:DropDownList ID="cbFamilia" runat="server" Height="23px" Width="128px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style11">Datos cobro:</td>
                            <td class="auto-style8">
                                <asp:DropDownList ID="cbCobro" runat="server" Height="23px" Width="128px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style12">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="auto-style9">
                    <table style="width:100%;">
                        <tr>
                            <td class="auto-style12">Dirección:</td>
                            <td>
                                <asp:TextBox ID="tbDireccion" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style11">Código postal:</td>
                            <td class="auto-style8">
                                <asp:TextBox ID="tbCodigoPostal" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style12">Población:</td>
                            <td>
                                <asp:TextBox ID="tbPoblacion" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table style="width:100%;">
                        <tr>
                            <td class="auto-style12">Provincia:</td>
                            <td>
                                <asp:TextBox ID="tbProvincia" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style12">Comunidad:</td>
                            <td>
                                <asp:DropDownList ID="cbComunidad" runat="server" Height="23px" Width="128px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style12">Pais:</td>
                            <td>
                                <asp:DropDownList ID="cbPais" runat="server" Height="23px" Width="128px">
                                </asp:DropDownList>
                            </td>
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
                <asp:ImageButton ID="ibAñadirSocios" runat="server" ImageAlign="Right" ImageUrl="~/images/Save64.png" OnClick="ibAñadirSocios_Click" />
            </td>
        </tr>
    </table>

</asp:Content>
