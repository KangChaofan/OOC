<<<<<<< HEAD
﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.17929
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace OOC.Instance.TaskProcessedDataService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TaskProcessedDataService.ITaskProcessedDataService")]
    public interface ITaskProcessedDataService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITaskProcessedDataService/CreateDataSet", ReplyAction="http://tempuri.org/ITaskProcessedDataService/CreateDataSetResponse")]
        string CreateDataSet(string taskGuid, string cmGuid, string className, string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITaskProcessedDataService/RemoveDataSetByGuid", ReplyAction="http://tempuri.org/ITaskProcessedDataService/RemoveDataSetByGuidResponse")]
        void RemoveDataSetByGuid(string dataSetGuid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITaskProcessedDataService/InsertIntoDataSet", ReplyAction="http://tempuri.org/ITaskProcessedDataService/InsertIntoDataSetResponse")]
        void InsertIntoDataSet(string dataSetGuid, string[] record);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITaskProcessedDataService/InsertMultipleIntoDataSet", ReplyAction="http://tempuri.org/ITaskProcessedDataService/InsertMultipleIntoDataSetResponse")]
        void InsertMultipleIntoDataSet(string dataSetGuid, string[][] records);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITaskProcessedDataService/QueryDataSet", ReplyAction="http://tempuri.org/ITaskProcessedDataService/QueryDataSetResponse")]
        string[][] QueryDataSet(string dataSetGuid, int start, int limit);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITaskProcessedDataServiceChannel : OOC.Instance.TaskProcessedDataService.ITaskProcessedDataService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TaskProcessedDataServiceClient : System.ServiceModel.ClientBase<OOC.Instance.TaskProcessedDataService.ITaskProcessedDataService>, OOC.Instance.TaskProcessedDataService.ITaskProcessedDataService {
        
        public TaskProcessedDataServiceClient() {
        }
        
        public TaskProcessedDataServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TaskProcessedDataServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TaskProcessedDataServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TaskProcessedDataServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string CreateDataSet(string taskGuid, string cmGuid, string className, string name) {
            return base.Channel.CreateDataSet(taskGuid, cmGuid, className, name);
        }
        
        public void RemoveDataSetByGuid(string dataSetGuid) {
            base.Channel.RemoveDataSetByGuid(dataSetGuid);
        }
        
        public void InsertIntoDataSet(string dataSetGuid, string[] record) {
            base.Channel.InsertIntoDataSet(dataSetGuid, record);
        }
        
        public void InsertMultipleIntoDataSet(string dataSetGuid, string[][] records) {
            base.Channel.InsertMultipleIntoDataSet(dataSetGuid, records);
        }
        
        public string[][] QueryDataSet(string dataSetGuid, int start, int limit) {
            return base.Channel.QueryDataSet(dataSetGuid, start, limit);
        }
    }
}
=======
﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.17929
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace OOC.Instance.TaskProcessedDataService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TaskProcessedDataService.ITaskProcessedDataService")]
    public interface ITaskProcessedDataService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITaskProcessedDataService/CreateDataSet", ReplyAction="http://tempuri.org/ITaskProcessedDataService/CreateDataSetResponse")]
        string CreateDataSet(string taskGuid, string cmGuid, string className, string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITaskProcessedDataService/RemoveDataSetByGuid", ReplyAction="http://tempuri.org/ITaskProcessedDataService/RemoveDataSetByGuidResponse")]
        void RemoveDataSetByGuid(string dataSetGuid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITaskProcessedDataService/InsertIntoDataSet", ReplyAction="http://tempuri.org/ITaskProcessedDataService/InsertIntoDataSetResponse")]
        void InsertIntoDataSet(string dataSetGuid, string[] record);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITaskProcessedDataService/InsertMultipleIntoDataSet", ReplyAction="http://tempuri.org/ITaskProcessedDataService/InsertMultipleIntoDataSetResponse")]
        void InsertMultipleIntoDataSet(string dataSetGuid, string[][] records);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITaskProcessedDataService/QueryDataSet", ReplyAction="http://tempuri.org/ITaskProcessedDataService/QueryDataSetResponse")]
        string[][] QueryDataSet(string dataSetGuid, int start, int limit);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITaskProcessedDataServiceChannel : OOC.Instance.TaskProcessedDataService.ITaskProcessedDataService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TaskProcessedDataServiceClient : System.ServiceModel.ClientBase<OOC.Instance.TaskProcessedDataService.ITaskProcessedDataService>, OOC.Instance.TaskProcessedDataService.ITaskProcessedDataService {
        
        public TaskProcessedDataServiceClient() {
        }
        
        public TaskProcessedDataServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TaskProcessedDataServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TaskProcessedDataServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TaskProcessedDataServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string CreateDataSet(string taskGuid, string cmGuid, string className, string name) {
            return base.Channel.CreateDataSet(taskGuid, cmGuid, className, name);
        }
        
        public void RemoveDataSetByGuid(string dataSetGuid) {
            base.Channel.RemoveDataSetByGuid(dataSetGuid);
        }
        
        public void InsertIntoDataSet(string dataSetGuid, string[] record) {
            base.Channel.InsertIntoDataSet(dataSetGuid, record);
        }
        
        public void InsertMultipleIntoDataSet(string dataSetGuid, string[][] records) {
            base.Channel.InsertMultipleIntoDataSet(dataSetGuid, records);
        }
        
        public string[][] QueryDataSet(string dataSetGuid, int start, int limit) {
            return base.Channel.QueryDataSet(dataSetGuid, start, limit);
        }
    }
}
>>>>>>> 0daec768afcc757c83c424118f28374d34e3dc64
