namespace Com.RePower.DeviceBase.BaseDevice
{
    public interface ISimulatorRecovery
    {
        public Func<byte[], byte[]>? RecoveryMethod { get; set; }
    }
}