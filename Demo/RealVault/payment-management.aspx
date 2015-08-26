<%@ Page Language="C#" AutoEventWireup="true" CodeFile="payment-management.aspx.cs" Inherits="RealVault_payment_management" MasterPageFile="~/Site.master" Title="RealVault Integration" %>

<asp:Content ID="FeaturedContent" runat="server" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
            </hgroup>
            <p>
                Manage a payer's payment methods.
            </p>
        </div>
    </section>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <h3>Request examples:</h3>
    <ol class="round">
        <li class="one">
            <h5>New Card</h5>
            Creates a new card within real vault.
            <asp:LinkButton ID="lbNewCard" runat="server" OnClick="lbNewCard_Click">Run</asp:LinkButton>
        </li>
        <li class="two">
            <h5>Update Card</h5>
            Updates an existing card within real vault.
            <asp:LinkButton ID="lbUpdateCard" runat="server" OnClick="lbUpdateCard_Click">Run</asp:LinkButton>
        </li>
        <li class="three">
            <h5>Cancel Card</h5>
            Removes an existing card from real vault.
            <asp:LinkButton ID="lbCancelCard" runat="server" OnClick="lbCancelCard_Click">Run</asp:LinkButton>
        </li>
        <li class="four">
            <h5>Result Message</h5>
            Result Code: <b><asp:Label ID="lblErrorCode" runat="server" /></b>
            <br />
            Result Message: <b><asp:Label ID="lblResult" runat="server" /></b>
        </li>
    </ol>

</asp:Content>
