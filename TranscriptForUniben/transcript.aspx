<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="transcript.aspx.cs" Inherits="TranscriptForUniben.transcript" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
    <div class="text-center">
      <div class="wow bounceInDown" data-wow-offset="0" data-wow-delay="0.3s">
        <h3>UNIBEN CENTRAL TRANSCRIPT SYSTEM</h3>
      </div>
      <div class="wow bounceInDown" data-wow-offset="0" data-wow-delay="0.6s">
        <h2>Please Apply For Transcript</h2> 
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
              <asp:TextBox ID="txtinstitution" class="form-control" Placeholder="Enter The Requesting Institution" runat="server"></asp:TextBox>
            <div class="validation"></div>
          </div>
     <div class="form-group">
               <p style="color:black;">Select The Start Sesssion:</p>
               <asp:DropDownList ID="ddlstartsession" class="form-control" runat="server">
               </asp:DropDownList>
          </div>
        <div class="form-group">
               <p style="color:black;">Select The End Sesssion:</p>
               <asp:DropDownList ID="ddlendsession" class="form-control" runat="server">
               </asp:DropDownList>
          </div>
           <div class="form-group">
              <asp:TextBox ID="txtdetails" class="form-control" Placeholder="Enter Additional Details" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
            <div class="validation"></div>
          </div>
                 
               <div class="text-center"> <asp:Button ID="btnsubmit" class="btn btn-primary btn-lg" runat="server" Text="SUBMIT" OnClick="btnsubmit_Click" />
                              </div>
       
  </div>
          </div><asp:Label ID="lbmsg" style= "color:green;" class="form-control" runat="server"  ><strong>Text=""</strong></asp:Label>

</asp:Content>
