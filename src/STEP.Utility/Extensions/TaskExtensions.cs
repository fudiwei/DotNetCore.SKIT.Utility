#if !NET35

namespace System.Threading.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="continueOnCapturedContext"></param>
        public static void GetAwaiterResult(this Task task, bool continueOnCapturedContext = false)
        {
            task.ConfigureAwait(continueOnCapturedContext).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="task"></param>
        /// <param name="continueOnCapturedContext"></param>
        /// <returns></returns>
        public static T GetAwaiterResult<T>(this Task<T> task, bool continueOnCapturedContext = false)
        {
            return task.ConfigureAwait(continueOnCapturedContext).GetAwaiter().GetResult();
        }
    }
}

#endif