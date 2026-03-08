// -----------------------------------------------------------------------------
//
// Use this sample example C# file to develop samples to guide usage of APIs
// in your package.
//
// -----------------------------------------------------------------------------

namespace Bubbs263.Savesystem
{
    /// <summary>
    /// Provide a general description of the public class.
    /// </summary>
    public class MyPublicSampleExampleClass
    {
        /// <summary>
        /// Provide a description of what this public method does.
        /// </summary>
        public void CountThingsAndDoStuffAndOutputIt()
        {
            var result = new MyPublicRuntimeExampleClass().CountThingsAndDoStuff(1, 2, false);
            Debug.Log("Call CountThingsAndDoStuffAndOutputIt returns " + result);
        }
    }
}