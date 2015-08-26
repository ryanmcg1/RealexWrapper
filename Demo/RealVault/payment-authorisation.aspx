<%@ Page Language="C#" AutoEventWireup="true" CodeFile="payment-authorisation.aspx.cs" Inherits="RealVault_payment_authorisation" MasterPageFile="~/Site.master" Title="RealVault Integration" %>

<asp:Content ID="FeaturedContent" runat="server" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
            </hgroup>
            <p>
                Authorise payments.
            </p>
        </div>
    </section>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <h3>Request examples:</h3>
    <ol class="round">
        <li class="one">
            <h5>Basic Reciept In</h5>
            Takes a payment from an existing card.
            <asp:LinkButton ID="lblbRecieptIn" runat="server" OnClick="lblbRecieptIn_Click">Run</asp:LinkButton>
        </li>
        <li class="two">
            <h5>3D Secure Reciept In</h5>
            Takes a payment from an existing card using 3D Secure. (doesnt work =], code is mostly implemented)
            <asp:LinkButton ID="lb3DSecureReciept" runat="server" OnClick="lb3DSecureReciept_Click">Run</asp:LinkButton>
        </li>
        <li class="three">
            <h5>Result Message</h5>
            Result Code: <b><asp:Label ID="lblErrorCode" runat="server" /></b>
            <br />
            Result Message: <b><asp:Label ID="lblResult" runat="server" /></b>
        </li>
    </ol>

</asp:Content>
