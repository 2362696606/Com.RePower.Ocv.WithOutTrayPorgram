using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.WuWei.Controllers
{
    public class FlowController
    {
        public FlowController()
        {
            ResetEvent = new ManualResetEvent(true);
            CancelTokenSource = new CancellationTokenSource();
        }
        public ManualResetEvent ResetEvent { get; set; }
        public CancellationTokenSource CancelTokenSource { get; set; }
        public CancellationToken CancelToken 
        {
            get
            { 
                return CancelTokenSource.Token;
            }
        }
    }
}
