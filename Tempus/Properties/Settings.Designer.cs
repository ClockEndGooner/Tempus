﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Tempus.WindowPlacement;

namespace Tempus.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.10.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"
                    <WindowPlacement xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                        <length>0</length>
                        <flags>0</flags>
                        <showCmd>0</showCmd>
                        <minPosition>
                            <X>0</X>
                            <Y>0</Y>
                        </minPosition>
                        <maxPosition>
                            <X>0</X>
                            <Y>0</Y>
                        </maxPosition>
                        <normalPosition>
                            <Left>100</Left>
                            <Top>100</Top>
                            <Right>425</Right>
                            <Bottom>425</Bottom>
                        </normalPosition>
                    </WindowPlacement>
                ")]
        public global::Tempus.WindowPlacement.WindowPlacement WindowPlacement
        {
            get
            {
                return ((global::Tempus.WindowPlacement.WindowPlacement)(this["WindowPlacement"]));
            }
                
            set
            {
                this["WindowPlacement"] = value;
            }
        }
    }
}
