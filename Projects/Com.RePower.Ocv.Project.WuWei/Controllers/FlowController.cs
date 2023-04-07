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