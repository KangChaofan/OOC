﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.17929
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace FileClient.FileService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FileSystemDescription", Namespace="http://schemas.datacontract.org/2004/07/OOC.Contract.Data.Common")]
    [System.SerializableAttribute()]
    public partial class FileSystemDescription : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime AccessTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime CreateTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsDirectoryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime ModifyTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long SizeField;
        
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
        public System.DateTime AccessTime {
            get {
                return this.AccessTimeField;
            }
            set {
                if ((this.AccessTimeField.Equals(value) != true)) {
                    this.AccessTimeField = value;
                    this.RaisePropertyChanged("AccessTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime CreateTime {
            get {
                return this.CreateTimeField;
            }
            set {
                if ((this.CreateTimeField.Equals(value) != true)) {
                    this.CreateTimeField = value;
                    this.RaisePropertyChanged("CreateTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsDirectory {
            get {
                return this.IsDirectoryField;
            }
            set {
                if ((this.IsDirectoryField.Equals(value) != true)) {
                    this.IsDirectoryField = value;
                    this.RaisePropertyChanged("IsDirectory");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ModifyTime {
            get {
                return this.ModifyTimeField;
            }
            set {
                if ((this.ModifyTimeField.Equals(value) != true)) {
                    this.ModifyTimeField = value;
                    this.RaisePropertyChanged("ModifyTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long Size {
            get {
                return this.SizeField;
            }
            set {
                if ((this.SizeField.Equals(value) != true)) {
                    this.SizeField = value;
                    this.RaisePropertyChanged("Size");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="FileEntityResponse", Namespace="http://schemas.datacontract.org/2004/07/OOC.Contract.Data.Response")]
    [System.SerializableAttribute()]
    public partial class FileEntityResponse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] ContentField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FileNameField;
        
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
        public byte[] Content {
            get {
                return this.ContentField;
            }
            set {
                if ((object.ReferenceEquals(this.ContentField, value) != true)) {
                    this.ContentField = value;
                    this.RaisePropertyChanged("Content");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FileName {
            get {
                return this.FileNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FileNameField, value) != true)) {
                    this.FileNameField = value;
                    this.RaisePropertyChanged("FileName");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="FileChunkResponse", Namespace="http://schemas.datacontract.org/2004/07/OOC.Contract.Data.Response")]
    [System.SerializableAttribute()]
    public partial class FileChunkResponse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] ChunkField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FileNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long LengthField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long OffsetField;
        
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
        public byte[] Chunk {
            get {
                return this.ChunkField;
            }
            set {
                if ((object.ReferenceEquals(this.ChunkField, value) != true)) {
                    this.ChunkField = value;
                    this.RaisePropertyChanged("Chunk");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FileName {
            get {
                return this.FileNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FileNameField, value) != true)) {
                    this.FileNameField = value;
                    this.RaisePropertyChanged("FileName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long Length {
            get {
                return this.LengthField;
            }
            set {
                if ((this.LengthField.Equals(value) != true)) {
                    this.LengthField = value;
                    this.RaisePropertyChanged("Length");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long Offset {
            get {
                return this.OffsetField;
            }
            set {
                if ((this.OffsetField.Equals(value) != true)) {
                    this.OffsetField = value;
                    this.RaisePropertyChanged("Offset");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="FileService.IFileService")]
    public interface IFileService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileService/Stat", ReplyAction="http://tempuri.org/IFileService/StatResponse")]
        FileClient.FileService.FileSystemDescription Stat(string fileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileService/Get", ReplyAction="http://tempuri.org/IFileService/GetResponse")]
        FileClient.FileService.FileEntityResponse Get(string fileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileService/Delete", ReplyAction="http://tempuri.org/IFileService/DeleteResponse")]
        void Delete(string path);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileService/Put", ReplyAction="http://tempuri.org/IFileService/PutResponse")]
        void Put(string fileName, byte[] content);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileService/Copy", ReplyAction="http://tempuri.org/IFileService/CopyResponse")]
        void Copy(string sourceFileName, string destFileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileService/List", ReplyAction="http://tempuri.org/IFileService/ListResponse")]
        FileClient.FileService.FileSystemDescription[] List(string path);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileService/CreateDirectory", ReplyAction="http://tempuri.org/IFileService/CreateDirectoryResponse")]
        void CreateDirectory(string path);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileService/Read", ReplyAction="http://tempuri.org/IFileService/ReadResponse")]
        FileClient.FileService.FileChunkResponse Read(string fileName, long offset, long length);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileService/Append", ReplyAction="http://tempuri.org/IFileService/AppendResponse")]
        void Append(string fileName, byte[] chunk);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileService/Head", ReplyAction="http://tempuri.org/IFileService/HeadResponse")]
        string Head(string fileName, int lines);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IFileServiceChannel : FileClient.FileService.IFileService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FileServiceClient : System.ServiceModel.ClientBase<FileClient.FileService.IFileService>, FileClient.FileService.IFileService {
        
        public FileServiceClient() {
        }
        
        public FileServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public FileServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FileServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FileServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public FileClient.FileService.FileSystemDescription Stat(string fileName) {
            return base.Channel.Stat(fileName);
        }
        
        public FileClient.FileService.FileEntityResponse Get(string fileName) {
            return base.Channel.Get(fileName);
        }
        
        public void Delete(string path) {
            base.Channel.Delete(path);
        }
        
        public void Put(string fileName, byte[] content) {
            base.Channel.Put(fileName, content);
        }
        
        public void Copy(string sourceFileName, string destFileName) {
            base.Channel.Copy(sourceFileName, destFileName);
        }
        
        public FileClient.FileService.FileSystemDescription[] List(string path) {
            return base.Channel.List(path);
        }
        
        public void CreateDirectory(string path) {
            base.Channel.CreateDirectory(path);
        }
        
        public FileClient.FileService.FileChunkResponse Read(string fileName, long offset, long length) {
            return base.Channel.Read(fileName, offset, length);
        }
        
        public void Append(string fileName, byte[] chunk) {
            base.Channel.Append(fileName, chunk);
        }
        
        public string Head(string fileName, int lines) {
            return base.Channel.Head(fileName, lines);
        }
    }
}
