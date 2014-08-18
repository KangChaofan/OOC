<<<<<<< HEAD
using OpenMI.Standard;

namespace Oatc.OpenMI.Sdk.Spatial
{
    public struct ElementMappingMethod
    {
        public int ID;
        public string Description;
        public ElementType FromElementsShapeType;
        public ElementType ToElementsShapeType;

        public ElementMappingMethod(int id, string description, 
                                    ElementType fromElementsShapeType, 
                                    ElementType toElementsShapeType)
        {
            ID = id;
            Description = description;
            FromElementsShapeType = fromElementsShapeType;
            ToElementsShapeType = toElementsShapeType;
        }
    }
=======
using OpenMI.Standard;

namespace Oatc.OpenMI.Sdk.Spatial
{
    public struct ElementMappingMethod
    {
        public int ID;
        public string Description;
        public ElementType FromElementsShapeType;
        public ElementType ToElementsShapeType;

        public ElementMappingMethod(int id, string description, 
                                    ElementType fromElementsShapeType, 
                                    ElementType toElementsShapeType)
        {
            ID = id;
            Description = description;
            FromElementsShapeType = fromElementsShapeType;
            ToElementsShapeType = toElementsShapeType;
        }
    }
>>>>>>> 0daec768afcc757c83c424118f28374d34e3dc64
}