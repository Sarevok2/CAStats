var searchHash;
var useSearchHash = false;

$(document).ready(function () {
    $("#startTime").AnyTime_picker(
                { format: "%H:%i", labelTitle: "Time", labelHour: "Hour", labelMinute: "Minute" }
            );
    $("#endTime").AnyTime_picker(
                { format: "%H:%i", labelTitle: "Time", labelHour: "Hour", labelMinute: "Minute" }
            );
    $("#startDate").AnyTime_picker(
                { format: "%Y/%c/%d", labelTitle: "Date", labelYear: "Year", labelMonth: "Month", labelDay: "Day" }
            );
    $("#endDate").AnyTime_picker(
                { format: "%Y/%c/%d", labelTitle: "Date", labelYear: "Year", labelMonth: "Month", labelDay: "Day" }
            );
    updateJobsetList(jobHash);
});

function jobsOrJobsetsOnClick() {
    radioGrp = document.controlsForm.jobsOrJobsets;
    if (radioGrp[0].checked) {
        document.getElementById("abortedOnly").disabled = false;
        document.getElementById("jobnameSelector").disabled = false;
    }
    else if (radioGrp[1].checked) {
        document.getElementById("abortedOnly").disabled = true;
        document.getElementById("jobnameSelector").disabled = true;
    }
}

function presetDate() {
    startDateField = document.getElementById("startDate");
    endDateField = document.getElementById("endDate");
    var chosenOption = document.controlsForm.dateRange.options[document.controlsForm.dateRange.selectedIndex];

    if (chosenOption.value != "na") {
        endDateField.value = createDateString(0);
        if (chosenOption.value == "day") { startDateField.value = createDateString(1); }
        if (chosenOption.value == "week") { startDateField.value = createDateString(7); }
        if (chosenOption.value == "month") { startDateField.value = createDateString(31); }
        if (chosenOption.value == "year") { startDateField.value = createDateString(365); }
    }
}

function presetTime() {
    startTimeField = document.getElementById("startTime");
    endTimeField = document.getElementById("endTime");
    var chosenOption = document.controlsForm.timeRange.options[document.controlsForm.timeRange.selectedIndex];

    if (chosenOption.value == "all") {
        startTimeField.value = "00:00";
        endTimeField.value = "23:59";
    }
    else if (chosenOption.value == "morning") {
        startTimeField.value = "06:00";
        endTimeField.value = "12:00";
    }
    else if (chosenOption.value == "afternoon") {
        startTimeField.value = "12:00";
        endTimeField.value = "17:00";
    }
    else if (chosenOption.value == "evening") {
        startTimeField.value = "17:00";
        endTimeField.value = "23:59";
    }
    else if (chosenOption.value == "night") {
        startTimeField.value = "00:00";
        endTimeField.value = "06:00";
    }
}

function createDateString(daysBack) {
    currentDate = new Date();
    currentDate.setDate(currentDate.getDate() - daysBack);
    return currentDate.getFullYear() + "/" + currentDate.getMonth() + "/" + currentDate.getDate();
}

function envRadio() {
    if (document.controlsForm.environment[0].checked) { return "pr"; }
    else if (document.controlsForm.environment[1].checked) { return "ig"; }
    else if (document.controlsForm.environment[2].checked) { return "13"; }
}

function sysRadio() {
    if (document.controlsForm.system[0].checked) { return "rb"; }
    else if (document.controlsForm.system[1].checked) { return "rf"; }
    else { return "other"; }
}

function jobTypeRadio() {
    if (document.controlsForm.jobType[0].checked) { return "batch"; }
    else if (document.controlsForm.jobType[1].checked) { return "report"; }
}

function updateJobsetList(jobs) {
    useSearchHash = false;
    document.controlsForm.jobset.options.length=1;
    var jobsetList = jobs[envRadio()][sysRadio()][jobTypeRadio()];
    for (var js in jobsetList) {
        addOption(document.controlsForm.jobset, js, js);
    }
    updateJobnameList(jobs);
}

function updateJobnameList(jobs) {
    var jobset = document.controlsForm.jobset.options[document.controlsForm.jobset.selectedIndex].value;
    document.controlsForm.jobname.options.length = 1;
    var jobList;
    if (useSearchHash) {jobList = searchHash[jobset];}
    else {jobList = jobs[envRadio()][sysRadio()][jobTypeRadio()][jobset]; }

    for (var i in jobList) {
        addOption(document.controlsForm.jobname, jobList[i], jobList[i]);
    }
}

function searchJobs(jobs, searchString) {
    searchHash = new Array();
    useSearchHash = true;
    document.controlsForm.jobset.options.length = 1;
    for (var env in jobs) {
        for (var sys in jobs[env]) {
            for (var jobType in jobs[env][sys]) {
                for (var jobset in jobs[env][sys][jobType]) {
                    if (jobset.toUpperCase().indexOf(searchString.toUpperCase()) != -1) {
                        addOption(document.controlsForm.jobset, jobset, jobset);
                        searchHash[jobset] = jobs[env][sys][jobType][jobset];
                    }
                    else {
                        for (var i in jobs[env][sys][jobType][jobset]) {
                            if (jobs[env][sys][jobType][jobset][i].toUpperCase().indexOf(searchString.toUpperCase()) != -1) {
                                addOption(document.controlsForm.jobset, jobset, jobset);
                                searchHash[jobset] = jobs[env][sys][jobType][jobset];
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
    updateJobnameList(jobs);
}

function addOption(selectBox,text,value ){
	var optn = document.createElement("option");
	optn.text = text;
	optn.value = value;
	var defaultPattern =/CMAH/i;
	if (!defaultPattern.test(value)){return;}
	selectBox.options.add(optn);
}

function test(jobs) {
    alert("done");
    var jobset = document.controlsForm.jobset.options[document.controlsForm.jobset.selectedIndex].value;
    
}