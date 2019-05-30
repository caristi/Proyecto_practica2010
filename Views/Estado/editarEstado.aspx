<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Sigacun.Models.estados>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Editar Estado
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Estado</legend>
            
            <div>Nombre estado</div>
            <div>
             <%= Html.DropDownList("est_id") %>
            </div>
            
            <div>Descripción</div>
            <div>
                <%= Html.TextBoxFor(model => model.est_descripcion) %>
                <%= Html.ValidationMessageFor(model => model.est_descripcion) %>
            </div>
            <br />
            <p>
                <input type="submit" value="Guardar" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%= Html.ActionLink("Atras", "inicioestado") %>
    </div>

</asp:Content>

