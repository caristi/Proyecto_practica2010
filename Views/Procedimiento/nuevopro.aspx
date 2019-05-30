<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Sigacun.Models.procedimiento>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Nuevo Procedimiento
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% using (Html.BeginForm("Upload", "Procedimiento", FormMethod.Post, new { enctype = "multipart/form-data" }))
       {%>
        <%= Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Procedimiento</legend> 
<center>
         <table>
           <tr>
                <td>
                    <div>Código UN</div>
                    <div>
                    <%= Html.TextBoxFor(model => model.pro_codigoun, new { style = "width:150px;" })%>
                    <%= Html.ValidationMessageFor(model => model.pro_codigoun) %>
                    </div>
                </td>
                <td>
                   <div>Nombre</div>
                   <div>
                    <%= Html.TextBoxFor(model => model.pro_nombre, new { style = "width:380px;" })%>
                    <%= Html.ValidationMessageFor(model => model.pro_nombre) %>
                   </div> 
                </td>
                <td>
                    <div>Área</div>
                    <div>
                    <%= Html.DropDownList("are_id", (SelectList)ViewData["areas"], "-Seleccionar uno-", new { style = "width:250px;" })%>
                    </div>                
                </td>
           </tr>
           </table>

           <table>
           <tr>
                <td>
                   <div>Descripción</div>
                   <div>
                    <%= Html.TextAreaFor(model => model.pro_describcion,new { style = "width:815px;" }) %>
                    <%= Html.ValidationMessageFor(model => model.pro_describcion) %>
                   </div>  
                </td>
           </tr>           
           </table>
           <table>
            <tr>
                <td>
                     <div>Adjuntar Archivo - MAXIMO 4M</div>
                     <input type="file" id="arp_ruta" name="arp_ruta" value="Examinar" />          
                </td>
            </tr>
           
           </table>

           
            <%= Html.Hidden("arp_fechacrea", ViewData["hora"])%>
            <!-- Variable de estado value en 1!-->
            <%= Html.Hidden("pro_activo", 1)%>
               
            <br />
            <p>
            <br />
                <input type="submit" value="Guardar" />
            </p>
</center>                                  
        </fieldset>


    <% } %>

    <div>
        <%= Html.ActionLink("Atras", "iniciopro") %>
    </div>

</asp:Content>

