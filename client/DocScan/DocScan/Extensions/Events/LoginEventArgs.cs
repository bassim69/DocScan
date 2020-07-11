using System;
namespace DocScan.Extension.Events
{
    public class LoginEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public LoginEventArgs(string message)
        {
            Message = message;
        }
    }
}
