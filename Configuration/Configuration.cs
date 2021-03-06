﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.0.30319.33440.
// 
namespace lafe.ShutdownService.Configuration {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public partial class Configuration {
        
        private TimerConfiguration timerField;
        
        private MonitoredRanges monitoredRangesField;
        
        private MonitoredTime[] monitoredTimesField;
        
        /// <remarks/>
        public TimerConfiguration Timer {
            get {
                return this.timerField;
            }
            set {
                this.timerField = value;
            }
        }
        
        /// <remarks/>
        public MonitoredRanges MonitoredRanges {
            get {
                return this.monitoredRangesField;
            }
            set {
                this.monitoredRangesField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public MonitoredTime[] MonitoredTimes {
            get {
                return this.monitoredTimesField;
            }
            set {
                this.monitoredTimesField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TimerConfiguration {
        
        private string checkIntervalField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="duration")]
        public string CheckInterval {
            get {
                return this.checkIntervalField;
            }
            set {
                this.checkIntervalField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Weekdays {
        
        private object allField;
        
        private object mondayField;
        
        private object tuesdayField;
        
        private object wednesdayField;
        
        private object thursdayField;
        
        private object fridayField;
        
        private object saturdayField;
        
        private object sundayField;
        
        /// <remarks/>
        public object All {
            get {
                return this.allField;
            }
            set {
                this.allField = value;
            }
        }
        
        /// <remarks/>
        public object Monday {
            get {
                return this.mondayField;
            }
            set {
                this.mondayField = value;
            }
        }
        
        /// <remarks/>
        public object Tuesday {
            get {
                return this.tuesdayField;
            }
            set {
                this.tuesdayField = value;
            }
        }
        
        /// <remarks/>
        public object Wednesday {
            get {
                return this.wednesdayField;
            }
            set {
                this.wednesdayField = value;
            }
        }
        
        /// <remarks/>
        public object Thursday {
            get {
                return this.thursdayField;
            }
            set {
                this.thursdayField = value;
            }
        }
        
        /// <remarks/>
        public object Friday {
            get {
                return this.fridayField;
            }
            set {
                this.fridayField = value;
            }
        }
        
        /// <remarks/>
        public object Saturday {
            get {
                return this.saturdayField;
            }
            set {
                this.saturdayField = value;
            }
        }
        
        /// <remarks/>
        public object Sunday {
            get {
                return this.sundayField;
            }
            set {
                this.sundayField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class MonitoredTime {
        
        private Weekdays weekdaysField;
        
        private System.DateTime startTimeField;
        
        private System.DateTime endTimeField;
        
        /// <remarks/>
        public Weekdays Weekdays {
            get {
                return this.weekdaysField;
            }
            set {
                this.weekdaysField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="time")]
        public System.DateTime StartTime {
            get {
                return this.startTimeField;
            }
            set {
                this.startTimeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="time")]
        public System.DateTime EndTime {
            get {
                return this.endTimeField;
            }
            set {
                this.endTimeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class MonitoredRange {
        
        private RangeType typeField;
        
        private string addressField;
        
        /// <remarks/>
        public RangeType Type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
        
        /// <remarks/>
        public string Address {
            get {
                return this.addressField;
            }
            set {
                this.addressField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    public enum RangeType {
        
        /// <remarks/>
        Ip,
        
        /// <remarks/>
        Dns,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class MonitoredRanges {
        
        private MonitoredRange[] monitoredRangeField;
        
        private string networkTimeoutField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("MonitoredRange")]
        public MonitoredRange[] MonitoredRange {
            get {
                return this.monitoredRangeField;
            }
            set {
                this.monitoredRangeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="duration")]
        public string NetworkTimeout {
            get {
                return this.networkTimeoutField;
            }
            set {
                this.networkTimeoutField = value;
            }
        }
    }
}
