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
        void Create(string name, string version, int authorUserId, string className);

        [OperationContract]
        void Create(string name, string version, int authorUserId, string className, DateTime creation,
                    DateTime modification, bool isPublic, bool isApproved);

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
    }
}