﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.UserServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="StructuralObject", Namespace="http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses", IsReference=true)]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(WebApplication1.UserServiceReference.EntityObject))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(WebApplication1.UserServiceReference.OOCEntityObject))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(WebApplication1.UserServiceReference.User))]
    public partial class StructuralObject : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EntityObject", Namespace="http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses", IsReference=true)]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(WebApplication1.UserServiceReference.OOCEntityObject))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(WebApplication1.UserServiceReference.User))]
    public partial class EntityObject : WebApplication1.UserServiceReference.StructuralObject {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private WebApplication1.UserServiceReference.EntityKey EntityKeyField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public WebApplication1.UserServiceReference.EntityKey EntityKey {
            get {
                return this.EntityKeyField;
            }
            set {
                if ((object.ReferenceEquals(this.EntityKeyField, value) != true)) {
                    this.EntityKeyField = value;
                    this.RaisePropertyChanged("EntityKey");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="OOCEntityObject", Namespace="http://schemas.datacontract.org/2004/07/OOC.Entity", IsReference=true)]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(WebApplication1.UserServiceReference.User))]
    public partial class OOCEntityObject : WebApplication1.UserServiceReference.EntityObject {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="User", Namespace="http://schemas.datacontract.org/2004/07/OOC.Entity", IsReference=true)]
    [System.SerializableAttribute()]
    public partial class User : WebApplication1.UserServiceReference.OOCEntityObject {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string aclField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double balanceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime creationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long idField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string mobileField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> modificationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string passhashField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string usernameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string acl {
            get {
                return this.aclField;
            }
            set {
                if ((object.ReferenceEquals(this.aclField, value) != true)) {
                    this.aclField = value;
                    this.RaisePropertyChanged("acl");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double balance {
            get {
                return this.balanceField;
            }
            set {
                if ((this.balanceField.Equals(value) != true)) {
                    this.balanceField = value;
                    this.RaisePropertyChanged("balance");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime creation {
            get {
                return this.creationField;
            }
            set {
                if ((this.creationField.Equals(value) != true)) {
                    this.creationField = value;
                    this.RaisePropertyChanged("creation");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long id {
            get {
                return this.idField;
            }
            set {
                if ((this.idField.Equals(value) != true)) {
                    this.idField = value;
                    this.RaisePropertyChanged("id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string mobile {
            get {
                return this.mobileField;
            }
            set {
                if ((object.ReferenceEquals(this.mobileField, value) != true)) {
                    this.mobileField = value;
                    this.RaisePropertyChanged("mobile");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> modification {
            get {
                return this.modificationField;
            }
            set {
                if ((this.modificationField.Equals(value) != true)) {
                    this.modificationField = value;
                    this.RaisePropertyChanged("modification");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string passhash {
            get {
                return this.passhashField;
            }
            set {
                if ((object.ReferenceEquals(this.passhashField, value) != true)) {
                    this.passhashField = value;
                    this.RaisePropertyChanged("passhash");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string username {
            get {
                return this.usernameField;
            }
            set {
                if ((object.ReferenceEquals(this.usernameField, value) != true)) {
                    this.usernameField = value;
                    this.RaisePropertyChanged("username");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EntityKey", Namespace="http://schemas.datacontract.org/2004/07/System.Data", IsReference=true)]
    [System.SerializableAttribute()]
    public partial class EntityKey : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EntityContainerNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private WebApplication1.UserServiceReference.EntityKeyMember[] EntityKeyValuesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EntitySetNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EntityContainerName {
            get {
                return this.EntityContainerNameField;
            }
            set {
                if ((object.ReferenceEquals(this.EntityContainerNameField, value) != true)) {
                    this.EntityContainerNameField = value;
                    this.RaisePropertyChanged("EntityContainerName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public WebApplication1.UserServiceReference.EntityKeyMember[] EntityKeyValues {
            get {
                return this.EntityKeyValuesField;
            }
            set {
                if ((object.ReferenceEquals(this.EntityKeyValuesField, value) != true)) {
                    this.EntityKeyValuesField = value;
                    this.RaisePropertyChanged("EntityKeyValues");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EntitySetName {
            get {
                return this.EntitySetNameField;
            }
            set {
                if ((object.ReferenceEquals(this.EntitySetNameField, value) != true)) {
                    this.EntitySetNameField = value;
                    this.RaisePropertyChanged("EntitySetName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EntityKeyMember", Namespace="http://schemas.datacontract.org/2004/07/System.Data")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(WebApplication1.UserServiceReference.User))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(WebApplication1.UserServiceReference.OOCEntityObject))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(WebApplication1.UserServiceReference.EntityKey))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(WebApplication1.UserServiceReference.EntityKeyMember[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(WebApplication1.UserServiceReference.EntityObject))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(WebApplication1.UserServiceReference.StructuralObject))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(string[]))]
    public partial class EntityKeyMember : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string KeyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private object ValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Key {
            get {
                return this.KeyField;
            }
            set {
                if ((object.ReferenceEquals(this.KeyField, value) != true)) {
                    this.KeyField = value;
                    this.RaisePropertyChanged("Key");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public object Value {
            get {
                return this.ValueField;
            }
            set {
                if ((object.ReferenceEquals(this.ValueField, value) != true)) {
                    this.ValueField = value;
                    this.RaisePropertyChanged("Value");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UserServiceReference.IUserService")]
    public interface IUserService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/Hash", ReplyAction="http://tempuri.org/IUserService/HashResponse")]
        string Hash(string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/Auth", ReplyAction="http://tempuri.org/IUserService/AuthResponse")]
        bool Auth(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/UpdatePassword", ReplyAction="http://tempuri.org/IUserService/UpdatePasswordResponse")]
        void UpdatePassword(int id, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/UpdateMobile", ReplyAction="http://tempuri.org/IUserService/UpdateMobileResponse")]
        void UpdateMobile(int id, string mobile);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/Create", ReplyAction="http://tempuri.org/IUserService/CreateResponse")]
        void Create(WebApplication1.UserServiceReference.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetByUsername", ReplyAction="http://tempuri.org/IUserService/GetByUsernameResponse")]
        WebApplication1.UserServiceReference.User GetByUsername(string Username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetById", ReplyAction="http://tempuri.org/IUserService/GetByIdResponse")]
        WebApplication1.UserServiceReference.User GetById(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/QueryAcl", ReplyAction="http://tempuri.org/IUserService/QueryAclResponse")]
        string[] QueryAcl(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/HasAclEntry", ReplyAction="http://tempuri.org/IUserService/HasAclEntryResponse")]
        bool HasAclEntry(int id, string entry);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/AddAclEntry", ReplyAction="http://tempuri.org/IUserService/AddAclEntryResponse")]
        void AddAclEntry(int id, string entry);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/RemoveAclEntry", ReplyAction="http://tempuri.org/IUserService/RemoveAclEntryResponse")]
        void RemoveAclEntry(int id, string entry);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserServiceChannel : WebApplication1.UserServiceReference.IUserService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UserServiceClient : System.ServiceModel.ClientBase<WebApplication1.UserServiceReference.IUserService>, WebApplication1.UserServiceReference.IUserService {
        
        public UserServiceClient() {
        }
        
        public UserServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UserServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string Hash(string password) {
            return base.Channel.Hash(password);
        }
        
        public bool Auth(string username, string password) {
            return base.Channel.Auth(username, password);
        }
        
        public void UpdatePassword(int id, string password) {
            base.Channel.UpdatePassword(id, password);
        }
        
        public void UpdateMobile(int id, string mobile) {
            base.Channel.UpdateMobile(id, mobile);
        }
        
        public void Create(WebApplication1.UserServiceReference.User user) {
            base.Channel.Create(user);
        }
        
        public WebApplication1.UserServiceReference.User GetByUsername(string Username) {
            return base.Channel.GetByUsername(Username);
        }
        
        public WebApplication1.UserServiceReference.User GetById(int id) {
            return base.Channel.GetById(id);
        }
        
        public string[] QueryAcl(int id) {
            return base.Channel.QueryAcl(id);
        }
        
        public bool HasAclEntry(int id, string entry) {
            return base.Channel.HasAclEntry(id, entry);
        }
        
        public void AddAclEntry(int id, string entry) {
            base.Channel.AddAclEntry(id, entry);
        }
        
        public void RemoveAclEntry(int id, string entry) {
            base.Channel.RemoveAclEntry(id, entry);
        }
    }
}