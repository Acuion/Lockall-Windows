using System.Threading.Tasks;

namespace Lockall_Windows.QrDisplay
{
    public interface IQrDisplayer
    {
        Task ShowQr(string prefix, byte[] bytes);
        Task HideQr();
    }
}