<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if (Request.IsAuthenticated) {
%>
        <h6><%= Html.Encode(Page.User.Identity.Name)%>
        <%= Html.ActionLink("Cerrar Sesión", "LogOff", "Usuario")%></h6>
<%
    }
%> 
