<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="viewtranscript.aspx.cs" Inherits="TranscriptForUniben.viewtranscript" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
    <div class="text-center">
      <div class="wow bounceInDown" data-wow-offset="0" data-wow-delay="0.3s">
        <h3>UNIBEN CENTRAL TRANSCRIPT SYSTEM</h3>
      </div>
      <div class="wow bounceInDown" data-wow-offset="0" data-wow-delay="0.6s">
        <h2>
            <asp:Label ID="lbusername" runat="server" Text=""></asp:Label> Please Search For Transcript</h2> 
      </div>
    </div>
  </div>
     <div class="contact-form">
    <div class="container">
      <div class="col-md-8 col-md-offset-2">
        <div id="sendmessage">Your message has been sent. Thank you!</div>
        <div id="errormessage"></div>
    
    <div class="validation"></div>
             <div class="form-group">
              <asp:TextBox ID="txtmatnumber" class="form-control" Placeholder="Enter The Student's MAT Number" runat="server"></asp:TextBox>
            <div class="validation"></div>
          </div>
          <div class="form-group">
              <asp:Button ID="btnsearch" runat="server" Text="Search Transcript" CssClass="btn btn-primary" OnClick="btnsearch_Click" />
              <div class="validation"></div>
          </div>
     <div class="form-group">
               <p style="color:black;">Select Transcript:</p>
               <asp:DropDownList ID="ddltranscripts" class="form-control" runat="server">
               </asp:DropDownList>
          </div>
        
                 
               <div class="text-center"> <asp:Button ID="btnsubmit" class="btn btn-primary btn-lg" runat="server" Text="SUBMIT" OnClick="btnsubmit_Click" />
                              </div>
       
  </div>
          </div><asp:Label ID="lbmsg" style= "color:green;" class="form-control" runat="server"  ><strong>Text=""</strong></asp:Label>
         </div>
</asp:Content>
