using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Guiet.kQuatre.Business
{
    public class ThreadedBindingList<T> : BindingList<T>
    {
        SynchronizationContext ctx = SynchronizationContext.Current;
        protected override void OnAddingNew(AddingNewEventArgs e)
        {
            if (ctx == null)
            {
                base.OnAddingNew(e);    
            }
            else
            {
                ctx.Send(delegate
                { BaseAddingNew(e); }, null);
            }            
        }

        void BaseAddingNew(AddingNewEventArgs e)
        {
            base.OnAddingNew(e);
        }

        protected override void OnListChanged(ListChangedEventArgs e)
        {
            if (ctx == null)
            {
                base.OnListChanged(e);
            }
            else
            {
                ctx.Send(delegate
                { BaseListChanged(e); }, null);
            }            
        }

        void BaseListChanged(ListChangedEventArgs e)
        {
            base.OnListChanged(e);
        }

    }
}
