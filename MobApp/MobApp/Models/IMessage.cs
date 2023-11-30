using System;
using System.Collections.Generic;
using System.Text;

namespace MobApp.Models
{
    public interface IMessage
    {
        void LongAlert(string message);
        void ShortAlert(string message);
    }
}
