# Azure DevOps Pipeline Monitor

[![Build Status](https://oatsoda.visualstudio.com/buildmonitor/_apis/build/status/oatsoda.BuildMonitor?branchName=master)](https://oatsoda.visualstudio.com/buildmonitor/_build/latest?definitionId=1&branchName=master)

A tray application to monitor Pipeline statuses on your Azure DevOps project.

[Latest installer](https://github.com/oatsoda/BuildMonitor/raw/master/Binaries/BuildMonitor.Setup.msi)

![Screenshot](https://raw.githubusercontent.com/oatsoda/BuildMonitor/master/screenshot.png)

# Authentication

Use Personal Access Tokens:
- Create Personal Access Token in Azure DevOps by going to the Account Menu item on the top toolbar -> Personal Access Tokens
- Require Read permission for Pipelines only.

# Approach

The app will give you an overall status of your pipelines. That is, it will display the worst status of your monitored pipeline definitions. It will popup a 
notification if that changes.  This means it is not informing you of every pipeline run, but rather the overall status of your pipelines.

# Build Dependencies

- Heatwave Wix Toolset
    - https://marketplace.visualstudio.com/items?itemName=FireGiant.FireGiantHeatWaveDev17
