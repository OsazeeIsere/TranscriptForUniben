<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="generatetranscript.aspx.cs" Inherits="TranscriptForUniben.generatetranscript" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <style>
         .items {
             background-color:white;
             padding:20px;
             margin:25px;
             width:25%;
          
             border-radius:5px;
         }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <a href="dashboard.aspx">Go Back</a>
            <asp:Button ID="btnverify" runat="server" Text="Verify Transcript" Visible="False" OnClick="btnverify_Click" />
            <asp:Button ID="btnapprove" runat="server" Text="Approve Transcript" Visible="False" OnClick="btnapprove_Click" />
            <asp:Button ID="btnviewappovedtranscript" runat="server" Text="View Approved Transcript" Visible="False" OnClick="btnviewappovedtranscript_Click" />
            <div runat="server" id="divprint" visible="false">
                <button id="btnprint" onclick="document.print">Print Transcript</button>
            </div>
            <div runat="server" id="divcoment" visible="False">

                <asp:TextBox ID="txtcoment" runat="server" Height="71px" TextMode="MultiLine" Width="430px"></asp:TextBox>

                <br />
                <asp:Button ID="btncoment" runat="server" Text="Forward Comment to Transcript Unit" OnClick="btncoment_Click" />
                <asp:Button ID="btnapprovalcoment" runat="server" OnClick="Button1_Click" Text="Forward Comment to Transcript Unit" />
                            <asp:Label ID="lbfeedback" runat="server"></asp:Label>

            </div>
        </div>
        <asp:Panel ID="pnltranscript" runat="server"></asp:Panel>
        <asp:Image ID="imgSignature" runat="server" Height="72px" class="items" ImageUrl="signature.PNG" Width="105px" />
    </form>
</body>
</html>
