# Azure DevOps Pipeline Monitor

[![Build Status](https://oatsoda.visualstudio.com/buildmonitor/_apis/build/status/oatsoda.BuildMonitor?branchName=master)](https://oatsoda.visualstudio.com/buildmonitor/_build/latest?definitionId=1&branchName=master)

A tray application to monitor Pipeline statuses on your Azure DevOps project.

[Latest installer](https://github.com/oatsoda/BuildMonitor/raw/master/Binaries/BuildMonitor.Setup.msi)

![Screenshot](https://raw.githubusercontent.com/oatsoda/BuildMonitor/master/screenshot.png)


# Authentication

Use Personal Access Tokens:
- Create Personal Access Token in Azure DevOps by going to the Account Menu item on the top toolbar -> Personal Access Tokens
- Require Read permission for Pipelines only.

# Conversion to .NET / TODO

- Fix icons looking low-quality
- Reset all settings option to aid debugging new install scenarios
- Rename Tfs/TfsOnline to ADO

- Decide on how to version. Could hardcode in the Directory.Build.props, or could just pass -p:Version=1.0.0 to the dotnet build command.
If the latter, will need to avoid the upgrage check on app startup when running Debug builds.

- Either fix up WiX installer, or thing of alternative approach.
    - What does installer need to do: just deploy files or other things? Is it just to aid updates?
	- Could I use .NET to package single file? User puts that somewhere and upgrade somehow on startup renames/swaps in? Is this possible?

