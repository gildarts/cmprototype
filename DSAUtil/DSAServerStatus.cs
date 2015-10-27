
namespace FISCA.DSAUtil
{
    /// <summary>
    /// �N�� DSA Server �����A�C
    /// </summary>
    public enum DSAServerStatus
    {
        /// <summary>
        /// �N��ʧ@�����C
        /// </summary>
        Successful = 0,
        /// <summary>
        /// �N�� DSA Server ���w���B�z�� Exception�C
        /// </summary>
        UnhandledException = 500,
        /// <summary>
        /// �A�Ȥ��s�b�C
        /// </summary>
        ServiceNotFound = 501,
        /// <summary>
        /// �{�Ҹ�T���X�k�C
        /// </summary>
        CredentialInvalid = 502,
        /// <summary>
        /// �s���Q�ڡC
        /// </summary>
        AccessDeny = 503,
        /// <summary>
        /// �A�Ȥ������~�C
        /// </summary>
        ServiceExecutionError = 504,
        /// <summary>
        /// �A�Ȧ��L�C
        /// </summary>
        ServiceBusy = 505,
        /// <summary>
        /// ���X�k���ӽФ��C
        /// </summary>
        InvalidRequestDocument = 506,
        /// <summary>
        /// ���X�k���^�Ф��C
        /// </summary>
        InvalidResponseDocument = 507,
        /// <summary>
        /// �A�ȱҰʿ��~�C
        /// </summary>
        ServiceActivationError = 508,
        /// <summary>
        /// DSA Server �պA�Ϊ��A�����T�C
        /// </summary>
        ServerUnavailable = 509,
        /// <summary>
        /// DSA Application �պA�Ϊ��A�����T�C
        /// </summary>
        ApplicationUnavailable = 510,
        /// <summary>
        /// Session �L���C
        /// </summary>
        SessionExpire = 511,
        /// <summary>
        /// DSA Passport �L���C
        /// </summary>
        PassportExpire = 512,
        /// <summary>
        /// ���������A�C
        /// </summary>
        Unknow = 999
    }
}
