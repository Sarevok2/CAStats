<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<CAStats.ViewModels.CAIndexViewModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link rel="stylesheet" type="text/css" href="/Content/anytime.css" />
    <link type="text/css" href="/Content/Site.css" rel="stylesheet" />

    <script type="text/javascript">
          <% Response.Write("var jobHash = " + Model.toJavascript() + ";"); %>
    </script>

    <script type="text/javascript" src="/Scripts/jquery-1.4.1.js"></script>
    <script type="text/javascript" src="/Scripts/anytimec.js"></script>
    <script type="text/javascript" src="/Scripts/Scripts.js"></script>
    <script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
    
    <title>Index</title>
</head>
<body>
    <div class="controls">
    <% using (Ajax.BeginForm("ShowData", null, new AjaxOptions { UpdateTargetId = "viewingArea" }, new { name = "controlsForm" }))
       { %>
        <table><tr>
            <td><fieldset class="controls-fieldset" >
                <legend>Job Selector</legend>
                <div class="div-float-left">
                    <table class="controls-radio-table">
                        <tr>
                            <td><input type="radio" name="environment" value="pr" onclick="updateJobsetList(jobHash)" checked="checked" />Prod</td>
                            <td><input type="radio" name="environment" value="ig" onclick="updateJobsetList(jobHash)" />IG</td>
                            <td><input type="radio" name="environment" value="13" onclick="updateJobsetList(jobHash)" />13</td>
                        </tr>
                        <tr>
                            <td class="middle"><input type="radio" name="system" value="rb" onclick="updateJobsetList(jobHash)" checked="checked" />BDS</td>
                            <td class="middle"><input type="radio" name="system" value="rf" onclick="updateJobsetList(jobHash)" />SMS</td>
                            <td class="middle"><input type="radio" name="system" value="other" onclick="updateJobsetList(jobHash)" />Other</td>
                        </tr>
                        <tr>
                            <td class="middle"><input type="radio" name="jobType" value="Batch" onclick="updateJobsetList(jobHash)" checked="checked" />Batch</td>
                            <td class="middle"><input type="radio" name="jobType" value="Report" onclick="updateJobsetList(jobHash)" />Report</td>
                            <td class="middle"></td>
                        </tr>
                        <tr>
                            <td class="middle"><input type="radio" name="jobsOrJobsets" value="jobs" checked="checked" onclick="jobsOrJobsetsOnClick()" />Jobs</td>
                            <td class="middle"><input type="radio" name="jobsOrJobsets" value="jobsets" onclick="jobsOrJobsetsOnClick()"/>Jobsets</td>
                            <td class="middle"></td>
                        </tr>
                    </table>
                </div>
                <div class="div-float-right">
                    <ul>
                        <li><input class="job-search-box" type="text" name="Search" id="searchbox" onchange="searchJobs(jobHash, searchbox.value)" />
                        <button type="button" onclick="searchJobs(jobHash, searchbox.value)">Search</button></li>
               
                        <li>Jobset: <select name="jobset" class="jobSelectorDropdown" onchange="updateJobnameList(jobHash)">
                            <option value="all">All</option>
                        </select></li>
                    
                        <li>Job: <select name="jobname" id="jobnameSelector" class="jobSelectorDropdown">
                            <option value="all">All</option>
                        </select></li>

                        <li><input type="checkbox" id="abortedOnly" />Show only aborted</li>
                    </ul>
                </div>
            </fieldset></td>

            <td><fieldset class="controls-fieldset">
                <legend>Date/Time</legend>
                <table>
                <tr>
                    <td>Start Date: </td>
                    <td><input class="dateTimeField" id="startDate" type="text" /></td>
                    <td><div class="padding-left">Preset Date Range:</div></td>
                </tr>
                <tr>
                    <td>End Date: </td>
                    <td><input class="dateTimeField" id="endDate" type="text" /></td>
                    <td><div class="padding-left"><select name="dateRange" onchange="presetDate()">
                        <option value="na">--Select--</option>
                        <option value="day">Past Day</option>
                        <option value="week">Past Week</option>
                        <option value="month">Past Month</option>
                        <option value="year">Past Year</option>
                    </select></div></td>
                </tr>
                <tr>
                    <td>Start Time: </td>
                    <td><input class="dateTimeField" type="text" id="startTime" value="00:00" /></td>
                    <td><div class="padding-left">Preset Time Range: </div></td>
                </tr>
                <tr>
                    <td>End Time: </td>
                    <td><input class="dateTimeField" type="text" id="endTime" value="23:59" /></td>
                    <td><div class="padding-left"><select name="timeRange" onchange="presetTime()">
                        <option value="all">All Day</option>
                        <option value="morning">Morning (6AM-12PM)</option>
                        <option value="afternoon">Afternoon (12PM-5PM)</option>
                        <option value="evening">Evening (5PM-12AM)</option>
                        <option value="night">Overnight (12AM-6AM)</option>
                    </select></div></td>
                </tr>
            </table>
            </fieldset></td>
		    
            <td><fieldset class="controls-fieldset">
                <legend>Chart Options</legend>
                <table cellspacing="3"><tr>
                    <td>
                        Type:<br/>
                        <input type="radio" name="chartType" value="Line" checked="checked" />Line<br/>
                        <input type="radio" name="chartType" value="Bar" />Bar<br/>
                        <input type="radio" name="chartType" value="Scatter" />Scatter<br/>
                     </td>
                    <td>
                        Size:<br/>
                        <input type="radio" name="chartSize" value="small" checked="checked" />Small<br/>
                        <input type="radio" name="chartSize" value="med" />Medium<br/>
                        <input type="radio" name="chartSize" value="large" />Large<br/>
                    </td>
                    <td>
                        Y Axis<br/>
                        <input type="radio" name="yAxis" value="duration" checked="checked" />Duration<br/>
                        <input type="radio" name="yAxis" value="time" />Time<br/>
                    </td>
                </tr></table>
            </fieldset></td>

            <td><fieldset>
                <legend>Display Data</legend>
	            <input type="submit" class="data-button" name="submit" value="<%: CAStats.ViewModels.CAIndexViewModel.SUBMIT_LIST %>" /><br />
                <input type="submit" class="data-button" name="submit" value="<%: CAStats.ViewModels.CAIndexViewModel.SUBMIT_CHART %>" /><br />
                <input type="submit" class="data-button" name="submit" value="<%: CAStats.ViewModels.CAIndexViewModel.SUBMIT_RUNBOOK %>" /><br />
            </fieldset></td>
        </tr></table>
    <% } %>
    </div>
    <br />
    <div class="viewingArea" id="viewingArea"></div>
</body>
</html>
