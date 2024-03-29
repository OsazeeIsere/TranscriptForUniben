﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="updateuser.aspx.cs" Inherits="TranscriptForUniben.updateuser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container">
    <div class="text-center">
      <div class="wow bounceInDown" data-wow-offset="0" data-wow-delay="0.3s">
        <h3>UNIBEN CENTRAL TRANSCRIPT SYSTEM</h3>
      </div>
      <div class="wow bounceInDown" data-wow-offset="0" data-wow-delay="0.6s">
        <h2>Please Update User Info</h2>
      </div>
    </div>
  </div>
     <div class="contact-form">
    <div class="container">
      <div class="col-md-8 col-md-offset-2" style="background-color: #FFFFFF">
        <div id="sendmessage">Your message has been sent. Thank you!</div>
        <div id="errormessage"></div>
       
          <div class="form-group">
              <asp:TextBox ID="txtsearch" class="form-control" Placeholder="Enter The User Name" runat="server" BackColor="#CCCCCC" BorderColor="White" BorderStyle="Double" ForeColor="#333333"></asp:TextBox>
            <div class="validation"></div>
          </div>
          <div class="form-group">
              <asp:Button ID="btnsearch" class="btn btn-primary" runat="server" Text="Search" OnClick="btnsearch_Click" />
              <div class="validation"></div>
          </div>
          <div class="form-group">
              <h3>Select User:</h3>
              <asp:DropDownList ID="ddlnames" class="form-control" runat="server" BorderColor="#FF8080" BorderStyle="Double" AutoPostBack="True" OnSelectedIndexChanged="ddlnames_SelectedIndexChanged"></asp:DropDownList>
                
             <div class="validation"></div>
          </div>
          <div class="form-group">
              <asp:TextBox ID="txtsurname" class="form-control" Placeholder="Please Enter Surname" runat="server" TextMode="MultiLine"></asp:TextBox>
             <div class="validation"></div>
          </div>
           <div class="form-group">
              <asp:TextBox ID="txtothernames" class="form-control" Placeholder="Enter The Other Names" runat="server" BorderColor="#FF8080" BorderStyle="Double"></asp:TextBox>
            <div class="validation"></div>
          </div>
          <div class="form-group">
                <asp:TextBox ID="txtemail" class="form-control" Placeholder="Enter The Email Address" runat="server" BorderColor="#FF8080" BorderStyle="Double"></asp:TextBox>
             <div class="validation"></div>
          </div>
           <div class="form-group">
                <asp:TextBox ID="txtusername" class="form-control" Placeholder="Enter The Username" runat="server" BorderColor="#FF8080" BorderStyle="Double"></asp:TextBox>
             <div class="validation"></div>
          </div>
          <div class="form-group">
                <asp:TextBox ID="txtpassword" class="form-control" Placeholder="Enter The Password" runat="server" BorderColor="#FF8080" BorderStyle="Double" TextMode="Password"></asp:TextBox>
             <div class="validation"></div>
          </div>
          <div class="form-group">
              <h3>Select Department:</h3>
              <asp:DropDownList ID="ddldepartment" class="form-control" runat="server" BorderColor="#FF8080" BorderStyle="Double"></asp:DropDownList>
                
             <div class="validation"></div>
          </div>
          <div class="form-group">
              <h3>Select User Role:</h3>
              <asp:DropDownList ID="ddluserrole" class="form-control" runat="server" BorderColor="#FF8080" BorderStyle="Double"></asp:DropDownList>  
              
             <div class="validation"></div>
          </div>

           
                               
          <div class="text-center"> <asp:Button ID="btnenter" class="btn btn-primary btn-lg" runat="server" Text="Enter" OnClick="btnenter_Click" /></div>
      </div>
    </div>
  </div>
     <asp:Label ID="lbusername" runat="server" Text=""></asp:Label>
    <asp:Label ID="lbmsg" style= "color:green;"  class="form-control" runat="server"  ><strong>Text=""</strong></asp:Label>

</asp:Content>
