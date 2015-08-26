<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
                <h2></h2>
            </hgroup>
            <p>
                This site contains the various information required to implement Realex payments into your solutions.
            <br />
                Only need RealexPayments.dll, pages show how to call each of the setup functions.
            </p>
        </div>
    </section>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Sections:</h3>
    <ol class="round">
        <li class="one">
            <h5>RealAuth</h5>
            Basic Realex authentication for taking payments. Includes both 3D Secure and normal.
            <a href="RealAuth/default.aspx">View…</a>
        </li>
        <li class="two">
            <h5>RealVault</h5>
            Allows the storing of users payment details and cards for taking payment later. Contains managment of users and cards as well as taking payments.
            <a href="RealVault/default.aspx">Learn more…</a>
        </li>
    </ol>
</asp:Content>