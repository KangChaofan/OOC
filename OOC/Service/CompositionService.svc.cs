using System.Linq;
using System.ServiceModel;
using System.Collections.Generic;
using OOC.Entity;
using OOC.Contract.Data.Common;
using OOC.Contract.Data.Request;
using OOC.Contract.Data.Response;
using OOC.Contract.Service;
using OOC.ServiceAttribute;
using OOC.Util;

namespace OOC.Service
{
    [ExposedService("CompositionService")]
    public class CompositionService : ICompositionService
    {
        public string Create(int authorUserId, string title, bool isShared, bool isFinished)
        {
            using (OOCEntities db = new OOCEntities())
            {
                Composition composition = new Composition()
                {
                    guid = GuidUtil.newGuid(),
                    authorUserId = authorUserId,
                    title = title,
                    isShared = isShared,
                    isFinished = isFinished
                };
                db.Composition.AddObject(composition);
                db.SaveChanges();
                return composition.guid;
            }
        }

        public List<CompositionModel> GetCompositionModels(string compositionGuid)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<CompositionModel> result = from o in db.CompositionModel
                                                      where o.compositionGuid == compositionGuid
                                                      select o;
                return result.ToList();
            }
        }

        public List<CompositionModelData> GetCompositionModelsData(string compositionGuid)
        {
            using (OOCEntities db = new OOCEntities())
            {
                List<CompositionModelData> data = new List<CompositionModelData>();
                IQueryable<CompositionModel> result = from o in db.CompositionModel
                                                      where o.compositionGuid == compositionGuid
                                                      select o;
                foreach (CompositionModel row in result)
                {
                    data.Add(new CompositionModelData(row));
                }
                return data;
            }
        }

        public string CreateCompositionModel(string compositionGuid, string modelGuid, CompositionModelProperties properties)
        {
            using (OOCEntities db = new OOCEntities())
            {
                CompositionModel compositionModel = new CompositionModel()
                {
                    guid = GuidUtil.newGuid(),
                    compositionGuid = compositionGuid,
                    modelGuid = modelGuid,
                    properties = properties.Serialized
                };
                db.CompositionModel.AddObject(compositionModel);
                db.SaveChanges();
                return compositionModel.guid;
            }
        }

        public void UpdateCompositionModelProperties(string cmGuid, CompositionModelProperties properties)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<CompositionModel> result = from o in db.CompositionModel
                                                      where o.guid == cmGuid
                                                      select o;
                CompositionModel compositionModel = result.First();
                compositionModel.properties = properties.Serialized;
                db.SaveChanges();
            }
        }

        public CompositionModel GetCompositionModel(string cmGuid)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<CompositionModel> result = from o in db.CompositionModel
                                                      where o.guid == cmGuid
                                                      select o;
                return result.First();
            }
        }

        public CompositionModelData GetCompositionModelData(string cmGuid)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<CompositionModel> result = from o in db.CompositionModel
                                                      where o.guid == cmGuid
                                                      select o;
                return new CompositionModelData(result.First());
            }
        }

        public void RemoveModel(string cmGuid)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<CompositionModel> result = from o in db.CompositionModel
                                                      where o.guid == cmGuid
                                                      select o;
                db.CompositionModel.DeleteObject(result.First());
                db.SaveChanges();
            }
        }

        public List<CompositionLink> GetCompositionLinks(string compositionGuid)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<CompositionLink> result = from o in db.CompositionLink
                                                     where o.compositionGuid == compositionGuid
                                                     select o;
                return result.ToList();
            }
        }

        public List<CompositionLinkData> GetCompositionLinksData(string compositionGuid)
        {
            using (OOCEntities db = new OOCEntities())
            {
                List<CompositionLinkData> data = new List<CompositionLinkData>();
                IQueryable<CompositionLink> result = from o in db.CompositionLink
                                                     where o.compositionGuid == compositionGuid
                                                     select o;
                foreach (CompositionLink row in result)
                {
                    data.Add(new CompositionLinkData(row));
                }
                return data;
            }
        }

        public string CreateCompositionLink(string compositionGuid, string sourceCmGuid, string targetCmGuid, string sourceQuantity, string targetQuantity, string sourceElementSet, string targetElementSet, LinkDataOperation dataOperation)
        {
            using (OOCEntities db = new OOCEntities())
            {
                CompositionLink compositionLink = new CompositionLink()
                {
                    guid = GuidUtil.newGuid(),
                    compositionGuid = compositionGuid,
                    sourceCmGuid = sourceCmGuid,
                    targetCmGuid = targetCmGuid,
                    sourceQuantity = sourceQuantity,
                    targetQuantity = targetQuantity,
                    sourceElementSet = sourceElementSet,
                    targetElementSet = targetElementSet,
                    dataOperation = dataOperation.Serialized
                };
                db.CompositionLink.AddObject(compositionLink);
                db.SaveChanges();
                return compositionLink.guid;
            }
        }

        public CompositionLink GetCompositionLink(string linkGuid)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<CompositionLink> result = from o in db.CompositionLink
                                                     where o.guid == linkGuid
                                                     select o;
                return result.First();
            }
        }

        public CompositionLinkData GetCompositionLinkData(string linkGuid)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<CompositionLink> result = from o in db.CompositionLink
                                                     where o.guid == linkGuid
                                                     select o;
                return new CompositionLinkData(result.First());
            }
        }

        public void RemoveCompositionLink(string linkGuid)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<CompositionLink> result = from o in db.CompositionLink
                                                     where o.guid == linkGuid
                                                     select o;
                db.CompositionLink.DeleteObject(result.First());
                db.SaveChanges();
            }
        }

        public CompositionData GetCompositionData(string compositionGuid)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<Composition> result = from o in db.Composition
                                                 where o.guid == compositionGuid
                                                 select o;
                return new CompositionData(result.First());
            }
        }
    }
}
