using OpenMI.Standard;

namespace Oatc.OpenMI.Sdk.Wrapper
{
    public interface ISteadyState
    {
        void ComputeSteadyState(ITime time);
    }
}