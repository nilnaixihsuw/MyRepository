using System;
using System.Collections;
using System.Threading;

namespace Mediinfo.Utility
{
    /// <summary>
    ///用C#托管代码实现的线程池
    /// 它在管理线程的时候，有一个缓存线程的池，即一个ArrayList对象
    /// 它一开始就初始化了一定数量的线程，并通过ProcessQueuedItems方法保证异步执行进入池中的队列任务（那个死循环有时可能导致CPU过分忙碌），
    /// 这样在分配异步任务的时候，就省去了频繁去创建（new）一个线程
    /// </summary>
    public class ManagedThreadPool
    {
        #region constants

        /// <summary>
        /// 线程池的线程的最大数量的处理。
        /// </summary>
        private const int _maxWorkerThreads = 10;

        #endregion

        #region member variables

        /// <summary>
        /// 所有回调的队列等待执行。
        /// </summary>
        private static Queue _waitingCallbacks;

        /// <summary>
        /// 用于信号,需要一个工作线程进行处理。请注意,可能需要多个线程同时,我们使用一个信号量,而不是一个自动重置的事件。
        /// </summary>
        private static Semaphore _workerThreadNeeded;

        /// <summary>
        /// 所有工作线程的列表处理的线程池。
        /// </summary>
        private static ArrayList _workerThreads;

        /// <summary>
        /// 当前活动线程的数量。
        /// </summary>
        private static int _inUseThreads;

        /// <summary>
        /// 可锁定的对象池。
        /// </summary>
        private static object _poolLock = new object();

        #endregion

        #region construction and finalization

        /// <summary>
        /// 初始化线程池。
        /// </summary>
        static ManagedThreadPool()
        {
            Initialize();
        }

        /// <summary>
        /// 初始化线程池。
        /// </summary>
        private static void Initialize()
        {
            /*
             * 创建线程存储; 我们可能会遇到处理同步自己情形下,多个操作需要原子。
             * 我们跟踪线程创建可能有效的措施;不需要任何核心功能。
             */
            _waitingCallbacks = new Queue();
            _workerThreads = new ArrayList();
            _inUseThreads = 0;

            // 创建"线程需要"事件
            _workerThreadNeeded = new Semaphore(1);

            // 创建的所有工作线程
            for (int i = 0; i < _maxWorkerThreads; i++)
            {
                // 创建一个新线程的线程,并将它添加到列表。
                Thread newThread = new Thread(new ThreadStart(ProcessQueuedItems));
                _workerThreads.Add(newThread);

                // 配置新线程和启动它
                newThread.Name = "ManagedPoolThread #" + i.ToString();
                newThread.IsBackground = true;
                newThread.Start();
            }
        }

        #endregion

        #region public methods

        /// <summary>队列线程池的用户工作项。</summary>
        /// <param name="callback">WaitCallback代表委托调用线程池中的线程回暖时工作项目。</param>
        public static void QueueUserWorkItem(WaitCallback callback)
        {
            // 队列委托没有状态
            //Logger.Debug(string.Format("添加线程操作方法:{0},Target:{1}", callback.Method.Name, callback.Target.ToString()));
            QueueUserWorkItem(callback, null);
        }

        /// <summary>队列线程池的用户工作项。</summary>
        /// <param name="callback">WaitCallback代表委托调用线程池中的线程回暖时工作项目。</param>
        /// <param name="state">的对象传递给委托的服务线程池。</param>
        public static void QueueUserWorkItem(WaitCallback callback, object state)
        {
            /*
             * 创建一个包含委托等待回调,它的状态。
             * 处理队列,数据和信号,等待。
             */
            WaitingCallback waiting = new WaitingCallback(callback, state);
            lock (_poolLock)
            {
                _waitingCallbacks.Enqueue(waiting);
            }
            _workerThreadNeeded.AddOne();
        }

        /// <summary>
        /// 空的工作队列排队工作项。重置所有线程池中。
        /// </summary>
        public static void Reset()
        {
            lock (_poolLock)
            {
                // 清理任何等待回调
                try
                {
                    // 尝试处理所有剩余的状态
                    foreach (object obj in _waitingCallbacks)
                    {
                        WaitingCallback callback = (WaitingCallback)obj;
                        if (callback.State is IDisposable)
                            ((IDisposable)callback.State).Dispose();
                    }
                }
                catch { }

                // 关闭所有现有的线程
                try
                {
                    foreach (Thread thread in _workerThreads)
                    {
                        if (thread != null) thread.Abort("reset");
                    }
                }
                catch { }

                // 重新启动池(创建新线程,等等)。
                Initialize();
            }
        }

        #endregion

        #region properties

        /// <summary>
        /// 得到的处理线程池的线程数量。
        /// </summary>
        public static int MaxThreads
        {
            get { return _maxWorkerThreads; }
        }

        /// <summary>
        /// 得到当前活动线程的数量的线程池。
        /// </summary>
        public static int ActiveThreads
        {
            get { return _inUseThreads; }
        }

