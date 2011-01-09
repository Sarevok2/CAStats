<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CAStats.Models.JobDataModel>" %>

    <table class="jobTable">
        <tr>
            <th>Jobset</th>
            <th>Job Name</th>
            <th>Qualifier</th>
            <th>Start date</th>
            <th>End Date</th>
            <th>Status</th>
        </tr>

    <% foreach (var jobset in Model.jobsets)
       {
           foreach (var job in jobset.jobs)
           {%>
    
        <tr>
            <td><%: job.jobset%></td>
            <td><%: job.jobname%></td>
            <td><%: job.qualifier%></td>
            <td><%: String.Format("{0:g}", job.start_date)%></td>
            <td><%: String.Format("{0:g}", job.end_date)%></td>
            <td><%: job.status.Equals("C") ? "Completed" : "Aborted" %></td>
        </tr>
    <%  }
    } %>
 

    </table>


