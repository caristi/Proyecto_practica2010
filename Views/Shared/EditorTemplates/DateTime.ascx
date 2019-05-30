<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DateTime?>" %>  
  
<%string name = ViewData.TemplateInfo.HtmlFieldPrefix;%>  
<%string id = name.Replace(".", "_");%>
<div>
    <%= Html.TextBoxFor(model => model) %>
</div> 
  
<script type="text/javascript">  
    $(document).ready(function() {  
  
        $("#<%=id%>").datepicker({  
            
            dateFormat: 'dd M yy',  
            changeMonth: true,
            changeYear: true,  
            showOn: 'button',
            buttonImage: '../../../Content/jquery-ui/redmond/images/calendar.png',
			buttonImageOnly: true

        });  
    });  
</script> 