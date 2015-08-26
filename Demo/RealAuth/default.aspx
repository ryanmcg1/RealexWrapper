<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="RealAuth_default" MasterPageFile="~/Site.master" Title="RealAuth Integration" %>

<asp:Content ID="FeaturedContent" runat="server" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
            </hgroup>
            <p>
                RealAuth integration demos. 
            </p>
        </div>
    </section>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <h3>Authorisation Demos:</h3>
    <ol class="round">
        <li class="one">
            <h5>Basic Authorisation</h5>
            This will run basic Realex authentication
            <asp:LinkButton ID="lbBasicAuth" runat="server" OnClick="lbBasicAuth_Click">Run</asp:LinkButton>
        </li>
        <li class="two">
            <h5>3D Secure Authorisation</h5>
            This will run 3D Secure Realex authentication
            <asp:LinkButton ID="lb3DSecureAuth" runat="server" OnClick="lb3DSecureAuth_Click">Run</asp:LinkButton>
        </li>
        <li class="three">
            <h5>Result Message</h5>
            Result Code: <b><asp:Label ID="lblErrorCode" runat="server" /></b>
            <br />
            Result Message: <b><asp:Label ID="lblResult" runat="server" /></b>
        </li>
    </ol>
    <h3>3D Secure Verification</h3>
    <asp:Panel ID="pnlACS" runat="server" Visible="false">
        Please verify.....<br />
        <br />
        <iframe width="400" height="400" frameborder="0" src="http://localhost:6191/3DSecure/ACSRequest.aspx" />
    </asp:Panel>


</asp:Content>
