using HslCommunication;
using HslCommunication.Core;

namespace Com.RePower.Device.Plc.Helper
{
    public static class ReadWriteNetHelper
    {
        public static OperateResult<TimeSpan> Wait(this IReadWriteNet readWriteNet, string address, bool waitValue, int readInterval, int waitTimeout, CancellationToken? cancellation = null)
        {
            DateTime now = DateTime.Now;
            while (true)
            {
                if (cancellation is { } cancel)
                {
                    if (cancel.IsCancellationRequested)
                    {
                        return new OperateResult<TimeSpan>("用户取消等待" + (DateTime.Now - now));
                    }
                }
                OperateResult<bool> operateResult = readWriteNet.ReadBool(address);
                if (!operateResult.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<TimeSpan>(operateResult);
                }

                if (operateResult.Content == waitValue)
                {
                    return OperateResult.CreateSuccessResult(DateTime.Now - now);
                }

                if (waitTimeout > 0 && (DateTime.Now - now).TotalMilliseconds > (double)waitTimeout)
                {
                    break;
                }

                Thread.Sleep(readInterval);
            }

            return new OperateResult<TimeSpan>(StringResources.Language.CheckDataTimeout + waitTimeout);
        }

        public static OperateResult<TimeSpan> Wait(this IReadWriteNet readWriteNet, string address, short waitValue, int readInterval, int waitTimeout, CancellationToken? cancellation = null)
        {
            DateTime now = DateTime.Now;
            while (true)
            {
                if (cancellation is { } cancel)
                {
                    if (cancel.IsCancellationRequested)
                    {
                        return new OperateResult<TimeSpan>("用户取消等待" + (DateTime.Now - now));
                    }
                }
                OperateResult<short> operateResult = readWriteNet.ReadInt16(address);
                if (!operateResult.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<TimeSpan>(operateResult);
                }

                if (operateResult.Content == waitValue)
                {
                    return OperateResult.CreateSuccessResult(DateTime.Now - now);
                }

                if (waitTimeout > 0 && (DateTime.Now - now).TotalMilliseconds > (double)waitTimeout)
                {
                    break;
                }

                Thread.Sleep(readInterval);
            }

            return new OperateResult<TimeSpan>(StringResources.Language.CheckDataTimeout + waitTimeout);
        }

        public static OperateResult<TimeSpan> Wait(this IReadWriteNet readWriteNet, string address, ushort waitValue, int readInterval, int waitTimeout, CancellationToken? cancellation = null)
        {
            DateTime now = DateTime.Now;
            while (true)
            {
                if (cancellation is { } cancel)
                {
                    if (cancel.IsCancellationRequested)
                    {
                        return new OperateResult<TimeSpan>("用户取消等待" + (DateTime.Now - now));
                    }
                }
                OperateResult<ushort> operateResult = readWriteNet.ReadUInt16(address);
                if (!operateResult.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<TimeSpan>(operateResult);
                }

                if (operateResult.Content == waitValue)
                {
                    return OperateResult.CreateSuccessResult(DateTime.Now - now);
                }

                if (waitTimeout > 0 && (DateTime.Now - now).TotalMilliseconds > (double)waitTimeout)
                {
                    break;
                }

                Thread.Sleep(readInterval);
            }

            return new OperateResult<TimeSpan>(StringResources.Language.CheckDataTimeout + waitTimeout);
        }

        public static OperateResult<TimeSpan> Wait(this IReadWriteNet readWriteNet, string address, int waitValue, int readInterval, int waitTimeout, CancellationToken? cancellation = null)
        {
            DateTime now = DateTime.Now;
            while (true)
            {
                if (cancellation is { } cancel)
                {
                    if (cancel.IsCancellationRequested)
                    {
                        return new OperateResult<TimeSpan>("用户取消等待" + (DateTime.Now - now));
                    }
                }
                OperateResult<int> operateResult = readWriteNet.ReadInt32(address);
                if (!operateResult.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<TimeSpan>(operateResult);
                }

                if (operateResult.Content == waitValue)
                {
                    return OperateResult.CreateSuccessResult(DateTime.Now - now);
                }

                if (waitTimeout > 0 && (DateTime.Now - now).TotalMilliseconds > (double)waitTimeout)
                {
                    break;
                }

                Thread.Sleep(readInterval);
            }

            return new OperateResult<TimeSpan>(StringResources.Language.CheckDataTimeout + waitTimeout);
        }

        public static OperateResult<TimeSpan> Wait(this IReadWriteNet readWriteNet, string address, uint waitValue, int readInterval, int waitTimeout, CancellationToken? cancellation = null)
        {
            DateTime now = DateTime.Now;
            while (true)
            {
                if (cancellation is { } cancel)
                {
                    if (cancel.IsCancellationRequested)
                    {
                        return new OperateResult<TimeSpan>("用户取消等待" + (DateTime.Now - now));
                    }
                }
                OperateResult<uint> operateResult = readWriteNet.ReadUInt32(address);
                if (!operateResult.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<TimeSpan>(operateResult);
                }

                if (operateResult.Content == waitValue)
                {
                    return OperateResult.CreateSuccessResult(DateTime.Now - now);
                }

                if (waitTimeout > 0 && (DateTime.Now - now).TotalMilliseconds > (double)waitTimeout)
                {
                    break;
                }

                Thread.Sleep(readInterval);
            }

            return new OperateResult<TimeSpan>(StringResources.Language.CheckDataTimeout + waitTimeout);
        }

