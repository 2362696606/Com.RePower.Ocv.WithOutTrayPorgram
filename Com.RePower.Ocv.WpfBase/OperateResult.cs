namespace Com.RePower.WpfBase
{
    public class OperateResult
    {
        public OperateResult()
        {
        }
        public static string ConvertExceptionMsg(Exception ex)
        {
            return ex.Message;
        }

        /// <summary>
        /// 是否失败
        /// </summary>
        public bool IsFailed => !IsSuccess;
        /// <summary>
        /// 使用指定的消息实例化一个默认的结果对象
        /// </summary>
        /// <param name="msg">错误消息</param>
        public OperateResult(string? msg)
        {
            this.Message = msg;
        }

        /// <summary>
        /// 使用错误代码，消息文本来实例化对象
        /// </summary>
        /// <param name="errorCode">错误代码</param>
        /// <param name="msg">错误消息</param>
        public OperateResult(int errorCode, string msg)
        {
            this.ErrorCode = errorCode;
            this.Message = msg;
        }

        /// <summary>
        /// 操作是否成功标识
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 操作返回信息
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// 操作错误代码(默认的错误代码为0，成功代码为1)
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        ///  结果返回
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"错误代码：{this.ErrorCode}   错误信息：{this.Message}";
        }


        /// <summary>
        /// 创建并返回一个失败的结果对象，该对象复制另一个结果对象的错误信息
        /// </summary>
        /// <typeparam name="T">目标数据类型</typeparam>
        /// <param name="result">之前的结果对象</param>
        /// <returns>带默认泛型对象的失败结果类</returns>
        public static OperateResult<T> CreateFailedResult<T>(OperateResult result)
        {
            return new OperateResult<T>()
            {
                ErrorCode = result.ErrorCode,
                Message = result.Message,
                IsSuccess = false
            };
        }

        public static OperateResult<T> CreateFailedResult<T>(string? msg = null)
        {
            return new OperateResult<T>(msg)
            {
                ErrorCode = 0,
                Message = msg,
                IsSuccess = false
            };
        }

        public static OperateResult<T> CreateFailedResult<T>(string msg, int errorCode)
        {
            return new OperateResult<T>(msg)
            {
                ErrorCode = errorCode,
                Message = msg,
                IsSuccess = false
            };
        }


        /// <summary>
        /// 创建失败的信息返回对象
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public static OperateResult CreateFailedResult(string msg, int errorCode)
        {
            return new OperateResult()
            {
                IsSuccess = false,
                ErrorCode = errorCode,
                Message = msg,
            };
        }

        public static string ConvertException(Exception ex)
        {
            return ex.Message + ex.InnerException + Environment.NewLine + ex.StackTrace;
        }

        /// <summary>
        /// 创建并返回一个失败的结果对象
        /// </summary>
        /// <returns>失败的结果对象</returns>
        public static OperateResult CreateFailedResult()
        {
            return new OperateResult()
            {
                IsSuccess = false,
                ErrorCode = 0,
                Message = "失败",
            };
        }
        /// <summary>
        /// 创建失败结果
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static OperateResult CreateFailedResult(string msg)
        {
            return new OperateResult()
            {
                IsSuccess = false,
                ErrorCode = 0,
                Message = msg,
            };
        }

        /// <summary>
        /// 创建一个带成功信息的成功对象
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static OperateResult CreateSuccessResult(string msg)
        {
            return new OperateResult()
            {
                IsSuccess = true,
                ErrorCode = 1,
                Message = msg,
            };
        }


        /// <summary>
        /// 创建并返回一个成功的结果对象
        /// </summary>
        /// <returns>成功的结果对象</returns>
        public static OperateResult CreateSuccessResult()
        {
            return new OperateResult()
            {
                IsSuccess = true,
                ErrorCode = 1,
                Message = "成功",
            };
        }

        /// <summary>
        /// 创建并返回一个成功的结果对象，并带有一个参数对象
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="value">类型的值对象</param>
        /// <returns>成功的结果对象</returns>
        public static OperateResult<T> CreateSuccessResult<T>(T value)
        {
            return new OperateResult<T>()
            {
                IsSuccess = true,
                ErrorCode = 1,
                Message = "成功",
                Content = value
            };
        }

        /// <summary>
        /// 失败的返回
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static OperateResult<T> CreateFailedResult<T>(T value, string msg)
        {
            return new OperateResult<T>()
            {
                IsSuccess = false,
                ErrorCode = 0,
                Message = msg,
                Content = value
            };
        }
    }
    public class OperateResult<T> : OperateResult
    {
        #region Constructor

        /// <summary>
        /// 实例化一个默认的结果对象
        /// </summary>
        public OperateResult()
            : base()
        {
        }

        /// <summary>
        /// 使用指定的消息实例化一个默认的结果对象
        /// </summary>
        /// <param name="msg">错误消息</param>
        public OperateResult(string? msg)
            : base(msg)
        {

        }

        /// <summary>
        /// 使用错误代码，消息文本来实例化对象
        /// </summary>
        /// <param name="errorCode">错误代码</param>
        /// <param name="msg">错误消息</param>
        public OperateResult(int errorCode, string msg)
            : base(errorCode, msg)
        {

        }

        #endregion

        /// <summary>
        /// 用户自定义的泛型数据
        /// </summary>
        public T? Content { get; set; }
    }
}
