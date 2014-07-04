using System;
using System.Collections.Generic;
using System.ServiceModel;
using OOC.Entity;

namespace OOC.Contract.Service
{
    [ServiceContract]
    public interface IModelService
    {
        [OperationContract]
        string Create(string name, string version, string modelAbstract, int authorUserId, Boolean isPublic, Boolean isApproved, string className, Int32 topId, Int32 typeId);

        [OperationContract]
        Model GetByGuid(string guid);

        [OperationContract]
        List<Model> ListByName(string name);

        [OperationContract]
        List<Model> ListByAuthorUserId(int authorUserId);

        [OperationContract]
        List<Model> ListByClassName(string className);

        [OperationContract]
        List<Model> ListByModelTags(List<ModelTag> modelTags);

        [OperationContract]
        bool Audit(string guid);

        [OperationContract]
        List<ModelProperty> GetModelProperties(string guid);

        [OperationContract]
        void AddModelProperty(string guid, ModelProperty modelProperty);

        [OperationContract]
        void RemoveModelProperty(string guid, string key);

        [OperationContract]
        void UpdateModelProperty(string guid, string key, ModelProperty modelProperty);

        [OperationContract]
        List<Model> ModelSimpleList();

        [OperationContract]
        ModelProperty GetRiverBasinByModelGuid(string modelGuid);

        [OperationContract]
        List<Model> ModelSimpleListByTopID(int TypeID);

        [OperationContract]
        List<Model> ModelSimpleListByTypeID(int TypeID);
    }
}