        public static OperateResult<TimeSpan> Wait(this IReadWriteNet readWriteNet, string address, long waitValue, int readInterval, int waitTimeout, CancellationToken? cancellation = null)
        {
            DateTime now = DateTime.Now;
            while (true)
            {
                if (cancellation is { } cancel)
                {
                    if (cancel.IsCancellationRequested)
                    {
                        return new OperateResult<TimeSpan>("用户取消等待" + (DateTime.Now - now));
                    }
                }
                OperateResult<long> operateResult = readWriteNet.ReadInt64(address);
                if (!operateResult.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<TimeSpan>(operateResult);
                }

                if (operateResult.Content == waitValue)
                {
                    return OperateResult.CreateSuccessResult(DateTime.Now - now);
                }

                if (waitTimeout > 0 && (DateTime.Now - now).TotalMilliseconds > (double)waitTimeout)
                {
                    break;
                }

                Thread.Sleep(readInterval);
            }

            return new OperateResult<TimeSpan>(StringResources.Language.CheckDataTimeout + waitTimeout);
        }

        public static OperateResult<TimeSpan> Wait(this IReadWriteNet readWriteNet, string address, ulong waitValue, int readInterval, int waitTimeout, CancellationToken? cancellation = null)
        {
            DateTime now = DateTime.Now;
            while (true)
            {
                if (cancellation is { } cancel)
                {
                    if (cancel.IsCancellationRequested)
                    {
                        return new OperateResult<TimeSpan>("用户取消等待" + (DateTime.Now - now));
                    }
                }
                OperateResult<ulong> operateResult = readWriteNet.ReadUInt64(address);
                if (!operateResult.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<TimeSpan>(operateResult);
                }

                if (operateResult.Content == waitValue)
                {
                    return OperateResult.CreateSuccessResult(DateTime.Now - now);
                }

                if (waitTimeout > 0 && (DateTime.Now - now).TotalMilliseconds > (double)waitTimeout)
                {
                    break;
                }

                Thread.Sleep(readInterval);
            }

            return new OperateResult<TimeSpan>(StringResources.Language.CheckDataTimeout + waitTimeout);
        }

        public static async Task<OperateResult<TimeSpan>> WaitAsync(this IReadWriteNet readWriteNet, string address, bool waitValue, int readInterval, int waitTimeout, CancellationToken? cancellation = null)
        {
            DateTime start = DateTime.Now;
            while (true)
            {
                if (cancellation is { } cancel)
                {
                    if (cancel.IsCancellationRequested)
                    {
                        return new OperateResult<TimeSpan>("用户取消等待" + (DateTime.Now - start));
                    }
                }
                OperateResult<bool> read = await readWriteNet.ReadBoolAsync(address);
                if (!read.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<TimeSpan>(read);
                }

                if (read.Content == waitValue)
                {
                    return OperateResult.CreateSuccessResult(DateTime.Now - start);
                }

                if (waitTimeout > 0 && (DateTime.Now - start).TotalMilliseconds > (double)waitTimeout)
                {
                    break;
                }

                await Task.Delay(readInterval);
            }

            return new OperateResult<TimeSpan>(StringResources.Language.CheckDataTimeout + waitTimeout);
        }

        public static async Task<OperateResult<TimeSpan>> WaitAsync(this IReadWriteNet readWriteNet, string address, short waitValue, int readInterval, int waitTimeout, CancellationToken? cancellation = null)
        {
            DateTime start = DateTime.Now;
            while (true)
            {
                if (cancellation is { } cancel)
                {
                    if (cancel.IsCancellationRequested)
                    {
                        return new OperateResult<TimeSpan>("用户取消等待" + (DateTime.Now - start));
                    }
                }
                OperateResult<short> read = await readWriteNet.ReadInt16Async(address);
                if (!read.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<TimeSpan>(read);
                }

                if (read.Content == waitValue)
                {
                    return OperateResult.CreateSuccessResult(DateTime.Now - start);
                }

                if (waitTimeout > 0 && (DateTime.Now - start).TotalMilliseconds > (double)waitTimeout)
                {
                    break;
                }

                await Task.Delay(readInterval);
            }

            return new OperateResult<TimeSpan>(StringResources.Language.CheckDataTimeout + waitTimeout);
        }

