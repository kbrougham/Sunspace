﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="JSONTest2.aspx.cs" Inherits="SunspaceDealerDesktop.JSONTest2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SecondaryNavigation" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModOverlay" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        
        var wallCount = '<%= wallCount %>';
        
        for (var i = 0; i < wallCount; i++) { 
            var jsonString = $('#hidWall' + i + 'Info').val();
            console.log(jsonString);
        }

        //var jsonString = <%--hidRealhidden.value--%>;
        //console.log(jsonstring);


        function runme()
        {
            jsonString.Name = "Ass";
            document.getElementById("<%=hidRealHidden.ClientID%>").value = JSON.stringify(jsonString);
        }
    </script>

    <asp:Button ID="btnFuck" runat="server" OnClick="btnFuck_Click" />
    <input id="hidRealHidden" type="hidden" runat="server" />
    <div id="hidWallInfo" runat="server"></div>
</asp:Content>
