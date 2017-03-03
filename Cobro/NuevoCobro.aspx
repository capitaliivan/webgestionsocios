<%@ Page Title="" Language="C#" MasterPageFile="~/Cobro/MPCobro.master" AutoEventWireup="true" CodeBehind="NuevoCobro.aspx.cs" Inherits="WebCABDN.Cobro.NuevoCobro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="server">
    <h2>Agregar Cobro</h2>

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
                            <td class="auto-style12">Número cuenta:</td>
                            <td class="auto-style17">
                                <asp:TextBox ID="tbNumeroCuenta" runat="server" Width="261px"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style12">Entidad:</td>
                            <td>
                                <asp:TextBox ID="tbEntidad" runat="server" Width="261px"></asp:TextBox>
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
                            <td class="auto-style16">D.N.I.</td>
                            <td class="auto-style17">
                                <asp:TextBox ID="tbDNI" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style11">Descripcion:</td>
                            <td class="auto-style8">
                                <asp:TextBox ID="tbDescripcion" runat="server" Width="261px"></asp:TextBox>
                            </td>
                        </tr>
                        </table>
                </td>
                <td>
                    <table style="width:100%;">
                        <tr>
                            <td class="auto-style12">Estado cobro:</td>
                            <td>
                                <asp:DropDownList ID="cbEstadoCobro" runat="server" Height="23px" Width="267px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style11">Motivo no cobro:</td>
                            <td class="auto-style8">
                                <asp:DropDownList ID="cbMotivoNoCobro" runat="server" Height="23px" Width="267px">
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
                <asp:ImageButton ID="ibAñadirCobros" runat="server" ImageAlign="Right" ImageUrl="~/images/Save64.png" OnClick="ibAñadirCobros_Click" />
            </td>
        </tr>
    </table>


</asp:Content>
