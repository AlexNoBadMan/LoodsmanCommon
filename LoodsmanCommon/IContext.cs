using System;
using System.Windows.Threading;

namespace LoodsmanCommon
{
    public interface IContext
    {
        bool IsBusy { get; set; }
        void Invoke(Action action);

        void BeginInvoke(Action action);
    }

    public class Context : IContext
    {
        private readonly Dispatcher _dispatcher;

        public bool IsBusy { get; set; }
        public Context() : this(Dispatcher.CurrentDispatcher) { }
        public Context(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }
        public void Invoke(Action action)
        {
            _dispatcher.Invoke(action);
        }

        public void BeginInvoke(Action action) 
        {
            _dispatcher.BeginInvoke(action);
        }
    }
}
