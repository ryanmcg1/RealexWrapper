<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="RealVault_default" MasterPageFile="~/Site.master" Title="RealVault Integration" %>

<asp:Content ID="FeaturedContent" runat="server" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
            </hgroup>
            <p>
                RealVault integration demos.
            </p>
        </div>
    </section>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <h3>RealValut sections:</h3>
    <ol class="round">
        <li class="one">
            <h5>Payer Management</h5>
            Manage payers within real vault.
            <a href="payer-management.aspx">View…</a>
        </li>
        <li class="two">
            <h5>Payment Management</h5>
            Manage payement methods within real vault.
            <a href="payment-management.aspx">View…</a>
        </li>
        <li class="three">
            <h5>Payment Authorisation</h5>
            Take payments via real vault.
            <a href="payment-authorisation.aspx">View…</a>
        </li>
    </ol>

</asp:Content>
