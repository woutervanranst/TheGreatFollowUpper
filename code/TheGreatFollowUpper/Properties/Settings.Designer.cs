﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel;

namespace TheGreatFollowUpper.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }

        [Category("Pop-Up")]
        [Description("Default value of the 'Move to Inbox' checkbox")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool DefaultMoveToInbox {
            get {
                return ((bool)(this["DefaultMoveToInbox"]));
            }
            set {
                this["DefaultMoveToInbox"] = value;
            }
        }

        [Category("Pop-Up")]
        [Description("Reminder time")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("08:30:00")]
        public global::System.TimeSpan DefaultOtherDayReminder {
            get {
                return ((global::System.TimeSpan)(this["DefaultOtherDayReminder"]));
            }
            set {
                this["DefaultOtherDayReminder"] = value;
            }
        }

        [Category("Pop-Up")]
        [Description("Reminder time for reminders that are to be followed up 'Later today'")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("18:00:00")]
        public global::System.TimeSpan DefaultSameDayReminder {
            get {
                return ((global::System.TimeSpan)(this["DefaultSameDayReminder"]));
            }
            set {
                this["DefaultSameDayReminder"] = value;
            }
        }

        [Browsable(false)]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"[""822c4baaba38162992e2a054f4da03a3"",""20599363c00c20dea8ee1cfdcb1a90e9"",""4bd2bc3a31d7bc0cf514c7c77bab9b31"",""9d197e938ac252d7ec537b348bd191bc"",""3b946df7f73c1dd749cf09d1878e4004"",""ac4dd429914363804b355575f4c1d976"",""78c9cc4dcb6cdad77b904d0980d901c9"",""f3b532ce159f09aae1ab7a385fdb078e""]")]
        public string Licenses {
            get {
                return ((string)(this["Licenses"]));
            }
            set {
                this["Licenses"] = value;
            }
        }

        [Category("Misc")]
        [Description("Ignore mails that were sent -x hours ago (eg. from your mobile phone). VALUE MUST BE NEGATIVE.")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("-4")]
        public int IgnoreSentItemsBeforeHours {
            get {
                return ((int)(this["IgnoreSentItemsBeforeHours"]));
            }
            set {
                this["IgnoreSentItemsBeforeHours"] = value;
            }
        }

        [Category("Pop-Up")]
        [Description("Close pop-up automatically after x seconds. Use 0 to disable autoclose.")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("60")]
        public int AutoCloseFormAfterSeconds {
            get {
                return ((int)(this["AutoCloseFormAfterSeconds"]));
            }
            set {
                this["AutoCloseFormAfterSeconds"] = value;
            }
        }

        [Category("Pop-Up")]
        [Description("Default value of the 'Clear Flag On Reply' checkbox")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool DefaultClearFlagOnReply {
            get {
                return ((bool)(this["DefaultClearFlagOnReply"]));
            }
            set {
                this["DefaultClearFlagOnReply"] = value;
            }
        }

        [Category("Misc")]
        [Description("Make a dump of all active tasks in the Documents folder each time Outlook starts. Useful if you accidentally rename a task.")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool DoTaskDump {
            get {
                return ((bool)(this["DoTaskDump"]));
            }
            set {
                this["DoTaskDump"] = value;
            }
        }

        [Category("Pop-Up")]
        [Description("Suppress accidental pressing on of the Cancel button. NEGATIVE VALUE. Special feature for NicolasVBD")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("-2")]
        public int SupressCancelDialogSeconds {
            get {
                return ((int)(this["SupressCancelDialogSeconds"]));
            }
            set {
                this["SupressCancelDialogSeconds"] = value;
            }
        }
    }
}
