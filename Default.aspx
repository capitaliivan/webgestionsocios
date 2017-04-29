<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebCABDN.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

        .auto-style1 {
            width: 84px;
        }
    .auto-style2 {
        width: 84px;
        height: 26px;
    }
    .auto-style3 {
        height: 26px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenidoPrincipal" runat="server">
    <h2>Bienvenidos a la web</h2>
    <p>
        <table style="width: 100%;">
            <tr>
                <td class="auto-style1">User:</td>
                <td>
                    <asp:TextBox ID="tbUser" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">Pass:</td>
                <td class="auto-style3">
                    <asp:TextBox ID="tbPass" TextMode="Password" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style3"></td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td>
                    <asp:Button ID="bLogin" runat="server" OnClick="bLogin_Click" Text="Login" />
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </p>

</asp:Content>
