<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DynamicTextBox.ascx.cs" Inherits="BonApetit.Controls.Forms.DynamicTextBox" %>

<div class="form-group">
    <asp:Label runat="server" AssociatedControlID="MainEntry" CssClass="col-md-2 control-label" ID="ControlLabel"></asp:Label>
    <div class="col-md-10">
        <div class="input-group">
            <asp:TextBox runat="server" ID="MainEntry" TextMode="SingleLine" CssClass="form-control" />
            <span class="input-group-btn">
                <button class="btn btn-default" type="button" runat="server" onserverclick="AddEntryButton_Click" causesvalidation="false"><span class="glyphicon glyphicon-plus"></span></button>
            </span>
        </div>      
        <asp:PlaceHolder ID="AdditionalEntries" runat="server">
            
        </asp:PlaceHolder>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="MainEntry" CssClass="text-danger" ErrorMessage="This field is required." />
    </div>
</div>