# The Great FollowUpper

## What is The Great FollowUpper?

The Great FollowUpper is an Outlook plug in that enables you to do great follow up, by leveraging Outlook's native Flagging & Categories.

When you send a mail that will require follow up, the following pop-up is shown, where you can specify when the Flag should be set, an optional reminder and what Category the mail should be set to.

![Main Window](docs/mainwindow.png)

Additionally, it adds extensions to the context menu of an Email, Appointment and RSS Item for shortcuts.

<center>
<img src="docs/contextmenu.png" width="150px">
</center>

## Installation

Installation can be tricky (as I do not have a trusted certificate to sign the plugin with), especially on enterpise workstations that put restrictions on what end users can do. The following works 'most of the time'.

1. Download the [Trust Prompt Tool](https://www.smartlux.com/software/trust-prompt-tool/), and Enable all prompts (Read from registry > click Enable on all dropdowns > Write to registry). When installing applications from unknown publishers (like me) it will _prompt_ you, instead of flat out refusing to install. Like this:

<center>
<img src="https://www.smartlux.com/wp-content/uploads/2018/10/Trust-Prompt-do-you-want-to-install-this-application.png" width="400px">
</center>

2. Download the ZIP of the [latest release](https://github.com/woutervanranst/TheGreatFollowUpper/releases/latest), extract the ZIP, double click the setup.exe file.

Alternate options, if you're out of luck:
* https://stackoverflow.com/a/45468516/1582323
* https://stackoverflow.com/a/61909573/1582323 with the certificate found under /code/TheGreatFollowUpper/TheGreatFollowUpper_TemporaryKey.pfx
* https://docs.microsoft.com/en-us/visualstudio/vsto/how-to-configure-inclusion-list-security?view=vs-2019#enable-the-inclusion-list


## Settings

## Advanced Features

* Use CTRL+Click on the Context Menu to open the main window