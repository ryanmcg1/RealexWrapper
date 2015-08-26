<%@ Page Language="C#" AutoEventWireup="true" CodeFile="payer-management.aspx.cs" Inherits="RealVault_payer_management" MasterPageFile="~/Site.master" Title="RealVault Integration" %>

<asp:Content ID="FeaturedContent" runat="server" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
            </hgroup>
            <p>
                Manage payers.
            </p>
        </div>
    </section>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <h3>Request examples:</h3>
    <ol class="round">
        <li class="one">
            <h5>New Payer</h5>
            Creates a new payer within real vault.
            <asp:LinkButton ID="lbNewPayer" runat="server" OnClick="lbNewPayer_Click">Run</asp:LinkButton>
        </li>
        <li class="two">
            <h5>Edit Payer</h5>
            Edits an existing payer within real vault.
            <asp:LinkButton ID="lbEditPayer" runat="server" OnClick="lbEditPayer_Click">Run</asp:LinkButton>
        </li>
        <li class="three">
            <h5>Result Message</h5>
            Result Code: <b><asp:Label ID="lblErrorCode" runat="server" /></b>
            <br />
            Result Message: <b><asp:Label ID="lblResult" runat="server" /></b>
        </li>
    </ol>

</asp:Content>