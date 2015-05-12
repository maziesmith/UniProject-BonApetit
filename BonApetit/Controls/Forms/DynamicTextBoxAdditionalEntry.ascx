<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DynamicTextBoxAdditionalEntry.ascx.cs" Inherits="BonApetit.Controls.Forms.DynamicTextBoxAdditionalEntry" %>

<div class="input-group">
    <asp:TextBox runat="server" ID="Entry" TextMode="SingleLine" CssClass="form-control" />
    <span class="input-group-btn">
        <button class="btn btn-default" type="button" runat="server" onserverclick="RemoveAdditionalEntry_ServerClick" causesvalidation="false"><span class="glyphicon glyphicon-minus"></span></button>
    </span>
</div>