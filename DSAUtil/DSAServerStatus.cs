
namespace FISCA.DSAUtil
{
    /// <summary>
    /// 代表 DSA Server 的狀態。
    /// </summary>
    public enum DSAServerStatus
    {
        /// <summary>
        /// 代表動作完成。
        /// </summary>
        Successful = 0,
        /// <summary>
        /// 代表 DSA Server 未預期處理的 Exception。
        /// </summary>
        UnhandledException = 500,
        /// <summary>
        /// 服務不存在。
        /// </summary>
        ServiceNotFound = 501,
        /// <summary>
        /// 認證資訊不合法。
        /// </summary>
        CredentialInvalid = 502,
        /// <summary>
        /// 存取被拒。
        /// </summary>
        AccessDeny = 503,
        /// <summary>
        /// 服務內部錯誤。
        /// </summary>
        ServiceExecutionError = 504,
        /// <summary>
        /// 服務忙碌。
        /// </summary>
        ServiceBusy = 505,
        /// <summary>
        /// 不合法的申請文件。
        /// </summary>
        InvalidRequestDocument = 506,
        /// <summary>
        /// 不合法的回覆文件。
        /// </summary>
        InvalidResponseDocument = 507,
        /// <summary>
        /// 服務啟動錯誤。
        /// </summary>
        ServiceActivationError = 508,
        /// <summary>
        /// DSA Server 組態或狀態不正確。
        /// </summary>
        ServerUnavailable = 509,
        /// <summary>
        /// DSA Application 組態或狀態不正確。
        /// </summary>
        ApplicationUnavailable = 510,
        /// <summary>
        /// Session 過期。
        /// </summary>
        SessionExpire = 511,
        /// <summary>
        /// DSA Passport 過期。
        /// </summary>
        PassportExpire = 512,
        /// <summary>
        /// 未知的狀態。
        /// </summary>
        Unknow = 999
    }
}