        public static async Task<OperateResult<TimeSpan>> WaitAsync(this IReadWriteNet readWriteNet, string address, ushort waitValue, int readInterval, int waitTimeout, CancellationToken? cancellation = null)
        {
            DateTime start = DateTime.Now;
            while (true)
            {
                if (cancellation is { } cancel)
                {
                    if (cancel.IsCancellationRequested)
                    {
                        return new OperateResult<TimeSpan>("用户取消等待" + (DateTime.Now - start));
                    }
                }
                OperateResult<ushort> read = await readWriteNet.ReadUInt16Async(address);
                if (!read.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<TimeSpan>(read);
                }

                if (read.Content == waitValue)
                {
                    return OperateResult.CreateSuccessResult(DateTime.Now - start);
                }

                if (waitTimeout > 0 && (DateTime.Now - start).TotalMilliseconds > (double)waitTimeout)
                {
                    break;
                }

                await Task.Delay(readInterval);
            }

            return new OperateResult<TimeSpan>(StringResources.Language.CheckDataTimeout + waitTimeout);
        }

        public static async Task<OperateResult<TimeSpan>> WaitAsync(this IReadWriteNet readWriteNet, string address, int waitValue, int readInterval, int waitTimeout, CancellationToken? cancellation = null)
        {
            DateTime start = DateTime.Now;
            while (true)
            {
                if (cancellation is { } cancel)
                {
                    if (cancel.IsCancellationRequested)
                    {
                        return new OperateResult<TimeSpan>("用户取消等待" + (DateTime.Now - start));
                    }
                }
                OperateResult<int> read = await readWriteNet.ReadInt32Async(address);
                if (!read.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<TimeSpan>(read);
                }

                if (read.Content == waitValue)
                {
                    return OperateResult.CreateSuccessResult(DateTime.Now - start);
                }

                if (waitTimeout > 0 && (DateTime.Now - start).TotalMilliseconds > (double)waitTimeout)
                {
                    break;
                }

                await Task.Delay(readInterval);
            }

            return new OperateResult<TimeSpan>(StringResources.Language.CheckDataTimeout + waitTimeout);
        }

        public static async Task<OperateResult<TimeSpan>> WaitAsync(this IReadWriteNet readWriteNet, string address, uint waitValue, int readInterval, int waitTimeout, CancellationToken? cancellation = null)
        {
            DateTime start = DateTime.Now;
            while (true)
            {
                if (cancellation is { } cancel)
                {
                    if (cancel.IsCancellationRequested)
                    {
                        return new OperateResult<TimeSpan>("用户取消等待" + (DateTime.Now - start));
                    }
                }
                OperateResult<uint> read = readWriteNet.ReadUInt32(address);
                if (!read.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<TimeSpan>(read);
                }

                if (read.Content == waitValue)
                {
                    return OperateResult.CreateSuccessResult(DateTime.Now - start);
                }

                if (waitTimeout > 0 && (DateTime.Now - start).TotalMilliseconds > (double)waitTimeout)
                {
                    break;
                }

                await Task.Delay(readInterval);
            }

            return new OperateResult<TimeSpan>(StringResources.Language.CheckDataTimeout + waitTimeout);
        }

        public static async Task<OperateResult<TimeSpan>> WaitAsync(this IReadWriteNet readWriteNet, string address, long waitValue, int readInterval, int waitTimeout, CancellationToken? cancellation = null)
        {
            DateTime start = DateTime.Now;
            while (true)
            {
                if (cancellation is { } cancel)
                {
                    if (cancel.IsCancellationRequested)
                    {
                        return new OperateResult<TimeSpan>("用户取消等待" + (DateTime.Now - start));
                    }
                }
                OperateResult<long> read = readWriteNet.ReadInt64(address);
                if (!read.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<TimeSpan>(read);
                }

                if (read.Content == waitValue)
                {
                    return OperateResult.CreateSuccessResult(DateTime.Now - start);
                }

                if (waitTimeout > 0 && (DateTime.Now - start).TotalMilliseconds > (double)waitTimeout)
                {
                    break;
                }

                await Task.Delay(readInterval);
            }

            return new OperateResult<TimeSpan>(StringResources.Language.CheckDataTimeout + waitTimeout);
        }

        public static async Task<OperateResult<TimeSpan>> WaitAsync(this IReadWriteNet readWriteNet, string address, ulong waitValue, int readInterval, int waitTimeout, CancellationToken? cancellation = null)
        {
            DateTime start = DateTime.Now;
            while (true)
            {
                if (cancellation is { } cancel)
                {
                    if (cancel.IsCancellationRequested)
                    {
                        return new OperateResult<TimeSpan>("用户取消等待" + (DateTime.Now - start));
                    }
                }
                OperateResult<ulong> read = readWriteNet.ReadUInt64(address);
                if (!read.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<TimeSpan>(read);
                }

                if (read.Content == waitValue)
                {
                    return OperateResult.CreateSuccessResult(DateTime.Now - start);
                }

                if (waitTimeout > 0 && (DateTime.Now - start).TotalMilliseconds > (double)waitTimeout)
                {
                    break;
                }

                await Task.Delay(readInterval);
            }

            return new OperateResult<TimeSpan>(StringResources.Language.CheckDataTimeout + waitTimeout);
        }
    }
}