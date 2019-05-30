<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Sigacun.Models.cargos>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Registrar cargo
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary("Ingrese correctamente los datos") %>

         <fieldset>
            <legend>Registrar cargo</legend>

            <div>Nombre de cargo</div>
            <div>
                <%= Html.TextBoxFor(model => model.car_nombre, new { style = "width:380px;" })%>
                <%= Html.ValidationMessageFor(model => model.car_nombre) %>
            </div>
            <br />
            <p>
                <input type="submit" value="Guardar" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%= Html.ActionLink("Atras", "iniciocargo") %>
    </div>

</asp:Content>