        /// <summary>
        /// 会回调的数量代表当前正在等待的线程池。
        /// </summary>
        public static int WaitingCallbacks
        {
            get
            {
                lock (_poolLock)
                {
                    return _waitingCallbacks.Count;
                }
            }
        }

        #endregion

        #region thread processing

#if NET1
        /// <summary>
        /// 事件提出当threadpool线程是一个例外。
        /// </summary>
        public static event UnhandledExceptionEventHandler UnhandledException;
#endif

        /// <summary>
        /// 一线工人工作队列函数处理项。
        /// </summary>
        private static void ProcessQueuedItems()
        {
            // 无限期地过程
            while (true)
            {
                _workerThreadNeeded.WaitOne();

                // 获得队列中的下一个项目。如果什么都没有,去睡觉一会儿,直到我们等待回调时醒来。
                WaitingCallback callback = null;

                // 试图让下一个回调。我们需要锁在队列上为了使我们的原子数检查和检索。
                lock (_poolLock)
                {
                    if (_waitingCallbacks.Count > 0)
                    {
                        try { callback = (WaitingCallback)_waitingCallbacks.Dequeue(); }
                        catch { } // 确保没有失败
                    }
                }

                if (callback != null)
                {
                    // 我们现在有一个回调。执行它。确保准确记录目前有多少回调执行。
                    try
                    {
                        Interlocked.Increment(ref _inUseThreads);
                        callback.Callback(callback.State);
                    }

                    catch (Exception)
                    {

                    }

                    finally
                    {
                        Interlocked.Decrement(ref _inUseThreads);
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// 用于保存回调代表和国家的代表。
        /// </summary>
        private class WaitingCallback
        {
            #region member variables

            /// <summary>
            /// 回调代表回调。
            /// </summary>
            private WaitCallback _callback;

            /// <summary>
            /// 状态来调用回调的委托。
            /// </summary>
            private object _state;

            #endregion

            #region construction

            /// <summary>
            /// 初始化的回调对象。
            /// </summary>
            /// <param name="callback">回调代表回调。</param>
            /// <param name="state">状态来调用回调的委托。</param>
            public WaitingCallback(WaitCallback callback, object state)
            {
                _callback = callback;
                _state = state;
            }

            #endregion

            #region properties

            /// <summary>
            /// 回调代表回调。
            /// </summary>
            public WaitCallback Callback
            {
                get { return _callback; }
            }

            /// <summary>
            /// 得到的状态调用回调的委托。
            /// </summary>
            public object State
            {
                get { return _state; }
            }

            #endregion
        }
    }

    /// <summary>
    /// Dijkstra算法的实现基于监控类光伏信号量。
    /// </summary>
    public class Semaphore
    {
        #region member variables

        /// <summary>
        /// 单位允许讲这个信号量的数量。
        /// </summary>
        private int _count;

        /// <summary>
        /// 锁的信号量。
        /// </summary>
        private object _semLock = new object();

        #endregion

        #region construction

        /// <summary>
        /// 初始化信号为二进制信号量。
        /// </summary>
        public Semaphore()
            : this(1)
        {

        }

        /// <summary>
        /// 初始化信号量计数信号量。
        /// </summary>
        /// <param name="count">最初的数量的线程,可以拿出单位从这个信号量。</param>
        /// <exception cref="ArgumentException">如果计数参数小于0。</exception>
        public Semaphore(int count)
        {
            if (count < 0)
                throw new ArgumentException("Semaphore must have a count of at least 0.", "count");
            _count = count;
        }

        #endregion

        #region synchronization operations

        /// <summary>
        /// V信号量(加1单位)。
        /// </summary>
        public void AddOne() { V(); }

        /// <summary>
        /// 警信号量(从中取出一个单位)。
        /// </summary>
        public void WaitOne() { P(); }

        /// <summary>
        /// 警信号量(从中取出一个单位)。
        /// </summary>
        public void P()
        {
            // 锁我们可以在和平工作。这个工作因为锁实际上是建立在监控器。
            lock (_semLock)
            {
                // 等到一个单元。我们需要等待在一个循环中,以防别人醒来。这可能发生,如果监视器。脉冲语句改变监控。PulseAll语句为了一些随机性引入线程的顺序中醒来。
                while (_count <= 0)
                    Monitor.Wait(_semLock, Timeout.Infinite);
                _count--;
            }
        }

        /// <summary>
        /// V信号量(加1单位)。
        /// </summary>
        public void V()
        {
            // 锁我们可以在和平工作。这个工作因为锁实际上是建立在监控器。
            lock (_semLock)
            {
                // 释放我们的控制单元。然后告诉每个人都在等待这个对象有一个单位。
                _count++;
                Monitor.Pulse(_semLock);
            }
        }

        /// <summary>
        /// 重置指定的信号量计数。应该谨慎使用。
        /// </summary>
        public void Reset(int count)
        {
            lock (_semLock)
            {
                _count = count;
            }
        }

        #endregion
    }
}
