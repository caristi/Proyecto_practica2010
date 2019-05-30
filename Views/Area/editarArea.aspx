<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Sigacun.Models.areas>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Editar Area
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Edición de área</legend>
            
            <div>Area Mesa de Ayuda</div>
            <div>
            <%= Html.DropDownList("are_id",null, new { style = "width:250px;" }) %>
            </div>
            
            <div>Nombre área</div>
            <div>
                <%= Html.TextBoxFor(model => model.are_nombre, new { style = "width:250px;" })%>
                <%= Html.ValidationMessageFor(model => model.are_nombre) %>
            </div>
            <div>Supervisor de Área</div>
            <div>
              <%= Html.DropDownList("are_supervisor", (SelectList)ViewData["supervisor"], new { style = "width:250px;" })%>
            </div>
            <br />
            <p>
                <input type="submit" value="Guardar" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%= Html.ActionLink("Atras", "inicioArea") %>
    </div>

</asp:Content>

